using CustomerService.Interface;
using CustomerService.Service;
using HttpClientService;
using MSQP.SDK.DOTNET;
using MSQP.Shared.Configuration;
using System.Diagnostics;

var testGithub = "github";
var testGithub2 = "github2";
var testGithub3 = "github3";
var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterHttpClientService();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Kafka configuration
var kafkaConf = builder.Configuration.GetSection(nameof(KafkaConfiguration)).Get<KafkaConfiguration>();
builder.Services.AddSingleton(kafkaConf);
//Add Queue service
builder.Services.AddSingleton<IQueueService, QueueService>();
builder.Services.AddSingleton<ICustomerFunc, CustomerRead>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/nosdk", async (IHttpService httpService) =>
{
    double customerElapsed = 0;
    double orderElapsed = 0;
    Stopwatch stopwatch = Stopwatch.StartNew();

    var newCustomer = await httpService.Post<Customer, Customer>("http://localhost:5020/newcustomer", new Customer("Jonh", "Robert", 10));
    stopwatch.Stop();
    customerElapsed = stopwatch.Elapsed.TotalSeconds;

    stopwatch.Start();
    var newOder = await httpService.Post<OpenOrders, OpenOrders>("http://localhost:5107/openorder", new OpenOrders("20220630-order", "20220630", "Apple", 50, 30000));
    stopwatch.Stop();
    orderElapsed = stopwatch.Elapsed.TotalSeconds;

    return Results.Ok(new { customer = $"Use time: {customerElapsed}", order = $"Use time: {orderElapsed}" });
})
.WithName("nosdk");

app.MapPost("/usesdk", async (IQueueService queueService) =>
{

    Stopwatch stopwatch = Stopwatch.StartNew();
    //await Task.WhenAll(NewCustomer(queueService), NewOrder(queueService));
    await Task.WhenAll(NewCustomer(queueService));
    stopwatch.Stop();

    return Results.Ok(new { time = $"Use Time: {stopwatch.Elapsed.TotalSeconds}"});
})
.WithName("usesdk");


app.MapPost("/customerfunc", async (IQueueService queueService) =>
{
    ICustomerFunc customerFunc = new CustomerRead();
    return Results.Ok(new { text = customerFunc.Read() });
})
.WithName("customerfunc");


app.Run();


async Task NewCustomer(IQueueService queueService)
{
    var conf = new QueueCredential() { Secret = "", ServiceID = "16566537392873284" };
    var sendPost = await queueService.SendAsync(new Customer("David", "Smith", 48), conf);
}

async Task NewOrder(IQueueService queueService)
{
    var conf = new QueueCredential() { Secret = "", ServiceID = "16566537954959343" };
    var sendPost = await queueService.SendAsync(new OpenOrders("0001-sdk", "20220630", "Orange", 30, 12000), conf);
}
internal record OpenOrders(string id, string datetime, string item, int amount, int price)
{

}
internal record Customer(string name, string lastname, int age)
{

}