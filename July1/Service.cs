using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1
{
    class Service
    {
     
        //List<Users>
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
            Console.WriteLine("Enter id, please");
            int id = 1;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return id;
        }

        public void CommentsUnderUserPost(int id)
        {

        }

        public void CommentUnderUserPostBodyMoreThen50(int id)
        {

        }

        public void CompletedTodosByUser(int id)
        {

        }

        public void UsersACSWithTodosDSC(int id)
        {

        }
        public void UsersInfo(int id)
        {

        }

        public void PostsInfo(int id)
        {

        }


    }
}
