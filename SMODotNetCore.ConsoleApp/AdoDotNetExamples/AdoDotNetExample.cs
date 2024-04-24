using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMODotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "11-02NOTE",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sa@123"
        };

        public void edit(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            String query = "select * from Tbl_Blog where BlogID = @BlogID";
            SqlCommand sqlCommand = new SqlCommand(query,connection);
            sqlCommand.Parameters.AddWithValue("@BlogID",id);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Not Found !");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine("ID" + dr["BlogID"]);
            Console.WriteLine("Title" + dr["BlogTitle"]);
            Console.WriteLine("Author" + dr["BlogAuthor"]);
            Console.WriteLine("Content" + dr["BlogContent"]);
            Console.ReadKey();
        }

        public void read()
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            String query = "select * from Tbl_Blog;";
            SqlCommand cmd = new SqlCommand(query, connection); 
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);   
            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("ID" + dr["BlogID"]);
                Console.WriteLine("Title" + dr["BlogTitle"]);
                Console.WriteLine("Author" + dr["BlogAuthor"]);
                Console.WriteLine("Content" + dr["BlogContent"]);
            }
            Console.ReadKey();
        }

        public void create(string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            String query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogTitle",title);
            cmd.Parameters.AddWithValue("@BlogAuthor",author);
            cmd.Parameters.AddWithValue("@BlogContent",content);
            int result = cmd.ExecuteNonQuery();  
            connection.Close();
            string message = result > 0 ? "Create Successfull !" : "Create Failed !" ; 
            Console.WriteLine(message); 
            Console.ReadKey();
        }

        public void update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            String query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID",id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update Successfull !" : "Update Failed !";
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public void delete(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();
            String query = @"delete from Tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
