using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models.Data
{
    public class ForwardMessageDTO
    {
        public int Id { get; set; }

        public int MainMessageId { get; set; }

        public int? RelatedMessageId { get; set; }
        
        public MessageDTO MainMessage { get; set; }

        public MessageDTO RelatedMessage { get; set; }
    }
}
