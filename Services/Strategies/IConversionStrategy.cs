namespace ProyectoFinal.Services.Strategies
{
    public interface IConversionStrategy
    {
        double Convert(double value);
        string FromUnit { get; }
        string ToUnit { get; }
    }
}

// Actualización de repositorio - 2026-04-08
