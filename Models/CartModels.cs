using System.Collections.Generic;
using System.Linq;

namespace Web_Programlama_Proje.Models
{
    public class CartItem
    {
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(MenuItem menuItem, int quantity)
        {
            var line = Items.FirstOrDefault(i => i.MenuItem?.Id == menuItem.Id);

            if (line == null)
            {
                Items.Add(new CartItem
                {
                    MenuItem = menuItem,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(MenuItem menuItem)
        {
            Items.RemoveAll(i => i.MenuItem?.Id == menuItem.Id);
        }

        public decimal ComputeTotalValue() => Items.Sum(e => (e.MenuItem?.Price ?? 0) * e.Quantity);

        public void Clear() => Items.Clear();
    }
}
