using SocialNetWork.DAL.Models.Users;
using System.Collections.Generic;

namespace SocialNetWork.BL.ViewModels.Account
{
    public class ChatViewModel
    {
        public MessageViewModel Message { get; set; }
        public User Recipient { get; set; }
        public User Sender { get; set; }
        public List<Message> History { get; set; }

        public ChatViewModel() { Message = new MessageViewModel(); }
    }
}
