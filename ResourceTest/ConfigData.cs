using Common.Models;

namespace ResourceTest
{
    public static class ConfigData
    {
        public static List<Position> positions { get; set; }
        public static void Load(IConfiguration configuration)
        {
            ConfigData.positions = configuration.GetSection("Positions").Get<List<Position>>();
        }
    }
}
