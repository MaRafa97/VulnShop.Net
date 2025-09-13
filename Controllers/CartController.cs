using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace VulnShop.Net.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly MySqlConnection _connection;
        public CartController(MySqlConnection connection) => _connection = connection;

        [HttpPost("add")]
        public IActionResult Add(int productId, int quantity, decimal price)
        {
            _connection.Open();

            //Price manipulation from client
            var cmd = new MySqlCommand(
                $"INSERT INTO cart(product_id, quantity, price_at_time) " +
                $"VALUES({productId},{quantity},{price})", _connection);

            cmd.ExecuteNonQuery();

            return Redirect("/cart");
        }
    }
}
