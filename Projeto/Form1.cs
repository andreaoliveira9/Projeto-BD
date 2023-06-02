using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Projeto
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;
        private int totalItems;
        private String comandoConfirmar;
        private String comandoConfirmarBilhetes;
        private String guardarNumber;
        private String guardarTeamID;
        private String guardarTeamID1;
        private String guardarCCNumber;
        private String guardarteamID2;
        private String guardarGameID;

        public Form1()
        {
            InitializeComponent();
            verifySGBDConnection();
            InitializeComboBox1();
            InitializeFiltroEquipa_JogadoresComboBox();
            InitializeComboBox2();
            InitializeComboBox6();
            InitializeComboBox7();
            InitializeComboBox5();
            InitializeComboBox3();
            InitializeTabela_Classificativa();
            InitializeComboBox4();
            InitializecomboBox8();
            InitializecomboBox9();

            updateListaJogadores();
            updateListaTreinadores();
            updateListaEquipas();
            updateListaJogos();
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

            this.comboBox1.SelectedIndexChanged +=
                new System.EventHandler(comboBox1_SelectedIndexChanged_1);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;

            clear("jogadores", "filtro");
            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void InitializeFiltroEquipa_JogadoresComboBox()
        {
            FiltroEquipa_Jogadores.Items.Clear();
            FiltroEquipa_Jogadores.DropDownStyle = ComboBoxStyle.DropDownList;
            FiltroEquipa_Jogadores.Items.Add("");

            SqlCommand cmd = new SqlCommand("select [Name] from NBA.Team", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FiltroEquipa_Jogadores.Items.Add(reader["Name"].ToString());
            }

            reader.Close();

            this.FiltroEquipa_Jogadores.SelectedIndexChanged +=
                new System.EventHandler(FiltroEquipa_JogadoresComboBox_SelectedIndexChanged_1);
        }

        private void FiltroEquipa_JogadoresComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;

            clear("jogadores", "filtro");
            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void InitializeComboBox2()
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add("");
            comboBox2.Items.Add("Sim");
            comboBox2.Items.Add("Nao");

            this.comboBox2.SelectedIndexChanged +=
                new System.EventHandler(comboBox2_SelectedIndexChanged_1);
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedTeam = (string)FiltroEquipa_Jogadores.SelectedItem;
            string selectedContract = (string)comboBox2.SelectedItem;
            string selectedPosition = (string)comboBox1.SelectedItem;

            clear("jogadores", "filtro");
            filtroJogadores(selectedTeam, selectedContract, selectedPosition);
        }

        private void InitializeComboBox6()
        {
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.Items.Add("");
            comboBox6.Items.Add("Sim");
            comboBox6.Items.Add("Nao");

            this.comboBox6.SelectedIndexChanged +=
                new System.EventHandler(comboBox6_SelectedIndexChanged_1);
        }

        private void comboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedContract = (string)comboBox6.SelectedItem;

            clear("treinadores", "filtro");
            filtroTreinadores(selectedContract);
        }

        private void InitializeComboBox7()
        {
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.Items.Add("");
            comboBox7.Items.Add("Western");
            comboBox7.Items.Add("Eastern");

            this.comboBox7.SelectedIndexChanged +=
                new System.EventHandler(comboBox7_SelectedIndexChanged_1);
        }

        private void comboBox7_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedConference = (string)comboBox7.SelectedItem;

            clear("equipas", "filtro");
            filtroEquipas(selectedConference);
        }

        private void InitializeComboBox5()
        {
            comboBox5.Items.Clear();
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.Items.Add("");

            SqlCommand cmd = new SqlCommand("select [Name] from NBA.Team", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox5.Items.Add(reader["Name"].ToString());
            }

            reader.Close();

            this.comboBox5.SelectedIndexChanged +=
                new System.EventHandler(comboBox5_SelectedIndexChanged_1);
        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedHomeTeam = (string)comboBox5.SelectedItem;
            string selectedAwayTeam = (string)comboBox3.SelectedItem;
            string selectedAconteceu = (string)comboBox4.SelectedItem;

            clear("jogos", "filtro");
            filtroJogos(selectedHomeTeam, selectedAwayTeam, selectedAconteceu);
        }

        private void InitializeComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.Items.Add("");

            SqlCommand cmd = new SqlCommand("select [Name] from NBA.Team", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader["Name"].ToString());
            }

            reader.Close();

            this.comboBox3.SelectedIndexChanged +=
                new System.EventHandler(comboBox3_SelectedIndexChanged_1);
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedHomeTeam = (string)comboBox5.SelectedItem;
            string selectedAwayTeam = (string)comboBox3.SelectedItem;
            string selectedAconteceu = (string)comboBox4.SelectedItem;

            clear("jogos", "filtro");
            filtroJogos(selectedHomeTeam, selectedAwayTeam, selectedAconteceu);
        }

        private void InitializeComboBox4()
        {
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.Items.Add("");
            comboBox4.Items.Add("Sim");
            comboBox4.Items.Add("Nao");

            this.comboBox4.SelectedIndexChanged +=
                new System.EventHandler(comboBox4_SelectedIndexChanged_1);
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedHomeTeam = (string)comboBox5.SelectedItem;
            string selectedAwayTeam = (string)comboBox3.SelectedItem;
            string selectedAconteceu = (string)comboBox4.SelectedItem;

            clear("jogos", "filtro");
            filtroJogos(selectedHomeTeam, selectedAwayTeam, selectedAconteceu);
        }

        private void InitializecomboBox8()
        {
            comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox8.Items.Add("Guard");
            comboBox8.Items.Add("Forward");
            comboBox8.Items.Add("Forward-Center");
            comboBox8.Items.Add("Guard-Forward");
            comboBox8.Items.Add("Center");
            comboBox8.Items.Add("Center-Forward");
        }

        private void InitializecomboBox9()
        {
            comboBox9.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox9.Items.Add("Eastern");
            comboBox9.Items.Add("Western");
        }

        private void InitializeTabela_Classificativa()
        {
            richTextBox1.Text = "";
            SqlCommand cmd = new SqlCommand("select * from NBA.GetTeamStandings() order by [Win%] desc", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            totalItems = 0;
            while (reader.Read())
            {
                Standing standing = new Standing();
                standing.TeamName = reader["Team_Name"].ToString();
                standing.GamesPlayed = reader["GamesPlayed"].ToString();
                standing.Wins = reader["Wins"].ToString();
                standing.Losses = reader["Losses"].ToString();
                standing.WinP = reader["Win%"].ToString();

                totalItems++;
                richTextBox1.Text += string.Format("Lugar {0,-5}  {1}\n", totalItems, standing);
            }
            reader.Close();
        }

        private void updateListaJogadores()
        {
            totalItems = 0;

            Lista_Jogadores.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.PersonPlayer", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Player player = new Player();
                player.CCNumber = reader["CCNumber"].ToString();
                player.Name = reader["Name"].ToString();
                player.Age = reader["Age"].ToString();
                player.Number = reader["Number"].ToString();
                player.ContractID = reader["Contract_ID"].ToString();
                player.Number = reader["Number"].ToString();
                player.Height = reader["Height"].ToString();
                player.Weight = reader["Weight"].ToString();
                player.Position = reader["Position"].ToString();
                player.TeamID = reader["Team_ID"].ToString();
                player.TeamName = reader["TeamName"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            label23.Text = "Total de jogadores: " + totalItems.ToString();
            reader.Close();

            this.Lista_Jogadores.SelectedIndexChanged +=
                new System.EventHandler(Lista_Jogadores_SelectedIndexChanged_1);
        }

        private void Lista_Jogadores_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;

            Player selectedPlayer = (Player)Lista_Jogadores.SelectedItem;
            if (selectedPlayer != null)
            {
                NumeroCC_Jogadores.Text = selectedPlayer.CCNumber;
                Name_Jogadores.Text = selectedPlayer.Name;
                Altura_Jogadores.Text = selectedPlayer.Height;
                Peso_Jogadores.Text = selectedPlayer.Weight;
                comboBox8.Text = selectedPlayer.Position;
                NumeroEquipamento_Jogadores.Text = selectedPlayer.Number;
                Idade_Jogadores.Text = selectedPlayer.Age;
                IDEquipa_Jogadores.Text = selectedPlayer.TeamID;
                IDContrato_Jogadores.Text = selectedPlayer.ContractID;
                textBox3.Text = selectedPlayer.TeamName;

                if (selectedPlayer.ContractID != "")
                {
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
                } 
                else
                {
                    Contrato_Jogador.Text = "Jogador sem contrato";
                }

                SqlCommand cmd1 = new SqlCommand("select * from NBA.GetPlayerStats(" + selectedPlayer.CCNumber + ")", cn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                PlayerStats playerStats = null;
                while (reader1.Read())
                {
                    playerStats = new PlayerStats();
                    playerStats.Points = reader1["Points"].ToString();
                    playerStats.Assists = reader1["Assists"].ToString();
                    playerStats.Rebounds = reader1["Rebounds"].ToString();
                    playerStats.Blocks = reader1["Blocks"].ToString();
                    playerStats.Steals = reader1["Steals"].ToString();
                    playerStats.FG = reader1["FG%"].ToString();
                    playerStats.PT3 = reader1["3PT%"].ToString();
                }
                textBox18.Text = playerStats.Points;
                textBox24.Text = playerStats.Assists;
                textBox26.Text = playerStats.Rebounds;
                textBox23.Text = playerStats.Blocks;
                textBox25.Text = playerStats.Steals;
                textBox22.Text = playerStats.FG;
                textBox19.Text = playerStats.PT3;
                if (playerStats.Points == "")
                {
                    Estatistica_Jogador.Text = "Sem estatística adicionada\nAltere o jogador para adicionar";
                }
                else
                {
                    Estatistica_Jogador.Text = playerStats.ToString();
                }
                reader1.Close();

                button9.Visible = true;
                button10.Visible = true;
            }
        }

        private void updateListaTreinadores()
        {
            totalItems = 0;

            Lista_Treinadores.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.PersonCoach", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Coach coach = new Coach();
                coach.CCNumber = reader["CCNumber"].ToString();
                coach.Name = reader["Name"].ToString();
                coach.Age = reader["Age"].ToString();
                coach.ContractID = reader["Contract_ID"].ToString();

                totalItems++;
                Lista_Treinadores.Items.Add(coach);

            }
            label2.Text = "Total de treinadores: " + totalItems.ToString();
            reader.Close();

            this.Lista_Treinadores.SelectedIndexChanged +=
                new System.EventHandler(Lista_Treinadores_SelectedIndexChanged);
        }

        private void Lista_Treinadores_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;

            Coach selectedCoach = (Coach)Lista_Treinadores.SelectedItem;
            if (selectedCoach != null)
            {
                textBox16.Text = selectedCoach.CCNumber;
                textBox20.Text = selectedCoach.Name;
                textBox14.Text = selectedCoach.Age;
                textBox1.Text = selectedCoach.ContractID;

                if (selectedCoach.ContractID != "") { 
                    SqlCommand cmd = new SqlCommand("select * from NBA.[Contract] as C inner join NBA.Person as P on C.ID = P.Contract_ID where C.ID = " + selectedCoach.ContractID, cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Contract contract = new Contract();
                        contract.ID = reader["ID"].ToString();
                        contract.Description = reader["Description"].ToString();
                        contract.Salary = reader["Salary"].ToString();
                        contract.StartDate = reader["Start_Date"].ToString();
                        contract.EndDate = reader["End_Date"].ToString();
                        richTextBox2.Text = contract.ToString();
                    }
                    reader.Close();
                } 
                else
                {
                    richTextBox2.Text = "";
                }

                button5.Visible = true;
                button6.Visible = true;
            }
        }

        private void updateListaEquipas()
        {
            totalItems = 0;
            Lista_Equipas.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.TeamCoachOwner", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Team team = new Team();
                team.ID = reader["ID"].ToString();
                team.TeamName = reader["Name"].ToString();
                team.City = reader["City"].ToString();
                team.Conference = reader["Conference"].ToString();
                team.FoundYear = reader["Found_Year"].ToString();
                team.CoachName = reader["CoachName"].ToString();
                team.OwnerName = reader["OwnerName"].ToString();
                team.CoachCCNumber = reader["CoachCCNumber"].ToString();
                team.OwnerCCNumber = reader["OwnerCCNumber"].ToString();

                totalItems++;
                Lista_Equipas.Items.Add(team);

            }
            label11.Text = "Total de equipas: " + totalItems.ToString();
            reader.Close();

            this.Lista_Equipas.SelectedIndexChanged +=
                new System.EventHandler(Lista_Equipas_SelectedIndexChanged);
        }

        private void Lista_Equipas_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;

            Team selectedTeam = (Team)Lista_Equipas.SelectedItem;
            if (selectedTeam != null)
            {
                textBox9.Text = selectedTeam.TeamName;
                textBox2.Text = selectedTeam.OwnerName;
                textBox4.Text = selectedTeam.CoachName;
                textBox8.Text = selectedTeam.City;
                comboBox9.Text = selectedTeam.Conference;
                textBox7.Text = selectedTeam.FoundYear;
                textBox28.Text = selectedTeam.CoachCCNumber;
                textBox27.Text = selectedTeam.OwnerCCNumber;
                guardarTeamID1 = selectedTeam.ID;

                Lista_Jogos_Equipa.Items.Clear();
                //Console.WriteLine("select * from NBA.GetTeamGames(" + "'" + selectedTeam.ID + "')");
                SqlCommand cmd = new SqlCommand("select * from NBA.GetTeamGames(" + "'" + selectedTeam.ID + "')", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Game game = new Game();
                    game.ID = reader["GameID"].ToString();
                    game.Time = reader["Time"].ToString();
                    game.Date = reader["Date"].ToString();
                    game.HomeScore = reader["Home_Score"].ToString();
                    game.AwayScore = reader["Away_Score"].ToString();
                    game.HomeID = reader["Home_Team_ID"].ToString();
                    game.AwayID = reader["Away_Team_ID"].ToString();
                    Lista_Jogos_Equipa.Items.Add(game.ToString());
                }
                reader.Close();

                SqlCommand cmd1 = new SqlCommand("select * from NBA.GetTeamAverageStats(" + selectedTeam.ID + ")", cn);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    TeamStats teamStats = new TeamStats();
                    teamStats.Points = reader["AveragePoints"].ToString();
                    teamStats.Assists = reader["AverageAssists"].ToString();
                    teamStats.Rebounds = reader["AverageRebounds"].ToString();
                    teamStats.Blocks = reader["AverageBlocks"].ToString();
                    teamStats.Steals = reader["AverageSteals"].ToString();
                    teamStats.FG = reader["AverageFGP"].ToString();
                    teamStats.PT3 = reader["Average3PTP"].ToString();
                    richTextBox3.Text = teamStats.ToString();
                }
                reader.Close();

                button15.Visible = true;
                button16.Visible = true;
            }
        }

        private void updateListaJogos()
        {
            totalItems = 0;

            Lista_Jogos.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from NBA.GamesTeamsStadium", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Game1 game = new Game1();
                game.ID = reader["ID"].ToString();
                game.Time = reader["Time"].ToString();
                game.Date = reader["Date"].ToString();
                game.HomeScore = reader["Home_Score"].ToString();
                game.AwayScore = reader["Away_Score"].ToString();
                game.HomeTeamName = reader["HomeTeamName"].ToString();
                game.AwayTeamName = reader["AwayTeamName"].ToString();
                game.HomeID = reader["HomeTeamID"].ToString();
                game.AwayID = reader["AwayTeamID"].ToString();
                game.StadiumName = reader["StadiumName"].ToString();
                game.StadiumID = reader["StadiumID"].ToString();

                Lista_Jogos.Items.Add(game);
                totalItems++;
            }
            label26.Text = "Total de jogos: " + totalItems.ToString();
            reader.Close();

            this.Lista_Jogos.SelectedIndexChanged +=
                new System.EventHandler(Lista_Jogos_SelectedIndexChanged);
        }

        private void Lista_Jogos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;
            Bilhetes_Jogo.Items.Clear();
            Game1 selectedGame = (Game1)Lista_Jogos.SelectedItem;

            textBox31.Text = "";
            textBox35.Text = "";
            textBox36.Text = "";
            textBox34.Text = "";
            textBox33.Text = "";
            textBox32.Text = "";

            if (selectedGame != null)
            {
                textBox15.Text = selectedGame.HomeTeamName;
                textBox6.Text = selectedGame.AwayTeamName;
                textBox21.Text = selectedGame.StadiumName;
                textBox29.Text = selectedGame.HomeID;
                textBox30.Text = selectedGame.AwayID;
                textBox5.Text = selectedGame.StadiumID;
                textBox12.Text = (selectedGame.Time).Substring(0,5);
                textBox13.Text = selectedGame.HomeScore;
                textBox11.Text = selectedGame.AwayScore;
                guardarGameID = selectedGame.ID;
                guardarteamID2 = selectedGame.HomeID;

                DateTime data = new DateTime(int.Parse(selectedGame.Date.Substring(6,4)), int.Parse(selectedGame.Date.Substring(3, 2)), int.Parse(selectedGame.Date.Substring(0, 2)));
                dateTimePicker1.Value = data;

                if (selectedGame.HomeScore != "")
                {
                    Bilhetes_Jogo.Items.Add("Bilhetes indisponíveis, pois jogo já aconteceu");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from NBA.GetGameTickets(" + "'" + selectedGame.ID + "')", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        Ticket ticket = new Ticket();
                        ticket.Type = reader["Type"].ToString();
                        ticket.Price = reader["Price"].ToString();
                        ticket.Restantes = reader["Restantes"].ToString();

                        Bilhetes_Jogo.Items.Add(ticket);
                        comandoConfirmarBilhetes = "alterar";

                        if (count == 1)
                        {
                            textBox31.Text = ticket.Type;
                            textBox35.Text = ticket.Price;
                            textBox36.Text = ticket.Restantes;
                        }
                        else
                        {
                            textBox34.Text = ticket.Type;
                            textBox33.Text = ticket.Price;
                            textBox32.Text = ticket.Restantes;
                        }

                        Console.WriteLine("oi");
                        button31.Visible = true;
                        button28.Visible = false;
                    }

                    if (count == 0)
                    {
                        Bilhetes_Jogo.Items.Add("Bilhetes por publicar");
                        comandoConfirmarBilhetes = "adicionar";
                        button31.Visible = false;
                        button28.Visible = true;
                    }

                    reader.Close();
                }

                button21.Visible = true;
                button22.Visible = true;
                button29.Visible = false;
                button30.Visible = false;
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
                player.TeamName = reader["TeamName"].ToString();

                totalItems++;
                Lista_Jogadores.Items.Add(player);

            }
            label23.Text = "Total de jogadores: " + totalItems.ToString();
            reader.Close();
        }

        private void filtroTreinadores(string contrato)
        {
            string query = "select * from NBA.filtrarTreinadoresPorContrato(";

            if (string.IsNullOrEmpty(contrato))
            {
                query += "null)";
            }
            else
            {
                query += "'" + contrato + "')";
            }

            //Console.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            Lista_Treinadores.Items.Clear();
            totalItems = 0;
            while (reader.Read())
            {
                Coach coach = new Coach();
                coach.CCNumber = reader["CCNumber"].ToString();
                coach.Name = reader["Name"].ToString();
                coach.Age = reader["Age"].ToString();
                coach.ContractID = reader["Contract_ID"].ToString();

                totalItems++;
                Lista_Treinadores.Items.Add(coach);

            }
            label2.Text = "Total de treinadores: " + totalItems.ToString();
            reader.Close();
        }

        private void filtroEquipas(string conferencia)
        {
            string query = "select * from NBA.filtrarEquipasPorConferencia(";

            if (string.IsNullOrEmpty(conferencia))
            {
                query += "null)";
            }
            else
            {
                query += "'" + conferencia + "')";
            }

            //Console.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            Lista_Equipas.Items.Clear();
            totalItems = 0;
            while (reader.Read())
            {
                Team team = new Team();
                team.ID = reader["ID"].ToString();
                team.TeamName = reader["Name"].ToString();
                team.City = reader["City"].ToString();
                team.Conference = reader["Conference"].ToString();
                team.FoundYear = reader["Found_Year"].ToString();
                team.CoachName = reader["CoachName"].ToString();
                team.OwnerName = reader["OwnerName"].ToString();
                team.CoachCCNumber = reader["CoachCCNumber"].ToString();
                team.OwnerCCNumber = reader["OwnerCCNumber"].ToString();

                totalItems++;
                Lista_Equipas.Items.Add(team);

            }
            label11.Text = "Total de equipass: " + totalItems.ToString();
            reader.Close();
        }

        private void filtroJogos(string casa, string fora, string aconteceu)
        {
            string query = "select * from NBA.filtrarJogosPorEquipaCasaEquipaForaESeAconteceu(";

            if (string.IsNullOrEmpty(casa))
            {
                query += "null,";
            }
            else
            {
                query += "'" + casa + "',";
            }

            if (string.IsNullOrEmpty(fora))
            {
                query += "null,";
            }
            else
            {
                query += "'" + fora + "',";
            }

            if (string.IsNullOrEmpty(aconteceu))
            {
                query += "null)";
            }
            else
            {
                query += "'" + aconteceu + "')";
            }

            //Console.WriteLine(query);
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            Lista_Jogos.Items.Clear();
            totalItems = 0;
            while (reader.Read())
            {
                Game1 game = new Game1();
                game.ID = reader["ID"].ToString();
                game.Time = reader["Time"].ToString();
                game.Date = reader["Date"].ToString();
                game.HomeScore = reader["Home_Score"].ToString();
                game.AwayScore = reader["Away_Score"].ToString();
                game.HomeTeamName = reader["HomeTeamName"].ToString();
                game.AwayTeamName = reader["AwayTeamName"].ToString();
                game.HomeID = reader["HomeTeamID"].ToString();
                game.AwayID = reader["AwayTeamID"].ToString();
                game.StadiumName = reader["StadiumName"].ToString();
                game.StadiumID = reader["StadiumID"].ToString();

                totalItems++;
                Lista_Jogos.Items.Add(game);

            }
            label26.Text = "Total de jogos: " + totalItems.ToString();
            reader.Close();
        }

        private void barraPesquisa(string nome, string tabela)
        {
            //Console.WriteLine("exec NBA.pesquisarPorNome @nome = " + "'" + nome + "'" + ", @esquema = 'NBA', @tabela = " + "'" + tabela + "'");
            SqlCommand cmd = new SqlCommand("NBA.pesquisarPorNome", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nome", nome));
            cmd.Parameters.Add(new SqlParameter("@esquema", "NBA"));
            cmd.Parameters.Add(new SqlParameter("@tabela", tabela));

            if (tabela == "PersonCoach")
            {
                totalItems = 0;

                if (!string.IsNullOrEmpty(nome))
                {
                    comboBox6.Enabled = false;
                    comboBox6.SelectedIndex = -1;
                }

                Lista_Treinadores.Items.Clear();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Coach coach = new Coach();
                    coach.CCNumber = reader["CCNumber"].ToString();
                    coach.Name = reader["Name"].ToString();
                    coach.Age = reader["Age"].ToString();
                    coach.ContractID = reader["Contract_ID"].ToString();

                    totalItems++;
                    Lista_Treinadores.Items.Add(coach);

                }
                label2.Text = "Total de jogadores: " + totalItems.ToString();
                reader.Close();
            } 
            else if (tabela == "PersonPlayer")
            {
                totalItems= 0;
                if (!string.IsNullOrEmpty(nome))
                {
                    comboBox2.Enabled = false;
                    FiltroEquipa_Jogadores.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.SelectedIndex = -1;
                    FiltroEquipa_Jogadores.SelectedIndex = -1;
                    comboBox1.SelectedIndex = -1;
                }

                Lista_Jogadores.Items.Clear();
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
                    player.TeamName = reader["TeamName"].ToString();

                    totalItems++;
                    Lista_Jogadores.Items.Add(player);

                }
                label23.Text = "Total de jogadores: " + totalItems.ToString();
                reader.Close();
            }
            else
            {
                totalItems = 0;

                if (!string.IsNullOrEmpty(nome))
                {
                    comboBox7.Enabled = false;
                    comboBox7.SelectedIndex = -1;
                }

                Lista_Equipas.Items.Clear();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Team team = new Team();
                    team.ID = reader["ID"].ToString();
                    team.TeamName = reader["Name"].ToString();
                    team.City = reader["City"].ToString();
                    team.Conference = reader["Conference"].ToString();
                    team.FoundYear = reader["Found_Year"].ToString();
                    team.CoachName = reader["CoachName"].ToString();
                    team.OwnerName = reader["OwnerName"].ToString();
                    team.CoachCCNumber = reader["CoachCCNumber"].ToString();
                    team.OwnerCCNumber = reader["OwnerCCNumber"].ToString();

                    totalItems++;
                    Lista_Equipas.Items.Add(team);

                }
                label11.Text = "Total de equipas: " + totalItems.ToString();
                reader.Close();
            }
        }

        // Adicionar/Alterar/Apagar jogador
        private void button11_Click(object sender, EventArgs e)
        {
            if (comandoConfirmar == "adicionar")
            {
                try
                {
                    String numeroCC = NumeroCC_Jogadores.Text;
                    String nome = Name_Jogadores.Text;
                    String altura = Altura_Jogadores.Text;
                    String numeroEquipamento = NumeroEquipamento_Jogadores.Text;
                    String peso = Peso_Jogadores.Text;
                    String posicao = comboBox8.Text;
                    String idade = Idade_Jogadores.Text;
                    String IDEquipa = IDEquipa_Jogadores.Text;
                    String IDContrato = IDContrato_Jogadores.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarJogador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", numeroCC));
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@Age", idade));
                    cmd.Parameters.Add(new SqlParameter("@Number", numeroEquipamento));
                    cmd.Parameters.Add(new SqlParameter("@Height", altura));
                    cmd.Parameters.Add(new SqlParameter("@Weight", peso));
                    cmd.Parameters.Add(new SqlParameter("@Position", posicao));
                    cmd.Parameters.Add(new SqlParameter("@Team_ID", IDEquipa));
                    if (IDContrato != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@Contract_ID", IDContrato));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));
                    cmd.Parameters.Add(new SqlParameter("@NumberOrTeamIDChanged", "Sim"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogador adicionado com sucesso!");
                    reader.Close();
                    clear("jogadores", "limpar");
                    resetJogadores();
                    Lista_Jogadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao adicionar jogador!");
                }
            }
            else if (comandoConfirmar == "apagar")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("NBA.apagarJogador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", NumeroCC_Jogadores.Text));
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogador apagado com sucesso!");
                    reader.Close();
                    clear("jogadores", "limpar");
                    resetJogadores();
                    Lista_Jogadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao apagar Jogador!");
                }
            }
            else if (comandoConfirmar == "alterar")
            {
                try
                {
                    String numeroCC = NumeroCC_Jogadores.Text;
                    String nome = Name_Jogadores.Text;
                    String altura = Altura_Jogadores.Text;
                    String numeroEquipamento = NumeroEquipamento_Jogadores.Text;
                    String peso = Peso_Jogadores.Text;
                    String posicao = comboBox8.Text;
                    String idade = Idade_Jogadores.Text;
                    String IDEquipa = IDEquipa_Jogadores.Text;
                    String IDContrato = IDContrato_Jogadores.Text;
                    String points = textBox18.Text.Replace(",", ".");
                    String assists = textBox24.Text.Replace(",", ".");
                    String rebounds = textBox26.Text.Replace(",", ".");
                    String blocks = textBox23.Text.Replace(",", ".");
                    String steals = textBox25.Text.Replace(",", ".");
                    String fg = textBox22.Text.Replace(",", ".");
                    String pt3 = textBox19.Text.Replace(",", ".");

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarJogador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", numeroCC));
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@Age", idade));
                    cmd.Parameters.Add(new SqlParameter("@Number", numeroEquipamento));
                    cmd.Parameters.Add(new SqlParameter("@Height", altura));
                    cmd.Parameters.Add(new SqlParameter("@Weight", peso));
                    cmd.Parameters.Add(new SqlParameter("@Position", posicao));
                    cmd.Parameters.Add(new SqlParameter("@Team_ID", IDEquipa));
                    if (IDContrato != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@Contract_ID", IDContrato));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));
                    if (numeroEquipamento != guardarNumber || IDEquipa != guardarTeamID)
                    {
                        cmd.Parameters.Add(new SqlParameter("@NumberOrTeamIDChanged", "Sim"));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@NumberOrTeamIDChanged", "Nao"));
                    }

                    if (points != "" || assists != "" || rebounds != "" || blocks != "" || steals != "" || fg != "" || pt3 != "")
                    {
                        Console.WriteLine("oi");
                        cmd.Parameters.Add(new SqlParameter("@Points", points));
                        cmd.Parameters.Add(new SqlParameter("@Assists", assists));
                        cmd.Parameters.Add(new SqlParameter("@Rebounds", rebounds));
                        cmd.Parameters.Add(new SqlParameter("@Blocks", blocks));
                        cmd.Parameters.Add(new SqlParameter("@Steals", steals));
                        cmd.Parameters.Add(new SqlParameter("@FG", fg));
                        cmd.Parameters.Add(new SqlParameter("@PT3", pt3));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogador alterado com sucesso!");
                    reader.Close();
                    clear("jogadores", "limpar");
                    resetJogadores();
                    Lista_Jogadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar Jogador!");
                }
            }
        }

        // Adicionar/Alterar/Apagar treinador
        private void button4_Click(object sender, EventArgs e)
        {
            if (comandoConfirmar == "adicionar")
            {
                try
                {
                    String numeroCC = textBox16.Text;
                    String nome = textBox20.Text;
                    String idade = textBox14.Text;
                    String IDContrato = textBox1.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarTreinador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", numeroCC));
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@Age", idade));
                    if (IDContrato != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@Contract_ID", IDContrato));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Treinador adicionado com sucesso!");
                    reader.Close();
                    clear("treinadores", "limpar");
                    updateListaTreinadores();
                    updateListaEquipas();
                    Lista_Treinadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao adicionar treinador!");
                }
            }
            else if (comandoConfirmar == "apagar")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("NBA.apagarTreinador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", textBox16.Text));
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Treinador apagado com sucesso!");
                    reader.Close();
                    clear("treinadores", "limpar");
                    updateListaTreinadores();
                    updateListaEquipas();
                    Lista_Treinadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao apagar treinador!");
                }
            }
            else if (comandoConfirmar == "alterar")
            {
                try
                {
                    String numeroCC = textBox16.Text;
                    String nome = textBox20.Text;
                    String idade = textBox14.Text;
                    String IDContrato = textBox1.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarTreinador", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CCNumber", numeroCC));
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@Age", idade));
                    if (IDContrato != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@Contract_ID", IDContrato));
                    }
                    cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Treinador alterado com sucesso!");
                    reader.Close();
                    clear("treinadores", "limpar");
                    updateListaTreinadores();
                    updateListaEquipas();
                    Lista_Treinadores.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar treinador!");
                }
            }
        }

        // Adicionar/Alterar/Apagar equipa
        private void button14_Click(object sender, EventArgs e)
        {
            if (comandoConfirmar == "adicionar")
            {
                try
                {
                    String nome = textBox9.Text;
                    String cidade = textBox8.Text;
                    String conferencia = comboBox9.Text;
                    String foundYear = textBox7.Text;
                    String coachCCNumber = textBox28.Text;
                    String ownerCCNumber = textBox27.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarEquipa", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@City", cidade));
                    cmd.Parameters.Add(new SqlParameter("@Conference", conferencia));
                    cmd.Parameters.Add(new SqlParameter("@FoundYear", foundYear));
                    cmd.Parameters.Add(new SqlParameter("@CoachCCNumber", coachCCNumber));
                    cmd.Parameters.Add(new SqlParameter("@OwnerCCNumber", ownerCCNumber));
                    cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));
                    cmd.Parameters.Add(new SqlParameter("@CoachChanged", "Sim"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Equipa adicionada com sucesso!");
                    reader.Close();
                    clear("equipas", "limpar");
                    resetEquipas();
                    Lista_Equipas.Enabled = true;
                    InitializeFiltroEquipa_JogadoresComboBox();
                    InitializeComboBox5();
                    InitializeComboBox3();
                    updateListaJogos();
                    updateListaTreinadores();
                    InitializeTabela_Classificativa();
                }
                catch
                {
                    MessageBox.Show("Erro ao adicionar equipa!");
                }
            }
            else if (comandoConfirmar == "apagar")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("NBA.apagarEquipa", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", guardarTeamID1));
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Equipa apagada com sucesso!");
                    reader.Close();
                    clear("equipas", "limpar");
                    resetEquipas();
                    Lista_Equipas.Enabled = true;
                    InitializeFiltroEquipa_JogadoresComboBox();
                    InitializeComboBox5();
                    InitializeComboBox3();
                    InitializeTabela_Classificativa();
                    updateListaJogos();
                    updateListaTreinadores();
                }
                catch
                {
                    MessageBox.Show("Erro ao apagar equipa!");
                }
            }
            else if (comandoConfirmar == "alterar")
            {
                try
                {
                    String nome = textBox9.Text;
                    String cidade = textBox8.Text;
                    String conferencia = comboBox9.Text;
                    String foundYear = textBox7.Text;
                    String coachCCNumber = textBox28.Text;
                    String ownerCCNumber = textBox27.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarEquipa", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", guardarTeamID1));
                    cmd.Parameters.Add(new SqlParameter("@Name", nome));
                    cmd.Parameters.Add(new SqlParameter("@City", cidade));
                    cmd.Parameters.Add(new SqlParameter("@Conference", conferencia));
                    cmd.Parameters.Add(new SqlParameter("@FoundYear", foundYear));
                    cmd.Parameters.Add(new SqlParameter("@CoachCCNumber", coachCCNumber));
                    cmd.Parameters.Add(new SqlParameter("@OwnerCCNumber", ownerCCNumber));
                    cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));
                    if (guardarCCNumber == coachCCNumber)
                    {
                        Console.WriteLine("oi1");
                        cmd.Parameters.Add(new SqlParameter("@CoachChanged", "Nao"));
                    }
                    else
                    {
                        Console.WriteLine("oi2");
                        cmd.Parameters.Add(new SqlParameter("@CoachChanged", "Sim"));
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Equipa alterada com sucesso!");
                    reader.Close();
                    clear("equipas", "limpar");
                    resetEquipas();
                    Lista_Equipas.Enabled = true;
                    InitializeFiltroEquipa_JogadoresComboBox();
                    InitializeComboBox5();
                    InitializeComboBox3();
                    updateListaJogos();
                    updateListaTreinadores();
                    InitializeTabela_Classificativa();
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar equipa!");
                }
            }
        }

        // Adicionar/Alterar/Apagar jogo
        private void button20_Click(object sender, EventArgs e)
        {
            if (comandoConfirmar == "adicionar")
            {
                try
                {
                    String time = textBox12.Text;
                    String date = dateTimePicker1.Text;
                    String date1 = date.Substring(6, 4) + "/" + date.Substring(3, 2) + "/" + date.Substring(0, 2);
                    String homeScore = textBox13.Text;
                    String awayScore = textBox11.Text;
                    String homeTeamID = textBox29.Text;
                    String awayTeamID = textBox30.Text;
                    String stadiumID = textBox5.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarJogo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Time", time));
                    cmd.Parameters.Add(new SqlParameter("@Date", date1));
                    if (homeScore != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@HomeScore", homeScore));
                    }
                    if (awayScore != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@AwayScore", awayScore));
                    }
                    cmd.Parameters.Add(new SqlParameter("@HomeTeamID", homeTeamID));
                    cmd.Parameters.Add(new SqlParameter("@AwayTeamID", awayTeamID));
                    cmd.Parameters.Add(new SqlParameter("@StadiumID", stadiumID));
                    cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogo adicionado com sucesso!");
                    reader.Close();
                    clear("jogos", "limpar");
                    resetJogos();
                    resetTabelaClassificativa();
                    Lista_Jogos.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao adicionar jogo!");
                }
            }
            else if (comandoConfirmar == "apagar")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("NBA.apagarJogo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", guardarGameID));
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogo apagado com sucesso!");
                    reader.Close();
                    resetTabelaClassificativa();
                    clear("jogos", "limpar");
                    resetJogos();
                    resetTabelaClassificativa();
                    Lista_Jogos.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao apagar jogo!");
                }
            }
            else if (comandoConfirmar == "alterar")
            {
                try
                {
                    String time = textBox12.Text;
                    String date = dateTimePicker1.Text;
                    String date1 = date.Substring(6, 4) + "/" + date.Substring(3, 2) + "/" + date.Substring(0, 2);
                    String homeScore = textBox13.Text;
                    String awayScore = textBox11.Text;
                    String homeTeamID = textBox29.Text;
                    String awayTeamID = textBox30.Text;
                    String stadiumID = textBox5.Text;

                    SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarJogo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", guardarGameID));
                    cmd.Parameters.Add(new SqlParameter("@Time", time));
                    cmd.Parameters.Add(new SqlParameter("@Date", date1));
                    if (homeScore != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@HomeScore", homeScore));
                    }
                    if (awayScore != "")
                    {
                        cmd.Parameters.Add(new SqlParameter("@AwayScore", awayScore));
                    }
                    Console.WriteLine("\n");
                    cmd.Parameters.Add(new SqlParameter("@HomeTeamID", homeTeamID));
                    cmd.Parameters.Add(new SqlParameter("@AwayTeamID", awayTeamID));
                    cmd.Parameters.Add(new SqlParameter("@StadiumID", stadiumID));
                    cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Jogo alterado com sucesso!");
                    reader.Close();
                    clear("jogos", "limpar");
                    resetJogos();
                    resetTabelaClassificativa();
                    Lista_Jogos.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar jogo!");
                }
            }
        }

        // Botão adicionar/alterar bilhetes
        private void button29_Click(object sender, EventArgs e)
        {
            String tipo1 = textBox31.Text;
            String preco1 = textBox35.Text.Replace(",", ".");
            String restantes1 = textBox36.Text;
            String tipo2 = textBox34.Text;
            String preco2 = textBox33.Text.Replace(",", ".");
            String restantes2 = textBox32.Text;

            if (comandoConfirmarBilhetes == "adicionar")
            {
                try
                {
                    if (tipo1 != "" && preco1 != "" && restantes1 != "")
                    {
                        SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarBilhetes", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Type", tipo1));
                        cmd.Parameters.Add(new SqlParameter("@Price", preco1));
                        cmd.Parameters.Add(new SqlParameter("@Restantes", restantes1));
                        cmd.Parameters.Add(new SqlParameter("@Game_ID", guardarGameID));
                        cmd.Parameters.Add(new SqlParameter("@Team_ID", guardarteamID2));
                        cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }

                    if (tipo2 != "" && preco2 != "" && restantes2 != "")
                    {
                        SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarBilhetes", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Type", tipo2));
                        cmd.Parameters.Add(new SqlParameter("@Price", preco2));
                        cmd.Parameters.Add(new SqlParameter("@Restantes", restantes2));
                        cmd.Parameters.Add(new SqlParameter("@Game_ID", guardarGameID));
                        cmd.Parameters.Add(new SqlParameter("@Team_ID", guardarteamID2));
                        cmd.Parameters.Add(new SqlParameter("@Command", "adicionar"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }

                    MessageBox.Show("Bilhetes adicionados com sucesso!");
                    clear("jogos", "limpar");
                    resetJogos();
                    resetTabelaClassificativa();
                    Lista_Jogos.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao publicar bilhetes!");
                }
            }
            else if (comandoConfirmarBilhetes == "alterar")
            {
                try
                {

                    if (tipo1 != "" && preco1 != "" && restantes1 != "")
                    {
                        SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarBilhetes", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Type", tipo1));
                        cmd.Parameters.Add(new SqlParameter("@Price", preco1));
                        cmd.Parameters.Add(new SqlParameter("@Restantes", restantes1));
                        cmd.Parameters.Add(new SqlParameter("@Game_ID", guardarGameID));
                        cmd.Parameters.Add(new SqlParameter("@Team_ID", guardarteamID2));
                        cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }

                    if (tipo2 != "" && preco2 != "" && restantes2 != "")
                    {
                        SqlCommand cmd = new SqlCommand("NBA.adicionarAlterarBilhetes", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Type", tipo2));
                        cmd.Parameters.Add(new SqlParameter("@Price", preco2));
                        cmd.Parameters.Add(new SqlParameter("@Restantes", restantes2));
                        cmd.Parameters.Add(new SqlParameter("@Game_ID", guardarGameID));
                        cmd.Parameters.Add(new SqlParameter("@Team_ID", guardarteamID2));
                        cmd.Parameters.Add(new SqlParameter("@Command", "alterar"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();
                    }

                    MessageBox.Show("Bilhetes altearados com sucesso!");
                    resetTabelaClassificativa();
                    clear("jogos", "limpar");
                    resetJogos();
                    resetTabelaClassificativa();
                    Lista_Jogos.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar bilhetes!");
                }
            }
        }

        // Botão Adicionar Jogadores
        private void button8_Click(object sender, EventArgs e)
        {
            clear("jogadores", "adicionar");
            updateListaJogadores();

            button11.Visible = true;
            button12.Visible = true;

            Lista_Jogadores.Enabled = false;

            comandoConfirmar = "adicionar";

            // Searchbar
            label21.Visible = false;
            Search_Jogadores.Visible = false;
            button1.Visible = false;
            button24.Visible = false;
            // Filtros
            label22.Visible = false;
            comboBox2.Visible = false;
            label7.Visible = false;
            FiltroEquipa_Jogadores.Visible = false;
            FiltroPosicao_Jogadores.Visible = false;
            comboBox1.Visible = false;
            label21.Visible = false;
            label21.Visible = false;
            // Estatistica
            label19.Visible = false;
            Estatistica_Jogador.Visible = false;
            // Campos preenchimento
            NumeroCC_Jogadores.Enabled = true;
            NumeroCC_Jogadores.BackColor = Color.White;
            Name_Jogadores.Enabled = true;
            Name_Jogadores.BackColor = Color.White;
            Altura_Jogadores.Enabled = true;
            Altura_Jogadores.BackColor = Color.White;
            NumeroEquipamento_Jogadores.Enabled = true;
            NumeroEquipamento_Jogadores.BackColor = Color.White;
            Peso_Jogadores.Enabled = true;
            Peso_Jogadores.BackColor = Color.White;
            comboBox8.Enabled = true;
            comboBox8.BackColor = Color.White;
            Idade_Jogadores.Enabled = true;
            Idade_Jogadores.BackColor = Color.White;
            IDEquipa_Jogadores.Enabled = true;
            IDEquipa_Jogadores.BackColor = Color.White;
            IDContrato_Jogadores.Enabled = true;
            IDContrato_Jogadores.BackColor = Color.White;

            this.IDEquipa_Jogadores.TextChanged +=
                new System.EventHandler(IDEquipa_Jogadores_TextChanged_1);

            this.IDContrato_Jogadores.TextChanged +=
                new System.EventHandler(IDContrato_Jogadores_TextChanged_1);
        }

        private void IDEquipa_Jogadores_TextChanged_1(object sender, EventArgs e)
        {
            String selectedTeamID = IDEquipa_Jogadores.Text;

            int count = 0;
            if (selectedTeamID != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Team where ID = " + "'" + selectedTeamID + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox3.Text = reader["Name"].ToString();
                }
                reader.Close();
            }

            if (selectedTeamID == "" || count == 0)
            {
                textBox3.Text = "Equipa inexistente";
            }
        }

        private void IDContrato_Jogadores_TextChanged_1(object sender, EventArgs e)
        {
            String selectedContractID = IDContrato_Jogadores.Text;

            int count = 0;
            if (selectedContractID != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.[Contract] where ID = '" + selectedContractID + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    Contract contract = new Contract();
                    contract.ID = reader["ID"].ToString();
                    contract.Description = reader["Description"].ToString();
                    contract.Salary = reader["Salary"].ToString();
                    contract.StartDate = reader["Start_Date"].ToString();
                    contract.EndDate = reader["End_Date"].ToString();
                    Contrato_Jogador.Text = contract.ToString();
                }
                reader.Close();
            }

            if (selectedContractID == "" || count == 0)
            {
                Contrato_Jogador.Text = "Contrato inexistente";
            }
        }

        // Botão Adicionar Treinadores
        private void button7_Click(object sender, EventArgs e)
        {
            clear("treinadores", "adicionar");
            updateListaTreinadores();

            button2.Visible = true;
            button4.Visible = true;

            Lista_Treinadores.Enabled = false;

            comandoConfirmar = "adicionar";

            // Searchbar
            label18.Visible = false;
            textBox17.Visible = false;
            button13.Visible = false;
            button25.Visible = false;
            // Filtros
            label41.Visible = false;
            comboBox6.Visible = false;
            // Campos preenchimento
            textBox16.Enabled = true;
            textBox16.BackColor = Color.White;
            textBox20.Enabled = true;
            textBox20.BackColor = Color.White;
            textBox14.Enabled = true;
            textBox14.BackColor = Color.White;
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White;

            this.textBox1.TextChanged +=
                new System.EventHandler(textBox1_TextChanged_1);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            String selectedContractID = textBox1.Text;

            int count = 0;
            if (selectedContractID != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.[Contract] where ID = '" + selectedContractID + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    Contract contract = new Contract();
                    contract.ID = reader["ID"].ToString();
                    contract.Description = reader["Description"].ToString();
                    contract.Salary = reader["Salary"].ToString();
                    contract.StartDate = reader["Start_Date"].ToString();
                    contract.EndDate = reader["End_Date"].ToString();
                    richTextBox2.Text = contract.ToString();
                }
                reader.Close();
            }

            if (selectedContractID == "" || count == 0)
            {
                richTextBox2.Text = "Contrato inexistente";
            }
        }

        //Botão Adicionar Equipas
        private void button17_Click(object sender, EventArgs e)
        {
            clear("equipas", "adicionar");
            updateListaEquipas();

            button14.Visible = true;
            button3.Visible = true;

            Lista_Equipas.Enabled = false;

            comandoConfirmar = "adicionar";

            // Searchbar
            label13.Visible = false;
            textBox10.Visible = false;
            button18.Visible = false;
            button26.Visible = false;
            // Filtros
            label43.Visible = false;
            comboBox7.Visible = false;
            // Jogos Equipa
            label47.Visible = false;
            Lista_Jogos_Equipa.Visible = false;
            // Estatistica
            label9.Visible = false;
            richTextBox3.Visible = false;
            // Campos preenchimento
            textBox9.Enabled = true;
            textBox9.BackColor = Color.White;
            textBox7.Enabled = true;
            textBox7.BackColor = Color.White;
            textBox28.Enabled = true;
            textBox28.BackColor = Color.White;
            textBox27.Enabled = true;
            textBox27.BackColor = Color.White;
            textBox8.Enabled = true;
            textBox8.BackColor = Color.White;
            comboBox9.Enabled = true;
            comboBox9.BackColor = Color.White;

            this.textBox28.TextChanged +=
                new System.EventHandler(textBox28_TextChanged_1);

            this.textBox27.TextChanged +=
                new System.EventHandler(textBox27_TextChanged_1);
        }

        private void textBox28_TextChanged_1(object sender, EventArgs e)
        {
            String selectedCoachCCNumber = textBox28.Text;

            int count = 0;
            if (selectedCoachCCNumber != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Person where CCNumber = " + "'" + selectedCoachCCNumber + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox4.Text = reader["Name"].ToString();
                }
                reader.Close();
            }

            if (selectedCoachCCNumber == "" || count == 0)
            {
                textBox4.Text = "Treinador inexistente";
            }
        }

        private void textBox27_TextChanged_1(object sender, EventArgs e)
        {
            String selectedOwnerCCNumber = textBox27.Text;

            int count = 0;
            if (selectedOwnerCCNumber != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Person where CCNumber = " + "'" + selectedOwnerCCNumber + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox2.Text = reader["Name"].ToString();
                }
                reader.Close();
            }

            if (selectedOwnerCCNumber == "" || count == 0)
            {
                textBox2.Text = "Presidente inexistente";
            }
        }

        // Botão Adicionar Jogos
        private void button23_Click(object sender, EventArgs e)
        {
            clear("jogos", "adicionar");
            updateListaJogos();

            button19.Visible = true;
            button20.Visible = true;

            Lista_Jogos.Enabled = false;

            comandoConfirmar = "adicionar";

            // Limpar
            button27.Visible = false;
            // Filtros
            label29.Visible = false;
            comboBox5.Visible = false;
            label36.Visible = false;
            comboBox3.Visible = false;
            label50.Visible = false;
            comboBox4.Visible = false;
            // Bilhetes Jogo
            label49.Visible = false;
            Bilhetes_Jogo.Visible = false;
            // Campos preenchimento
            textBox29.Enabled = true;
            textBox29.BackColor = Color.White;
            textBox30.Enabled = true;
            textBox30.BackColor = Color.White;
            textBox5.Enabled = true;
            textBox5.BackColor = Color.White;
            textBox12.Enabled = true;
            textBox12.BackColor = Color.White;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.BackColor = Color.White;
            textBox13.Enabled = true;
            textBox13.BackColor = Color.White;
            textBox11.Enabled = true;
            textBox11.BackColor = Color.White;

            this.textBox29.TextChanged +=
                new System.EventHandler(textBox29_TextChanged_1);

            this.textBox30.TextChanged +=
                new System.EventHandler(textBox30_TextChanged_1);

            this.textBox5.TextChanged +=
                new System.EventHandler(textBox5_TextChanged_1);
        }

        private void textBox29_TextChanged_1(object sender, EventArgs e)
        {
            String selectedHomeTeam = textBox29.Text;

            int count = 0;
            if (selectedHomeTeam != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Team where ID = " + "'" + selectedHomeTeam + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox15.Text = reader["Name"].ToString();
                }
                reader.Close();
            }
            
            if (selectedHomeTeam == "" || count == 0)
            {
                textBox15.Text = "Equipa inexistente";
            }
        }

        private void textBox30_TextChanged_1(object sender, EventArgs e)
        {
            String selectedAwayTeam = textBox30.Text;

            int count = 0;
            if (selectedAwayTeam != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Team where ID = " + "'" + selectedAwayTeam + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox6.Text = reader["Name"].ToString();
                }
                reader.Close();
            }

            if (selectedAwayTeam == "" || count == 0)
            {
                textBox6.Text = "Equipa inexistente";
            }
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            String selectedStadium = textBox5.Text;

            int count = 0;
            if (selectedStadium != "")
            {
                SqlCommand cmd = new SqlCommand("select * from NBA.Stadium where ID = " + "'" + selectedStadium + "'", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    textBox21.Text = reader["Name"].ToString();
                }
                reader.Close();
            }

            if (selectedStadium == "" || count == 0)
            {
                textBox21.Text = "Arena inexistente";
            }
        }

        // Botão publicar bilhetes
        private void button28_Click(object sender, EventArgs e)
        {
            button29.Visible = true;
            button30.Visible = true;
            Bilhetes_Jogo.Visible = false;
            label65.Visible = true;
            label66.Visible = true;
            label67.Visible = true;

            textBox31.Visible = true;
            textBox32.Visible = true;
            textBox33.Visible = true;
            textBox34.Visible = true;
            textBox35.Visible = true;
            textBox36.Visible = true;

            textBox31.Enabled = true;
            textBox32.Enabled = true;
            textBox33.Enabled = true;
            textBox34.Enabled = true;
            textBox35.Enabled = true;
            textBox36.Enabled = true;

            textBox31.BackColor = Color.White;
            textBox32.BackColor = Color.White;
            textBox33.BackColor = Color.White;
            textBox34.BackColor = Color.White;
            textBox35.BackColor = Color.White;
            textBox36.BackColor = Color.White;
        }

        // Botão Alterar Jogadores
        private void button10_Click(object sender, EventArgs e)
        {
            button11.Visible = true;
            button12.Visible = true;

            Lista_Jogadores.Enabled = false;

            guardarNumber = NumeroEquipamento_Jogadores.Text;
            guardarTeamID = IDEquipa_Jogadores.Text;

            comandoConfirmar = "alterar";

            // Searchbar
            label21.Visible = false;
            Search_Jogadores.Visible = false;
            button1.Visible = false;
            button24.Visible = false;
            // Filtros
            label22.Visible = false;
            comboBox2.Visible = false;
            label7.Visible = false;
            FiltroEquipa_Jogadores.Visible = false;
            FiltroPosicao_Jogadores.Visible = false;
            comboBox1.Visible = false;
            label21.Visible = false;
            label21.Visible = false;
            // Estatistica
            Estatistica_Jogador.Visible = false;
            label53.Visible = true;
            label54.Visible = true;
            label55.Visible = true;
            label56.Visible = true;
            label57.Visible = true;
            label58.Visible = true;
            label59.Visible = true;
            textBox18.Visible = true;
            textBox24.Visible = true;
            textBox26.Visible = true;
            textBox23.Visible = true;
            textBox25.Visible = true;
            textBox22.Visible = true;
            textBox19.Visible = true;
            // Campos preenchimento
            Name_Jogadores.Enabled = true;
            Name_Jogadores.BackColor = Color.White;
            Altura_Jogadores.Enabled = true;
            Altura_Jogadores.BackColor = Color.White;
            NumeroEquipamento_Jogadores.Enabled = true;
            NumeroEquipamento_Jogadores.BackColor = Color.White;
            Peso_Jogadores.Enabled = true;
            Peso_Jogadores.BackColor = Color.White;
            comboBox8.Enabled = true;
            comboBox8.BackColor = Color.White;
            Idade_Jogadores.Enabled = true;
            Idade_Jogadores.BackColor = Color.White;
            IDEquipa_Jogadores.Enabled = true;
            IDEquipa_Jogadores.BackColor = Color.White;
            IDContrato_Jogadores.Enabled = true;
            IDContrato_Jogadores.BackColor = Color.White;

            this.IDEquipa_Jogadores.TextChanged +=
                new System.EventHandler(IDEquipa_Jogadores_TextChanged_1);

            this.IDContrato_Jogadores.TextChanged +=
                new System.EventHandler(IDContrato_Jogadores_TextChanged_1);

            clear("jogadores", "alterar");
        }

        // Botão Alterar Treinadores
        private void button5_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = true;

            Lista_Treinadores.Enabled = false;

            comandoConfirmar = "alterar";

            // Searchbar
            label18.Visible = false;
            textBox17.Visible = false;
            button13.Visible = false;
            button25.Visible = false;
            // Filtros
            label41.Visible = false;
            comboBox6.Visible = false;
            // Campos preenchimento
            textBox20.Enabled = true;
            textBox20.BackColor = Color.White;
            textBox14.Enabled = true;
            textBox14.BackColor = Color.White;
            textBox1.Enabled = true;
            textBox1.BackColor = Color.White;

            this.textBox1.TextChanged +=
                new System.EventHandler(textBox1_TextChanged_1);

            clear("treinadores", "alterar");
        }

        // Botão Alterar Equipas
        private void button15_Click(object sender, EventArgs e)
        {
            button14.Visible = true;
            button3.Visible = true;

            Lista_Equipas.Enabled = false;

            guardarCCNumber = textBox28.Text;

            comandoConfirmar = "alterar";

            // Searchbar
            label13.Visible = false;
            textBox10.Visible = false;
            button18.Visible = false;
            button26.Visible = false;
            // Filtros
            label43.Visible = false;
            comboBox7.Visible = false;
            // Jogos Equipa
            label47.Visible = false;
            Lista_Jogos_Equipa.Visible = false;
            // Estatistica
            label9.Visible = false;
            richTextBox3.Visible = false;
            // Campos preenchimento
            textBox9.Enabled = true;
            textBox9.BackColor = Color.White;
            textBox7.Enabled = true;
            textBox7.BackColor = Color.White;
            textBox28.Enabled = true;
            textBox28.BackColor = Color.White;
            textBox27.Enabled = true;
            textBox27.BackColor = Color.White;
            textBox8.Enabled = true;
            textBox8.BackColor = Color.White;
            comboBox9.Enabled = true;
            comboBox9.BackColor = Color.White;

            this.textBox28.TextChanged +=
                new System.EventHandler(textBox28_TextChanged_1);

            this.textBox27.TextChanged +=
                new System.EventHandler(textBox27_TextChanged_1);

            clear("equipas", "alterar");
        }

        // Botão Alterar Jogos
        private void button21_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            button20.Visible = true;

            Lista_Jogos.Enabled = false;

            comandoConfirmar = "alterar";

            // Limpar
            button27.Visible = false;
            // Filtros
            label29.Visible = false;
            comboBox5.Visible = false;
            label36.Visible = false;
            comboBox3.Visible = false;
            label50.Visible = false;
            comboBox4.Visible = false;
            // Bilhetes Jogo
            label49.Visible = false;
            Bilhetes_Jogo.Visible = false;
            // Campos preenchimento
            textBox29.Enabled = true;
            textBox29.BackColor = Color.White;
            textBox30.Enabled = true;
            textBox30.BackColor = Color.White;
            textBox5.Enabled = true;
            textBox5.BackColor = Color.White;
            textBox12.Enabled = true;
            textBox12.BackColor = Color.White;
            dateTimePicker1.Enabled = true;
            dateTimePicker1.BackColor = Color.White;
            textBox13.Enabled = true;
            textBox13.BackColor = Color.White;
            textBox11.Enabled = true;
            textBox11.BackColor = Color.White;

            this.textBox29.TextChanged +=
                new System.EventHandler(textBox29_TextChanged_1);

            this.textBox30.TextChanged +=
                new System.EventHandler(textBox30_TextChanged_1);

            this.textBox5.TextChanged +=
                new System.EventHandler(textBox5_TextChanged_1);

            clear("jogos", "alterar");
        }

        // Botão alterar bilhetes
        private void button31_Click(object sender, EventArgs e)
        {
            button29.Visible = true;
            button30.Visible = true;
            Bilhetes_Jogo.Visible = false;
            label65.Visible = true;
            label66.Visible = true;
            label67.Visible = true;

            textBox31.Visible = true;
            textBox32.Visible = true;
            textBox33.Visible = true;
            textBox34.Visible = true;
            textBox35.Visible = true;
            textBox36.Visible = true;

            textBox31.Enabled = false;
            textBox32.Enabled = true;
            textBox33.Enabled = true;
            textBox34.Enabled = false;
            textBox35.Enabled = true;
            textBox36.Enabled = true;

            textBox31.BackColor = Color.LightSteelBlue;
            textBox32.BackColor = Color.White;
            textBox33.BackColor = Color.White;
            textBox34.BackColor = Color.LightSteelBlue;
            textBox35.BackColor = Color.White;
            textBox36.BackColor = Color.White;
        }

        // Botão Apagar Jogadores
        private void button9_Click(object sender, EventArgs e)
        {
            button11.Visible = true;
            button12.Visible = true;

            Lista_Jogadores.Enabled = false;

            comandoConfirmar = "apagar";
        }

        // Botão Apagar Treinadores
        private void button6_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = true;

            Lista_Treinadores.Enabled = false;

            comandoConfirmar = "apagar";
        }

        // Botão Apagar Equipas
        private void button16_Click(object sender, EventArgs e)
        {
            button14.Visible = true;
            button3.Visible = true;

            Lista_Equipas.Enabled = false;

            comandoConfirmar = "apagar";
        }

        // Botão Apagar jogos
        private void button22_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            button20.Visible = true;

            Lista_Jogos.Enabled = false;

            comandoConfirmar = "apagar";
        }

        // Botão Cancelar Jogadores
        private void button12_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            button12.Visible = false;

            Lista_Jogadores.Enabled = true;

            resetJogadores();
            clear("jogadores", "cancelar");
        }

        // Botão Cancelar Treinadores
        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = false;

            Lista_Treinadores.Enabled = true;

            resetTreinadores();
            clear("treinadores", "cancelar");
        }

        // Botão Cancelar Equipas
        private void button3_Click_1(object sender, EventArgs e)
        {
            button14.Visible = false;
            button3.Visible = false;

            Lista_Equipas.Enabled = true;

            resetEquipas();
            clear("equipas", "cancelar");
        }

        // Botão Cancelar Jogos
        private void button19_Click(object sender, EventArgs e)
        {
            button19.Visible = false;
            button20.Visible = false;

            Lista_Jogos.Enabled = true;

            resetJogos();
            clear("jogos", "cancelar");
        }

        // Botão cancelar bilhetes
        private void button30_Click(object sender, EventArgs e)
        {
            button29.Visible = false;
            button30.Visible = false;

            Lista_Jogos.Enabled = true;

            resetJogos();
            clear("jogos", "bilhetes");
        }

        private void resetJogadores()
        {
            // Searchbar
            label21.Visible = true;
            Search_Jogadores.Visible = true;
            button1.Visible = true;
            button24.Visible = true;
            // Filtros
            label22.Visible = true;
            comboBox2.Visible = true;
            label7.Visible = true;
            FiltroEquipa_Jogadores.Visible = true;
            FiltroPosicao_Jogadores.Visible = true;
            comboBox1.Visible = true;
            label21.Visible = true;
            label21.Visible = true;
            // Estatistica
            label19.Visible = true;
            Estatistica_Jogador.Visible = true;
            label53.Visible = false;
            label54.Visible = false;
            label55.Visible = false;
            label56.Visible = false;
            label57.Visible = false;
            label58.Visible = false;
            label59.Visible = false;
            textBox18.Visible = false;
            textBox24.Visible = false;
            textBox26.Visible = false;
            textBox23.Visible = false;
            textBox25.Visible = false;
            textBox22.Visible = false;
            textBox19.Visible = false;
            // Contrato
            label20.Visible = true;
            Contrato_Jogador.Visible = true;
            // Campos preenchimento
            NumeroCC_Jogadores.Enabled = false;
            NumeroCC_Jogadores.BackColor = Color.LightSteelBlue;
            Name_Jogadores.Enabled = false;
            Name_Jogadores.BackColor = Color.LightSteelBlue;
            Altura_Jogadores.Enabled = false;
            Altura_Jogadores.BackColor = Color.LightSteelBlue;
            NumeroEquipamento_Jogadores.Enabled = false;
            NumeroEquipamento_Jogadores.BackColor = Color.LightSteelBlue;
            Peso_Jogadores.Enabled = false;
            Peso_Jogadores.BackColor = Color.LightSteelBlue;
            comboBox8.Enabled = false;
            comboBox8.BackColor = Color.LightSteelBlue;
            Idade_Jogadores.Enabled = false;
            Idade_Jogadores.BackColor = Color.LightSteelBlue;
            IDEquipa_Jogadores.Enabled = false;
            IDEquipa_Jogadores.BackColor = Color.LightSteelBlue;
            IDContrato_Jogadores.Enabled = false;
            IDContrato_Jogadores.BackColor = Color.LightSteelBlue;
        }

        private void resetTreinadores()
        {
            // Searchbar
            label18.Visible = true;
            textBox17.Visible = true;
            button13.Visible = true;
            button25.Visible = true;
            // Filtros
            label41.Visible = true;
            comboBox6.Visible = true;
            // Contrato
            label24.Visible = true;
            richTextBox2.Visible = true;
            // Campos preenchimento
            textBox16.Enabled = false;
            textBox16.BackColor = Color.LightSteelBlue;
            textBox20.Enabled = false;
            textBox20.BackColor = Color.LightSteelBlue;
            textBox14.Enabled = false;
            textBox14.BackColor = Color.LightSteelBlue;
            textBox1.Enabled = false;
            textBox1.BackColor = Color.LightSteelBlue;
        }

        private void resetEquipas()
        {
            // Searchbar
            label13.Visible = true;
            textBox10.Visible = true;
            button18.Visible = true;
            button26.Visible = true;
            // Filtros
            label43.Visible = true;
            comboBox7.Visible = true;
            // Jogos Equipa
            label47.Visible = true;
            Lista_Jogos_Equipa.Visible = true;
            // Estatistica
            label9.Visible = true;
            richTextBox3.Visible = true;
            // Campos preenchimento
            textBox9.Enabled = false;
            textBox9.BackColor = Color.LightSteelBlue;
            textBox7.Enabled = false;
            textBox7.BackColor = Color.LightSteelBlue;
            textBox28.Enabled = false;
            textBox28.BackColor = Color.LightSteelBlue;
            textBox27.Enabled = false;
            textBox27.BackColor = Color.LightSteelBlue;
            textBox8.Enabled = false;
            textBox8.BackColor = Color.LightSteelBlue;
            comboBox9.Enabled = false;
            comboBox9.BackColor = Color.LightSteelBlue;
        }

        private void resetJogos()
        {
            // Limpar
            button27.Visible = true;
            // Filtros
            label29.Visible = true;
            comboBox5.Visible = true;
            label36.Visible = true;
            comboBox3.Visible = true;
            label50.Visible = true;
            comboBox4.Visible = true;
            // Bilhetes Jogo
            label49.Visible = true;
            Bilhetes_Jogo.Visible = true;
            // Campos preenchimento
            textBox29.Enabled = false;
            textBox29.BackColor = Color.LightSteelBlue;
            textBox30.Enabled = false;
            textBox30.BackColor = Color.LightSteelBlue;
            textBox5.Enabled = false;
            textBox5.BackColor = Color.LightSteelBlue;
            textBox12.Enabled = false;
            textBox12.BackColor = Color.LightSteelBlue;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.BackColor = Color.LightSteelBlue;
            textBox13.Enabled = false;
            textBox13.BackColor = Color.LightSteelBlue;
            textBox11.Enabled = false;
            textBox11.BackColor = Color.LightSteelBlue;
        }

        private void resetTabelaClassificativa()
        {
            richTextBox1.Text = "";
            InitializeTabela_Classificativa();
        }

        private void clear(string janela, string botao)
        {
            if (janela == "jogadores")
            {
                if (botao != "filtro")
                {
                    comboBox2.Enabled = true;
                    FiltroEquipa_Jogadores.Enabled = true;
                    comboBox1.Enabled = true;
                    if (botao != "alterar")
                    {
                        comboBox2.SelectedIndex = -1;
                        FiltroEquipa_Jogadores.SelectedIndex = -1;
                        comboBox1.SelectedIndex = -1;
                    }
                }

                if (botao != "pesquisar")
                {
                    Search_Jogadores.Text = "";
                }

                button9.Visible = false;
                button10.Visible = false;

                if (botao != "adicionar" && botao != "alterar")
                {
                    button11.Visible = false;
                    button12.Visible = false;
                }

                if (botao != "alterar")
                {
                    NumeroCC_Jogadores.Text = "";
                    Name_Jogadores.Text = "";
                    Altura_Jogadores.Text = "";
                    Peso_Jogadores.Text = "";
                    NumeroEquipamento_Jogadores.Text = "";
                    comboBox8.SelectedIndex = -1;
                    Idade_Jogadores.Text = "";
                    IDEquipa_Jogadores.Text = "";
                    IDContrato_Jogadores.Text = "";
                    Estatistica_Jogador.Text = "";
                    Contrato_Jogador.Text = "";
                    textBox3.Text = "";
                }

                if (botao == "limpar")
                {
                    updateListaJogadores();
                }
            }
            else if (janela == "treinadores")
            {
                if (botao != "filtro")
                {
                    comboBox6.Enabled = true;
                    if (botao != "alterar")
                    {
                        comboBox6.SelectedIndex = -1;
                    }
                }

                if (botao != "pesquisar")
                {
                    textBox17.Text = "";
                }

                button6.Visible = false;
                button5.Visible = false;

                if (botao != "adicionar" && botao != "alterar")
                {
                    button4.Visible = false;
                    button2.Visible = false;
                }

                if (botao != "alterar")
                {
                    textBox17.Text = "";
                    textBox16.Text = "";
                    textBox20.Text = "";
                    textBox14.Text = "";
                    textBox1.Text = "";
                    richTextBox2.Text = "";
                }

                if (botao == "limpar")
                {
                    updateListaTreinadores();
                }
            }
            else if (janela == "equipas")
            {
                if (botao != "filtro")
                {
                    comboBox7.Enabled = true;
                    if (botao != "alterar")
                    {
                        comboBox7.SelectedIndex = -1;
                    }
                }

                if (botao != "pesquisar")
                {
                    textBox10.Text = "";
                }

                button16.Visible = false;
                button15.Visible = false;

                if (botao != "adicionar" && botao != "alterar")
                {
                    button14.Visible = false;
                    button3.Visible = false;
                }

                if (botao != "alterar")
                {
                    textBox10.Text = "";
                    textBox9.Text = "";
                    textBox4.Text = "";
                    textBox2.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    comboBox9.SelectedIndex = -1;
                    textBox28.Text = "";
                    textBox27.Text = "";
                    richTextBox3.Text = "";
                    Lista_Jogos_Equipa.Items.Clear();
                }

                if (botao == "limpar")
                {
                    updateListaEquipas();
                }
            }
            else if (janela == "jogos")
            {
                if (botao != "filtro" && botao != "bilhetes")
                {
                    comboBox5.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    if (botao != "alterar" && botao != "bilhetes")
                    {
                        comboBox5.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        comboBox4.SelectedIndex = -1;
                    }
                }

                button22.Visible = false;
                button21.Visible = false;
                button28.Visible = false;
                button29.Visible = false;
                button30.Visible = false;

                if (botao != "bilhetes")
                {
                    button31.Visible = false;
                }

                label65.Visible = false;
                label66.Visible = false;
                label67.Visible = false;

                textBox31.Visible = false;
                textBox32.Visible = false;
                textBox33.Visible = false;
                textBox34.Visible = false;
                textBox35.Visible = false;
                textBox36.Visible = false;

                if (botao != "adicionar" && botao != "alterar" && botao != "bilhetes")
                {
                    button20.Visible = false;
                    button19.Visible = false;
                    button28.Visible = false;
                    button29.Visible = false;
                    button30.Visible = false;
                }

                if (botao != "alterar" && botao != "bilhetes")
                {
                    textBox15.Text = "";
                    textBox6.Text = "";
                    textBox21.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                    textBox11.Text = "";
                    textBox29.Text = "";
                    textBox30.Text = "";
                    textBox5.Text = "";
                    Bilhetes_Jogo.Items.Clear();
                }

                if (botao == "limpar" && botao != "bilhetes")
                {
                    updateListaJogos();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string playerSearch = (string)Search_Jogadores.Text;

            clear("jogadores", "pesquisar");
            barraPesquisa(playerSearch, "PersonPlayer");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string coachSearch = (string)textBox17.Text;

            clear("treinadores", "pesquisar");
            barraPesquisa(coachSearch, "PersonCoach");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string teamSearch = (string)textBox10.Text;

            clear("equipas", "pesquisar");
            barraPesquisa(teamSearch, "TeamCoachOwner");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            clear("treinadores", "limpar");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            clear("equipas", "limpar");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            clear("jogadores", "limpar");
        }
        private void button27_Click(object sender, EventArgs e)
        {
            clear("jogos", "limpar");
        }

        private void Treinadores_Tab_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tabela_Classificativa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lista_Equipas_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void Jogadores_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lista_Jogos_Equipa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void IDEquipa_Jogadores_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bilhetes_Jogo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }
    }
}