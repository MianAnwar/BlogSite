using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class DBHandler
    {
        SqlConnection connection = null;
        List<Post> list;
        List<User> list_2;
        public DBHandler()
        {
            connection = server();
            list = new List<Post>();
            list_2 = new List<User>();
        }
        public SqlConnection server()
        {
          //string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=BlogApplication_Database;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Mian Muhammad Anwar\Documents\BlogSiteDB.mdf';Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }


        public List<Post> ViewAllPosts()
        {

            string query = $"select* from Post";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Post u = new Post();
                u.Id = Int32.Parse(dr.GetValue(0).ToString());
                u.Title = dr.GetValue(1).ToString();
                u.Content = dr.GetValue(2).ToString();
                u.Username = dr.GetValue(3).ToString();
                u.Date = (DateTime)dr.GetValue(4);

                list.Add(u);
            }
            dr.Close();
            return list;

        }

        public List<User> ViewAllUsers()
        {

            string query = $"select* from UserDB";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                User u = new User();
                u.Id = Int32.Parse(dr.GetValue(0).ToString());
                u.Name = dr.GetValue(1).ToString();
                u.Age = Int32.Parse(dr.GetValue(2).ToString());
                u.Username = dr.GetValue(3).ToString();
                u.Email = dr.GetValue(4).ToString();
                u.Password = dr.GetValue(5).ToString();

                list_2.Add(u);
            }
            dr.Close();
            return list_2;

        }









        public bool DeletePost(int id)
        {
            string query = $"delete from Post where id = @I";
            SqlParameter p1 = new SqlParameter("I", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            int deleted = cmd.ExecuteNonQuery();
            if (deleted >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteUser(int id)
        {
            string query = $"delete from UserDB where id = @I";
            SqlParameter p1 = new SqlParameter("I", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            int deleted = cmd.ExecuteNonQuery();
            if (deleted >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool AddUser(User u)
        {

            //SQL STRINGS
            string query = $"insert into UserDB(username,password,name,age,email) values(@U,@P,@N,@A,@E)";
            SqlParameter P1 = new SqlParameter("U", u.Username);
            SqlParameter P2 = new SqlParameter("P", u.Password);
            SqlParameter P3 = new SqlParameter("N", u.Name);
            SqlParameter P4 = new SqlParameter("A", u.Age);
            SqlParameter P5 = new SqlParameter("E", u.Email);

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            cmd.Parameters.Add(P2);
            cmd.Parameters.Add(P3);
            cmd.Parameters.Add(P4);
            cmd.Parameters.Add(P5);

            int inserted = cmd.ExecuteNonQuery();
            if (inserted >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        public User CheckUserValidation(string username, string password)
        {
            User u = new User();
            string query = $"select * from UserDB where username =@U AND password=@P";
            SqlParameter P1 = new SqlParameter("U", username);
            SqlParameter P2 = new SqlParameter("P", password);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            cmd.Parameters.Add(P2);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                u.Id = Int32.Parse(dr.GetValue(0).ToString());
                u.Name = dr.GetValue(1).ToString();
                u.Age = Int32.Parse(dr.GetValue(2).ToString());
                u.Username = dr.GetValue(3).ToString();
                u.Email = dr.GetValue(4).ToString();
                u.Password = dr.GetValue(5).ToString();
            }
            dr.Close();
            return u;


        }


        public User findUser(string username)
        {
            User u = new User();
            string query = $"select * from UserDB where username = @U";
            SqlParameter P1 = new SqlParameter("U", username);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                u.Id = Int32.Parse(dr.GetValue(0).ToString());
                u.Name = dr.GetValue(1).ToString();
                u.Age = Int32.Parse(dr.GetValue(2).ToString());
                u.Username = dr.GetValue(3).ToString();
                u.Email = dr.GetValue(4).ToString();
                u.Password = dr.GetValue(5).ToString();
            }
            dr.Close();
            return u;


        }


        public User findUser(int id)
        {
            User u = new User();
            string query = $"select * from UserDB where id = @I";
            SqlParameter P1 = new SqlParameter("I", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                u.Id = Int32.Parse(dr.GetValue(0).ToString());
                u.Name = dr.GetValue(1).ToString();
                u.Age = Int32.Parse(dr.GetValue(2).ToString());
                u.Username = dr.GetValue(3).ToString();
                u.Email = dr.GetValue(4).ToString();
                u.Password = dr.GetValue(5).ToString();
            }
            dr.Close();
            return u;


        }






        public bool UpdatePost(Post p)
        {
            string query = $"update Post set title=@T, content=@C, username=@U,date=@D where id=@I";
            SqlParameter P1 = new SqlParameter("T", p.Title);
            SqlParameter P2 = new SqlParameter("C", p.Content);
            SqlParameter P3 = new SqlParameter("U", p.Username);
            SqlParameter P4 = new SqlParameter("D", p.Date);
            SqlParameter P5 = new SqlParameter("I", p.Id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            cmd.Parameters.Add(P2);
            cmd.Parameters.Add(P3);
            cmd.Parameters.Add(P4);
            cmd.Parameters.Add(P5);
            int updated = cmd.ExecuteNonQuery();
            if (updated >= 1)
            {
                return true;

            }
            else
            {
                return false;
            }

        }








        public bool UpdateUser(User u)
        {
            string query = $"update UserDB set name=@N, password=@P, email=@E,age=@A where username=@U";
            SqlParameter P1 = new SqlParameter("N", u.Name);
            SqlParameter P2 = new SqlParameter("P", u.Password);
            SqlParameter P3 = new SqlParameter("E", u.Email);
            SqlParameter P4 = new SqlParameter("A", u.Age);
            SqlParameter P5 = new SqlParameter("U", u.Username);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            cmd.Parameters.Add(P2);
            cmd.Parameters.Add(P3);
            cmd.Parameters.Add(P4);
            cmd.Parameters.Add(P5);
            int updated = cmd.ExecuteNonQuery();
            if (updated >= 1)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        public Post findPost(int id)
        {
            Post p = new Post();
            string query = $"select * from Post where id = @I";
            SqlParameter P1 = new SqlParameter("I", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                p.Id = Int32.Parse(dr.GetValue(0).ToString());
                p.Title = dr.GetValue(1).ToString();
                p.Content = dr.GetValue(2).ToString();
                p.Username = dr.GetValue(3).ToString();
                p.Date = (DateTime)dr.GetValue(4);
            }
            dr.Close();
            return p;


        }
        public bool CreatePost(Post p)
        {
            //SQL STRINGS
            string query = $"insert into Post(title,content,username,date) values(@T,@C,@U,@D)";
            SqlParameter P1 = new SqlParameter("T", p.Title);
            SqlParameter P2 = new SqlParameter("C", p.Content);
            SqlParameter P3 = new SqlParameter("U", p.Username);
            SqlParameter P4 = new SqlParameter("D", p.Date);

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(P1);
            cmd.Parameters.Add(P2);
            cmd.Parameters.Add(P3);
            cmd.Parameters.Add(P4);

            //InsertedData
            int inserted = cmd.ExecuteNonQuery();
            if (inserted >= 1)
            {
                Console.WriteLine("Post is Created");
                return true;
            }
            else
            {
                Console.WriteLine("Post is not Created");
                return false;
            }


        }
        public bool ValidateCustomer(string user, string pass)
        {
            string query = $"select* from Customers where username = @U AND password= @P";
            SqlParameter p1 = new SqlParameter("U", user);
            SqlParameter p2 = new SqlParameter("P", pass);

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
