using Edstem.Services.ShoppingCartAPI.Models.Dto;
using Edstem.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Edstem.Services.ShoppingCartAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartRepository _repository;
    private readonly ICouponRepository _couponRepository;
    private readonly ILogger<ShoppingCartController> _logger;

    public ShoppingCartController(IShoppingCartRepository repository, ICouponRepository couponRepository,
        ILogger<ShoppingCartController> logger)
    {
        _repository = repository;
        _couponRepository = couponRepository;
        _logger = logger;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCart([FromBody] CartDto cartDto)
    {
        try
        {
            var response = await _repository.CreateOrUpdateCart(cartDto);
            _logger.LogInformation("Cart added successfully");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while adding cart", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/{userId}")]
    public async Task<IActionResult> Get([FromRoute] string userId)
    {
        try
        {
            return Ok(await _repository.GetCartByUserId(userId));
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable To get Cart", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("remove/{cartDetailsId}")]
    public async Task<IActionResult> Delete([FromRoute] int cartDetailsId)
    {
        try
        {
            var success = await _repository.RemoveFromCart(cartDetailsId);
            if (!success)
            {
                return Problem("Unable to remove from cart");
            }
            _logger.LogInformation("Cart Removed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Unable to Remove Items", ex);
            return BadRequest(ex.Message);
        }


        return Ok();
    }

    [HttpPost("clear/{userId}")]
    public async Task<IActionResult> ClearCart([FromRoute] string userId)
    {
        try
        {
            var success = await _repository.ClearCart(userId);
            if (!success)
            {
                return Problem("Unable to clear cart");
            }
            _logger.LogInformation("Cart Cleared");
        }
        catch (Exception ex)
        {
            _logger.LogError("Can't Clear Cart", ex);
            return BadRequest(ex.Message);
        }


        return Ok();
    }

    [HttpPost("addCoupon/{userId}/{couponCode}")]
    public async Task<IActionResult> AddCoupon([FromRoute] string userId, string couponCode)
    {
        try
        {
            var success = await _repository.ApplyCoupon(userId, couponCode);
            if (!success)
            {
                return Problem("Unable to add coupon");
            }
            _logger.LogInformation("Coupon Code Applied");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while Adding Coupon Code", ex);
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPost("removeCoupon/{userId}")]
    public async Task<IActionResult> RemoveCoupon([FromRoute] string userId)
    {
        try
        {
            var success = await _repository.RemoveCoupon(userId);
            if (!success)
            {
                return Problem("Unable to remove coupon");
            }
            _logger.LogInformation("Coupon Code Removed");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while Removing Coupon Code", ex);
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
    {
        if (!string.IsNullOrEmpty(checkoutDto.CouponCode))
        {
            var coupon = await _couponRepository.GetCoupon(checkoutDto.CouponCode);
            if (coupon == null || string.IsNullOrEmpty(coupon.CouponCode))
            {
                return BadRequest("Invalid coupon");
            }
            // verify the discount applied is correct
        }

        // get the cart details
        var cartDto = await _repository.GetCartByUserId(checkoutDto.UserId);
        // checkoutDto.CartDetails = cartDto.CartDetails;

        // TODO:: send the message to the order queue 

        return Ok();
    }
}