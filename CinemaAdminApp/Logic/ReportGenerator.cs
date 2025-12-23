using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CinemaAdminApp.Data;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public static class ReportGenerator
    {
        public static string GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            var tickets = DataAccess.LoadTickets();
            var sessions = DataAccess.LoadSessions();
            var films = DataAccess.LoadFilms();

            var endDateTime = endDate.Date.AddDays(1).AddSeconds(-1);

            var salesInPeriod = tickets
                .Where(t => t.SaleTime >= startDate && t.SaleTime <= endDateTime)
                .ToList();

            if (!salesInPeriod.Any())
            {
                return "Отчет: Нет данных о продажах за выбранный период.";
            }

            var totalRevenue = salesInPeriod.Sum(t => t.Price);
            var totalTickets = salesInPeriod.Count;

            var sb = new StringBuilder();
            sb.AppendLine($"--- Отчет о продажах ---");
            sb.AppendLine($"Период: с {startDate:d} по {endDate:d}");
            sb.AppendLine($"Общее количество проданных билетов: {totalTickets}");
            sb.AppendLine($"Общая выручка: {totalRevenue:C}");
            sb.AppendLine($"--------------------------");

            var sessionSales = salesInPeriod
                .GroupBy(t => t.SessionId)
                .Select(g => new
                {
                    SessionId = g.Key,
                    Revenue = g.Sum(t => t.Price),
                    TicketsCount = g.Count()
                })
                .OrderByDescending(x => x.Revenue);

            foreach (var s in sessionSales)
            {
                var sessionInfo = sessions.FirstOrDefault(sess => sess.Id == s.SessionId);
                var filmInfo = sessionInfo != null ? films.FirstOrDefault(f => f.Id == sessionInfo.FilmId) : null;

                string sessionTitle = filmInfo != null && sessionInfo != null ?
                    $"{filmInfo.Title} ({sessionInfo.StartTime:g})" :
                    $"Сеанс ID {s.SessionId}";

                sb.AppendLine($"{sessionTitle}: Продано билетов: {s.TicketsCount}, Выручка: {s.Revenue:C}");
            }

            sb.AppendLine($"Отчет сформирован: {DateTime.Now}");
            return sb.ToString();
        }

        public static string GenerateFinancialSummary(DateTime startDate, DateTime endDate)
        {
            var operations = DataAccess.LoadOperations();
            var endDateTime = endDate.Date.AddDays(1).AddSeconds(-1);

            var opsInPeriod = operations
                .Where(o => o.DateTime >= startDate && o.DateTime <= endDateTime)
                .ToList();

            decimal income = opsInPeriod
                .Where(o => o.Type == OperationType.TicketSale || o.Type == OperationType.InventorySale || o.Type == OperationType.Revenue)
                .Sum(o => o.Amount);

            decimal expense = opsInPeriod
                .Where(o => o.Type == OperationType.Expense || o.Type == OperationType.InventoryPurchase)
                .Sum(o => o.Amount);

            var sb = new StringBuilder();
            sb.AppendLine($"--- Финансовый отчет ---");
            sb.AppendLine($"Период: с {startDate:d} по {endDate:d}");
            sb.AppendLine($"Общий доход (Билеты + Товары + Прочее): {income:C}");
            sb.AppendLine($"Общий расход (Закупки): {Math.Abs(expense):C}");
            sb.AppendLine($"Чистая прибыль: {income + expense:C}");
            sb.AppendLine($"--------------------------");

            if (!opsInPeriod.Any())
            {
                sb.AppendLine("Нет финансовых операций за выбранный период.");
            }

            sb.AppendLine($"Отчет сформирован: {DateTime.Now}");
            return sb.ToString();
        }

        public static string GenerateInventoryReport()
        {
            var inventory = DataAccess.LoadInventory();
            var sb = new StringBuilder();

            sb.AppendLine($"--- Отчет о запасах ---");

            foreach (var item in inventory.OrderByDescending(i => i.CurrentStock))
            {
                sb.AppendLine($"Товар: {item.Name} | На складе: {item.CurrentStock} {item.UnitOfMeasure} | Себестоимость: {item.CostPrice:C} | Цена продажи: {item.SalePrice:C}");
            }

            if (!inventory.Any())
            {
                sb.AppendLine("Справочник запасов пуст.");
            }

            sb.AppendLine($"Отчет сформирован: {DateTime.Now}");
            return sb.ToString();
        }
    }
}