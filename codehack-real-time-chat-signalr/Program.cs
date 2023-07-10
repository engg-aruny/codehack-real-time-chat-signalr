using codehack_realtime_chat_signalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedSpecificOriginsPolicy = "_AllowedSpecificOriginsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedSpecificOriginsPolicy, builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


builder.Services.AddSignalR()
       .AddAzureSignalR(options =>
       {
           options.ConnectionString = "Endpoint=https://signalr-codehack-demo.service.signalr.net;AccessKey=m4DQP9db2Kzp7lHo9+39tgnKnScWybXIbMXicf7yC7M=;Version=1.0;";
       });

var app = builder.Build();

app.UseCors(allowedSpecificOriginsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat");


app.Run();
