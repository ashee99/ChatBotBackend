using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankChatbot
{
    public class JwtServices
    {
        public string Secretkey { get; set; }
        // public string Tokenduration { get; set; }
        public readonly IConfiguration config;

        public JwtServices(IConfiguration _config)
        {
            config = _config;
            this.Secretkey = config.GetSection("jwtconfig").GetSection("key").Value;
            //this.Tokenduration =Int32.Parse( config.GetSection("jwtconfig").GetSection("duration").Value);   
        }
        public string GenerateToken(string username, string password)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Secretkey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var payLoad = new[]
            {
                /*new Claim("id",id),*/
                new Claim("username",username),
                new Claim("password",password)
            };
            var token = new JwtSecurityToken(
                     issuer: "localhost",
                     audience: "localhost",
                     claims: payLoad,
                     expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: signature
                   );

            string result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;

        }
    }
}
