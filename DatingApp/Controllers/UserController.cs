using AutoMapper;
using DatingApp.Data;
using DatingApp.Data.Models;
using DatingApp.DTOs;
using DatingApp.Extentions;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogActivity))]
    [ApiController]
    [Route("/api/[Controller]")]
    public class UserController:ControllerBase
    {
        private readonly IRepository _UserRepository;

        private readonly IMapper _Mapper;

        private readonly IPhotoService _PhotoService;
        public UserController(IRepository userRepository,IMapper mapper , IPhotoService photoService) 
        { 
            _UserRepository = userRepository;
            _Mapper = mapper;
            _PhotoService = photoService;
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult<MemberDto>> getUserByUsername()
        {
            var username=User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _UserRepository.GetUserByUsername(username);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetAllUser([FromQuery] UserParams userParams)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var Currantuser = await _UserRepository.GetUserByUsername(username);

            userParams.Username = username;

            if (userParams.Gender == null) {
                userParams.Gender = Currantuser.Gender == "male" ? "female" : "male";
            }

            var users = await _UserRepository.GetAllUser(userParams);

            Response.AddPaginationHeader(users.CurrantPage,users.PageSize,users.TotalCount,users.TotalPages);

            return Ok(users);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<MemberUpdateDto>> UpdateUser(MemberUpdateDto memberUpdate)
        {
            var username=User.FindFirst(ClaimTypes.Name)?.Value;
            var user=await _UserRepository.GetUserByUsername(username);
            
            if (user == null)
            {
                return BadRequest("user is not found");
            }

            _Mapper.Map(memberUpdate, user);
            if (await _UserRepository.SaveAllAsync()) return Ok("document is saved");

            return BadRequest("fialed to update document");
        }

        [Authorize]
        [HttpPost("add-photo")]

        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _UserRepository.GetUserByUsername(username);

            if (user == null) return NotFound();

            var result = await _PhotoService.AddPhotoAsync(file);

            if (result.Error!=null)return BadRequest(result.Error.Message);

            var userphoto = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
            };

            if (user.Photos.Count==0) userphoto.isMain = true;

            user.Photos.Add(userphoto);

            if (await _UserRepository.SaveAllAsync()) return _Mapper.Map<PhotoDto>(userphoto);

            return BadRequest("error in uploading the image");

        }
    }
}
