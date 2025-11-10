using RabbitMQ.Client;
using System;
using System.Text;

using System.Configuration;

namespace RabbitMQExample
{
    public class RabbitPublisher
    {
        public void EnviarMensagem(string mensagem)
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConfigurationManager.AppSettings["RABBITMQ_HOST"],
                Port = int.Parse(ConfigurationManager.AppSettings["RABBITMQ_PORT"]), // Converte porta para int
                UserName = ConfigurationManager.AppSettings["RABBITMQ_USER"],
                Password = ConfigurationManager.AppSettings["RABBITMQ_PASS"],
                VirtualHost = "/"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declara (ou garante) a fila
                channel.QueueDeclare(
                    queue: "fila_teste", //Criei essa fila manualmente no painel
                    durable: true, //Mudei pra true porque eu configurei a fila_teste no painel como durable na hora da criação
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(mensagem);

                // Publica a mensagem
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "fila_teste",
                    basicProperties: null,
                    body: body
                );

                Console.WriteLine($"[x] Enviado: {mensagem}");
            }
        }
    }
}
