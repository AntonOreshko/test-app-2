using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

var builder = WebApplication.CreateBuilder(args);

var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
var accessSecret = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
var region = Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION");
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var awsOptions = new AWSOptions
{
    Credentials = new BasicAWSCredentials(accessKey, accessSecret),
    Region = RegionEndpoint.GetBySystemName(region)
};

var path = $"/test-app-2/{environment}/";
builder.Configuration.AddSystemsManager(path, reloadAfter: TimeSpan.FromMinutes(5), awsOptions: awsOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddDefaultAWSOptions(awsOptions);

//var awsOptions = builder.Configuration.GetAWSOptions();
//builder.Services.AddDefaultAWSOptions(awsOptions);

var app = builder.Build();

app.MapHealthChecks("/health"); 

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();