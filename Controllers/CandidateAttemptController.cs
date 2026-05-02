using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using NEWPROJ.Models;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateAttemptController : ControllerBase
    {
        private readonly string ConnectionString;

        public CandidateAttemptController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ===================== GET =====================
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Get("CandidateAttempt");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            var attempts = new List<CandidateAttempt>();

            while (sqlDataReader.Read())
            {
                var attempt = new CandidateAttempt();

                attempt.Id = int.Parse(sqlDataReader[0].ToString());
                attempt.CandidateId = int.Parse(sqlDataReader[1].ToString());
                attempt.TestId = int.Parse(sqlDataReader[2].ToString());
                attempt.StartTime = DateTime.Parse(sqlDataReader[3].ToString());

                if (sqlDataReader[4] != DBNull.Value)
                    attempt.EndTime = DateTime.Parse(sqlDataReader[4].ToString());

                attempt.Status = sqlDataReader[5].ToString();

                attempts.Add(attempt);
            }

            return Ok(attempts);
        }

        // ===================== POST =====================
        [HttpPost]
        public IActionResult CreateData(CandidateAttempt attempt)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "CandidateId, TestsId, StartTime, EndTime, Status";
            string vls = "@CandidateId, @TestsId, @StartTime, @EndTime, @Status";

            string query = Crud.Insert("CandidateAttempt", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@CandidateId", attempt.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestsId", attempt.TestId);
            sqlCommand.Parameters.AddWithValue("@StartTime", attempt.StartTime);
            sqlCommand.Parameters.AddWithValue("@EndTime", (object?)attempt.EndTime ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Status", attempt.Status);

            sqlCommand.ExecuteNonQuery();

            return Ok(attempt);
        }

        // ===================== PUT =====================
        [HttpPut]
        public IActionResult UpdateData(int id, CandidateAttempt attempt)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "CandidateId = @CandidateId, TestsId = @TestsId, StartTime = @StartTime, EndTime = @EndTime, Status = @Status";
            string query = Crud.Update("CandidateAttempt", cols);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CandidateId", attempt.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestsId", attempt.TestId);
            sqlCommand.Parameters.AddWithValue("@StartTime", attempt.StartTime);
            sqlCommand.Parameters.AddWithValue("@EndTime", (object?)attempt.EndTime ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Status", attempt.Status);

            sqlCommand.ExecuteNonQuery();

            return Ok(attempt);
        }

        // ===================== DELETE =====================
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Delete("CandidateAttempt");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();

            return Ok("Deleted Successfully");
        }
    }
}
