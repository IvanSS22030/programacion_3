using System;

namespace ProyectoFinal.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Entrada" or "Salida"
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}

// Actualización de repositorio - 2026-04-08

// Update data model schemas
