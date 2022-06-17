using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using ProjectNixon.Model;
using System.Text.Json;


namespace ProjectNixon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly ILogger<MessageController> _logger;
        private readonly ServiceBusClient client;

        public MessageController(ILogger<MessageController> logger, ServiceBusClient client)
        {
            _logger = logger;
            this.client = client;
        }


        private static List<Message> messages = new List<Message>
            {
                /*new Message { Id = 1,Subject = "First Bus Service", Content = "Are you at the Bus Stop?"},
                new Message { Id = 2,Subject = "Second Message", Content = "Second Message Contents"},
                new Message { Id = 3, Subject = "Third Message", Content = "Third Message Contents" },*/
            };

        [HttpGet]
        public async Task<ActionResult<List<Message>>> Get()
        {
 
            var body = JsonSerializer.Serialize(messages[0]);
            var sender = client.CreateSender("messagecreated");
            var sbMessage = new ServiceBusMessage(body);
            sender.SendMessageAsync(sbMessage).Wait();
            
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<List<Message>>> AddMessage(Message message)
        {
            var body = JsonSerializer.Serialize(message);

            var sender = client.CreateSender("bus-stop");

            var sbMessage = new ServiceBusMessage(body);
            sender.SendMessageAsync(sbMessage).Wait();

            messages.Add(message);

           
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(int id)
        {
            var message = messages.Find(i => i.Id == id);

            if (message == null)
                return BadRequest("Message is not found");
            return Ok(message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMessage(Message request)
        {
            var message = messages.Find(i => i.Id == request.Id);

            if (message == null)
                return BadRequest("Message is not found");

            message.Subject = request.Subject;
            message.Content = request.Content;

            return Ok(message);

        }

        [HttpDelete]


        public async Task<ActionResult> DeleteMessage(int id)
        {
            var message = messages.Find(i => i.Id == id);

            if (message != null)
                return BadRequest("Message is not Found!");

            messages.Remove(message);
            return Ok("Message is deleted!");
        }
    }
}
