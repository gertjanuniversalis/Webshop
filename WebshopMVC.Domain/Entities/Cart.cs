using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopMVC.Domain.Abstract;

namespace WebshopMVC.Domain.Entities
{
    public class Cart
    {
		public static event EventHandler<CartChangedArgs> CartContentChanged;

		public IEnumerable<CartItem> Items => ItemsInCart.OrderBy(I => I.Product.ID);
		public decimal TotalValue => ItemsInCart.Sum(p => p.TotalPrice);



		private List<CartItem> ItemsInCart = new List<CartItem>();		

        public void AddItem(Product product, int quantity)
        {
            CartItem cartItem = ItemsInCart.Where(p => p.Product.ID == product.ID).FirstOrDefault();

            if (cartItem == null)
            {
                ItemsInCart.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
			CartContentChanged?.Invoke(this, new CartChangedArgs(this));
		}

		public void RemoveItem(Product product, int quantity)
        {
            CartItem cartItem = ItemsInCart.Where(p => p.Product.ID == product.ID).FirstOrDefault();
    
            if (cartItem != null)
            {
                if(cartItem.Quantity <= quantity)
                {
                    RemoveItem(product);
                }
                else
                {
                    cartItem.Quantity -= quantity;
                }

				CartContentChanged?.Invoke(this, new CartChangedArgs(this));
            }
        }

        public void RemoveItem(Product product)
        {
            ItemsInCart.RemoveAll(p => p.Product.ID == product.ID);
        }

		public void Empty()
		{
			ItemsInCart.Clear();
			CartContentChanged?.Invoke(this, new CartChangedArgs(this));
		}
	}

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

		public decimal TotalPrice => Product.Price * Quantity;
    }

	public class CartChangedArgs : EventArgs
	{
		public Cart Cart;

		public CartChangedArgs(Cart cart)
		{
			this.Cart = cart;
		}
	}
}
