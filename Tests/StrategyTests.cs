using Xunit;
using ProyectoFinal.Services.Strategies;

namespace ProyectoFinal.Tests
{
    public class StrategyTests
    {
        [Fact]
        public void KgToLb_Conversion_Correct()
        {
            var strategy = new KgToLbStrategy();
            double result = strategy.Convert(10); // 10 kg
            Assert.Equal(22.0462, result, 4);
        }

        [Fact]
        public void LbToKg_Conversion_Correct()
        {
            var strategy = new LbToKgStrategy();
            double result = strategy.Convert(10); // 10 lb
            Assert.Equal(4.53592, result, 5);
        }
    }
}
