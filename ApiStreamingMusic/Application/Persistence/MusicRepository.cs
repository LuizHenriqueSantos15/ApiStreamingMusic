using ApiStreamingMusic.Domain.Entities;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiStreamingMusic.Application.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using Supabase.Interfaces;
using Supabase;
using Supabase.Storage;

namespace ApiStreamingMusic.Application.Persistence
{


    public class MusicRepository : IMusicRepository
    {
        private readonly IMusicRepository _musicRepository;
        private readonly HttpClient _httpClient;
        private readonly string _supabaseStorageUrl;
        private readonly string _supabaseApiKey;
        private readonly DapperContext _context;
        private readonly Supabase.Client _supabaseClient;
        public MusicRepository(DapperContext context, IConfiguration configuration, Supabase.Client client)
        {
            _context = context;
            _httpClient = new HttpClient();
            _supabaseClient = client;
            _supabaseStorageUrl = configuration["Supabase:StorageUrl"];
            _supabaseApiKey = configuration["Supabase:ApiKey"];
        }

        public async Task<IEnumerable<Music>> GetAllAsync()
        {
            var query = "SELECT * FROM Musics";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Music>(query);
            }
        }

        public async Task<string> UploadMusicAsync(Music dto, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

            var bucketName = "musics";

            using var memoryStream = new MemoryStream();

            try
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    byte[] fileBytes = stream.ToArray();

                    // 🔹 Fazer o upload para o Supabase
                    var storage = _supabaseClient.Storage;
                    var bucket = storage.From(bucketName);

                    var options = new Supabase.Storage.FileOptions
                    {
                        ContentType = file.ContentType
                    };

                    var uploadResult = await bucket.Upload(fileBytes, $"{file.FileName}.mpeg" , options);

                    if (uploadResult == null)
                        throw new Exception("Erro ao fazer upload da música.");

                    var fileUrl = $"https://yaozonzrdgcpzadhwqjo.supabase.co/storage/v1/s3/storage/v1/object/public/{bucketName}/{file.FileName}";

                    // 🔹 Salvar os metadados no banco
                    var music = new Music(dto.Title, dto.Artist, dto.Genre, dto.Year, fileUrl);
                    await AddAsync(music);
                    return fileUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao copiar o arquivo: {ex.Message}");
                throw new Exception("Erro ao processar o arquivo antes do upload.");
            }



            return "";
        }


        public async Task<Music?> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Musics WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Music>(query, new { Id = id });
            }
        }

        public async Task AddAsync(Music music)
        {
            var query = "INSERT INTO musics (Id, Title, Artist, Genre, Year) VALUES (@Id, @Title, @Artist, @Genre, @Year)";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, music);
            }
        }

        public async Task UpdateAsync(Music music)
        {
            var query = "UPDATE Musics SET Title = @Title, Artist = @Artist, Genre = @Genre, Year = @Year WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, music);
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
