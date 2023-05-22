using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        SqlCommand command;
        private int totalItems;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            verifySGBDConnection();
            InitializeComboBox1();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p4g1; uid = p4g1; password = Andre_Duarte8");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void InitializeComboBox1()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Guard");
            comboBox1.Items.Add("Forward");
            comboBox1.Items.Add("Forward Center");
            comboBox1.Items.Add("Guard Forward");
            comboBox1.Items.Add("Center");
            comboBox1.Items.Add("Center Forward");

            // Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.comboBox1.SelectedIndexChanged +=
                new System.EventHandler(comboBox1_SelectedIndexChanged_1);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedEmployee = (string)comboBox1.SelectedItem;
            MessageBox.Show(selectedEmployee);
        }

        private void Jogadores_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_click(object sender, EventArgs e)
        {

        }

        private void NumeroCC_Jogadores_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Lista_Jogadores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void updateListJogadores()
        {
            totalItems = 0;
            if (!verifySGBDConnection())
            {
                return;
            }

            Lista_Jogadores.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.obterJogadores()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Player player = new Player();
                player.Nome = reader["Nome"].ToString();
                player.Id = reader["ID"].ToString();
                player.Email = reader["Email"].ToString();
                player.Telemovel = reader["Telemovel"].ToString();
                player.SSN = reader["SSN"].ToString();
                player.NIF = reader["NIF"].ToString();
                player.Morada = reader["Morada"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            total_items_label.Text = "Total de funcionários:";
            total_items_textbox.Text = totalItems.ToString();
            reader.Close();
            cn.Close();
            totalItems = 0;
            try
            {
                this.Func_list.SelectedIndexChanged -= new EventHandler(Prod_list_SelectedIndexChanged);
            }
            catch { }
            try
            {
                this.Func_list.SelectedIndexChanged -=
                    new System.EventHandler(Func_list_SelectedIndexChanged);
            }
            catch { }

            this.Func_list.SelectedIndexChanged +=
                new System.EventHandler(Func_list_SelectedIndexChanged);
            if (!verifySGBDConnection())
            {
                return;
            }
            SqlCommand cmd2 = new SqlCommand("select * from Mercado.obterFuncionariosAtuais()", cn);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            reader2.Read();
            while (reader2.Read())
            {
                totalItems++;
            }
            total_items_Atuais_label.Text = "Total de funcionários Atuais:";
            Func_atuais_textBox.Text = totalItems.ToString();

            reader2.Close();
            cn.Close();
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
