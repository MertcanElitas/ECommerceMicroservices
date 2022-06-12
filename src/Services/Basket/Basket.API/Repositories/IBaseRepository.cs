using System;
using Basket.API.Entities;

namespace Basket.API.Repositories
{
	public interface IBaseRepository<T> where T : class,new()
	{
		Task<T> GetModel(string key);

		Task<T> UpdateModel(string key, T model);

		Task DeleteBasket(string key);
	}
}

