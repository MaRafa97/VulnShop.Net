using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace VulnShop.Net.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly MySqlConnection _connection;
        public AuthController(MySqlConnection connection) => _connection = connection;

        [HttpGet("login")]
        public IActionResult Login() => View();

        [HttpPost("login")]
        public IActionResult LoginPost(string userName , string password)
        {
            _connection.Open();

            //Direct concatenation vulneratbility (SQL Injection)
            var cmd = new MySqlCommand(
                $"SELECT * FROM user WHERE userName = '{userName}' AND password= '{password}", _connection);

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                HttpContext.Session.SetString("user", userName);
                return Redirect("/products");
             }

            return Unauthorized("Credenciales invalidas");
        }
    }
}
