using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1.Hierarhy
{
    class Post
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }

        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"Id:{this.Id} | Created at:{this.CreatedAt} | Title:{this.Title} | Body:{this.Body}";
        }
    }
}
