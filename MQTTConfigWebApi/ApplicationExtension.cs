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
                        CameraId = Guid.Parse("B7312B62-ACE1-4F7D-BB05-F1502FAD67C4"),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "local"
                    });

                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "20.208.128.223",
                        CameraId = Guid.Parse("B424CAA3-5F83-4F10-8A9A-13A7305FD9D4"),
                        Port = "1883",
                        BaseTopicName = "MauiCamMqttClient",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "Azure"
                    });
                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "broker.hivemq.com",
                        CameraId = Guid.Parse("8F102BDD-B337-4FC7-B24D-A493E32CFAAE"),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "video/home/room",
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
