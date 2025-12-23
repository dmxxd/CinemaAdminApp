using System;

namespace CinemaAdminApp.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal TotalCostValue => CostPrice * CurrentStock;
        public decimal TotalSaleValue => SalePrice * CurrentStock;
        public decimal PotentialProfit => TotalSaleValue - TotalCostValue;
    }
}
