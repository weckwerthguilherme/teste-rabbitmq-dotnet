using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQExample;


namespace ConsumerSolo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consumer = new RabbitConsumer();

            Console.WriteLine("Você está consumindo mensagens");
            consumer.ConsumirMensagens();
        }
    }
}
