using System;
namespace Basket.API.Entities
{
	public class ShoppingCart
	{
		public ShoppingCart(string userName)
		{
			UserName = userName;
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var cartItem in ShoppingCartItems)
                {
                    totalPrice += cartItem.Price * cartItem.Quantity;
                }

                return totalPrice;
            }
        }
    }
}

