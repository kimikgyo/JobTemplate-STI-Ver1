namespace Common.Models
{
    public class Position
    {
        public string _id { get; set; }
        public string positionId { get; set; }
        public string source { get; set; }
        public string mapId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double theta { get; set; }
        public string groupId { get; set; }
        public bool isDisplayed { get; set; }
        public bool isEnabled { get; set; }
        public bool isOccupied { get; set; }
        public string linkedFacility { get; set; }
        public string linkedRobotId { get; set; }
        public bool hasCharger { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
}