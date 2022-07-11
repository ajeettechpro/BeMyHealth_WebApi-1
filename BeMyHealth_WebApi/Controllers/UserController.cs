using AutoMapper;
using BeMyHealth_WebApi.Constants;
using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Helpers;
using BeMyHealth_WebApi.Models;
using BeMyHealth_WebApi.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyHealth_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register([FromForm] UserDto request)
        {
            try
            {
                User users = _mapper.Map<User>(request);
                var data = await _userService.CreateRegister(users);
                if(data == true)
                {
                    return Ok(ApiResponse.CreateResponse(true, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode404));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(string.Empty, StatusConstants.StatusCode500));
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(string emailIdOrmobileNumber, string password)
        {
            try
            {
                var data = await _userService.Login(emailIdOrmobileNumber, password);
                return Ok(ApiResponse.CreateResponse(data, StatusConstants.StatusCode200));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse("User Not Found", StatusConstants.StatusCode400));
            }
        }
    }
}
