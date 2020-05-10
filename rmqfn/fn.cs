using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace rmqfn
{
    public static class fn
    {
        [FunctionName("fn")]
        public static void Run(
            [RabbitMQTrigger("%InputQueueName%", ConnectionStringSetting = "RabbitMqConnection")] string inputMessage,
            [RabbitMQ(ConnectionStringSetting = "RabbitMqConnection")] IModel client,
            ILogger log)
        {
            RMQMessage message = JsonConvert.DeserializeObject<RMQMessage>(inputMessage);
            log.LogInformation($"Message received {inputMessage}.");
            log.LogInformation($"DeviceID {message.deviceid}.");
            log.LogInformation($"Temperature {message.temperature}.");

            if(message.temperature < 20.0f)
            {
                string notificationMessage = inputMessage;
                var body = Encoding.UTF8.GetBytes(notificationMessage);
                var queuename = Environment.GetEnvironmentVariable("OutputQueueName");

                QueueDeclareOk queue = client.QueueDeclare(queuename, true, false, false, null);

                client.BasicPublish(exchange: "", routingKey: queuename, basicProperties: null, body: body);
            }
        }
    }
}
