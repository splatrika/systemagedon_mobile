namespace Systemagedon.App.Services
{
    public interface IStarSystemGenerator
    {
        public StarSystemSnapshot Generate(int planets);
        public int CalculateMaxPlanets();
    }
}