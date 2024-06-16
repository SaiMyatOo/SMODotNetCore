using Dapper;
using SMODotNetCore.ConsoleApp.Dtos;
using SMODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMODotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperExample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }
        public DapperExample()
        {

        }
        public void Run()
        {
            //Read();
            //Edit(11);
            //Create("Dota1","Blizzard","Warcraft III");
            //Update(1,"General","EA Games","EA");
            Delete(1);
        }
        private void Read()
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = dbConnection.Query<BlogDto>("select * from Tbl_Blog").ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------------------------");
            }
        }
        private void Edit(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var item = dbConnection.Query<BlogDto>("select * from Tbl_Blog where BlogId=@BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("Data Not Found !");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------------------------");
        }
        private void Create(string title, string author, string content)
        {
            var data = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            int result = db.Execute(query, data);
            string message = result > 0 ? "Create Successfull !" : "Create Error !";
            Console.WriteLine(message);
            Console.ReadKey();
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId;";
            var result = db.Execute(query, item);
            string message = result > 0 ? "Update Successfull !" : "Update Error !";
            Console.WriteLine(message);
            Console.ReadKey();
        }
        private void Delete(int id)
        {
            var data = new BlogDto
            {
                BlogId = id
            };
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@BlogId";
            int result = db.Execute(query, data);
            string message = result > 0 ? "Delete Successfull !" : "Delete Error !";
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
