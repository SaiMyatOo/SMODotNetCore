using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMODotNetCore.RestApi.ConnectionManager;
using SMODotNetCore.RestApi.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using SMODotNetCore.Share;

namespace SMODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly AdoDotNetService _service = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_Blog";
            /*
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection); 
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            */

            /*
            List<BlogDto> list = new List<BlogDto>();
            foreach(DataRow dr in dt.Rows)
            {
                BlogDto b = new BlogDto();
                b.BlogID = Convert.ToInt32(dr["BlogID"]);
                b.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                b.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                b.BlogContent = Convert.ToString(dr["BlogContent"]);
                list.Add(b);    
            }
            */

            /*
            List<BlogDto> list = dt.AsEnumerable().Select(dr => new BlogDto
            {
                BlogID = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();   
            */
            var list = _service.Query<BlogDto>(query);
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            string query = "select * from Tbl_Blog where BlogID = @BlogID";
            /*
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Data Not Found !");
            }
            DataRow dr = dt.Rows[0];
            BlogDto item = new BlogDto
            {
                BlogID = Convert.ToInt32(dr["BlogID"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
            */
            var item = _service.QueryFirstOrDeafult<BlogDto>(query, new AdoDotNetParameter("@BlogID", id));
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogDto blogDto)
        {
            String query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            /*
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blogDto.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogDto.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogDto.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            */
            int result = _service.Execute(query, new AdoDotNetParameter("@BlogTitle", blogDto.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blogDto.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blogDto.BlogContent)
                );
            string message = result > 0 ? "Create Successfull !" : "Create Failed !";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogDto blogDto)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blogDto.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogDto.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogDto.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update Successfull !" : "Update Failed !";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogDto blogDto)
        {
            var updates = new List<string>();
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(blogDto.BlogTitle))
            {
                updates.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new SqlParameter("@BlogTitle", blogDto.BlogTitle));
            }

            if (!string.IsNullOrEmpty(blogDto.BlogAuthor))
            {
                updates.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new SqlParameter("@BlogAuthor", blogDto.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blogDto.BlogContent))
            {
                updates.Add("[BlogContent] = @BlogContent");
                parameters.Add(new SqlParameter("@BlogContent", blogDto.BlogContent));
            }

            if (updates.Count == 0)
            {
                return BadRequest("No Data Not Found !.");
            }

            var query = $"UPDATE [dbo].[Tbl_Blog] SET {string.Join(", ", updates)} WHERE [BlogID] = @BlogID";
            parameters.Add(new SqlParameter("@BlogID", id));

            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection); 
            cmd.Parameters.AddRange(parameters.ToArray());
            int result = cmd.ExecuteNonQuery();
            connection.Close(); 
            string message = result > 0 ? "Patch successful!" : "Patch failed!";
            return Ok(message);
        }
        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            String query = @"delete from Tbl_Blog where BlogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Successfull !" : "Delete Failed !";
            return Ok(message);
        }
    }
}
