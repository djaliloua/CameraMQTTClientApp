using DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Models;

namespace MQTTConfigWebApi
{
    public static class ApplicationExtension
    {
        public static WebApplication RunMigrations(this WebApplication app, ILogger<Program> logger)
        {
            var datacontext = new MQTTConfigContext();
            logger.LogInformation(datacontext.Database.GetConnectionString());
            datacontext.Database.Migrate();
            if (datacontext != null)
            {
                if (datacontext.MQTTConfigs.Count() == 0)
                {
                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "192.168.1.131",
                        CameraId = Guid.NewGuid(),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "local"
                    });

                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "20.208.128.223",
                        CameraId = Guid.NewGuid(),
                        Port = "1883",
                        BaseTopicName = "MauiCamMqttClient",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "Azure"
                    });
                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "broker.hivemq.com",
                        CameraId = Guid.NewGuid(),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "Hive"
                    });
                    
                    datacontext.SaveChanges();
                }

            }
            return app;
        }
    }
}
