using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using SocialNetWork.DAL.Models.Users;
using SocialNetWork.DAL;
using SocialNetWork.BL.ViewModels.Account;
using SocialNetWork.DAL.Repository;
using SocialNetWork.BL.Extentions;

namespace SocialNetWork.Controllers.Account
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private IUnitOfWork _unitOfWork;
        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Shared/Login");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null )
        {
            return View(new LoginViewModel {ReturnUrl=returnUrl });
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           // if (ModelState.IsValid)
            //{

                var user = _mapper.Map<User>(model);

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
             if (result.Succeeded)
              {
            
                    return RedirectToAction("MyPage", "AccountManager");
              }
             else
              {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return RedirectToAction("Index", "Home");
            }
          //  }
           // return RedirectToAction("Index", "Home");
        }

        private async Task<List<User>> GetAllFriend(User user)
        {
            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(user);
        }

        [Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var model = new UserViewModel(result);

            model.Friends = await GetAllFriend(model.User);

            return View("MyPage", model);
        }


        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Route("GoBack")]
        [HttpGet]
        [Authorize]
        public IActionResult GoBack()
        {


            return Redirect("/MyPage");
        }


        [Route("UserEdit")]
        [HttpGet]
        public IActionResult UserEdit()
        {
            var user = User;

            var result = _userManager.GetUserAsync(user);

            var editmodel = _mapper.Map<UserEditViewModel>(result.Result);

            return View("UserEdit", editmodel);
        }

        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                user.Convert(model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    return RedirectToAction("UserEdit", "AccountManager");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("UserEdit", model);
            }
        }

        private async Task<SearchViewModel> CreateSearch(string search)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var list = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList();
            var withfriend = await GetAllFriend();

            var data = new List<UserWithFriendExt>();
            list.ForEach(x =>
            {
                var t = _mapper.Map<UserWithFriendExt>(x);
                t.IsFriendWithCurrent = withfriend.Where(y => y.Id == x.Id || x.Id == result.Id).Count() != 0;
                data.Add(t);
            });

            var model = new SearchViewModel()
            {
                UserList = data
            };

            return model;
        }

        private async Task<List<User>> GetAllFriend()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            return repository.GetFriendsByUser(result);
        }

        [Route("UserList")]
        [HttpPost]
        public async Task<IActionResult> UserList(string search)
        {
            var model = await CreateSearch(search);

            return View("UserList", model);
        }

        [Route("AddFriend")]
        [HttpPost]
        public async Task<IActionResult> AddFriend(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            repository.AddFriend(result, friend);

            return RedirectToAction("MyPage", "AccountManager");

        }

        [Route("DeleteFriend")]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);

            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

            repository.DeleteFriend(result, friend);

            return RedirectToAction("MyPage", "AccountManager");

        }


        [Route("ChatMessageList")]
        [HttpGet]
        public async Task<IActionResult> ChatMessageList(string id)
        {
            var model = await GenerateChat(id);

            return View("ChatMessageList", model);
        }

        [Route("Chat")]
        [HttpPost]
        public async Task<IActionResult> Chat(string id)
        {
            var model = await GenerateChat(id);
            return View("Chat", model);
        }

        private async Task<ChatViewModel> GenerateChat(string id)
        {
            var currentuser = User;

            var result = await _userManager.GetUserAsync(currentuser);
            var friend = await _userManager.FindByIdAsync(id);

            var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;

            var mess = repository.GetMessages(result, friend);

            var model = new ChatViewModel()
            {
                Sender = result,
                Recipient = friend,
                History = mess.OrderBy(x => x.Id).ToList(),
            };

            return model;
        }

        [Route("NewMessage")]
        [HttpPost]
        public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
        {
            if (!string.IsNullOrEmpty(chat.Message.Text))
            {
                var currentuser = User;

                var result = await _userManager.GetUserAsync(currentuser);
                var friend = await _userManager.FindByIdAsync(id);

                var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;

                var item = new Message()
                {
                    Sender = result,
                    Recipient = friend,
                    Text = chat.Message.Text,
                };
                repository.Create(item);

                ModelState.Clear();
            }

            var model = await GenerateChat(id);
            return View("Chat", model);
        }


        [Route("FriendList")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FriendList()
        {
            var user = User;

            var result = await _userManager.GetUserAsync(user);

            var model = new UserViewModel(result);

            model.Friends = await GetAllFriend(model.User);

            return View("FriendList", model);
        }


    }
}
