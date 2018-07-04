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
        public HttpClient Client { get; private set; }
        List<User> users;
        public Service()
        {
           
        }

        public async Task CreateHierarhy()
        {
            try
            {
                Client = new HttpClient();
                Client.BaseAddress = new Uri("https://5b128555d50a5c0014ef1204.mockapi.io/");
                HttpResponseMessage result = await Client.GetAsync(Client.BaseAddress + "users");
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
                settings.DateFormatString = "YYYY-MM-DDTHH:mm:ss.FFFZ";

                users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseBody,settings);

                result = await Client.GetAsync(Client.BaseAddress + "comments");
                result.EnsureSuccessStatusCode();
                responseBody = await result.Content.ReadAsStringAsync();
                var comments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comment>>(responseBody, settings);

                result = await Client.GetAsync(Client.BaseAddress + "posts");
                result.EnsureSuccessStatusCode();
                responseBody = await result.Content.ReadAsStringAsync();
                var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Post>>(responseBody, settings);
                posts.ForEach(p => p.Comments = comments.Where(c => c.PostId == p.Id).ToList());

                users.ForEach(u => u.Posts = posts.Where(p => p.UserId == u.Id).ToList());

                result = await Client.GetAsync(Client.BaseAddress + "todos");
                result.EnsureSuccessStatusCode();
                responseBody = await result.Content.ReadAsStringAsync();
                var todos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Todo>>(responseBody, settings);

                users.ForEach(u => u.Todos = todos.Where(t => t.UserId == u.Id).ToList());

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public MenuClass.MenuMethod[] GetMethods()
        {
            return new MenuClass.MenuMethod[]
            {
                this.CommentsUnderUserPost,
                this.CommentUnderUserPostBodyMoreThen50,
                this.CompletedTodosByUser,
                this.UsersACSWithTodosDSC,
                this.UsersInfo,
                this.PostsInfo
            };
        }
        public int GetID()
        {
            Console.Clear();
            Console.WriteLine("Enter id, please");
            int id = 1;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return id;
        }

        public void CommentsUnderUserPost(int id)
        {
            var result = users.Single(u => u.Id == id).Posts.Select(x => new { post = x, count = x.Comments.Count });
            Console.WriteLine($"Posts of user {id}");
            foreach (var item in result)
            {
                Console.WriteLine($"Post:\n{item.post.ToString()}\nComments count:{item.count}");
            }
        }

        public void CommentUnderUserPostBodyMoreThen50(int id)
        {
            var result = users.Single(u => u.Id == id).Posts.SelectMany(x => x.Comments.Where(p => p.Body.Length > 50));
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void CompletedTodosByUser(int id)
        {
            var result = users.Single(u => u.Id == id).Todos.Where(t => t.IsComplete == true).Select(r => new { id = r.Id, name = r.Name });
            Console.WriteLine($"Completed todos of user {id}");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.id} | {item.name}");
            }
        }

        public void UsersACSWithTodosDSC(int id)
        {
            var orderedUsers = users.Select(u => new
            {
                user = u,
                posts = u.Posts.OrderByDescending(p => p.Title)
            }).OrderBy(u => u.user.Name);
            foreach(var item in orderedUsers)
            {
                Console.WriteLine(item.user.ToString());
                foreach(var post in item.posts)
                    Console.WriteLine(post.ToString());
            }
        }
        public void UsersInfo(int id)
        {
            var result = users.Where(u => u.Id == id).Select(u => new
            {
                user = u,
                lastPost = u.Posts.OrderByDescending(p => p.CreatedAt).First(),
                comments = u.Posts.OrderByDescending(p => p.CreatedAt).First().Comments.Count,
                todos = u.Todos.Where(t => t.IsComplete == false).Count(),
                popularComment = u.Posts.OrderBy(p => p.Comments.Where(c=>c.Body.Length>80).Count()).Last(),
                likedPost=u.Posts.OrderBy(p=>p.Likes).Last()
            }).First();
            Console.WriteLine(result.user.ToString());
            Console.WriteLine(result.lastPost);
            Console.WriteLine(result.comments);
            Console.WriteLine(result.todos);
            Console.WriteLine(result.popularComment);
            Console.WriteLine($"{result.likedPost}, likes ={result.likedPost.Likes}");

        }

        public void PostsInfo(int id)
        {
            var result = users.SelectMany(u => u.Posts.Where(p => p.Id == id).Select(p => new
            {
                post = p,
                longestComment = p.Comments.OrderBy(c => c.Body.Length).Last(),
                likedComment = p.Comments.OrderBy(c => c.Likes).Last(),
                commentsCount = p.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count()
            })).First();
            Console.WriteLine(result.post.ToString());
            Console.WriteLine(result.longestComment.ToString());
            Console.WriteLine($"{result.likedComment.ToString()}, Likes: {result.likedComment.Likes}");
            Console.WriteLine(result.commentsCount);
        }
    

    }
}
