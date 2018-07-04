using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using July1.Hierarhy;

namespace July1
{
    class Service
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        List<User> users;
        public Service()
        {
            settings.DateFormatString = "YYYY-MM-DDTHH:mm:ss.FFFZ";
            CreateHierarhy();
        }

        private List<T> GetItems<T>(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://5b128555d50a5c0014ef1204.mockapi.io/");
                var result = client.GetStringAsync(client.BaseAddress + endpoint).Result;
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(result, settings);
            }
        }

        public void CreateHierarhy()
        {
            try
            {
                
                users = GetItems<User>("users");

                var comments = GetItems<Comment>("comments");

                var posts = GetItems<Post>("posts");

                var todos = GetItems<Todo>("todos");

                posts.ForEach(p => p.Comments = comments.Where(c => c.PostId == p.Id).ToList());

                users.ForEach(u => u.Posts = posts.Where(p => p.UserId == u.Id).ToList());

                users.ForEach(u => u.Todos = todos.Where(t => t.UserId == u.Id).ToList());

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        

        public dynamic CommentsUnderUserPost(int id)
        {
            return users.Single(u => u.Id == id).Posts.Select(x => new { post = x, count = x.Comments.Count });
        }

        public IEnumerable<Comment> CommentUnderUserPostBodyMoreThen50(int id)
        {
            return users.Single(u => u.Id == id).Posts.SelectMany(x => x.Comments.Where(p => p.Body.Length > 50));
        }

        public dynamic CompletedTodosByUser(int id)
        {
            return users.Single(u => u.Id == id).Todos.Where(t => t.IsComplete == true).Select(r => new { id = r.Id, name = r.Name });
        }

        public IEnumerable<User> UsersACSWithTodosDSC()
        {
            Console.Clear();
            return users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Avatar = u.Avatar,
                CreatedAt = u.CreatedAt,
                Email = u.Email,
                Posts = u.Posts,
                Todos = u.Todos.OrderByDescending(t => t.Name.Length).ToList()
            }).OrderBy(u => u.Name).ToList();
        }
        public dynamic UsersInfo(int id)
        {
            try
            {
                return users.Where(u => u.Id == id).Select(u => new
                {
                    user = u,
                    lastPost = u.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault(),
                    comments = u.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault().Comments.Count,
                    todos = u.Todos.Where(t => t.IsComplete == false).Count(),
                    popularComment = u.Posts.OrderBy(p => p.Comments.Where(c => c.Body.Length > 80).Count()).LastOrDefault(),
                    likedPost = u.Posts.OrderBy(p => p.Likes).LastOrDefault()
                }).First();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public dynamic PostsInfo(int id)
        {
            try
            {
                return users.SelectMany(u => u.Posts.Where(p => p.Id == id).Select(p => new
                {
                    post = p,
                    longestComment = p.Comments.OrderBy(c => c.Body.Length).LastOrDefault(),
                    likedComment = p.Comments.OrderBy(c => c.Likes).LastOrDefault(),
                    commentsCount = p.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count()
                })).First();
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
