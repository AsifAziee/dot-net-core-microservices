using Edstem.Services.ShoppingCartAPI.Models.Dto;

namespace Edstem.Services.ShoppingCartAPI.Repository;

public interface IShoppingCartRepository
{
    Task<CartDto> CreateOrUpdateCart(CartDto cart);
    Task<CartDto> GetCartByUserId(string userId);
    Task<bool> RemoveFromCart(int cartDetailsId);
    Task<bool> ClearCart(string userId);

    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> RemoveCoupon(string userId);
}