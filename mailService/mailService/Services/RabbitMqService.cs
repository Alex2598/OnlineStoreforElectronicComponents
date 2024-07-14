using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using mailService.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Connections;

namespace mailService.Services
{
    public class RabbitMqService
    {
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public RabbitMqService(IConfiguration configuration, IMailService mailService)
        {
            _configuration = configuration;
            _mailService = mailService;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetValue<string>("RabbitMq:Host"),
                Port = _configuration.GetValue<int>("RabbitMq:Port"),
                UserName = _configuration.GetValue<string>("RabbitMq:Username"),
                Password = _configuration.GetValue<string>("RabbitMq:Password")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var queueName = "checkout_orders"; // Имя вашей очереди

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Десериализация JSON сообщения
                var mailData = JsonConvert.DeserializeObject<MailData>(message);

                // Отправка письма
                await _mailService.SendAsync(mailData);

                // Подтверждение получения сообщения
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            Console.WriteLine("Слушатель RabbitMQ запущен. Ожидание сообщений...");
            Console.ReadLine(); // Ждем, чтобы консоль не закрылась
        }
    }
}