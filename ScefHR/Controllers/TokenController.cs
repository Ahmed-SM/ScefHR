﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScefHR.Helpers;
using ScefHR.Models;

namespace ScefHR.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class TokenController : ControllerBase
    //{
    //    private const string SECRET_KEY = "TQvgjeABMPOwCycOqah5EQu5yyVjpmVG";
    //    public static readonly SymmetricSecurityKey SIGNING_KEY = new
    //                  SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

    //    [HttpGet]
    //    public IActionResult Get(string username, string password)
    //    {
            
    //            return new ObjectResult(GenerateToken(username));
           
    //    }

    //    // Generate a Token with expiration date and Claim meta-data.
    //    // And sign the token with the SIGNING_KEY
    //    private string GenerateToken(string username)
    //    {
    //        var token = new JwtSecurityToken(
    //            claims: new Claim[] { new Claim(ClaimTypes.Name, username) },
    //            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
    //            expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
    //            signingCredentials: new SigningCredentials(SIGNING_KEY,
    //                                                SecurityAlgorithms.HmacSha256)
    //            );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}