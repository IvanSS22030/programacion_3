namespace ProyectoFinal.Services.Strategies
{
    public class LbToKgStrategy : IConversionStrategy
    {
        public string FromUnit => "Libras";
        public string ToUnit => "Kilogramos";

        public double Convert(double value)
        {
            // 1 lb = 0.453592 kg
            return value * 0.453592;
        }
    }
}

// Actualización de repositorio - 2026-04-08

// Enhance service logic execution
