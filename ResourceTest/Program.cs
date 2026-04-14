using ResourceTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Kestrel 설정을 appsettings.json에서 읽어오도록 설정
builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});
// builder.Services.AddControllers():
// ASP.NET Core 웹 API에서 컨트롤러를 사용하기 위해 서비스 컬렉션에 컨트롤러를 추가합니다. 이 메서드는 기본적으로 JSON 형식으로 응답을 반환하도록 설정합니다.
// .AddJsonOptions(o => { ... }):
// JSON 직렬화 옵션을 설정하기 위해 사용됩니다. 이 메서드는 JsonSerializerOptions 객체를 매개변수로 받습니다.
// o.JsonSerializerOptions.IncludeFields = true;:
// JSON 직렬화 시 필드를 포함하도록 설정합니다. 기본적으로 필드는 직렬화에서 제외됩니다. 이 옵션을 true로 설정하면 필드도 JSON 응답에 포함됩니다
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.IncludeFields = true;
});

// ★ 서비스 모드 활성화
builder.Host.UseWindowsService();

var app = builder.Build();

// ===============================
//  서비스 수명 이벤트에 log 연결
// ===============================
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

// 1) 서비스 시작됨
lifetime.ApplicationStarted.Register(() =>
{
    //EventLogger.Info($"[SERVICE] Started  | PID={Environment.ProcessId}");
    Console.WriteLine($"[SERVICE] Started  | PID={Environment.ProcessId}");
});

// 2) 서비스 중지 진행 중 (Stop 눌렀을 때 바로 찍힘)
lifetime.ApplicationStopping.Register(() =>
{
    Console.WriteLine($"[SERVICE] Stopping | PID={Environment.ProcessId}");
});

// 3) 서비스 완전히 종료됨
lifetime.ApplicationStopped.Register(() =>
{
    Console.WriteLine($"[SERVICE] Stopped  | PID={Environment.ProcessId}");
});

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    //Config 데이터 Load
    ConfigData.Load(config);
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    // Swagger JSON 엔드포인트 등록 (필수)
    // 이 부분에서 Swagger 문서의 URL과 표시 이름을 지정합니다.
    // "/swagger/v1/swagger.json"은 기본 경로이고,
    // "Sorted API v1"은 Swagger UI 상단 드롭다운에 보이는 이름입니다.
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

    // operationsSorter는 Swagger UI에서 HTTP 메서드(GET, POST 등) 또는 이름 기준으로
    // API 엔드포인트를 정렬하도록 지정하는 옵션입니다.
    // 기본값은 등록 순서이며, 아래처럼 설정하지 않으면 정렬이 되지 않습니다.
    // method: GET → POST → PUT → DELETE 순서로 정렬
    // alpha:  경로 문자열을 알파벳 순으로 정렬
    // 최신 Swashbuckle에서는 아래와 같이 설정해야 정상 적용됩니다.
    c.ConfigObject.AdditionalItems["operationsSorter"] = "method";

    // ?? Swagger UI의 <head> 영역에 HTML을 직접 삽입할 수 있도록 하는 속성입니다.
    // 아래는 HTML <style> 태그를 삽입하여 특정 UI 요소를 CSS로 숨깁니다.
    c.HeadContent = @"
        <style>
            /* Swagger UI에서 'Schemas(models)' 섹션을 통째로 숨깁니다 */

            /* .models는 왼쪽 하단에 자동 생성되는 스키마 목록 영역을 의미합니다 */
            .swagger-ui .models {
                display: none !important;
            }

            /* section.models는 'Schemas'라는 제목 텍스트 부분을 의미합니다 */
            .swagger-ui section.models {
                display: none !important;
            }
        </style>";
});
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();