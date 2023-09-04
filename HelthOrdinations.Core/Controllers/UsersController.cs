using Azure.Core;
using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Helpers.Auth;
using HelthOrdinations.Core.Helpers.EmailSender;
using HelthOrdinations.Core.Models;
using HelthOrdinations.Core.Models.Enums;
using HelthOrdinations.Core.Responses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Locations.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly HODbContext _dbContext;
    private JWTTokenHelper _jwtTokenHelper;

    public UsersController(HODbContext dbContext, JWTTokenHelper jwtTokenHelper)
    {
        _dbContext = dbContext;
        _jwtTokenHelper = jwtTokenHelper;
    }

    [HttpGet("LoginUser")]
    public ActionResult<LoginResponse> LoginUser(string email, string password)
    {
        var response = new LoginResponse();
        var loginInfo = _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        var hasher = new PasswordHasher();

        if (loginInfo != null && hasher.VerifyHashedPassword(loginInfo.Password, password) != PasswordVerificationResult.Failed)
        {
            response.Message = "Valid user.";
            response.IsSuccess = true;
            response.UserToken = _jwtTokenHelper.GenerateToken(loginInfo);
            return response;
        }
        else
        {
            response.Message = "Invalid user.";
            response.IsSuccess = false;
            response.UserToken = "";
            return response;
        }
    }

    [HttpPost("ForgotPassword")]
    public ActionResult<bool> ForgotPassword(ForgotPassword request)
    {
        try
        {
            string body = "Click the link below to reset your password:<br /><a href='deeplink://resetpassword?email="+request.Email+"'>Reset Password</a>";

            SendEmailHelper.SendEmail(request.Email, body, "Reset password");
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    [HttpPost("SaveUser")]
    public ActionResult<bool> SaveUser(UserInfo user)
    {
        try
        {
            var passwordHasher = new PasswordHasher();
            user.Password = passwordHasher.HashPassword(user.Password);

            var newUser = new UserInfo
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                UserStatusId = (int)UsersStatusEnum.Inactive
            };

            _dbContext.Users.Add(newUser);

            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    [HttpPost("ResetPassword")]
    public ActionResult<bool> ResetPassword(ResetPassword request)
    {
        try
        {
            var passwordHasher = new PasswordHasher();
            request.NewPassword= passwordHasher.HashPassword(request.NewPassword);

            var user = _dbContext.Users.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
            user.Password = request.NewPassword;

            _dbContext.Users.Update(user);

            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
