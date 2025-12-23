using System;

namespace CinemaAdminApp.Models
{
    public enum OperationType
    {
        TicketSale = 1,
        InventorySale = 2,
        InventoryPurchase = 3,
        Expense = 4,
        Revenue = 5
    }
    public class FinancialOperation
    {
        public int Id { get; set; }
        public OperationType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public int? RelatedId { get; set; } 
    }
}