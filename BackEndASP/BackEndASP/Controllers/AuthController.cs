﻿using BackEndASP.DTOs.AuthDTOs;
using BackEndASP.Entities;
using BackEndASP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


[Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;


        private readonly ITokenRepository _tokenRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SystemDbContext _dbContext;

    public AuthController(ITokenRepository tokenRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository, SystemDbContext dbContext)
    {
        _tokenRepository = tokenRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _dbContext = dbContext;
    }



    [HttpPost("createRole/{roleName}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            // Verifica se a role já existe
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Cria a nova role
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    // Retorna uma resposta de sucesso
                    return StatusCode(StatusCodes.Status200OK,
                        new ResponseDTO { Status = "Success", Message = $"Role {roleName} added successfully!" }
                    );
                }

                // Retorna uma resposta de erro se houver problemas ao adicionar a nova role
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseDTO { Status = "Error", Message = $"Issue adding the new {roleName} role" }
                );
            }

            // Retorna uma resposta de erro se a role já existir
            return StatusCode(StatusCodes.Status400BadRequest,
                new ResponseDTO { Status = "Error", Message = $"Role already exists" }
            );
        }

       
        [HttpPost("AddUserToRole/{email}/{roleName}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> AddUserRole(string email, string roleName)
        {
            // Procura o usuário pelo email
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Adiciona o usuário à role especificada
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                    // Retorna uma resposta de sucesso
                    return StatusCode(StatusCodes.Status200OK,
                        new ResponseDTO { Status = "Success", Message = $"User {user.Email} added to the {roleName} role!" }
                    );
                }

                _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
                // Retorna uma resposta de erro se houver problemas ao adicionar o usuário à role
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseDTO { Status = "Error", Message = $"Error: Unable to add user {user.Email} to the {roleName} role" }
                );
            }

            // Retorna uma resposta de erro se o usuário não for encontrado
            return StatusCode(StatusCodes.Status400BadRequest,
                new ResponseDTO { Status = "Error", Message = $"Unable to find User" }
            );
        }



        // Método para lidar com a solicitação de login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Procura o usuário pelo nome de usuário
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            // Verifica se o usuário existe e a senha está correta
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password!))
            {
                // Obtém as funções (roles) do usuário
                var userRoles = await _userManager.GetRolesAsync(user);

                // Cria uma lista de reivindicações (claims) de autenticação, que serao exibidos no PAYLOAD do token
                var authClaims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // Adiciona as reivindicações de funções (roles) à lista
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Gera um token de acesso
                var token = _tokenRepository.GenerateAccessToken(authClaims, _configuration);

                // Gera um token de atualização
                var refreshToken = _tokenRepository.GenerateRefreshToken();

                // Obtém o tempo de validade do token de atualização a partir da configuração
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                // Atualiza as informações do token de atualização no usuário
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
                await _userManager.UpdateAsync(user);

                // Retorna os tokens gerados
                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    Email = user.Email!,
                    Role = userRoles,
                    Id = user.Id
                });
            }

            // Retorna uma resposta não autorizada se o login falhar
            return Unauthorized("Email ou senha inválidos");
        }


        // adicionar um refresh token a um usuário

        [HttpPut("add-refresh-token")]
        [Authorize(Policy = "StudentOrOwner")]
        public async Task<ActionResult> RefreshToken(RevokeDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
                {
                    Status = "Error",
                    Message = "Email does not exists!"
                });
            }

            user.RefreshToken = Guid.NewGuid().ToString();

            return new ObjectResult(new
            {
                refreshToken = user.RefreshToken
            });
        }


        // Método para lidar com a solicitação de registro de student
        [HttpPost("register/student")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RegisterStudent([FromBody] RegisterUserDTO registerDTO)
        {

           
            // Verifica se o usuário já existe
            var userExists = await _userManager.FindByEmailAsync(registerDTO.Email);

            // Retorna erro se o usuário já existir
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ResponseDTO
                {
                    Status = "Error",
                    Message = "Email already exists!"
                });
            }

        // Criar um UserName aleatório com base no nome
        string randomUserName = GenerateRandomUserName(registerDTO.Name);


        // Cria uma nova instância de ApplicationUser com os dados fornecidos
        Student user = new()
            {
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = randomUserName,
                Name = registerDTO.Name,
                PhoneNumber = registerDTO.PhoneNumber,
                PhoneNumberConfirmed = false,
                BirthDate = registerDTO.BirthDate
            };

            // Tenta criar o usuário
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            // Retorna uma resposta apropriada com base no resultado da criação do usuário
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
                {
                    Status = "Error",
                    Message = "User creation failed"
                });
            }

            await _userManager.AddToRoleAsync(user, "Student");



            return StatusCode(StatusCodes.Status201Created, new ResponseDTO
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }



        // Método para lidar com a solicitação de registro de um Owner
        [HttpPost("register/owner")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RegisterOwner([FromBody] RegisterUserDTO registerDTO)
        {


        // Verifica se o usuário já existe
        var userExists = await _userManager.FindByNameAsync(registerDTO.Name);

        // Retorna erro se o usuário já existir
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseDTO
            {
                Status = "Error",
                Message = "Email already exists!"
            });
        }

        // Criar um UserName aleatório com base no nome
        string randomUserName = GenerateRandomUserName(registerDTO.Name);


        // Cria uma nova instância de Owner e salva no DB com os dados fornecidos
        Owner user = new()
        {
            Email = registerDTO.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            Name = registerDTO.Name,
            UserName = randomUserName,
            PhoneNumber = registerDTO.PhoneNumber,
            PhoneNumberConfirmed = false,
            BirthDate = registerDTO.BirthDate
        };


        // Tenta criar o usuário
        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        // Retorna uma resposta apropriada com base no resultado da criação do usuário
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
            {
                Status = "Error",
                Message = "User creation failed"
            });
        }

        await _userManager.AddToRoleAsync(user, "Owner");

     
        return StatusCode(StatusCodes.Status201Created, new ResponseDTO
        {
            Status = "Success",
            Message = "User created successfully!"
        });
        }


    // Método para lidar com a solicitação de registro de um Owner
    [HttpPut("updatePassword")]
    [Authorize(Policy = "StudentOrOwner")]
    public async Task<ActionResult> UpdatePasswordOwner([FromBody] ChangePasswordDTO dto)
    {
        // Pegar userId
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // pega o usuário logado
        var userLogged = await _userManager.FindByIdAsync(userId);

        // Crie um PasswordHasher
        var passwordHasher = new PasswordHasher<IdentityUser>();

        // Verifique se a senha fornecida no DTO é igual à senha armazenada no banco de dados
        var result = passwordHasher.VerifyHashedPassword(userLogged, userLogged.PasswordHash, dto.OldPassword);

        if (result == PasswordVerificationResult.Success)
        {
            // Verifica se a nova senha é igual à senha confirmada
            if (dto.NewPassword == dto.ConfirmPassword)
            {
                // Gera o hash da nova senha
                var hashedPassword = passwordHasher.HashPassword(userLogged, dto.ConfirmPassword);

                // Atribuir o hash da senha confirmada ao usuário
                userLogged.PasswordHash = hashedPassword;

                // Atualizar o usuário no banco de dados
                await _userManager.UpdateAsync(userLogged);

                // Retornar uma resposta de sucesso
                return StatusCode(StatusCodes.Status201Created, new ResponseDTO
                {
                    Status = "Success",
                    Message = "Password changed successfully!"
                });
            }
            else
            {
                // As senhas não coincidem, retornar BadRequest
                return BadRequest("The passwords do not match");
            }
        }
        else
        {
            // Senha antiga incorreta, retornar BadRequest
            return BadRequest("Incorrect old password");
        }
    }



    // Método para lidar com a renovação de token
    [HttpPost("refresh-token")]
        [Authorize(Policy = "StudentOrOwner")]
        public async Task<ActionResult> RefreshToken(TokenDTO tokenDTO)
        {
            // Verifica se os tokens são válidos e se o usuário associado ao token ainda existe
            if (tokenDTO == null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenDTO.AccessToken ?? throw new ArgumentNullException(nameof(tokenDTO));
            string? refreshToken = tokenDTO.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDTO));

            // Obtém o principal associado ao token de acesso expirado
            var principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            // Retorna um erro se o token de acesso/refresh for inválido
            if (principal == null)
            {
                return BadRequest("Invalid AccessToken / RefreshToken");
            }

            // Obtém o nome de usuário do principal
            string username = principal.Identity.Name;

            // Procura o usuário pelo nome de usuário
            var user = await _userManager.FindByNameAsync(username!);

            // Retorna um erro se o usuário não existir, ou se o token de atualização for inválido ou expirado
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid AccessToken / RefreshToken");
            }

            // Gera um novo token de acesso
            var newAccessToken = _tokenRepository.GenerateAccessToken(principal.Claims.ToList(), _configuration);

            // Gera um novo token de atualização
            var newRefreshToken = _tokenRepository.GenerateRefreshToken();

            // Atualiza as informações do token de atualização no usuário
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            // Retorna os novos tokens gerados
            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }


        // Método para revogar um token (requer autenticação)
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("revoke")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Revoke(RevokeDTO emailDTO)
        {
            // Procura o usuário pelo Nome
            var user = await _userManager.FindByEmailAsync(emailDTO.Email);

            // Retorna um erro se o usuário não existir
            if (user == null)
            {
                _logger.LogInformation(3, $"Usuário não encontrado para o e-mail: {emailDTO}");
                return BadRequest("Invalid email");

            }

            // Remove o token de atualização do usuário
            user.RefreshToken = null;

            // Atualiza as informações do usuário no banco de dados
            await _userManager.UpdateAsync(user);

            _logger.LogInformation(4,$"Token revogado para o usuário com e-mail: {emailDTO}");


            // Retorna uma resposta indicando que a revogação foi bem-sucedida
            return NoContent();
        }


    private static string GenerateRandomUserName(string name)
    {
        // Remover espaços em branco e converter para minúsculas
        string formattedName = name.Replace(" ", "").ToLower();

        // Gerar uma parte aleatória com 4 dígitos
        Random random = new Random();
        int randomNumber = random.Next(0, 9999999); // Números de 0 a 9999

        // Combinação do nome formatado com o número aleatório
        string randomUserName = formattedName + randomNumber;

        return randomUserName;
    }


}

