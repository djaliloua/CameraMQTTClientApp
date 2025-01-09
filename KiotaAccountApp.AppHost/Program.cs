var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MQTTConfigWebApi>("mqttconfigwebapi");

builder.Build().Run();
