using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public enum JobTemplateType
    {
        None,
        Move,
        Transport,
        TRANSPORT_CHEMICAL_SUPPLY,
        TRANSPORT_CHEMICAL_RECOVERY,
        TRANSPORT_SLURRY_SUPPLY,
        TRANSPORT_SLURRY_RECOVERY,
        TRANSPORT_AICERO_SUPPLY,
        TRANSPORT_AICERO_RECOVERY,
        Charge,
        Wait,
        Cancel,
        Reset,
    }

    public enum JobTemplateSubType
    {
        None,
        SimpleMove,             // 같은 층 간 이동
        MoveWithEV,             // 다른 층 이동
        PickOnly,               // 같은 층 자재 픽업만 수행
        PickWithEV,             // 다른 층 자재 픽업만 수행
        DropOnly,               // 같은 층 자재 Drop만 수행
        DropWithEV,             // 다른 층 자재 Drop만 수행
        PickDrop,               // 같은 층 자재 Pick → Drop
        PickDropWithEV,         // 자재 Pick → E/V → Drop
        Charge,            // 같은 층 충전소 이동
        ChargerWithEV,      // E/V 포함 충전소 이동
        CHARGECOMPLETE,
        Wait,               // 같은 층 대기
        WaitWithEV,             // E/V 포함 대기 위치 이동
        Reset,              // 복구 위치 이동 (같은 층)
        ResetWithEV,            // 복구 위치 이동 (다른 층 + E/V)
        Cancel                  // 현재 작업 취소
    }

    public class JobTemplate
    {
        [JsonPropertyOrder(1)]
        public int id { get; set; }

        [JsonPropertyOrder(2)]
        public string group { get; set; }

        [JsonPropertyOrder(3)]

        public string type { get; set; }

        [JsonPropertyOrder(4)]
        public string subType { get; set; }

        [JsonPropertyOrder(5)]
        public string carrierType { get; set; }

        [JsonPropertyOrder(6)]
        public bool isLocked { get; set; }

        [JsonPropertyOrder(7)]
        public List<MissionTemplate> missionTemplates = new List<MissionTemplate>();

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
}
