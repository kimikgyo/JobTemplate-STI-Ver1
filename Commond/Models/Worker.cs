namespace Common.Models
{
    public class Worker
    {



        public string _id { get; set; }
        public string source { get; set; }
        public string workerId { get; set; }
        public int __v { get; set; }
        public List<capabilities> capabilities { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public string ipAddress { get; set; }
        public bool isActive { get; set; }
        public bool isOccupied { get; set; }
        public bool isOnline { get; set; }
        public string loginId { get; set; }
        public string modelName { get; set; }
        public string password { get; set; }
    }

    public class RawData
    {

    }
    public class Middleware
    {
        public bool isOnline { get; set; }
        public bool isActive { get; set; }
        public string Carrier { get; set; }
        public string acsmissionId { get; set; }
        public string servicemissionId { get; set; }
        public string state { get; set; }
    }


    public class capabilities
    {
    }
}