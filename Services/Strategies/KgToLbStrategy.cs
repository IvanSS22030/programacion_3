namespace ProyectoFinal.Services.Strategies
{
    public class KgToLbStrategy : IConversionStrategy
    {
        public string FromUnit => "Kilogramos";
        public string ToUnit => "Libras";

        public double Convert(double value)
        {
            // 1 kg = 2.20462 lb
            return value * 2.20462;
        }
    }
}
