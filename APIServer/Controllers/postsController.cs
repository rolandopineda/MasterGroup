using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace APIServer.Controllers
{
    public class postsController : ApiController
    {
        List<Clases.posts> Posts = new List<Clases.posts>();
        List<Clases.comments> Comments = new List<Clases.comments>();

        List<Clases.posts> PostsDeleted = new List<Clases.posts>();
        List<Clases.postsModified> PostsModified = new List<Clases.postsModified>();
        List<Clases.comments> CommentsModified = new List<Clases.comments>();

        // GET: api/posts
        // *** Obtienen el listado de todos los posts
        public List<Clases.posts> Get()
        {
            string mRetorna = "";

            List<Clases.posts> mPosts = new List<Clases.posts>();
            List<Clases.comments> mComments = new List<Clases.comments>();

            try
            {
                mPosts = ObtienePosts();
                Posts = mPosts;

                mComments = ObtieneComments();
                Comments = mComments;

                //mRetorna = Convert.ToString(responseString);
                mRetorna = JsonConvert.SerializeObject(mPosts);
            }
            catch (Exception ex)
            {
                mRetorna = CatchClass.ExMessage(ex, "postsController", "Get");
            }

            return mPosts;
            //return mRetorna;
        }

        List<Clases.posts> ObtienePosts()
        {
            List<Clases.posts> mPosts = new List<Clases.posts>();

            //Posts
            WebRequest request = WebRequest.Create("https://jsonplaceholder.typicode.com/posts");

            request.Method = "GET";
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            mPosts = JsonConvert.DeserializeObject<List<Clases.posts>>(responseString);

            return mPosts;
        }

        List<Clases.comments> ObtieneComments()
        {
            List<Clases.comments> mComments = new List<Clases.comments>();

            //Comments
            WebRequest request2 = WebRequest.Create("http://jsonplaceholder.typicode.com/comments");

            request2.Method = "GET";
            request2.ContentType = "application/json";

            var response2 = (HttpWebResponse)request2.GetResponse();
            var responseString2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();

            mComments = JsonConvert.DeserializeObject<List<Clases.comments>>(responseString2);

            return mComments;
        }

        // GET: api/posts/5
        // *** Obtienen la información de un solo post con sus respectivos comentarios
        public Clases.postsComments Get(int id)
        {
            Posts = ObtienePosts();
            Comments = ObtieneComments();

            Clases.postsComments mPostsComments = new Clases.postsComments();

            var q = from a in Posts
                    where a.id == id
                    orderby a.id
                    select a;
            foreach (var item in q)
            {
                mPostsComments.post.userId = item.userId;
                mPostsComments.post.id = item.id;
                mPostsComments.post.title = item.title;
                mPostsComments.post.body = item.body;
            }

            var qq = from a in Comments
                     where a.postId == id
                     orderby a.postId, a.id
                     select a;
            foreach (var item in qq)
            {
                Clases.comments mItem = new Clases.comments();
                mItem.postId = item.postId;
                mItem.id = item.id;
                mItem.name = item.name;
                mItem.email = item.email;
                mItem.body = item.body;
                mPostsComments.comentarios.Add(mItem);
            }

            return mPostsComments;
        }

        // POST: api/posts
        // *** Ingresa un nuevo post
        public void Post([FromBody]Clases.posts value)
        {
            Clases.posts mItem = new Clases.posts();
            mItem.userId = value.userId;
            mItem.id = value.id;
            mItem.title = value.title;
            mItem.body = value.body;
            Posts.Add(mItem);
        }

        // POST: api/posts
        // *** Ingresa un nuevo comentario al post con <id> especificado
        public void PostComment(int id, [FromBody]Clases.comments value)
        {
            int mIdComment = (from c in Comments select c).Count();

            Clases.comments mItem = new Clases.comments();
            mItem.postId = id;
            mItem.id = mIdComment + 1;
            mItem.name = value.name;
            mItem.email = value.email;
            mItem.body = value.body;
            Comments.Add(mItem);
        }

        // PUT: api/posts/5
        // *** Modifica el o los campos especificados del post, ya sea el title o el body
        public void Put(int id, [FromBody]Clases.posts value)
        {
            var q = from c in Posts where c.id == id select c;
            foreach (var item in q)
            {
                //Guardo una bitácora de los cambios realizados al posts
                Clases.postsModified mItem = new Clases.postsModified();
                mItem.userId = item.userId;
                mItem.id = item.id;

                if (value.title != null)
                {
                    mItem.oldTitle = item.title;
                    mItem.newTitle = value.title;

                    item.title = value.title;
                }
                if (value.body != null)
                {
                    mItem.oldBody = item.body;
                    mItem.newBody = value.body;

                    item.body = value.body;
                }

                PostsModified.Add(mItem);

            }
        }

        // DELETE: api/posts/5
        // *** Elimina el post con id especificado
        public void Delete(int id)
        {
            //Primero guardo un registro del posts a elminar en la lista PostsDeleted
            var q = from c in Posts where c.id == id select c;
            foreach (var item in q)
            {
                Clases.posts mItem = new Clases.posts();
                mItem.userId = item.userId;
                mItem.id = item.id;
                mItem.title = item.title;
                mItem.body = item.body;
                PostsDeleted.Add(mItem);
            }

            Posts.RemoveAt(id);
        }
    }
}
