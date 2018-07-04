using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1.Hierarhy
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }


        public List<Post> Posts { get; set; }
        public List<Todo> Todos { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
