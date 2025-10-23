namespace Common.Models
{
    public class Map
    {
        public string _id { get; set; }
        public string mapId { get; set; }
        public string source { get; set; }
        public int __v { get; set; }
        public DateTime createdAt { get; set; }
        public string imageId { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public double originTheta { get; set; }
        public double originX { get; set; }
        public double originY { get; set; }
        public double resolution { get; set; }
        public DateTime updatedAt { get; set; }
    }
}