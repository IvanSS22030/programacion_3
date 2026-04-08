# Proyecto Final - Sistema de Gestión

Aplicación Windows Forms desarrollada en C# (.NET 5.0) para gestión de inventario, pacientes y conversión de unidades.

## Requisitos

- Windows 10/11
- .NET 5.0 SDK
- PostgreSQL 17 (con base de datos `pacientesdb` configurada)

## Instalación de Base de Datos

1. Abrir pgAdmin o psql
2. Crear base de datos: `CREATE DATABASE pacientesdb;`
3. Ejecutar los scripts en orden:
   - `Scripts/create_pacientes_db.sql`
   - `Scripts/create_inventory_table.sql`

## Ejecución

```powershell
dotnet run --project ProyectoFinal.csproj
```

## Funcionalidades

### Conversores (Menú: Conversores)
- **Kilogramos a Libras**: Convierte kg → lb (factor: 2.20462)
- **Libras a Kilogramos**: Convierte lb → kg (factor: 0.453592)
- **Conversor Combinado**: Selector de tipo + historial de últimas 5 conversiones

### Inventario (Menú: Inventario → Gestión de Inventario)
- Agregar movimientos (Entrada/Salida)
- Eliminar registros
- Totales automáticos (Entradas, Salidas, Stock Neto)
- Doble clic para editar
- Calendario 100% en español
- Persistencia en PostgreSQL

### Pacientes (Menú: Pacientes → Gestión de Pacientes)
- **Agregar**: Crear nuevo paciente
- **Actualizar**: Doble clic en fila → modificar → Actualizar
- **Eliminar**: Eliminar paciente seleccionado
- Campos: Nombre, Apellido, Dirección, Teléfono, Estatus

## Estructura del Proyecto

```
├── Forms/              # Formularios de Windows
├── Models/             # Clases de datos
├── Services/           # Lógica de negocio
│   └── Strategies/     # Patrón Strategy para conversiones
├── Scripts/            # Scripts SQL
└── Program.cs          # Punto de entrada
```

## Autor

Ivan Joel Sánchez Santana - UAPA Universidad

## Licencia

Proyecto académico - Programación 2

<!-- Actualizaci�n de repositorio - 2026-04-08 -->


