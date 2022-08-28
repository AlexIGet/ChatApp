using ChatApp.Models.Data;
using ChatApp.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatDBContext _context;
        private readonly IHubContext<MessangerHub> _hubContext;

        public ChatController(ChatDBContext context, IHubContext<MessangerHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: ChatController
        public async Task<IActionResult> Index(int UserId, int ChatId)
        {
            UserChatVM model = new UserChatVM();
            var userChatIdList = _context.Participants
                .Include(item => item.User)
                .Where(item => item.User.Id == UserId)
                .Select(item => item.ChatID);

            var userChats = _context.Chats
                .Where(item => userChatIdList.Contains(item.Id))
                .Include(item => item.Participants)
                .Include(item => item.Messages)
                .ThenInclude(m => m.User);

            model.SelectedChat = await userChats.FirstOrDefaultAsync(chat => chat.Id == ChatId);
            model.CurrentUser = await _context.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            model.Chats = await userChats.ToListAsync();
            
            return View(model);
        }
        [Route("~/Chat/SendMessage")]
        [HttpPost]
        public async Task<Task> SendMessage(
            [FromForm]string body, 
            [FromForm] MessageType messageType,
            [FromForm] int chatId,
            [FromForm] int userId,
            [FromForm] string connection
            )
        {

            MessageDTO messageDTO = new MessageDTO();
            messageDTO.Body = body;
            messageDTO.MessageType = messageType;
            messageDTO.CreatedDate = DateTime.Now;
            messageDTO.ChatId = chatId;
            messageDTO.UserId = userId;

            _context.Messages.Add(messageDTO);
            _context.SaveChanges();

            messageDTO.User = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            string json ="";
            try
            {
                json = JsonSerializer.Serialize(messageDTO, jsonSerializerOptions);
            }
            catch
            {
            }

            await _hubContext.Clients.All.SendAsync("Notify", json);
            return Task.CompletedTask;
        }
    }
}
