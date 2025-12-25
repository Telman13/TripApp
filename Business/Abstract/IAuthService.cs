using Core.Utilities.Results.Abstract;
using Entities.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> LoginAsync(LoginDto model);
        Task<IResult> RegisterAsync(RegisterDto model);
        Task<IResult> LogoutAsync();
    }
}