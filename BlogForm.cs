using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Lab01
{
    public partial class BlogForm : Form
    {
        BlogContext bContext;

        public BlogForm()
        {
            InitializeComponent();
        }

        private void BlogForm_Load(object sender, EventArgs e)
        {
            bContext = new BlogContext();
            bContext.Blogs.Load();
            bContext.Posts.Load();
            this.blogBindingSource.DataSource = bContext.Blogs.Local.ToBindingList();
            this.postsBindingSource.DataSource = bContext.Posts.Local.ToBindingList();
        }

        private void blogDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (blogDataGridView.SelectedRows.Count == 1 && e.ColumnIndex == 0)
            {
                int id = (int) blogDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                Console.WriteLine(id);
                //DataGridViewRow row = blogDataGridView.SelectedRows[0];
                bContext.Posts.Where(post => post.BlogId == id).Load();
                this.postsBindingSource.DataSource = bContext.Posts.Local.ToBindingList();
            }
        }

        private void blogBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            bContext.SaveChanges();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            bContext.SaveChanges();
        }

        private void postsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            bContext.SaveChanges();
        }

        private void blogDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int id = (int)blogDataGridView.Rows[e.RowIndex].Cells[0].Value;
                //Console.WriteLine(id);
                
                bContext.Posts.Where(post => post.BlogId == id).Load();
                // figure out how to remove entries
                //this.postsBindingSource.DataSource = null;
                //this.postsDataGridView.Rows.Clear();
                this.postsBindingSource.DataSource = bContext.Posts.Local.ToBindingList();
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPostForm form = new AddPostForm();
            form.ShowDialog();
        }

       

    }
}
