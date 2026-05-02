using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly string ConnectionString;
        public TestController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("Tests");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var test = new List<Tests>();
            while (sqlDataReader.Read())
            {
                var tst = new Tests();
                tst.Id = int.Parse(sqlDataReader[0].ToString());
                tst.Name = sqlDataReader[1].ToString();
                tst.Venue = sqlDataReader[2].ToString();
                tst.QuestionMarks = sqlDataReader[4].ToString();
                tst.TotalMarks = int.Parse(sqlDataReader[5].ToString());
                tst.Duration = int.Parse(sqlDataReader[6].ToString());
                test.Add(tst);
            }
            return Ok(test);

        }
        // return Ok(TEST);


        [HttpPost]
        public IActionResult CreateData(Tests test)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "Name , Venue  , QuestionMarks , TotalMarks, Duration";
            string vls = "@Name , @Venue , @QuestionMarks , @TotalMarks, @Duration";
            string query = Crud.Insert("Tests", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", test.Id);
            sqlCommand.Parameters.AddWithValue("Name", test.Name);
            sqlCommand.Parameters.AddWithValue("Venue", test.Venue);
            sqlCommand.Parameters.AddWithValue("QuestionMarks", test.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("TotalMarks", test.TotalMarks);
            sqlCommand.Parameters.AddWithValue("Duration", test.Duration);
            sqlCommand.ExecuteNonQuery();
            return Ok(test);
        }
        [HttpPut]
        public IActionResult UpdateData(int id, Tests test)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "Name = @Name, Venue = @Venue, QuestionMarks = @QuestionMarks ,TotalMarks = @TotalMarks, Duration = @Duration";
            string query = Crud.Update("Tests", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", test.Name);
            sqlCommand.Parameters.AddWithValue("@Venue", test.Venue);
            sqlCommand.Parameters.AddWithValue("@QuestionMarks", test.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", test.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@Duration", test.Duration);
            sqlCommand.ExecuteNonQuery();
            return Ok(test);
        }
        [HttpDelete]
        public IActionResult Delete(int id, Tests test)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("Tests");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(test);
        }
    }
    
}