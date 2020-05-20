using AutoMapper;
using JavaCourseAPI.DTOs;
using JavaCourseAPI.Helpers;
using JavaCourseAPI.Models;
using JavaCourseAPI.Repositories.AuthRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services
{
    public class AuthService : ControllerBase, IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthService(IUserRepository userRepository, IMapper mapper,
                            IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /*public async Task<ActionResult<string>> GetAuthAsync(LoginDTO loginDTO)
        {
            //tymczasowe dane logowania
            string login = "user";
            string password = "user";

            string answer = "";

           if( loginDTO.login.Equals(login) && loginDTO.password.Equals(password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:JWTSecret").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, loginDTO.login)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                answer = tokenHandler.WriteToken(token);

            }
            else
            {
                answer = null;
            }

            return answer;
        }*/

        public async Task<IActionResult> AuthenticateUser(LoginDTO loginDTO)
        {
            var user = CheckUserByLoginAndPassword(loginDTO.login, loginDTO.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = Encoding.ASCII.GetBytes("Ptaki latajOM kluczem");
            string userRole = String.Empty;

            if (user.Admin == true)
            {
                userRole = "admin";
            }
            else if (user.Teacher == true)
            {
                userRole = "teacher";
            }
            else
            {
                userRole = "student";
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Actor, user.Login.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            AuthorisationDTO authDTO = new AuthorisationDTO();
            authDTO.idUser = user.IdUser;
            authDTO.login = user.Login;
            authDTO.imie = user.Imie;
            authDTO.nazwisko = user.Nazwisko;
            authDTO.token = tokenString;

            return Ok(authDTO);
        }

        public User CheckUserByLoginAndPassword(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetByLogin(login);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IActionResult RegisterNewUser(RegisterDTO registerDTO)
        {
            var user = _mapper.Map<User>(registerDTO);

            try
            {
                Create(user, registerDTO.password);

                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        private void Create(User user, string password)
        {
            // validation
            _userRepository.CanICreateUser(user, password);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.AddNewUser(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(
                                                "Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException(
                                         "Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException(
                                        "Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
