using ChatApp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ChatDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ChatDBContext>>()))
            {
                List<bool> isMigrated = new List<bool>();
                isMigrated.Add(context.Users.Any());
                isMigrated.Add(context.Participants.Any());
                isMigrated.Add(context.Messages.Any());
                isMigrated.Add(context.ForwardMessages.Any());
                isMigrated.Add(context.Chats.Any());

                if (isMigrated.Any(x => x == true))
                {
                    return;
                }

                context.Users.AddRange(
                    new UserDTO
                    {
                        FirstName = "FN User 1",
                        LastName = "LN User 1",
                        Email = "user1@gmail.com"
                    },
                    new UserDTO
                    {
                        FirstName = "FN User 2",
                        LastName = "LN User 2",
                        Email = "user2@gmail.com"
                    },
                    new UserDTO
                    {
                        FirstName = "FN User 3",
                        LastName = "LN User 3",
                        Email = "user3@gmail.com"
                    }
                );
                context.SaveChanges();
                context.Chats.AddRange(
                    new ChatDTO
                    {
                        Title = "User1_User2",
                        CreatedDate = DateTime.Now.AddDays(-1),
                        UserID = 1
                    },
                    new ChatDTO
                    {
                        Title = "Groups",
                        CreatedDate = DateTime.Now.AddDays(-2),
                        UserID = 1
                    }
                );
                context.SaveChanges();
                context.Participants.AddRange(
                    new ParticipantDTO
                    {
                        UserId = 1,
                        ChatID = 1
                    },
                    new ParticipantDTO
                    {
                        UserId = 2,
                        ChatID = 1
                    },
                    new ParticipantDTO
                    {
                        UserId = 1,
                        ChatID = 2
                    },
                    new ParticipantDTO
                    {
                        UserId = 2,
                        ChatID = 2
                    },
                    new ParticipantDTO
                    {
                        UserId = 3,
                        ChatID = 2
                    }
                );
                context.SaveChanges();
                context.Messages.AddRange(
                    //1
                    new MessageDTO
                    {
                        Body = "Private message: Hi, I'm user 1",
                        CreatedDate = DateTime.Now.AddMinutes(-30),
                        MessageType = MessageType.NORMAL,
                        ChatId = 1,
                        UserId = 1
                    },
                    //2
                    new MessageDTO
                    {
                        Body = "Private message: Hi, I'm user 2",
                        CreatedDate = DateTime.Now.AddMinutes(-31),
                        MessageType = MessageType.NORMAL,
                        ChatId = 1,
                        UserId = 2
                    },
                    //3
                    new MessageDTO
                    {
                        Body = "Group message: Hi, I'm user 1",
                        CreatedDate = DateTime.Now.AddMinutes(-30),
                        MessageType = MessageType.NORMAL,
                        ChatId = 2,
                        UserId = 1
                    },
                    //4
                    new MessageDTO
                    {
                        Body = "Group message: Hi, I'm user 2",
                        CreatedDate = DateTime.Now.AddMinutes(-31),
                        MessageType = MessageType.NORMAL,
                        ChatId = 2,
                        UserId = 2
                    },
                    //5
                    new MessageDTO
                    {
                        Body = "Group message: Hi, I'm user 3",
                        CreatedDate = DateTime.Now.AddMinutes(-32),
                        MessageType = MessageType.NORMAL,
                        ChatId = 2,
                        UserId = 3
                    },
                    //6
                    new MessageDTO
                    {
                        Body = "Group message: Hi, User 2 I'm user 3",
                        CreatedDate = DateTime.Now.AddMinutes(-33),
                        MessageType = MessageType.FORWARD,
                        ChatId = 2,
                        UserId = 3
                    }
                );
                context.SaveChanges();
                context.ForwardMessages.Add(
                    new ForwardMessageDTO
                    {
                        MainMessageId = 6,
                        RelatedMessageId = 4,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
