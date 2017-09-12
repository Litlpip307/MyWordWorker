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

namespace ProjectForm
{
    public partial class Form1 : Form
    {

        SqlConnection con= new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Рамирос\Documents\Visual Studio 2015\Projects\ProjectForm\MyProjectDB.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            disp_table();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!= "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" )
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employee(FirstName,SecondName,LastName,DepartmentId,Telephone) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                int count = cmd.ExecuteNonQuery();
                con.Close();
                disp_table();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                MessageBox.Show(count + " record inserted successfully!");
            }
        }

        private void disp_table()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employee";
            int count = cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        

        bool hint = true;
        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (hint)
            {
                textBox6.Clear();
                textBox6.ForeColor = Color.Black;
                hint = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!hint)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Employee where id='"+textBox6.Text+"'";
                int count = cmd.ExecuteNonQuery();
                con.Close();
                disp_table();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                MessageBox.Show(count + " record deleted successfully!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!hint)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee set FirstName='" + textBox1.Text + "',SecondName='" + textBox2.Text + "',LastName='" + textBox3.Text + "',DepartmentId='" + textBox4.Text + "',Telephone='" + textBox5.Text + "' where id='" + textBox6.Text + "'";
                int count = cmd.ExecuteNonQuery();
                con.Close();
                disp_table();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                MessageBox.Show(count + " record deleted successfully!");
            }
        }
    }
}
