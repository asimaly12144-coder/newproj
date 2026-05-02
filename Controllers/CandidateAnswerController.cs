using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateAnswerController : ControllerBase
    {
        private readonly string ConnectionString;

        public CandidateAnswerController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ===================== GET =====================
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Get("CandidateAnswer");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var list = new List<CandidateAnswer>();

            while (reader.Read())
            {
                var answer = new CandidateAnswer();

                answer.Id = int.Parse(reader[0].ToString());
                answer.CandidateId = int.Parse(reader[1].ToString());
                answer.TestQuestionId = int.Parse(reader[2].ToString());
                answer.OptionId = int.Parse(reader[3].ToString());

                list.Add(answer);
            }

            return Ok(list);
        }

        // ===================== POST =====================
        [HttpPost]
        public IActionResult CreateData(CandidateAnswer answer)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "CandidateId, TestQuestionId, OptionId";
            string vls = "@CandidateId, @TestQuestionId, @OptionId";

            string query = Crud.Insert("CandidateAnswer", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@CandidateId", answer.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestQuestionId", answer.TestQuestionId);
            sqlCommand.Parameters.AddWithValue("@OptionId", answer.OptionId);

            sqlCommand.ExecuteNonQuery();

            return Ok(answer);
        }

        // ===================== PUT =====================
        [HttpPut]
        public IActionResult UpdateData(int id, CandidateAnswer answer)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "CandidateId = @CandidateId, TestQuestionId = @TestQuestionId, OptionId = @OptionId";
            string query = Crud.Update("CandidateAnswer", cols);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CandidateId", answer.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestQuestionId", answer.TestQuestionId);
            sqlCommand.Parameters.AddWithValue("@OptionId", answer.OptionId);

            sqlCommand.ExecuteNonQuery();

            return Ok(answer);
        }

        // ===================== DELETE =====================
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Delete("CandidateAnswer");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();

            return Ok("Deleted Successfully");
        }
    }
}
