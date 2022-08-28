namespace ChatApp.Models.Data
{
    public class ParticipantDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ChatID { get; set; }
        public UserDTO User { get; set; }
        public ChatDTO Chat { get; set; }
    }
}
