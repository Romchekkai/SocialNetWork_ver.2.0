using SocialNetWork.DAL.Models.Users;
using System;
using System.Collections.Generic;

namespace SocialNetWork.BL.ViewModels.Account
{
    public class SearchViewModel
    {
        public List<UserWithFriendExt> UserList { get; set; }
    }
}
