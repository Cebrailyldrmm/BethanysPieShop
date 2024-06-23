namespace BethanysPieShop.Models
{
	public interface IShoppingCart
	{
		void AddToCart(Pie pie);
		int RemoveFromCart(Pie pie);
		List<ShoppingCartItem> GetShoppingCartItems();
		void Clearcart();
		decimal GetShoppingCartTotal();
		List<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
