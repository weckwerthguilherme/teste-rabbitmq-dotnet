using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;

namespace RabbitMQExample
{
    public class RabbitConsumer
    {
        public void ConsumirMensagens()
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConfigurationManager.AppSettings["RABBITMQ_HOST"],
                Port = int.Parse(ConfigurationManager.AppSettings["RABBITMQ_PORT"]), // Converte porta para int
                UserName = ConfigurationManager.AppSettings["RABBITMQ_USER"],
                Password = ConfigurationManager.AppSettings["RABBITMQ_PASS"],
                VirtualHost = "/"
            };

            // Conexão e canal "à moda antiga"
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "fila_teste",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                // Em RabbitMQ.Client 5.x, Body é byte[] (não ReadOnlyMemory)
                var body = ea.Body;
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Recebido: {mensagem}");
            };

            channel.BasicConsume(
                queue: "fila_teste",
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("🟢 Aguardando mensagens... Pressione ENTER para sair.");
            Console.ReadLine();

            // Libera recursos ao encerrar
            channel.Close();
            connection.Close();
        }
    }
}
