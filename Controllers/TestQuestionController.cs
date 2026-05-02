using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestQuestionController : ControllerBase
    {
        private readonly string ConnectionString;
        public TestQuestionController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("TestQuestion");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var TESTQUESTION = new List<TestQuestion>();
            while (sqlDataReader.Read())
            {
                var testQuestion = new TestQuestion();
                testQuestion.Id = int.Parse(sqlDataReader[0].ToString());
                testQuestion.TestId = int.Parse(sqlDataReader[1].ToString());
                testQuestion.QuestionId = int.Parse(sqlDataReader[2].ToString());
                testQuestion.IsNegativeMarking = bool.Parse(sqlDataReader[3].ToString());
                TESTQUESTION.Add(testQuestion);
            }
            return Ok(TESTQUESTION);
        }

        [HttpPost]
        public IActionResult CreateData(TestQuestion testQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "TestId , QuestionId , IsNegativeMarking";
            string vls = "@TestId , @QuestionId , @IsNegativeMarking";
            string query = Crud.Insert("TestQuestion", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", testQuestion.Id);
            sqlCommand.Parameters.AddWithValue("TestId", testQuestion.TestId);
            sqlCommand.Parameters.AddWithValue("QuestionId", testQuestion.QuestionId);
            sqlCommand.Parameters.AddWithValue("IsNegativeMarking", testQuestion.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();
            return Ok(testQuestion);
        }

        [HttpPut]
        public IActionResult UpdateData(int id, TestQuestion testQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "TestId = @TestId, QuestionId = @QuestionId, IsNegativeMarking = @IsNegativeMarking";
            string query = Crud.Update("TestQuestion", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@TestId", testQuestion.TestId);
            sqlCommand.Parameters.AddWithValue("@QuestionId", testQuestion.QuestionId);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testQuestion.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();
            return Ok(testQuestion);
        }

        [HttpDelete]
        public IActionResult Delete(int id, TestQuestion testQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("TestQuestion");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(testQuestion);
        }
    }
}
