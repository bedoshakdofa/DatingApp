using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DatingApp.Data.Models;
using DatingApp.DTOs;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LikesController:ControllerBase
    {
        private readonly IRepository _userRepository;
        private readonly ILikeRespository _likeRespository;

        public LikesController(IRepository userRepository,ILikeRespository likeRespository)
        {
            _userRepository = userRepository;
            _likeRespository = likeRespository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult>AddLike(string username)
        {
            var sourceUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var targetUser=_userRepository.GetUserByUsername(username);

            if (targetUser == null) return NotFound("cant find this username");

            var sourceUser=await _likeRespository.GetUserWithLikes(int.Parse(sourceUserId));

            if (sourceUser.UserName == username) return BadRequest("you can't like your yourself");

            var TheLike = await _likeRespository.GetTheLike(int.Parse(sourceUserId), targetUser.Id);

            if (TheLike != null) return BadRequest("you have liked this user before");

            TheLike = new UserLikes
            {
                SourceUserId=int.Parse(sourceUserId),
                TragetUserId=targetUser.Id,
            };

            sourceUser.LikedUsers.Add(TheLike);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Faild to Like user");
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserLikesDto>>>GetUserLikes(string predicate)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _likeRespository.GetUserLikes(predicate, int.Parse(userId)));
        }
    }
}
