using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly string ConnectionString;
        public ResultController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Get("Result");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var RESULT = new List<Result>();
            while (sqlDataReader.Read())
            {
                var result = new Result();
                result.Id = int.Parse(sqlDataReader[0].ToString());
                result.CandidateId = int.Parse(sqlDataReader[1].ToString());
                result.TestId = int.Parse(sqlDataReader[2].ToString());
                result.TotalMarks = int.Parse(sqlDataReader[3].ToString());
                result.ObtainedMarks = int.Parse(sqlDataReader[4].ToString());
                result.RankCandidate = sqlDataReader[5].ToString();
                RESULT.Add(result);
            }
            return Ok(RESULT);
        }

        [HttpPost]
        public IActionResult CreateData(Result result)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string cols = "CandidateId , TestId , TotalMarks , ObtainedMarks , RankCandidate";
            string vls = "@CandidateId , @TestId , @TotalMarks , @ObtainedMarks , @RankCandidate";
            string query = Crud.Insert("Result", cols, vls);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("Id", result.Id);
            sqlCommand.Parameters.AddWithValue("CandidateId", result.CandidateId);
            sqlCommand.Parameters.AddWithValue("TestId", result.TestId);
            sqlCommand.Parameters.AddWithValue("TotalMarks", result.TotalMarks);
            sqlCommand.Parameters.AddWithValue("ObtainedMarks", result.ObtainedMarks);
            sqlCommand.Parameters.AddWithValue("RankCandidate", result.RankCandidate);
            sqlCommand.ExecuteNonQuery();
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateData(int id, Result result)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string clos = "CandidateId = @CandidateId, TestId = @TestId, TotalMarks = @TotalMarks, ObtainedMarks = @ObtainedMarks, RankCandidate = @RankCandidate";
            string query = Crud.Update("Result", clos);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CandidateId", result.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestId", result.TestId);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", result.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@ObtainedMarks", result.ObtainedMarks);
            sqlCommand.Parameters.AddWithValue("@RankCandidate", result.RankCandidate);
            sqlCommand.ExecuteNonQuery();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id, Result result)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string query = Crud.Delete("Result");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            return Ok(result);
        }
    }
}
