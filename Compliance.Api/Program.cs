using Compliance.Api;
using Compliance.Shared;
using Compliance.Application;
using Compliance.Infrastructure;
using static Compliance.Api.Extensions;
using Compliance.Infrastructure.ApiClient;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApiClient();
builder.AddLogging()
       .AddConfiguration();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8068);
    

});

builder.Services
    .AddCors("CorsPolicy")
    .AddCors("AllowAll")
    .AddSwagger()
    .AddAuthentication(builder.Configuration)
    .AddCaching(builder)
    .AddShared(GetDefaultConnectionString(builder.Configuration))
    .AddApplication(builder.Configuration)
    .AddInfrastructure(
       GetDefaultConnectionString(builder.Configuration),
        builder.Configuration);

builder.Services.AddAutofacServices();
builder.Services.AddApiClient();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("CorsPolicy");AllowAll
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseInfrastructure();

app.MapControllers();

app.Run();