using ChatApp.Models.Data;

namespace ChatApp.Models.ViewModel
{
    public class UserChatVM
    {
        public List<ChatDTO> Chats { get; set; }
        public ChatDTO SelectedChat { get; set; }
        public UserDTO CurrentUser { get; set; }
        public MessageDTO Message { get; set; }

    }
}
