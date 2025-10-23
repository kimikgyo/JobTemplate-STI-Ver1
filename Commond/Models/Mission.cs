using System.Text.Json.Serialization;

namespace Common.Models
{
    public class Mission
    {
        [JsonPropertyOrder(1)] public string guid { get; set; }
        [JsonPropertyOrder(2)] public string jobId { get; set; }
        [JsonPropertyOrder(3)] public string service { get; set; }
        [JsonPropertyOrder(4)] public string type { get; set; }
        [JsonPropertyOrder(5)] public string subType { get; set; }
        [JsonPropertyOrder(6)] public int sequence { get; set; }                   //현재 명령의 실행 순서 이 값은 실행 전 재정렬에 따라 변경될 수 있음
        [JsonPropertyOrder(7)] public bool isLocked { get; set; }                   // 취소 불가
        [JsonPropertyOrder(8)] public int sequenceChangeCount { get; set; } = 0;   // 시퀀스가 변경된 누적 횟수 예: 재정렬이 3번 발생했다면 3
        [JsonPropertyOrder(9)] public int retryCount { get; set; } = 0;            // 명령 실패 시 재시도한 횟수 (기본값은 0)
        [JsonPropertyOrder(10)] public string state { get; set; }
        [JsonPropertyOrder(11)] public string? serviceMissionId { get; set; }                //Command Post 성공시 ReturnId 받아야함
        [JsonPropertyOrder(12)] public string specifiedWorkerId { get; set; }            //order 지정된 Worker
        [JsonPropertyOrder(13)] public string assignedWorkerId { get; set; }             //할당된 Worker
        [JsonPropertyOrder(14)] public DateTime createdAt { get; set; }                  // 생성 시각
        [JsonPropertyOrder(15)] public DateTime? updatedAt { get; set; }
        [JsonPropertyOrder(16)] public DateTime? finishedAt { get; set; }
        [JsonPropertyOrder(17)] public DateTime? sequenceUpdatedAt { get; set; }  // 시퀀스가 마지막으로 변경된 시간 재정렬 발생 시 이 값이 갱신됨
        [JsonPropertyOrder(18)] public string parametersJson { get; set; }        // DB 파라메타를 저장하기 위하여
        [JsonPropertyOrder(19)] public List<Parameta> parameters { get; set; } = new List<Parameta>();          // 명령 실행 시 필요한 추가 옵션을 JSON 문자열로 저장  예: 속도, 방향, 특수 처리 조건 등

    }
}
