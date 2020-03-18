using EventTable.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;

namespace EventTable.Controllers
{
    /// <summary>
    /// Контроллер, в который приходит ответ от пользователя telegram
    /// </summary>
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();

            foreach (var command in commands)
            {
                //Так как много комманд - сделать их через case
                //Здесь идет сопоставление пришедших комманд с существующими 
                //Происходит их выполнение
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }

            return Ok();
        }

    }
}