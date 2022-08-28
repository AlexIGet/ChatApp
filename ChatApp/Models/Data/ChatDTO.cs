using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Data
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{dddd, dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }

        public ICollection<MessageDTO> Messages { get; set; }
        public ICollection<ParticipantDTO> Participants { get; set; }
    }
}
