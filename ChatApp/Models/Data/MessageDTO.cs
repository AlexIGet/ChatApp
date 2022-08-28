using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models.Data
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public MessageType MessageType { get; set; }
        public int ChatId { get; set; }
        public ChatDTO Chat { get; set; }
        public int? UserId { get; set; }
        public UserDTO User { get; set; }

        [InverseProperty("MainMessage")]
        public ICollection<ForwardMessageDTO> MainMessages { get; set; }

        [InverseProperty("RelatedMessage")]
        public ICollection<ForwardMessageDTO> RelatedMessages { get; set; }
    }
    
    public enum MessageType
    {
        FORWARD,
        NORMAL
    }
}
