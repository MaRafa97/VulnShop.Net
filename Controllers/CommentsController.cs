using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace VulnShop.Net.Controllers
{
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly MySqlConnection _connection;
        public CommentsController(MySqlConnection connection) => _connection = connection;

        [HttpGet("{productId}")]
        public IActionResult Index(int productId)
        {
            _connection.Open();
            var cmd = new MySqlCommand(
                $"SELECT * FROM comments WHERE product_id = {productId}", _connection);

            var reader = cmd.ExecuteReader();
            var html = "<h1>Comments</h1><ul>";

            while (reader.Read())
            {
                //No data scape Vulnerability (XSS)
                html = html + $"<li><b>{reader["author"]}</b>: {reader["body"]}</li>";
            }
            html = html + "</ul>";

            return Content(html, "text/html");
        }

        [HttpPost("{productId}")]
        public IActionResult Add(int productId, string author, string body)
        {
            _connection.Open();

            //Sql inyection and XSS vulnerability
            var cmd = new MySqlCommand(
                $"INSERT INTO comments(product_id, author, body) " +
                $"VALUES({productId},{author},{body})",_connection);

            cmd.ExecuteNonQuery();

            return Redirect($"/comments/{productId}");
         }
    }
}
