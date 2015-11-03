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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Blogs to choose:");
            comboBox1.SelectedItem = comboBox1.Items[0];

            using (var bContext = new BlogContext()) {
                var blogs = bContext.Blogs.Select(s => s.Name);
                foreach (var blog in blogs) {
                    if (blog!= null)
                        comboBox1.Items.Add(blog);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bContext = new BlogContext())    {
                label2.Text = (from b in bContext.Blogs
                               from p in bContext.Posts
                               where (b.BlogId == p.BlogId && b.Name == comboBox1.SelectedItem)
                               select b.Posts).ToList().Count().ToString();
                label2.Update();
            }

        }
    }
}
