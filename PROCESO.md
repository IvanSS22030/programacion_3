# Proceso de Desarrollo

Documentación del proceso de creación del Proyecto Final de Programación 2.

## Cronología de Desarrollo

### 5 de Enero 2026 - Inicio del Proyecto

1. **Configuración Inicial**
   - Creación del proyecto C# Windows Forms con .NET 5.0
   - Configuración de la estructura de carpetas (Forms, Models, Services)

2. **Implementación del Patrón Strategy**
   - Creación de `IConversionStrategy` (interfaz)
   - Implementación de `KgToLbStrategy` y `LbToKgStrategy`
   - Esto permite agregar nuevos tipos de conversión fácilmente

3. **Creación de Formularios**
   - `KgToLbForm`: Conversión de kilogramos a libras
   - `LbToKgForm`: Conversión de libras a kilogramos
   - `CombinedConverterForm`: Selector con historial
   - `InventoryForm`: Gestión de inventario con DataGridView
   - `MainForm`: Menú principal con navegación

4. **Base de Datos PostgreSQL**
   - Script `create_pacientes_db.sql` para tabla Pacientes
   - Procedimientos almacenados para CRUD
   - Despliegue exitoso usando `psql` con contraseña automatizada

---

### 7 de Enero 2026 - Mejoras y Nuevas Funcionalidades

5. **Persistencia de Inventario**
   - Instalación de paquete `Npgsql` para conexión a PostgreSQL
   - Creación de tabla `Inventario` en la base de datos
   - Conexión del formulario a la base de datos

6. **Gestión de Pacientes**
   - Modelo `Paciente.cs` con atributos DisplayName
   - Servicio `PacientesManager.cs` con operaciones CRUD
   - Formulario `PacientesForm.cs` con interfaz completa

---

## El Desafío de la Fecha en Español

### El Problema

El control `DateTimePicker` de Windows Forms mostraba las fechas en inglés ("Wednesday, January 7, 2026") a pesar de configurar la cultura a español.

### Intentos Fallidos

1. **CultureInfo en Program.cs**: Se configuró `Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES")` pero no funcionó.

2. **CultureInfo en el Constructor del Form**: Mismo resultado.

3. **CustomFormat del DateTimePicker**: Usando `dtpDate.CustomFormat = "dddd, dd 'de' MMMM 'de' yyyy"` el texto seguía en inglés.

4. **DefaultThreadCurrentCulture**: No tuvo efecto.

5. **InvariantGlobalization = false en csproj**: Tampoco resolvió el problema.

### La Causa Raíz

El `DateTimePicker` es un **control nativo de Windows** (Win32) que ignora la configuración de cultura de .NET. Siempre usa el idioma del sistema operativo Windows.

### La Solución Final

Se creó un **calendario completamente personalizado**:

```csharp
// Arrays con nombres en español
private readonly string[] _meses = { "Enero", "Febrero", "Marzo", ... };
private readonly string[] _diasCortos = { "Dom", "Lun", "Mar", "Mié", ... };
private readonly string[] _diasLargos = { "domingo", "lunes", "martes", ... };

// Función para generar texto en español
private void UpdateDateText()
{
    string dia = _diasLargos[(int)_selectedDate.DayOfWeek];
    string mes = _meses[_selectedDate.Month - 1].ToLower();
    txtDate.Text = $"{dia}, {_selectedDate.Day:00} de {mes} de {_selectedDate.Year}";
}
```

Se reemplazó el DateTimePicker por:
- **TextBox** (solo lectura) mostrando la fecha en español
- **Botón ▼** para abrir el calendario
- **Panel personalizado** con botones para cada día del mes
- **Navegación** con flechas < > para cambiar de mes

### Lección Aprendida

Los controles nativos de Windows no respetan la configuración de cultura de .NET. Para localización completa, es necesario crear controles personalizados o usar librerías de terceros.

---

## Tecnologías Utilizadas

| Tecnología | Versión | Uso |
|------------|---------|-----|
| C# | 9.0 | Lenguaje principal |
| .NET | 5.0 | Framework |
| Windows Forms | - | Interfaz gráfica |
| PostgreSQL | 17 | Base de datos |
| Npgsql | 6.0.0 | Conector PostgreSQL |

---

## Commits del Proyecto

1. `52b85d0` - A bug with the calendar only showing in English was fixed
2. `401c737` - Paciente was added

---

*Documento generado el 7 de Enero de 2026*

<!-- Actualizaci�n de repositorio - 2026-04-08 -->


