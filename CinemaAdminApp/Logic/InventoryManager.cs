using System;
using System.Collections.Generic;
using System.Linq;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class InventoryManager
    {
        private static List<InventoryItem> _items = new List<InventoryItem>();
        private static int _nextId = 1;

        public InventoryManager()
        {
            
        }

        public List<InventoryItem> GetAllItems()
        {
            return _items.OrderBy(i => i.Name).ToList();
        }

        public InventoryItem GetItemById(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public bool CreateItem(InventoryItem item)
        {
            if (item == null) return false;

            if (_items.Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            item.Id = _nextId++;
            item.CurrentStock = 0;
            _items.Add(item);
            return true;
        }

        public bool UpdateItem(InventoryItem item)
        {
            if (item == null) return false;

            InventoryItem existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null) return false;

            if (_items.Any(i => i.Id != item.Id && i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            existingItem.Name = item.Name;
            existingItem.UnitOfMeasure = item.UnitOfMeasure;
            existingItem.CostPrice = item.CostPrice;
            existingItem.SalePrice = item.SalePrice;

            return true;
        }

        public bool DeleteItem(int id)
        {
            InventoryItem item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _items.Remove(item);
                return true;
            }
            return false;
        }

        public bool AddStock(int itemId, int quantity)
        {
            InventoryItem item = GetItemById(itemId);
            if (item != null && quantity > 0)
            {
                item.CurrentStock += quantity;
                return true;
            }
            return false;
        }

        public bool SellStock(int itemId, int quantity)
        {
            InventoryItem item = GetItemById(itemId);
            if (item != null && item.CurrentStock >= quantity && quantity > 0)
            {
                item.CurrentStock -= quantity;
                return true;
            }
            return false;
        }
    }
}