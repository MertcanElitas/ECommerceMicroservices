﻿using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
  private readonly ILogger<CatalogController> _logger;
  private readonly IProductRepository _productRepository;

  public CatalogController(ILogger<CatalogController> logger, IProductRepository productRepository)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
  {
    var products = await _productRepository.GetProducts();
    return Ok(products);
  }

  // [HttpGet]
  // [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
  // public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
  // {
  //   return Ok(_data);
  // }

  [HttpGet("{id:length(24)}", Name = "GetProduct")]
  [ProducesResponseType((int)HttpStatusCode.NotFound)]
  [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Product>> GetProductById(string id)
  {
    var product = await _productRepository.GetProduct(id);
    if (product == null)
    {
      _logger.LogError($"Product with id: {id}, not found.");
      return NotFound();
    }
    return Ok(product);
  }

  [Route("[action]/{category}", Name = "GetProductByCategory")]
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
  {
    var products = await _productRepository.GetProductByCategory(category);
    return Ok(products);
  }

  [HttpPost]
  [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
  {
    await _productRepository.CreateProduct(product);

    return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
  }

  [HttpPut]
  [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> UpdateProduct([FromBody] Product product)
  {
    return Ok(await _productRepository.UpdateProduct(product));
  }

  [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
  [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> DeleteProductById(string id)
  {
    return Ok(await _productRepository.DeleteProduct(id));
  }
}

