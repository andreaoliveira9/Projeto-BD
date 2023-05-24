using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        private int totalItems;

        public Form1()
        {
            InitializeComponent();
            verifySGBDConnection();
            InitializeComboBox1();
            InitializeFiltroEquipa_JogadoresComboBox();
            InitializeComboBox2();

            updateListaJogadores();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            comboBox1.Items.Add("");
            comboBox1.Items.Add("Guard");
            comboBox1.Items.Add("Forward");
            comboBox1.Items.Add("Forward-Center");
            comboBox1.Items.Add("Guard-Forward");
            comboBox1.Items.Add("Center");
            comboBox1.Items.Add("Center-Forward");

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
            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;
            //MessageBox.Show(selectedEmployee);

            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void InitializeFiltroEquipa_JogadoresComboBox()
        {
            FiltroEquipa_Jogadores.DropDownStyle = ComboBoxStyle.DropDownList;
            FiltroEquipa_Jogadores.Items.Add("");
            FiltroEquipa_Jogadores.Items.Add("Los Angeles Lakers");
            FiltroEquipa_Jogadores.Items.Add("Boston Celtics");
            FiltroEquipa_Jogadores.Items.Add("Golden State Warriors");
            FiltroEquipa_Jogadores.Items.Add("New York Knicks");
            FiltroEquipa_Jogadores.Items.Add("Chicago Bulls");
            FiltroEquipa_Jogadores.Items.Add("Miami Heat");
            FiltroEquipa_Jogadores.Items.Add("Dallas Mavericks");
            FiltroEquipa_Jogadores.Items.Add("San Antonio Spurs");
            FiltroEquipa_Jogadores.Items.Add("Houston Rockets");
            FiltroEquipa_Jogadores.Items.Add("Portland Trail Blazers");

            // Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.FiltroEquipa_Jogadores.SelectedIndexChanged +=
                new System.EventHandler(FiltroEquipa_JogadoresComboBox_SelectedIndexChanged_1);
        }

        private void FiltroEquipa_JogadoresComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;

            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void InitializeComboBox2()
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add("");
            comboBox2.Items.Add("Sim");
            comboBox2.Items.Add("Nao");

            // Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.comboBox2.SelectedIndexChanged +=
                new System.EventHandler(comboBox2_SelectedIndexChanged_1);
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;

            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void Jogadores_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string playerSearch = (string)Search_Jogadores.Text;
            totalItems = 0;

            if (!string.IsNullOrEmpty(playerSearch))
            {
                comboBox2.Enabled = false;
                FiltroEquipa_Jogadores.Enabled = false;
                comboBox1.Enabled = false;
            } 

            Lista_Jogadores.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.pesquisarJogadoresPorNome(" + "'" + playerSearch + "'" + ")", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Player player = new Player();
                player.CCNumber = reader["CCNumber"].ToString();
                player.Name = reader["Name"].ToString();
                player.Age = reader["Age"].ToString();
                player.ContractID = reader["Contract_ID"].ToString();
                player.Number = reader["Number"].ToString();
                player.Height = reader["Height"].ToString();
                player.Weight = reader["Weight"].ToString();
                player.Position = reader["Position"].ToString();
                player.TeamID = reader["Team_ID"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            label23.Text = "Total de jogadores: " + totalItems.ToString();
            reader.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Search_Jogadores.Text = "";
            comboBox2.Enabled = true;
            FiltroEquipa_Jogadores.Enabled = true;
            comboBox1.Enabled = true;
            updateListaJogadores();
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

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Estatistica_Jogador_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateListaJogadores()
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
                player.CCNumber = reader["CCNumber"].ToString();
                player.Name = reader["Name"].ToString();
                player.Age = reader["Age"].ToString();
                player.ContractID = reader["Contract_ID"].ToString();
                player.Number = reader["Number"].ToString();
                player.Height = reader["Height"].ToString();
                player.Weight = reader["Weight"].ToString();
                player.Position = reader["Position"].ToString();
                player.TeamID = reader["Team_ID"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            label23.Text = "Total de jogadores: " + totalItems.ToString();
            reader.Close();
            cn.Close();

            this.Lista_Jogadores.SelectedIndexChanged +=
                new System.EventHandler(Lista_Jogadores_SelectedIndexChanged_1);

            if (!verifySGBDConnection())
            {
                return;
            }
        }

        private void Lista_Jogadores_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;

            Player selectedPlayer = (Player)Lista_Jogadores.SelectedItem;
            //MessageBox.Show(selectedPlayer.Name);
            if (selectedPlayer != null)
            {
                NumeroCC_Jogadores.Text = selectedPlayer.CCNumber;
                Name_Jogadores.Text = selectedPlayer.Name;
                Altura_Jogadores.Text = selectedPlayer.Height;
                Peso_Jogadores.Text = selectedPlayer.Weight;
                Posicao_Jogadores.Text = selectedPlayer.Position;
                Idade_Jogadores.Text = selectedPlayer.Age;
                IDEquipa_Jogadores.Text = selectedPlayer.TeamID;
                IDContrato_Jogadores.Text = selectedPlayer.ContractID;

                SqlCommand cmd = new SqlCommand("select * from NBA.[Contract] as C inner join NBA.Person as P on C.ID = P.Contract_ID where C.ID = " + selectedPlayer.ContractID, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contract contract = new Contract();
                    contract.ID = reader["ID"].ToString();
                    contract.Description = reader["Description"].ToString();
                    contract.Salary = reader["Salary"].ToString();
                    contract.StartDate = reader["Start_Date"].ToString();
                    contract.EndDate = reader["End_Date"].ToString();
                    Contrato_Jogador.Text = contract.ToString();
                }
                reader.Close();

                SqlCommand cmd1 = new SqlCommand("select * from NBA.GetPlayerStats(" + selectedPlayer.CCNumber + ")", cn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    PlayerStats playerStats = new PlayerStats();
                    playerStats.Points = reader1["Points"].ToString();
                    playerStats.Assists = reader1["Assists"].ToString();
                    playerStats.Rebounds = reader1["Rebounds"].ToString();
                    playerStats.Blocks = reader1["Blocks"].ToString();
                    playerStats.Steals = reader1["Steals"].ToString();
                    playerStats.FG = reader1["FG%"].ToString();
                    playerStats.PT3 = reader1["3PT%"].ToString();
                    Estatistica_Jogador.Text = playerStats.ToString();
                }
                reader1.Close();

                button9.Visible = true;
                button10.Visible = true;
            }
        }

        private void filtroJogadores(string equipa, string contrato, string posicao)
        {
            string query = "select * from NBA.filtrarJogadoresPorEquipaEContratoEPosicao(";
            if (string.IsNullOrEmpty(equipa))
            {
                query += "null,";
            }
            else
            {
                query += "'" + equipa + "',";
            }

            if (string.IsNullOrEmpty(contrato))
            {
                query += "null,";
            }
            else
            {
                query += "'" + contrato + "',";
            }

            if (string.IsNullOrEmpty(posicao))
            {
                query += "null)";
            }
            else
            {
                query += "'" + posicao + "')";
            }

            //Console.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            Lista_Jogadores.Items.Clear();
            totalItems = 0;
            while (reader.Read())
            {
                Player player = new Player();
                player.CCNumber = reader["CCNumber"].ToString();
                player.Name = reader["Name"].ToString();
                player.Age = reader["Age"].ToString();
                player.ContractID = reader["Contract_ID"].ToString();
                player.Number = reader["Number"].ToString();
                player.Height = reader["Height"].ToString();
                player.Weight = reader["Weight"].ToString();
                player.Position = reader["Position"].ToString();
                player.TeamID = reader["Team_ID"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            label23.Text = "Total de jogadores: " + totalItems.ToString();
            reader.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button11.Visible = true;
            button12.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button11.Visible = true;
            button12.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button11.Visible = true;
            button12.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            button12.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            button12.Visible = false;
        }
    }
}
