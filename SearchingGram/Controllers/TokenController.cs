using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SearchingGram.Data;
using SearchingGram.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//For interact with Authentification DataBase, if user use API first time, he must call CreateToken Method,
//  if user already exist, he must call GetToken Method, where he get token 
//  tokens are needed for identification client and acess user data.
namespace SearchingGram.Controllers
{
    public class TokenController : Controller
    {
        // Authentification DB context
        private DataDbContext _db;

        public TokenController(DataDbContext context)
        {
            this._db = context;
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("It works!");
        }
        //++++++++++
        //Create new User from username and password parameters, add new user to Authentification DataBase
        //++++++++++
        [HttpPost]
        [Route("[controller]/create")]
        public async Task<IActionResult> CreateToken(string username, string password)
        {

            if (_db.Users.FirstOrDefault(x => x.Login == username) != null)
            {
                //If user is exist we don`t create new 
                var res = new
                {
                    access_token = "",
                    username = username,
                    is_exist = true
                };

                return Json(res);

            }
            var identity = GetNewIdentity(username, password);
            var now = DateTime.UtcNow;

            //Generation unique user token 

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                is_exist = true
            };
            _db.Users.Add(new User() { Login = username, Password = password, Token = encodedJwt });
            await _db.SaveChangesAsync();

            return Json(response);


        }
        //+++++++++++++
        //Return user from password and username
        //+++++++++++++
        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetToken(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (user == null)
            {
                //If user don`t exist in DB
                return Json(new
                {
                    is_exist = false,
                    token = "",
                    username = "",
                    password = ""

                });
            }

            return Json(new
            {
                is_exist = true,
                token = user.Token,
                username = user.Login,
                password = user.Password
            });
        }

        //For Generation Token
        private ClaimsIdentity GetNewIdentity(string username, string password)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, password)
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
