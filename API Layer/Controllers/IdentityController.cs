using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Parking_System.Application_Layer.Dtos.Auth;
using Smart_Parking_System.Infrastructure_Layer.Services;

namespace Smart_Parking_System.API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        public IdentityController()
        {

        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        //{
           
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        //{

        //}

        

    }
}