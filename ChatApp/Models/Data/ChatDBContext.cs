using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models.Data
{
    public class ChatDBContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<ParticipantDTO> Participants { get; set; }
        public DbSet<MessageDTO> Messages { get; set; }
        public DbSet<ForwardMessageDTO> ForwardMessages { get; set; }
        public DbSet<ChatDTO> Chats { get; set; }
        public ChatDBContext(DbContextOptions<ChatDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
