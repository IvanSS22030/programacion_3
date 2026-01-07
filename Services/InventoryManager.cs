using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services
{
    public class InventoryManager
    {
        private string connString = "Host=localhost;Username=postgres;Password=Lucario2030*;Database=pacientesdb";

        public void AddItem(InventoryItem item)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("CALL sp_insert_inventory(@code, @name, @type, @qty, @date)", conn))
                {
                    cmd.Parameters.AddWithValue("code", item.Code);
                    cmd.Parameters.AddWithValue("name", item.Name);
                    cmd.Parameters.AddWithValue("type", item.Type);
                    cmd.Parameters.AddWithValue("qty", item.Quantity);
                    cmd.Parameters.AddWithValue("date", item.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveItem(InventoryItem item)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Using hard SQL for delete for simplicity as I defined sp_delete_inventory but need Id
                using (var cmd = new NpgsqlCommand("DELETE FROM Inventario WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<InventoryItem> GetItems()
        {
            var list = new List<InventoryItem>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT id, code, name, type, quantity, date FROM Inventario ORDER BY date DESC", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InventoryItem
                        {
                            Id = reader.GetInt32(0),
                            Code = reader.GetString(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            Quantity = reader.GetInt32(4),
                            Date = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return list;
        }

        public int GetTotalEntries()
        {
             // Could be optimized with SQL select sum, but reusing GetItems matches previous logic pattern locally
             int total = 0;
             foreach(var item in GetItems())
             {
                 if (item.Type == "Entrada") total += item.Quantity;
             }
             return total;
        }

        public int GetTotalExits()
        {
             int total = 0;
             foreach(var item in GetItems())
             {
                 if (item.Type == "Salida") total += item.Quantity;
             }
             return total;
        }

        public int GetNetStock()
        {
            return GetTotalEntries() - GetTotalExits();
        }
    }
}
