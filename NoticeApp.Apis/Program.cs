using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoticeApp.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region CORS
// [CORS][1] CORS ��� ���
// [CORS][1][1] �⺻ : ��� ���
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOrigin", 
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

// [CORS][1][2] ���� : ��� ���
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllPolicy",
		builder => builder
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

// [CORS][1][3] ����: Ư�� �����θ� ���
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

// [CORS][2]CORS ��� ���
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// ��������(Notice) ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ����
/// </summary>
/// <param name="builder">
void AddDependencyInjectionContainerForNoticeApp(WebApplicationBuilder builder)
{
	// NoticeAppDbContext.cs Inject
	builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
	builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();
}