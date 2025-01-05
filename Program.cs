using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

class Program
{
    private const string connectionString = "Endpoint=sb://orderprocessingnamespace.servicebus.windows.net/;SharedAccessKeyName=duplicatesas;SharedAccessKey=qvGWcCibg0N8vTOL3uzbIVfllEj7PioQU+ASbHDY6DQ=;EntityPath=duplicatequeue";
    private const string queueName = "duplicatequeue";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Sending messages to Azure Service Bus...");
        await SendMessagesAsync();
    }

    static async Task SendMessagesAsync()
    {
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(queueName);

        try
        {
            string messageId = "1234567"; // Simulating duplicate messages

            // Create original and duplicate messages
            var originalMessage = new ServiceBusMessage("Original Message")
            {
                MessageId = messageId
            };

            var duplicateMessage = new ServiceBusMessage("Duplicate Message")
            {
                MessageId = messageId
            };

            // Send messages
            await sender.SendMessageAsync(originalMessage);
            Console.WriteLine("Sent: Original Message");

            await sender.SendMessageAsync(duplicateMessage);
            Console.WriteLine("Sent: Duplicate Message");
        }
        finally
        {
            await sender.DisposeAsync();
        }
    }
}
