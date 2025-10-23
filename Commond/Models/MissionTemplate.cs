using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public enum Service
    {
        Worker,
        Elevator,
        Middleware
    }

    public enum MissionTemplateType
    {
        None,
        Move,
        Action,
        SupplyAction,
        RecoveryAction,
    }

    public enum MissionTemplateSubType
    {
        None,

        //Move
        ChargerMove,

        WaitMove,
        ResetMove,
        PositionMove,
        SourceMove,
        StopoverMove,
        ElevatorWaitMove,
        ElevatorEnterMove,
        ElevatorExitMove,
        DestinationMove,

        //Action Amkor
        Pick,

        Drop,
        Wait,
        Charge,
        CHARGECOMPLETE,
        Reset,
        Cancel,
        Call,
        DoorOpen,
        EnterComplete,
        DoorClose,
        ExitComplete,
    }

    public class MissionTemplate
    {
        //[JsonProperty(Order = 1)]
        //public int jobTemplateId { get; set; }

        [JsonPropertyOrder(1)]
        public string name { get; set; }

        [JsonPropertyOrder(2)]
        public string service { get; set; }

        [JsonPropertyOrder(3)]
        public string type { get; set; }

        [JsonPropertyOrder(4)]
        public string subType { get; set; }

        [JsonPropertyOrder(5)]
        public bool isLook { get; set; }

        [JsonPropertyOrder(6)]
        public List<Parameta> parameters = new List<Parameta>();

        [JsonPropertyOrder(7)]
        public List<PreReport> preReports = new List<PreReport>();

        [JsonPropertyOrder(8)]
        public List<PostReport> postReports = new List<PostReport>();

        public override string ToString()
        {
            //return JsonConvert.SerializeObject(this, Formatting.Indented);

            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                IncludeFields = true,
                WriteIndented = false, // 로그용은 한 줄 출력 추천
                //WriteIndented = true, // Json 보기좋게 표현방법
            });
        }
    }

    public class Parameta
    {
        [JsonPropertyOrder(1)]
        public string key { get; set; }

        [JsonPropertyOrder(2)]
        public string value { get; set; }
    }

    public class PreReport
    {
        [JsonPropertyOrder(1)]
        public int ceid { get; set; }

        [JsonPropertyOrder(2)]
        public string eventName { get; set; }

        [JsonPropertyOrder(3)]
        public int rptid { get; set; }
    }

    public class PostReport
    {
        [JsonPropertyOrder(1)]
        public int ceid { get; set; }

        [JsonPropertyOrder(2)]
        public string eventName { get; set; }

        [JsonPropertyOrder(3)]
        public int rptid { get; set; }
    }

    public class Report
    {
        [JsonPropertyOrder(1)]
        public int ceid { get; set; }

        [JsonPropertyOrder(2)]
        public string eventName { get; set; }

        [JsonPropertyOrder(3)]
        public int rptid { get; set; }
    }
}