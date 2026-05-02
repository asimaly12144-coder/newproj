namespace NEWPROJ.Model
{
    public class Crud
    {
        public static string Get(string tablename)
        {
            string qurey = "Select * from " + tablename;
            return qurey;
        }
        public static string Insert(string tablename, string cols, string values)
        {
            string query = "Insert into " + tablename + "(" + cols + ")" + " values " + "(" + values + ")";
            return query;
        }
        public static string Update(string tablename, string values)
        {
            string query = "Update " + tablename + " SET " + values + " Where Id = @Id";
            return query;
        }
        public static string Delete(string tablename)
        {
            string query = "Delete From " + tablename + " where Id = @Id";
            return query;
        }
    }
}
