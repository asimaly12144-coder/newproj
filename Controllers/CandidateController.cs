using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;

namespace NEWPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly string ConnectionString;

        public CandidateController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: /Candidate
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Get("Candidate");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            var candidates = new List<Candidate>();

            while (reader.Read())
            {
                var candidate = new Candidate();

                candidate.Id = int.Parse(reader[0].ToString());
                candidate.Name = reader[1].ToString();
                candidate.Contact = reader[2].ToString();
                candidate.Email = reader[3].ToString();

                candidates.Add(candidate);
            }

            return Ok(candidates);
        }

        // POST: /Candidate
        [HttpPost]
        public IActionResult CreateData(Candidate candidate)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "Name, Contact, Email";
            string vls = "@Name, @Contact, @Email";

            string query = Crud.Insert("Candidate", cols, vls);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Name", candidate.Name);
            sqlCommand.Parameters.AddWithValue("@Contact", candidate.Contact);
            sqlCommand.Parameters.AddWithValue("@Email", candidate.Email);

            sqlCommand.ExecuteNonQuery();

            return Ok(candidate);
        }

        // PUT: /Candidate?id=1
        [HttpPut]
        public IActionResult UpdateData(int id, Candidate candidate)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "Name = @Name, Contact = @Contact, Email = @Email";

            string query = Crud.Update("Candidate", cols);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", candidate.Name);
            sqlCommand.Parameters.AddWithValue("@Contact", candidate.Contact);
            sqlCommand.Parameters.AddWithValue("@Email", candidate.Email);

            sqlCommand.ExecuteNonQuery();

            return Ok(candidate);
        }

        // DELETE: /Candidate?id=1
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Delete("Candidate");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);

            sqlCommand.ExecuteNonQuery();

            return Ok("Deleted Successfully");
        }
    }
}
