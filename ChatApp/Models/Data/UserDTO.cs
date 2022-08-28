namespace ChatApp.Models.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }    
        
        public ICollection<MessageDTO> Messages { get; set; }
        public ICollection<ParticipantDTO> Participants { get; set; }
        public ICollection<ChatDTO> Chats { get; set; }
    }
}
