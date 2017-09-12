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

    public partial class Form2 : Form
    {
        public string mainSpecFirstName;
        public string mainSpecSecondName;
        public string mainSpecLastName;
        public string bossFirstName;
        public string bossSecondName;
        public string bossLastName;
        public string manageBossFirstName;
        public string manageBossSecondName;
        public string manageBossLastName;
        public string firstEmpFirstName;
        public string firstEmpSecondName;
        public string firstEmpLastName;
        public string secondEmpFirstName;
        public string secondEmpSecondName;
        public string secondEmpLastName;

        Form1 dbform;

        public Form2()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dbform = new Form1();
            dbform.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Рамирос\Documents\Visual Studio 2015\Projects\ProjectForm\MyProjectDB.mdf;Integrated Security=True;Connect Timeout=30");
            
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                    
                    winword.ShowAnimation = true;
                    
                    winword.Visible = true;
                    
                    object missing = System.Reflection.Missing.Value;
                    
                    object myfilename = "d:\\template.docx";
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(myfilename);

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    //Кому
                    cmd.CommandText = "select FirstName, SecondName, LastName from Employee where id ='" + textBox1.Text + "'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            mainSpecFirstName=reader[0].ToString();
                            mainSpecSecondName = reader[1].ToString();
                            mainSpecLastName = reader[2].ToString();
                        }
                    }
                    document.Tables[1].Cell(1, 2).Range.Text = "Главному специалисту \nотдела собственной безопасности "+ mainSpecSecondName+" "+ mainSpecFirstName+ " " + mainSpecLastName;
                    reader.Close();
                    //Начальник
                    cmd.CommandText = "select FirstName, SecondName, LastName from Employee where id ='" + textBox2.Text + "'";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            bossFirstName = reader[0].ToString();
                            bossSecondName = reader[1].ToString();
                            bossLastName = reader[2].ToString();
                        }
                    }
                    document.Tables[3].Cell(1, 1).Range.Text = "Начальник, " +bossSecondName + " " + bossFirstName + " " + bossLastName;
                    reader.Close();
                    //Начальник управления
                    cmd.CommandText = "select FirstName, SecondName, LastName from Employee where id ='" + textBox3.Text + "'";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            manageBossFirstName = reader[0].ToString();
                            manageBossSecondName = reader[1].ToString();
                            manageBossLastName = reader[2].ToString();
                        }
                    }
                    document.Tables[4].Cell(1, 1).Range.Text = "Начальник управления, " + manageBossSecondName + " " + manageBossFirstName + " " + manageBossLastName;
                    reader.Close();
                    //Сотрудник 1 
                    cmd.CommandText = "select FirstName, SecondName, LastName from Employee where id ='" + textBox4.Text + "'";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            firstEmpFirstName = reader[0].ToString();
                            firstEmpSecondName = reader[1].ToString();
                            firstEmpLastName = reader[2].ToString();
                        }
                    }
                    document.Tables[2].Cell(2, 3).Range.Text =  firstEmpSecondName + " " + firstEmpFirstName + " " + firstEmpLastName;
                    reader.Close();
                    //Сотрудник 2
                    cmd.CommandText = "select FirstName, SecondName, LastName from Employee where id ='" + textBox5.Text + "'";
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            secondEmpFirstName = reader[0].ToString();
                            secondEmpSecondName = reader[1].ToString();
                            secondEmpLastName = reader[2].ToString();
                        }
                    }
                    document.Tables[2].Cell(3, 3).Range.Text =  secondEmpSecondName + " " + secondEmpFirstName + " " + secondEmpLastName;
                    reader.Close();

                    object filename = saveFileDialog1.FileName;

                    //Save the document

                    //object filename = @"d:\temp1.docx";

                    
                    document.SaveAs2(ref filename);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    con.Close();
                    MessageBox.Show("Document created successfully !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            
        }
    }
}
