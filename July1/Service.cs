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
        public Service()
        {
           
        }

        public async void CreateHierarhy()
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
                List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseBody,settings);
                foreach(var item in users)
                {
                    Console.WriteLine(item.Name);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
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
