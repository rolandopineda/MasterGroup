using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIServer
{
    public class Clases
    {

        public class posts
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }

        public class postsModified
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string oldTitle { get; set; }
            public string oldBody { get; set; }
            public string newTitle { get; set; }
            public string newBody { get; set; }
        }

        public class comments
        {
            public int postId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string body { get; set; }
        }

        public class postsComments
        {
            public posts post { get; set; }
            public List<comments> comentarios { get; set; }

            public postsComments()
            {
                post = new posts();
                comentarios = new List<comments>();
            }
        }

    }
}