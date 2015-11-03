using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class AddPostForm : Form
    {
        public AddPostForm()
        {
            InitializeComponent();
            this.Load += AddPostForm_Load;
        }

        private void AddPostForm_Load(object sender, EventArgs e)   {
            comboBox1.Items.Add("Blogs to choose:");
            comboBox1.SelectedItem = comboBox1.Items[0];
            using (var context = new BlogContext()) {
                var blogs = from blog in context.Blogs
                            orderby blog.Name
                            select blog.Name;

                foreach (var blog in blogs) {
                    if (blog != null)
                        comboBox1.Items.Add(blog);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var selectedBlog = (string)comboBox1.SelectedItem;
            int blogId;

            using (var bContext = new BlogContext())
            {
                blogId = (from b in bContext.Blogs where b.Name == selectedBlog select b.BlogId).FirstOrDefault();
                if (blogId == 0)
                {
                    MessageBox.Show("No blog selected.");
                    return;
                }
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("No title!");
                return;
            }
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Post should have some content...");
                return;
            }
            var post = new Post();
            post.BlogId = blogId;
            post.Content = richTextBox1.Text;
            post.Title = textBox1.Text;
            using (var bContext = new BlogContext())
            {
                bContext.Posts.Add(post);
                bContext.SaveChanges();
            }
            Hide();
            DestroyHandle();
        }

    }
}
