
using Amld.Extensions.Logging;
using Amld.Extensions.Logging.AspNetCore;
using Amld.Extensions.Logging.Kafka;
using Elastic.Clients.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile($"appsettings.json", true, true);

//builder.Host.ConfigureLogging((context, logging) =>
//{
//    logging.ClearProviders();
//    logging.AddAmldLogger()
//    .AddKafkaWriter(context.Configuration);
//});

builder.Logging
    .ClearProviders()
    .AddAmldLogger()
    .AddKafkaWriter(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var constr = builder.Configuration.GetValue<string>("Elasticsearch");
builder.Services.AddSingleton(x=>new ElasticsearchClient(new ElasticsearchClientSettings(new Uri(constr))));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseLogTrace();
app.UseAuthorization();
app.MapControllers();

app.Run();
