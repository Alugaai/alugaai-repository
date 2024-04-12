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
        private IWebHostEnvironment _environment;

        public ImageService(SystemDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        // salvar a imagem de usuário no banco de dados
        public async Task InsertImageForAUser(IFormFileCollection file, string userId)
        {
            User user = _dbContext.Users.AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            // imagem aceitando apenas essas extensões
            string fileExtension = Path.GetExtension(file[0].FileName).ToLower();

            if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
            {
                throw new ArgumentException($"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
            }
            //

            // Gera um novo nome de arquivo único
            var newFileName = "Image_" + Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);

            // Obtém o caminho completo do diretório para salvar o arquivo
            var imagePath = Path.Combine(_environment.ContentRootPath, "Images", newFileName);

            // Salva o arquivo no diretório
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await file[0].CopyToAsync(stream);
            }

            // Cria uma nova entidade de imagem com o caminho do arquivo
            var image = new Image
            {
                ImagePath = imagePath,
                InsertedOn = DateTime.Now,
                UserId = userId
            };

            // Adiciona a entidade ao contexto do banco de dados
            _dbContext.Images.Add(image);


            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();

            user.ImageId = image.Id;
            _dbContext.Users.Update(user);
        }




        // salvar a imagem de um building no banco de dados
        public async Task InsertImageForProperty(IFormFileCollection files, string userId, int propertyId)
        {
            Owner user = await _dbContext.Owners.Include(o => o.Properties).OfType<Owner>().FirstOrDefaultAsync(s => s.Id == userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist or is not an owner");

            // Check if the provided propertyId exists and belongs to the user
            Property property = user.Properties.FirstOrDefault(p => p.Id == propertyId);
            if (property == null)
            {
                throw new ArgumentException($"Property with id {propertyId} does not exist or does not belong to the user");
            }

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {

                    // imagem aceitando apenas essas extensões
                    string fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                    {
                        throw new ArgumentException($"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                    }
                    //

                    // Gera um novo nome de arquivo único
                    var newFileName = "Image_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Obtém o caminho completo do diretório para salvar o arquivo
                    var imagePath = Path.Combine(_environment.ContentRootPath, "Images", newFileName);

                    // Salva o arquivo no diretório
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Cria uma nova entidade de imagem com o caminho do arquivo
                    var image = new Image
                    {
                        ImagePath = imagePath,
                        InsertedOn = DateTime.Now,
                        BuildingId = propertyId
                    };

                    // Adiciona a entidade ao contexto do banco de dados
                    _dbContext.Images.Add(image);
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
                        throw new ArgumentException($"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
                    }
                    //

                    // Gera um novo nome de arquivo único
                    var newFileName = "Image_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Obtém o caminho completo do diretório para salvar o arquivo
                    var imagePath = Path.Combine(_environment.ContentRootPath, "Images", newFileName);

                    // Salva o arquivo no diretório
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Cria uma nova entidade de imagem com o caminho do arquivo
                    var image = new Image
                    {
                        ImagePath = imagePath,
                        InsertedOn = DateTime.Now,
                        BuildingId = collegeId
                    };

                    // Adiciona a entidade ao contexto do banco de dados
                    _dbContext.Images.Add(image);
                }
            }

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();
        }
    }
}






    
