var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MQTTConfigWebApi>("mqttconfigwebapi");
builder.AddProject<Projects.IdentityServerApp>("kiotaaccountapp");

builder.Build().Run();
