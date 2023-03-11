using BankChatbot.Model;
using BankChatbot.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankChatbot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    { 
        private readonly IBankRepository complainRepository;
        private readonly IConfiguration config;
        public BankController(IBankRepository _complainRepository, IConfiguration config)
        {
            complainRepository = _complainRepository;
            this.config = config;
        }
        [HttpPost("register-complain")]
        public async Task<IActionResult> AddComplainAsync(Complain comp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var compl = await complainRepository.AddComplainAsync(comp);
                return Created("Succesfully created",compl );
            }
        }
        
        [HttpPost("report-fraud")]
        //[Authorize]
        public async Task<IActionResult> AddFraudAsync(Fraud fraud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var fraud1 = await complainRepository.AddFraudAsync(fraud);
                return Created("Succesfully created", fraud1);
            }
        }
       // [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await complainRepository.GetBank(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Loginuser([FromBody] AuthenticationRequest bank)
        {
            string un = bank.username;
            string pw = bank.password;
            var users = await complainRepository.Login(un, pw);

            if (users == null)
            {
                return Unauthorized();
            }
            else
            {
                string username = users.UserName;
                string password = users.Password;
                int id = users.Id;
                var res = new JwtServices(config).GenerateToken(username, password);
                var response = new AuthenticationResponse
                {
                    token = res,
                    userId = id
                };
                return Ok(response);



            }

        }

    }
}
