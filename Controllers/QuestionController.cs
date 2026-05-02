using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly string ConnectionString;

        public QuestionController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }     
   
        // GET: /Question
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Get("Question");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            var questions = new List<Question>();

            while (reader.Read())
            {
                var question = new Question();

                question.Id = int.Parse(reader[0].ToString());
                question.Statement = reader[1].ToString();
                question.SubId = int.Parse(reader[2].ToString());

                questions.Add(question);
            }

            return Ok(questions);
        }

        // POST: /Question
        [HttpPost]
        public IActionResult CreateData(Question question)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "Statement, SubId";
            string vls = "@Statement, @SubId";

            string query = Crud.Insert("Question", cols, vls);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Statement", question.Statement);
            sqlCommand.Parameters.AddWithValue("@SubId", question.SubId);

            sqlCommand.ExecuteNonQuery();

            return Ok(question);
        }

        // PUT: /Question?id=1
        [HttpPut]
        public IActionResult UpdateData(int id, Question question)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "Statement = @Statement, SubId = @SubId";

            string query = Crud.Update("Question", cols);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Statement", question.Statement);
            sqlCommand.Parameters.AddWithValue("@SubId", question.SubId);

            sqlCommand.ExecuteNonQuery();

            return Ok(question);
        }

        // DELETE: /Question?id=1
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Delete("Question");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);

            sqlCommand.ExecuteNonQuery();

            return Ok("Deleted Successfully");
        }
    }
}
