using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
	public class ShoppinCartViewModel
	{
		public IShoppingCart ShoppingCart { get;  }
		public decimal ShoppingCartTotal{ get;  }

        public ShoppinCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }
    }
}
