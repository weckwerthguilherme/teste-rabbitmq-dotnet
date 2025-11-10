using System;

namespace RabbitMQExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TODO: Adicionar uma opção pra logging também no menu principal para ver o que foi feito.

            bool rodando = true;

            while(rodando)
            {
                Console.WriteLine("Escolha uma opção: \n 1. Publicar mensagem \n 2. Consumir mensagens \n 3. Sair");
                int opcao = int.Parse(Console.ReadLine());
                switch (opcao) 
                {
                    case 1:
                        var publisher = new RabbitPublisher();
                        bool publicando = true;
                        Console.WriteLine("Você está publicando mensagens");
                        Console.WriteLine("digite 'sair' para parar");
                        while (publicando)
                        {
                            var mensagem = Console.ReadLine();
                            if (mensagem == "sair")
                            {
                                publicando = false;
                            } else
                            {
                                publisher.EnviarMensagem(mensagem);
                            }
                            

                        }
                        break;
                    case 2:
                        var consumer = new RabbitConsumer();
                        
                        Console.WriteLine("Você está consumindo mensagens");
                        consumer.ConsumirMensagens(); 
                        break;
                    case 3:
                        rodando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadLine();
                        break;
                }
            }

            
        }
    }
}
