using azicloud.res.Application.Interfaces;
using azicloud.res.Application.Responsitory;
using azicloud.res.bo;
using azicloud.res.GrpcServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(option =>
{
    option.ListenAnyIP(7076, configure =>
    {
        configure.UseHttps();
        configure.Protocols = HttpProtocols.Http1;
    });
    option.ListenAnyIP(5076, configure =>
    {       
        configure.Protocols = HttpProtocols.Http1;
    });
    option.ListenAnyIP(7086, configure =>
    {
        configure.UseHttps();
        configure.Protocols = HttpProtocols.Http2;
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddResService(builder.Configuration.GetConnectionString("db_task"));
builder.Services.AddTransient<IWikiCategoryService, WikiCategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<WikiCategoryGrpcService>();

app.Run();
