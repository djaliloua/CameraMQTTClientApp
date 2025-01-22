using OpenAIClient;
using System.Net.Security;

//IApiService apiService = new ApiService();
//var config = await apiService.GetMQTTConfigByGuidAsync(Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"));
//Console.WriteLine(config.HostName);
var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) =>
    {
        if (policyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
        {
            // Log the issue (optional)
            //Console.WriteLine("Ignoring RemoteCertificateNameMismatch");
        }

        // Ignore all certificate errors in development
        return true;
    }
};
//var certificate = new X509Certificate2("./../../../certificate.pfx", "YourPassword");
//handler.ClientCertificates.Add(certificate);
var client = new Client("https://192.168.1.131:5001", new HttpClient(handler));

var x = await client.MqttConfigAllAsync();
Console.WriteLine(x.Count);


