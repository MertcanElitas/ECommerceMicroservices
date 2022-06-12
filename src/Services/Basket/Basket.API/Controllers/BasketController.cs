using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly ILogger<BasketController> _logger;

    public BasketController(
        ILogger<BasketController> logger,
        IBasketRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{key}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string key)
    {
        var basket = await _repository.GetModel(key);
        return Ok(basket ?? new ShoppingCart(key));
    }

    [HttpDelete("{key}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string key)
    {
        await _repository.DeleteBasket(key);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        var model = await _repository.UpdateModel(basket.UserName, basket);
        return Ok(model);
    }
}

