using AutoMapper;
using Edstem.Services.ShoppingCartAPI.Models;
using Edstem.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;


namespace Edstem.Services.ShoppingCartAPI.Repository.Impl;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ShoppingCartRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<CartDto> CreateOrUpdateCart(CartDto cartDto)
    {
        var cart = _mapper.Map<Cart>(cartDto);

        // check if the cart header exists for user
        var cartHeaderFromDb = await _dataContext.CartHeaders.AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

        // if cart header does not exist, create a new one
        if (cartHeaderFromDb == null)
        {
            _dataContext.CartHeaders.Add(cart.CartHeader);
            await _dataContext.SaveChangesAsync();

            // update cart details with the newly created cart header, complete the bi-directional relationship
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
            _dataContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());

            await _dataContext.SaveChangesAsync();
        }
        else
        {
            // if the header is not null, check the details has the same product
            var cartDetailsFromDb = await _dataContext.CartDetails.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                                          c.CartHeaderId == cartHeaderFromDb.CartHeaderId);
            if (cartDetailsFromDb == null)
            {
                // create details
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                _dataContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                // update the count / details
                cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                cart.CartDetails.FirstOrDefault().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                _dataContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _dataContext.SaveChangesAsync();
            }
        }

        // return new dto, cartDto can't be returned because of missing IDs
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> GetCartByUserId(string userId)
    {
        var cart = new Cart()
        {
            CartHeader = await _dataContext.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartDetails = _dataContext.CartDetails.AsNoTracking()
            .Where(c => c.CartHeaderId == cart.CartHeader.CartHeaderId).ToList();

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<bool> RemoveFromCart(int cartDetailsId)
    {
        try
        {
            var cartDetails = await _dataContext.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);
            var count = _dataContext.CartHeaders.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
            if (count == 1)
            {
                // the header also should be removed
                var cartHeader =
                    await _dataContext.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                _dataContext.CartHeaders.Remove(cartHeader);
            }

            _dataContext.CartDetails.Remove(cartDetails);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> ClearCart(string userId)
    {
        try
        {
            var cartHeader = await _dataContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeader != null)
            {
                _dataContext.CartDetails.RemoveRange(
                    _dataContext.CartDetails.Where(u => u.CartHeaderId == cartHeader.CartHeaderId));
                _dataContext.CartHeaders.Remove(cartHeader);
                await _dataContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        try
        {
            var cartHeader = await _dataContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartHeader.CouponCode = couponCode;
            _dataContext.CartHeaders.Update(cartHeader);
            return await _dataContext.SaveChangesAsync() > 0;



        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        try
        {
            var cartHeader = await _dataContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartHeader.CouponCode = string.Empty;
            _dataContext.CartHeaders.Update(cartHeader);
            return await _dataContext.SaveChangesAsync() > 0;

        }
        catch (Exception)
        {

            return false;
        }
    }
}