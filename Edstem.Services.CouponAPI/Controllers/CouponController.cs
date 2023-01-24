using AutoMapper;
using Edstem.Services.CouponAPI.Models;
using Edstem.Services.CouponAPI.Models.Dto;
using Edstem.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using static Edstem.Services.CouponAPI.DomainException.DomainException;

namespace Edstem.Services.CouponAPI.Controllers;

[ApiController]
[Route("api/coupons")]
public class CouponController : Controller
{
    private readonly ICouponRepository _couponRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CouponController> _logger;
    public CouponController(ICouponRepository couponRepository, IMapper mapper, ILogger<CouponController>  logger)
    {
        _couponRepository = couponRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var coupons = await _couponRepository.GetCouponsAsync();
            var couponsResponse = _mapper.Map<List<CouponDto>>(coupons);
            return Ok(couponsResponse);
        }
        catch(Exception e)
        {
            _logger.LogError("Error while getting coupons", e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CouponDto couponDto)
    {
        try
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            await _couponRepository.CreateCouponAsync(coupon);
            var couponsResponse = _mapper.Map<CouponDto>(coupon);
            var uri = "api/coupons/" + coupon.CouponId;
            return Created(uri, couponsResponse);
        }
        catch(Exception e)
        {
            _logger.LogError("Error while creating coupon", e);
            return BadRequest(e.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Update(CouponDto coupon)
    {
        try
        {
            var couponInfo = await _couponRepository.GetCouponsByIdAsync(coupon.CouponId);
            if (couponInfo == null)
            {
                return NotFound($"Coupon with {coupon.CouponId} not found");
            }

            couponInfo.CouponCode = coupon.CouponCode;
            couponInfo.DiscountAmount = coupon.DiscountAmount;
            await _couponRepository.UpdateCouponAsync(couponInfo);
            return Ok(_mapper.Map<CouponDto>(couponInfo));
        }
        catch(Exception e)
        {
            _logger.LogError("Error while update coupon", e);
            return BadRequest(e.Message);
        }
        
        
    }

    [HttpDelete("{couponId}")]
    public async Task<IActionResult> Delete([FromRoute] int couponId)
    {
        try
        {
            var couponDelete = await _couponRepository.GetCouponsByIdAsync(couponId);
            if (couponDelete == null)
            {
                return NotFound("No record found");
            }

            var success = await _couponRepository.DeleteCouponAsync(couponDelete);
            if (!success)
            {
                return Problem("Error deleting coupon", statusCode: 500);
            }

            return Ok();
        }
        catch(Exception e)
        {
            _logger.LogError("Error while deleting coupon", e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{couponId}")]
    public async Task<IActionResult> Get([FromRoute] int couponId)
    {
        try
        {
            var coupon = await _couponRepository.GetCouponsByIdAsync(couponId);
            if (coupon == null)
            {
                throw new  NotFoundException("No record found");
            }

            var couponsResponse = _mapper.Map<CouponDto>(coupon);
            return Ok(couponsResponse);
        }
        catch(Exception e)
        {
            _logger.LogError("Error while getting coupon", e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("code/{couponCode}")]
    public async Task<IActionResult> Get([FromRoute] string couponCode)
    {
        try
        {
            var coupon = await _couponRepository.GetCouponsByCodeAsync(couponCode);
            if (coupon == null)
            {
                return NotFound("No record found");
            }

            var couponsResponse = _mapper.Map<CouponDto>(coupon);
            return Ok(couponsResponse);
        }
        catch(Exception e)
        {
            _logger.LogError("Error while getting coupon", e);
            return BadRequest(e.Message);
        }
    }
}