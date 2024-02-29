using System.ComponentModel.DataAnnotations;

namespace SocialNetWork.DAL.Models.Users
{
    public class Friend
    {
        [Key]
        //[Comment("sdfsdfsdf")]
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string CurrentFriendId { get; set; }

        public User CurrentFriend { get; set; }
    }
}
