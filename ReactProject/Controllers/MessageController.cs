using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceBus.Messaging;
using System.Text.Json;
using System.Threading.Tasks;


namespace ReactProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly ServiceBusClient _client;

        public MessageController(ILogger<MessageController> logger, ServiceBusClient client)
        {
            this._logger = logger;
            this._client = client;
        }

        private static List<Message> messages = new List<Message>
        {
            new Message { Subject = "First Bus Service", Content = "Are you at the Bus Stop?"},
            /*new Message { Subject = "Second Message", Content = "Second Message Contents"},
            new Message { Subject = "Third Message", Content = "Third Message Contents" },*/
        };

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var body = JsonSerializer.Serialize(messages[0]);
            //var sender = _client.CreateSender("messagecreated");
            var sbMessage = new ServiceBusMessage(body);

            //sender.SendMessageAsync(sbMessage).Wait();

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Message message)
        {
            var body = JsonSerializer.Serialize(message);
            //var sender = _client.CreateSender("messagecreated");
            var sbMessage = new ServiceBusMessage(body);

            //sender.SendMessageAsync(sbMessage).Wait();

            return Ok(message);
        }

        static string connectionString = "Endpoint=sb://jakejakejakehello.servicebus.windows.net/;SharedAccessKeyName=ErolsPolicy;SharedAccessKey=d8Qx9bYC8OVE0UqTalfZwzVr9deI1xi3IFTXJw9rGqE=;EntityPath=erolqueue";
        static string queueName = "erolqueue";
        static ServiceBusClient client = new ServiceBusClient(connectionString);
        static ServiceBusProcessor processor;



        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            await args.CompleteMessageAsync(args.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        { 
            /* TODO: uncomment when ready to consume message from queue
            Console.WriteLine("Delete is revoked");
            var body = JsonSerializer.Serialize(messages[0]);
            var sender = client.CreateSender(queueName);
            var sbMessage = new ServiceBusMessage(body);

            var receiver = client.CreateReceiver(queueName);
            var msj = await receiver.ReceiveMessageAsync();
            */


            //sender.SendMessageAsync(sbMessage).Wait();

            return Ok("I have called the delete endpoint");
        }
    }

}
