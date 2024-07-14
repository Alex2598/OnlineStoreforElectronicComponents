using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

public class RabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendCheckoutMessage(RabbitMqMessage model)
    {
        // Получение настроек подключения из appsettings.json
        var host = _configuration.GetValue<string>("RabbitMq:Host");
        var port = _configuration.GetValue<int>("RabbitMq:Port");
        var username = _configuration.GetValue<string>("RabbitMq:Username");
        var password = _configuration.GetValue<string>("RabbitMq:Password");

        // Создание соединения
        var factory = new ConnectionFactory() { HostName = host, Port = port, UserName = username, Password = password };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Определение очереди, обмена и ключа маршрутизации
        var queueName = "checkout_orders";
        var exchangeName = "checkout_exchange";
        var routingKey = "checkout_orders";

        // Создание очереди, если она не существует
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        // Создание обмена, если он не существует
        channel.ExchangeDeclare(exchange: exchangeName, type: "direct", durable: true, autoDelete: false, arguments: null);

        // Связывание очереди с обменом
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

        // Создание сообщения 
        var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

        // Отправка сообщения в очередь
        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: message);
    }
}