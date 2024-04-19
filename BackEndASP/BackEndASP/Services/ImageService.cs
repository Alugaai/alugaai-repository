using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BackEndASP.Services
{
    public class ImageService : IImageRepository
    {

        private readonly SystemDbContext _dbContext;

        public ImageService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertImageForAUser(IFormFileCollection files, string userId)
        {
            User user = _dbContext.Users.AsNoTracking().Include(u => u.Image).FirstOrDefault(s => s.Id == userId)
                        ?? throw new ArgumentException($"This id {userId} does not exist");

            if (user.Image != null)
            {
                throw new ArgumentException($"This id {userId} already have a image");
            }

            if (files != null && files.Count > 0)
            {
                // imagem aceitando apenas essas extensões
                string fileExtension = Path.GetExtension(files[0].FileName).ToLower();

                if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                {
                    throw new ArgumentException(
                        $"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                }

                using (var ms = new MemoryStream())
                {
                    // Copia o conteúdo da imagem para a MemoryStream
                    await files[0].CopyToAsync(ms);

                    // Converte a imagem para base64
                    string base64Image = Convert.ToBase64String(ms.ToArray());

                    // Cria uma nova entidade de imagem com o conteúdo base64
                    var image = new Image
                    {
                        ImageData64 = base64Image,
                        InsertedOn = DateTime.Now,
                        UserId = userId
                    };

                    // Adiciona a entidade ao contexto do banco de dados
                    _dbContext.Images.Add(image);

                    // Salva as alterações no banco de dados
                    await _dbContext.SaveChangesAsync();

                    // Atualiza o ID da imagem no usuário
                    user.ImageId = image.Id;
                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                throw new ArgumentException("No image provided");
            }
        }





        // salvar a imagem de um building no banco de dados
        public async Task InsertImageForProperty(IFormFileCollection files, string userId, int propertyId)
        {
            Owner user = await _dbContext.Owners.Include(o => o.Properties).OfType<Owner>()
                             .FirstOrDefaultAsync(s => s.Id == userId)
                         ?? throw new ArgumentException($"User with id {userId} does not exist or is not an owner");

            // Check if the provided propertyId exists and belongs to the user
            Property property = user.Properties.FirstOrDefault(p => p.Id == propertyId);
            if (property == null)
            {
                throw new ArgumentException(
                    $"Property with id {propertyId} does not exist or does not belong to the user");
            }

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    // imagem aceitando apenas essas extensões
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                    {
                        throw new ArgumentException(
                            $"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                    }

                    using (var ms = new MemoryStream())
                    {
                        // Copia o conteúdo da imagem para a MemoryStream
                        await file.CopyToAsync(ms);

                        // Converte a imagem para base64
                        string base64Image = Convert.ToBase64String(ms.ToArray());

                        // Cria uma nova entidade de imagem com o conteúdo base64
                        var image = new Image
                        {
                            ImageData64 = base64Image,
                            InsertedOn = DateTime.Now,
                            BuildingId = propertyId
                        };

                        // Adiciona a entidade ao contexto do banco de dados
                        _dbContext.Images.Add(image);
                    }
                }
            }

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();
        }




        public async Task InsertImageForCollege(IFormFileCollection files, int collegeId)
        {
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    // imagem aceitando apenas essas extensões
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                    {
                        throw new ArgumentException(
                            $"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                    }

                    using (var ms = new MemoryStream())
                    {
                        // Copia o conteúdo da imagem para a MemoryStream
                        await file.CopyToAsync(ms);

                        // Converte a imagem para base64
                        string base64Image = Convert.ToBase64String(ms.ToArray());

                        // Cria uma nova entidade de imagem com o conteúdo base64
                        var image = new Image
                        {
                            ImageData64 = base64Image,
                            InsertedOn = DateTime.Now,
                            BuildingId = collegeId
                        };

                        // Adiciona a entidade ao contexto do banco de dados
                        _dbContext.Images.Add(image);
                    }
                }
            }

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();
        }

        public async Task PutImageForAUser(IFormFileCollection files, string userId)
        {
            User user = _dbContext.Users.AsNoTracking().Include(u => u.Image).FirstOrDefault(s => s.Id == userId)
                        ?? throw new ArgumentException($"This id {userId} does not exist");

            if (files != null && files.Count > 0)
            {
                // imagem aceitando apenas essas extensões
                string fileExtension = Path.GetExtension(files[0].FileName).ToLower();

                if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                {
                    throw new ArgumentException(
                        $"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                }

                using (var ms = new MemoryStream())
                {
                    // Copia o conteúdo da imagem para a MemoryStream
                    await files[0].CopyToAsync(ms);

                    // Converte a imagem para base64
                    string base64Image = Convert.ToBase64String(ms.ToArray());

                    // Pega a imagem do usuário logado do banco de dados e atualizando as informações dela com base na nova imagem
                    if (user.Image != null)
                    {
                        user.Image.ImageData64 = base64Image;
                        user.Image.InsertedOn = DateTime.Now;
                    }

                    // Salva as alterações no banco de dados
                    await _dbContext.SaveChangesAsync();

                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                throw new ArgumentException("No image provided");
            }
        }

        public async Task<bool> DeleteImageForAUser(string userId)
        {
            User user = _dbContext.Users.AsNoTracking().Include(u => u.Image).FirstOrDefault(s => s.Id == userId)
                       ?? throw new ArgumentException($"This id {userId} does not exist");

            if (user.Image != null)
            {
                _dbContext.Remove(user.Image);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}








