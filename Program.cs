using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Serialization;
using System.Security;
using System.Data.Entity.Core.Objects;

namespace Lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj nazwę bloga: ");
            String blogName = Console.ReadLine();
            Blog firstBlog = new Blog { Name = blogName };
            BlogContext blogContext = new BlogContext();
            blogContext.Blogs.Add(firstBlog);
            blogContext.SaveChanges();

            printBlogs(blogContext);
            //listBlogsAndPosts(blogContext);
            //listBlogsAndPostsNav(blogContext);
            listBlogsAndPostsNavQuery(blogContext);
            countPosts(blogContext);

            BlogForm form = new BlogForm();
            form.ShowDialog();
            

            Console.ReadLine();
             
        }

        static void printBlogs(BlogContext blogContext)
        {
            Console.WriteLine("Blogi w projekcie: ");
            IQueryable<string> blogNames = blogContext.Blogs.Select(b => b.Name).OrderByDescending(name => name);
            foreach (String name in blogNames)
            {
                Console.WriteLine(name);
            }
        }

        static void printBlogImm(BlogContext blogContext)
        {
            Console.WriteLine("Blogi w projekcie: ");
            String[] blogNames = blogContext.Blogs.Select(b => b.Name).OrderByDescending(name => name).ToArray();
            foreach (String name in blogNames)
            {
                Console.WriteLine(name);
            }
        }

        static void listBlogsAndPosts(BlogContext blogContext)
        {
            DbSet<Blog> blogs = blogContext.Blogs;
            DbSet<Post> posts = blogContext.Posts;
            
            var query = blogs.Join(
                posts,
                post => post.BlogId,
                blog => blog.BlogId,
                (blog, post) => new
                {
                    BlogID = blog.BlogId,
                    BlogName = blog.Name,
                    PostTitle = post.Title
                });
            foreach (var postItem in query)
            {
                Console.WriteLine("BlogID: {0} BlogName: {1} Post: {2}", postItem.BlogID, postItem.BlogName, postItem.PostTitle);
            }

        }

        static void listBlogsAndPostsQuery(BlogContext blogContext)
        {
            DbSet<Blog> blogs = blogContext.Blogs;
            DbSet<Post> posts = blogContext.Posts;

            var query = from blog in blogs
                        join post in posts
                        on blog.BlogId equals post.BlogId
                        select new
                        {
                            BlogID = blog.BlogId,
                            BlogName = blog.Name,
                            PostTitle = post.Title
                        };
            foreach (var postItem in query)
            {
                Console.WriteLine("BlogID: {0} BlogName: {1} Post: {2}", postItem.BlogID, postItem.BlogName, postItem.PostTitle);
            }

        }

        static void listBlogsAndPostsNav(BlogContext bc)
        {
            var blogs = bc.Blogs.Include(b => b.Posts).ToList();
            foreach (var b in blogs)
            {
                Console.WriteLine("BlogID: {0} BlogName: {1} Posts: ", b.BlogId, b.Name);
                foreach (var p in b.Posts)
                {
                    Console.WriteLine("title: {0} id: {1}", p.Title, p.PostId);
                }
            }
        }

        static void listBlogsAndPostsNavQuery(BlogContext bc)
        {
            var blogs = from b in bc.Blogs
                        select new
                            {
                                BlogName = b.Name,
                                BlogID = b.BlogId,
                                Posts = b.Posts
                            };
                //bc.Blogs.Include(b => b.Posts).ToList();
            foreach (var b in blogs)
            {
                Console.WriteLine("BlogID: {0} BlogName: {1} Posts: ", b.BlogID, b.BlogName);
                foreach (var p in b.Posts)
                {
                    Console.WriteLine("title: {0} id: {1}", p.Title, p.PostId);
                }
            }
        }

        static void countPosts(BlogContext bc)
        {
            var query = from post in bc.Posts
                        group post by post.BlogId into BlogGroup
                        select new { BlogID = BlogGroup.Key, PostCount = BlogGroup.Count() };
            foreach (var b in query)
            {
                Console.WriteLine("BlogID: {0} PostCount: {1}", b.BlogID, b.PostCount );
            }

        }
    }
}
