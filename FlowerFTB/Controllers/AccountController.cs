using FlowerFTB.Data;
using FlowerFTB.Models.IdentityModels;
using FlowerFTB.Services;
using FlowerFTB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace FlowerFTB.Controllers
{
    public class AccountController : Controller

    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMailService mailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _mailManager = mailService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var existUser = await _userManager.FindByNameAsync(model.Username);

            if (existUser != null)
            {
                ModelState.AddModelError("", "Username təkrarlana bilməz..!");
                return View();
            }

            var user = new User
            {
                FullName = model.Fullname,
                UserName = model.Username,
                Email = model.Email
            };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var existUser = await _userManager.FindByNameAsync(model.Username);

                if (existUser == null)
                {
                    ModelState.AddModelError("", "Username isnot correct");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(existUser, model.Password, false, true);

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email tesdiqlenmelidir");
                    return View();
                }

              

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid credentials");
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Login));
            };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null) return BadRequest();
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                return View();

            }
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email  daxil edilməlidir..!");
                return View();
            }

            var existUser = await _userManager.FindByEmailAsync(model.Email);

            if(existUser is null)
            {
                ModelState.AddModelError("", "Belə email mövcud deyil..!");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            var resetLink = Url.Action(nameof(ResetPassword), "Account", new {email = model.Email,token},Request.Scheme,Request.Host.ToString());
            var mailRequest = new RequestEmail
            {
                ToEmail = model.Email,
                Body = resetLink,
                Subject = "Reset Link"
            };

          await  _mailManager.SendEmailAsync(mailRequest);
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel
            {
                Email = email,  
                Token = token
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Düzgün doldurulmalıdır..!");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return BadRequest();
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
             {
                return RedirectToAction(nameof(Login));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

    }
}
