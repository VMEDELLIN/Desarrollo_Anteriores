using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Consumer
{
    public static void Ejecutar()
    {
        // Establecer conexión con RabbitMQ
        var factory = new ConnectionFactory() { HostName = "localhost" };


        var connection2 = factory.CreateConnection();
        var channel2 = connection2.CreateModel();        
        // Crear la cola de mensajes
        channel2.QueueDeclare(queue: "mi_cola_retorno",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Crear la cola de mensajes
            channel.QueueDeclare(queue: "mi_cola",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Configurar el consumidor
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensaje recibido: {0}", message);

                var messageRetorno = $"{message } Retorno";
                var datoretornar = System.Text.Encoding.UTF8.GetBytes(messageRetorno);
                channel2.BasicPublish(exchange: "",
                                     routingKey: "mi_cola_retorno",
                                     basicProperties: null,
                                     body: datoretornar);
            };

            // Comenzar a consumir mensajes
            channel.BasicConsume(queue: "mi_cola",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Esperando mensajes...");
            Console.WriteLine("Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }

       
    }
}