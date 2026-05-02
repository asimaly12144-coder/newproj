using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestSubjectController : ControllerBase
    {
        private readonly string ConnectionString;
        public TestSubjectController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("TestSubject");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var testSubjects = new List<TestSubject>();
            while (sqlDataReader.Read())
            {
                var testSubject = new TestSubject();
                testSubject.Id = int.Parse(sqlDataReader[0].ToString());
                testSubject.TestsId = int.Parse(sqlDataReader[1].ToString());
                testSubject.SubId = int.Parse(sqlDataReader[2].ToString());
                testSubject.QuestionType = sqlDataReader[3].ToString();
                testSubject.IsNegativeMarking = bool.Parse(sqlDataReader[4].ToString());
                testSubjects.Add(testSubject);
            }
            return Ok(testSubjects);
        }

        [HttpPost]
        public IActionResult CreateData(TestSubject testSubject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "TestsId, SubId, QuestionType, IsNegativeMarking";
            string vls = "@TestsId, @SubId, @QuestionType, @IsNegativeMarking";
            string query = Crud.Insert("TestSubject", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", testSubject.Id);
            sqlCommand.Parameters.AddWithValue("TestsId", testSubject.TestsId);
            sqlCommand.Parameters.AddWithValue("SubId", testSubject.SubId);
            sqlCommand.Parameters.AddWithValue("QuestionType", testSubject.QuestionType);
            sqlCommand.Parameters.AddWithValue("IsNegativeMarking", testSubject.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();
            return Ok(testSubject);
        }

        [HttpPut]
        public IActionResult UpdateData(int id, TestSubject testSubject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "TestsId = @TestsId, SubId = @SubId, QuestionType = @QuestionType, IsNegativeMarking = @IsNegativeMarking";
            string query = Crud.Update("TestSubject", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@TestsId", testSubject.TestsId);
            sqlCommand.Parameters.AddWithValue("@SubId", testSubject.SubId);
            sqlCommand.Parameters.AddWithValue("@QuestionType", testSubject.QuestionType);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testSubject.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();
            return Ok(testSubject);
        }

        [HttpDelete]
        public IActionResult Delete(int id, TestSubject testSubject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("TestSubject");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(testSubject);
        }
    }
}
