using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SWE_Final_Lab_Test
{

    public partial class Form1 : Form
    {
        private SqlConnection conn = new SqlConnection();
        private string ConString = "Server=DESKTOP-HFA36BB; Database = ProductDB; User = sa; Password = 1114012833m";
        private SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = ConString;
            cmd = conn.CreateCommand();

            try
            {
                string query = "sp_showall";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridView1.DataSource = dt;

                reader.Close();

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Load(this, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pid = pidbox.Text;
            string pname = pnamebox.Text;
            string pqa = pqabox.Text;
            string ped = pxdbox.Text;



            if ((pid == "") || (pname == "") || (pqa == "") || (ped == ""))
            {
                string msg = "No textbox can be empty";
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                cmd = conn.CreateCommand();

                try
                {

                    string query = "Insert into ProductInformation values(@pid,@pname,@pqa,@ped)";

                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@pid", SqlDbType.VarChar);
                    cmd.Parameters["@pid"].Value = pid;
                    cmd.Parameters.Add("@pname", SqlDbType.VarChar);
                    cmd.Parameters["@pname"].Value = pname;
                    cmd.Parameters.Add("@pqa", SqlDbType.VarChar);
                    cmd.Parameters["@pqa"].Value = pqa;
                    cmd.Parameters.Add("@ped", SqlDbType.VarChar);
                    cmd.Parameters["@ped"].Value = ped;


                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();


                }
                catch (Exception e1)
                {
                    string msg = e1.Message.ToString();
                    string caption = "Error";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deletebox.Text);
            if (id != 0)
            {
                cmd = conn.CreateCommand();
                try
                {

                    string command = "sp_delete";
                    cmd = new SqlCommand(command, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Show Delete Result");

                    reader.Close();

                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    string caption = "Error";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
            else
            {
                string msg = "No textbox can be empty";
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void datagridclick(object sender, DataGridViewCellEventArgs e)
        {
           pidbox.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
           pnamebox.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
           pqabox.Text=dataGridView1.CurrentRow.Cells[2].Value.ToString();
           pxdbox.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string pname = pnamebox.Text;
            string pqa = pqabox.Text;
            string ped = pxdbox.Text;
            string pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            int pid2 = Convert.ToInt32(pid);
            cmd = conn.CreateCommand();

            try
            {

                string query = "UPDATE ProductInformation SET PID=@pid2,PName = @pname, PQuantityAvailable = @pqa, PExpiryDate = @ped WHERE PID = @pid2 ";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@pid2", SqlDbType.VarChar);
                cmd.Parameters["@pid2"].Value = pid;
                cmd.Parameters.Add("@pname", SqlDbType.VarChar);
                cmd.Parameters["@pname"].Value = pname;
                cmd.Parameters.Add("@pqa", SqlDbType.VarChar);
                cmd.Parameters["@pqa"].Value = pqa;
                cmd.Parameters.Add("@ped", SqlDbType.VarChar);
                cmd.Parameters["@ped"].Value = ped;


                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteScalar();


            }
            catch (Exception e1)
            {
                string msg = e1.Message.ToString();
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pidbox.Text = "";
            pnamebox.Text = "";
            pqabox.Text = "";
            pxdbox.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deletebox.Text);
            if (id != 0)
            {
                cmd = conn.CreateCommand();
                try
                {

                    string command = "select * from ProductInformation where PID=@id";
                    cmd = new SqlCommand(command, conn);

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Press OK to show result");
                    
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    dataGridView1.DataSource = dt;

                    reader.Close();
                    reader.Close();

                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    string caption = "Error";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
            else
            {
                string msg = "No textbox can be empty";
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
