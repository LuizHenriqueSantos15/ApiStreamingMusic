using ApiStreamingMusic.Application.Dtos;
using ApiStreamingMusic.Application.Interfaces;
using ApiStreamingMusic.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiStreamingMusic.Application.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;

        public MusicService(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        public async Task<IEnumerable<MusicDto>> GetAllAsync()
        {
            var musics = await _musicRepository.GetAllAsync();
            return musics.Select(m => new MusicDto(m.Id, m.Title, m.Artist, m.Genre, m.Year));
        }

        public Task<IEnumerable<MusicDto>> GetAllMusics()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadMusic(Music musica, IFormFile file)
        {
            if (!string.IsNullOrEmpty(musica.Title))
            {
                var result = await _musicRepository.UploadMusicAsync(musica, file);
                return result.Length > 0;
            }
            return true;
        }
    }
}
