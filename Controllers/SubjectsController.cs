using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NEWPROJ.Model;

namespace NEWPROJ.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly string ConnectionString;

        public SubjectsController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: /Subjects
        [HttpGet]
        public ActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Get("Subjects");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            var subjects = new List<Subjects>();

            while (reader.Read())
            {
                var subject = new Subjects();

                subject.Id = int.Parse(reader[0].ToString());
                subject.SubName = reader[1].ToString();
                subject.Code = reader[2].ToString();

                subjects.Add(subject);
            }

            return Ok(subjects);
        }

        // POST: /Subjects
        [HttpPost]
        public IActionResult CreateData(Subjects subject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "SubName, Code";
            string vls = "@SubName, @Code";

            string query = Crud.Insert("Subjects", cols, vls);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@SubName", subject.SubName);
            sqlCommand.Parameters.AddWithValue("@Code", subject.Code);

            sqlCommand.ExecuteNonQuery();

            return Ok(subject);
        }

        // PUT: /Subjects?id=1
        [HttpPut]
        public IActionResult UpdateData(int id, Subjects subject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string cols = "SubName = @SubName, Code = @Code";

            string query = Crud.Update("Subjects", cols);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@SubName", subject.SubName);
            sqlCommand.Parameters.AddWithValue("@Code", subject.Code);

            sqlCommand.ExecuteNonQuery();

            return Ok(subject);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            string query = Crud.Delete("Subjects");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", id);

            sqlCommand.ExecuteNonQuery();

            return Ok("Deleted Successfully");
        }








    }
}
