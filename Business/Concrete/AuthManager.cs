using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Entities.Dtos.AuthDtos;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IAuthService
    {
        public async Task<IResult> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return new ErrorResult(HttpStatusCode.NotFound);

            SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return new SuccessResult(HttpStatusCode.OK);
            else
                return new ErrorResult(HttpStatusCode.NotFound);
        }

        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            var exists = await userManager.FindByEmailAsync(model.Email);
            if (exists is not null)
                return new ErrorResult(HttpStatusCode.Conflict, "İstifadəçi artıq mövcuddur");

            User newUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
                return new SuccessResult(HttpStatusCode.Created);
            else
            {
                string response = string.Empty;
                foreach (IdentityError error in result.Errors)
                {
                    response += error.Description;
                }
                return new ErrorResult(HttpStatusCode.BadRequest, response);
            }
        }

        public async Task<IResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return new SuccessResult(HttpStatusCode.OK);
        }
    }
}