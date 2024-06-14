using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WebBanGear_63131848.Models
{
    public class Cart
    {

        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void AddItem(SanPham product, int quantity)
        {
            var existingItem = Items.Find(item => item.Product.MaSP == product.MaSP);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
        }

        public void RemoveItem(string productId)
        {
            Items.RemoveAll(item => item.Product.MaSP == productId);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }

    public class CartItem
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
    }

}