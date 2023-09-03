﻿using HelthOrdinations.Core.DB;
using HelthOrdinations.Core.Helpers.Auth;
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
}