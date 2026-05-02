using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionTypeController : ControllerBase
    {
        private readonly string ConnectionString;
        public QuestionTypeController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("QuestionType");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var QUESTIONTYPE = new List<QuestionType>();
            while (sqlDataReader.Read())
            {
                var questionType = new QuestionType();
                questionType.Id = int.Parse(sqlDataReader[0].ToString());
                questionType.QuestionId = int.Parse(sqlDataReader[1].ToString());
                questionType.TypeName = sqlDataReader[2].ToString();
                QUESTIONTYPE.Add(questionType);
            }
            return Ok(QUESTIONTYPE);
        }

        [HttpPost]
        public IActionResult CreateData(QuestionType questionType)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "QuestionId , TypeName";
            string vls = "@QuestionId , @TypeName";
            string query = Crud.Insert("QuestionType", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", questionType.Id);
            sqlCommand.Parameters.AddWithValue("QuestionId", questionType.QuestionId);
            sqlCommand.Parameters.AddWithValue("TypeName", questionType.TypeName);
            sqlCommand.ExecuteNonQuery();
            return Ok(questionType);
        }

        [HttpPut]
        public IActionResult UpdateData(int id, QuestionType questionType)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "QuestionId = @QuestionId, TypeName = @TypeName";
            string query = Crud.Update("QuestionType", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@QuestionId", questionType.QuestionId);
            sqlCommand.Parameters.AddWithValue("@TypeName", questionType.TypeName);
            sqlCommand.ExecuteNonQuery();
            return Ok(questionType);
        }

        [HttpDelete]
        public IActionResult Delete(int id, QuestionType questionType)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("QuestionType");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(questionType);
        }
    }
}
