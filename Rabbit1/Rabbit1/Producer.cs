using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Producer
{
    public static void Ejecutar()
    {
        // Establecer conexión con RabbitMQ
        var factory = new ConnectionFactory() { HostName = "localhost" };

        var connection2 = factory.CreateConnection();
        var channel2 = connection2.CreateModel();
        channel2.QueueDeclare(queue: "mi_cola_retorno",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        var consumer = new EventingBasicConsumer(channel2);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine("Mensaje recibido retorno: {0}", message);
        };
        // Comenzar a consumir mensajes
        channel2.BasicConsume(queue: "mi_cola_retorno",
                             autoAck: true,
                             consumer: consumer);



        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();        
        // Crear la cola de mensajes
        channel.QueueDeclare(queue: "mi_cola",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);            

        // Enviar un mensaje                        
        Console.WriteLine("Introdusca un texto");
        string dato = string.Empty;
        do
        {
            dato = Console.ReadLine();
            var datoenviar = System.Text.Encoding.UTF8.GetBytes(dato);
            channel.BasicPublish(exchange: "",
                                    routingKey: "mi_cola",
                                    basicProperties: null,
                                    body: datoenviar);

            Console.WriteLine("Mensaje enviado: {0}", dato);

        } while (dato != "exit");
            
        

        Console.WriteLine("Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}