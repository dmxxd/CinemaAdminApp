using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Data
{
    public static class DataAccess
    {
        private static readonly string DataDirectory = "Data";

        static DataAccess()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        private static List<T> Load<T>(string fileName) where T : new()
        {
            string filePath = Path.Combine(DataDirectory, fileName);
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        private static void Save<T>(List<T> data, string fileName)
        {
            string filePath = Path.Combine(DataDirectory, fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static List<Film> LoadFilms() => Load<Film>("Films.json");
        public static void SaveFilms(List<Film> films) => Save(films, "Films.json");

        public static List<Hall> LoadHalls() => Load<Hall>("Halls.json");
        public static void SaveHalls(List<Hall> halls) => Save(halls, "Halls.json");

        public static List<Session> LoadSessions() => Load<Session>("Sessions.json");
        public static void SaveSessions(List<Session> sessions) => Save(sessions, "Sessions.json");

        public static List<Personal> LoadPersonal() => Load<Personal>("Personal.json");
        public static void SavePersonal(List<Personal> personal) => Save(personal, "Personal.json");

        public static List<Ticket> LoadTickets() => Load<Ticket>("Tickets.json");
        public static void SaveTickets(List<Ticket> tickets) => Save(tickets, "Tickets.json");

        public static List<InventoryItem> LoadInventory() => Load<InventoryItem>("Inventory.json");
        public static void SaveInventory(List<InventoryItem> inventory) => Save(inventory, "Inventory.json");

        public static List<FinancialOperation> LoadOperations() => Load<FinancialOperation>("Operations.json");
        public static void SaveOperations(List<FinancialOperation> operations) => Save(operations, "Operations.json");

        public static bool BackupData(string backupPath)
        {
            try
            {
                string targetDir = Path.Combine(backupPath, $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}");
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                foreach (var file in Directory.GetFiles(DataDirectory))
                {
                    File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
