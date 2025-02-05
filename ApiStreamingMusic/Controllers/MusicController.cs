using ApiStreamingMusic.Application.Dtos;
using ApiStreamingMusic.Application.Interfaces;
using ApiStreamingMusic.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiStreamingMusic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService service)
        {
            _musicService = service;
        }

        [Authorize]
        [HttpPost]
        [Route("/upload")]
        public IActionResult UploadMusic([FromForm] Music musica, IFormFile file)
        {
            try
            {
                _musicService.UploadMusic(musica, file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/get")]
        public IActionResult GetMusics()
        {
            return Ok();
        }

    }
}
