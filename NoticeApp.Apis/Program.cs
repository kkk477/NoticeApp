using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoticeApp.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region CORS
// [CORS][1] CORS 사용 등록
// [CORS][1][1] 기본 : 모두 허용
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOrigin", 
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

// [CORS][1][2] 참고 : 모두 허용
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllPolicy",
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

// [CORS][1][3] 참고: 특정 도메인만 허용
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecific",
		builder => builder
		.WithOrigins("https://localhost:44356")
		.WithMethods("GET","POST", "PUT", "PATCH", "DELETE")
		.WithHeaders("accept", "content-type", "origin", "X-TotalRecordCount"));
});
#endregion

AddDependencyInjectionContainerForNoticeApp(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// [CORS][2]CORS 사용 허용
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// 공지사항(Notice) 관련 의존성(종속성) 주입 관련 코드만 따로 모아서 관리
/// </summary>
/// <param name="builder">
void AddDependencyInjectionContainerForNoticeApp(WebApplicationBuilder builder)
{
	// NoticeAppDbContext.cs Inject
	builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
	builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();
}