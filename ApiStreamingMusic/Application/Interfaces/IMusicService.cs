using ApiStreamingMusic.Application.Dtos;
using ApiStreamingMusic.Domain.Entities;

namespace ApiStreamingMusic.Application.Interfaces
{
    public interface IMusicService
    {
        Task<IEnumerable<MusicDto>> GetAllMusics();

        Task<bool> UploadMusic(Music musica, IFormFile file);
    }
}
