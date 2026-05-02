using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly string ConnectionString;
        public OptionsController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("Options");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var OPTIONS = new List<Options>();
            while (sqlDataReader.Read())
            {
                var option = new Options();
                option.Id = int.Parse(sqlDataReader[0].ToString());
                option.QuestionId = int.Parse(sqlDataReader[1].ToString());
                option.IsCorrect = bool.Parse(sqlDataReader[2].ToString());
                option.OptionName = sqlDataReader[3].ToString();
                OPTIONS.Add(option);
            }
            return Ok(OPTIONS);
        }

        [HttpPost]
        public IActionResult CreateData(Options option)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "QuestionId, IsCorrect, OptionName";
            string vls = "@QuestionId, @IsCorrect, @OptionName";
            string query = Crud.Insert("Options", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", option.Id);
            sqlCommand.Parameters.AddWithValue("QuestionId", option.QuestionId);
            sqlCommand.Parameters.AddWithValue("IsCorrect", option.IsCorrect);
            sqlCommand.Parameters.AddWithValue("OptionName", option.OptionName);
            sqlCommand.ExecuteNonQuery();
            return Ok(option);
        }

        [HttpPut]
        public IActionResult UpdateData(int id, Options option)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "QuestionId = @QuestionId, IsCorrect = @IsCorrect, OptionName = @OptionName";
            string query = Crud.Update("Options", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@QuestionId", option.QuestionId);
            sqlCommand.Parameters.AddWithValue("@IsCorrect", option.IsCorrect);
            sqlCommand.Parameters.AddWithValue("@OptionName", option.OptionName);
            sqlCommand.ExecuteNonQuery();
            return Ok(option);
        }

        [HttpDelete]
        public IActionResult Delete(int id, Options option)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("Options");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(option);
        }
    }
}
