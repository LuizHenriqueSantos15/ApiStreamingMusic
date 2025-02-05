using ApiStreamingMusic.Application.Dtos;
using ApiStreamingMusic.Domain.Entities;
using System.Threading.Tasks;

namespace ApiStreamingMusic.Application.Interfaces
{
    public interface IMusicRepository
    {
        Task<IEnumerable<Music>> GetAllAsync();
        Task<Music?> GetByIdAsync(Guid id);
        Task AddAsync(Music music);
        Task UpdateAsync(Music music);
        Task DeleteAsync(Guid id);

        Task<string> UploadMusicAsync(Music music, IFormFile file);
    }
}
