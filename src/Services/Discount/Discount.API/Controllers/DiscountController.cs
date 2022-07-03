using System.Net;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{

  private readonly ILogger<DiscountController> _logger;
  private readonly IDiscountRepository _discountRepository;

  public DiscountController(
  ILogger<DiscountController> logger,
  IDiscountRepository discountRepository)
  {
    _logger = logger;
    _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
  }

  [HttpGet("{productName}", Name = "GetDiscount")]
  [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Coupon>> GetDiscount(string productName)
  {
    var coupon = await _discountRepository.GetDiscount(productName);

    return coupon;
  }

  [HttpPost]
  [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
  {
    await _discountRepository.CreateDiscount(coupon);
    return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
  }

  [HttpPut]
  [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
  {
    return Ok(await _discountRepository.UpdateDiscount(coupon));
  }

  [HttpDelete("{productName}", Name = "DeleteDiscount")]
  [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<bool>> DeleteDiscount(string productName)
  {
    return Ok(await _discountRepository.DeleteDiscount(productName));
  }
}

