using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSystemsManager("/test-app-2/Development/", reloadAfter: TimeSpan.FromMinutes(5));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

// way 1
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
// way2
// var awsOptions = builder.Configuration.GetAWSOptions();
// builder.Services.AddDefaultAWSOptions(awsOptions);
// way3
// var awsOptions = new AWSOptions 
// {
//     Credentials = new BasicAWSCredentials("yourAccessKey", "yourAccessSecret")
// };
// builder.Services.AddDefaultAWSOptions(awsOptions);

var app = builder.Build();

app.MapHealthChecks("/health"); 

app.UseSwagger();
app.UseSwaggerUI();

/**/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();