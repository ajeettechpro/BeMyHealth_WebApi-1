using BeMyHealth_WebApi.Constants;
using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BeMyHealth_WebApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly BeMyHealthDbContext _beMyHealthDbContext;
        private readonly IConfiguration _configuration;
        public UserService(BeMyHealthDbContext beMyHealthDbContext, IConfiguration configuration)
        {
            _beMyHealthDbContext = beMyHealthDbContext;
            _configuration = configuration;
        }
        public async Task<bool> CreateRegister(User user)
        {
            var isExist = _beMyHealthDbContext.Users.Where(x => x.EmailId.ToLower() == user.EmailId.ToLower() && x.MobileNumber == user.MobileNumber).FirstOrDefault();
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(user.EmailId);
            if (match.Success)
            {
                if (isExist == null)
                {
                    using var md5Hash = MD5.Create();
                    user.Password = GetMd5PasswordEncryption(md5Hash, user.Password);
                    _beMyHealthDbContext.Users.Add(user);
                    await _beMyHealthDbContext.SaveChangesAsync();
                    return true;    
                }
                else
                {
                    return false;   
                }
            }
            return false;
        }
        private bool VerifyMd5Password(MD5 md5Hash, string input, string hash)
        {
            var hashOfInput = GetMd5PasswordEncryption(md5Hash, input);

            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash) == 0;
        }
        private string GetMd5PasswordEncryption(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public async Task<string> Login(string emailIdOrmobileNumber, string password)
        {
            using var md5Hash = MD5.Create();
            var encryptPassword = password;
            password = GetMd5PasswordEncryption(md5Hash, password);
            var user = _beMyHealthDbContext.Users.FirstOrDefault(x => x.EmailId == emailIdOrmobileNumber || x.MobileNumber == emailIdOrmobileNumber);

            if (user != null)
            {
                var verify = VerifyMd5Password(md5Hash, encryptPassword, user.Password);
                if (verify)
                {    
                        var token = CreateToken(user);
                        return token;               
                }
                return null;
            }
            return null;
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.EmailId)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
