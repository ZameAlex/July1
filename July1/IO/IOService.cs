using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1.IO
{
    class IOService
    {
        Service service;
        public IOService()
        {
            service = new Service();
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

        public string[] GetNames()
        {
            return new string[]
            {
                "Comments under users post",
                "Comments under post, where body more than 50",
                "Completed user`s ToDos",
                "Users with ToDos (Users sorted ascending, ToDos Descending",
                "User, Last post, Comments count, Not completed ToDos, Post with most likes and characters >80, Post with most likes",
                "Post, Largest comment, Most popular comment, count comments with 0 likes or text >80"
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
                if (id < 1 || id > 100)
                    throw new FormatException("Value should be between 1 and 100. Default value is 1.");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            return id;
        }

        public void CommentsUnderUserPost(int id)
        {

            var result = service.CommentsUnderUserPost(id);
            Console.WriteLine($"Posts of user {id}");
            foreach (var item in result)
            {
                Console.WriteLine($"Post:\n{item.post.ToString()}\nComments count:{item.count}");
            }
        }

        public void CommentUnderUserPostBodyMoreThen50(int id)
        {
            var result = service.CommentUnderUserPostBodyMoreThen50(id);
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void CompletedTodosByUser(int id)
        {
            var result = service.CompletedTodosByUser(id);
            Console.WriteLine($"Completed todos of user {id}");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.id} | {item.name}");
            }
        }

        public void UsersACSWithTodosDSC(int id)
        {
            Console.Clear();
            var orderedUsers = service.UsersACSWithTodosDSC();
            foreach (var item in orderedUsers)
            {
                Console.WriteLine(item.ToString());
                foreach (var post in item.Todos)
                    Console.WriteLine(post.ToString());
                Console.WriteLine();
            }
        }
        public void UsersInfo(int id)
        {
            try
            {
                var result = service.UsersInfo(id);
                Console.WriteLine(result.user.ToString());
                Console.WriteLine(result.lastPost);
                Console.WriteLine(result.comments);
                Console.WriteLine(result.todos);
                Console.WriteLine(result.popularComment);
                Console.WriteLine($"{result.likedPost}, likes ={result.likedPost.Likes}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error during collecting data. Check, if the current user have any posts");
            }
            

        }

        public void PostsInfo(int id)
        {
            try
            {
                var result = service.PostsInfo(id);
                Console.WriteLine(result.post.ToString());
                Console.WriteLine(result.longestComment.ToString());
                Console.WriteLine($"{result.likedComment.ToString()}, Likes: {result.likedComment.Likes}");
                Console.WriteLine(result.commentsCount);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during collecting data. Check, if the current post have any comments");
            }
        }
    }
}
