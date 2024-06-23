using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
	public class ShoppingCart : IShoppingCart
	{
		private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

		public string? ShoppingCartId { get; set; }

		public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

		private ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
		{
			_bethanysPieShopDbContext = bethanysPieShopDbContext;
		}

		public static ShoppingCart GetCart(IServiceProvider services)//hali hazırda bir kart ıd olup olamdığını kontrol ediyor 
		{
			ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session; //oturuma erişmeye çalışıyorum.

			BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");//dbcontexte erişimeye çalışıyorum

			string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString(); //gelen kullsnıcı için CartId adında bir değer olup olmadığını oturuma göre kontrol eder yok ise yeni bit cartıd oluşturacak

			session?.SetString("CartId", cartId); //burada ekleme yapıyoruz

			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}

		public void AddToCart(Pie pie)
		{
			var shoppingCartItem =
					_bethanysPieShopDbContext.ShoppingCartItemss.SingleOrDefault(
						s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = ShoppingCartId,
					Pie = pie,
					Amount = 1
				};

				_bethanysPieShopDbContext.ShoppingCartItemss.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}
			_bethanysPieShopDbContext.SaveChanges();
		}

		public int RemoveFromCart(Pie pie)
		{
			var shoppingCartItem =
					_bethanysPieShopDbContext.ShoppingCartItemss.SingleOrDefault(
						s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

			var localAmount = 0;

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_bethanysPieShopDbContext.ShoppingCartItemss.Remove(shoppingCartItem);
				}
			}

			_bethanysPieShopDbContext.SaveChanges();

			return localAmount;
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??=
					   _bethanysPieShopDbContext.ShoppingCartItemss.Where(c => c.ShoppingCartId == ShoppingCartId)
						   .Include(s => s.Pie)
						   .ToList();
		}

		public decimal GetShoppingCartTotal()
		{
			var total = _bethanysPieShopDbContext.ShoppingCartItemss.Where(c => c.ShoppingCartId == ShoppingCartId)
				.Select(c => c.Pie.PiePrice * c.Amount).Sum();
			return total;
		}

		public void Clearcart()
		{
			var cartItems = _bethanysPieShopDbContext
				.ShoppingCartItemss
				.Where(cart => cart.ShoppingCartId == ShoppingCartId);

			_bethanysPieShopDbContext.ShoppingCartItemss.RemoveRange(cartItems);

			_bethanysPieShopDbContext.SaveChanges();
		}
	}

}