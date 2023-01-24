using Edstem.Services.ShoppingCartAPI.Models.Dto;
using Newtonsoft.Json;

namespace Edstem.Services.ShoppingCartAPI.Repository.Impl;

public class CouponRepository : ICouponRepository
{
    private readonly HttpClient _client;

    public CouponRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<CouponDto?> GetCoupon(string couponCode)
    {
        var response = await _client.GetAsync($"/api/coupons/code/{couponCode}");
        return response.IsSuccessStatusCode
            ? JsonConvert.DeserializeObject<CouponDto>(response.Content.ReadAsStringAsync().Result)
            : new CouponDto();
    }
}