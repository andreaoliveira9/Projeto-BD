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
            InitializeComboBox6();
            InitializeComboBox7();
            InitializeComboBox5();
            InitializeComboBox3();
            InitializeTabela_Classificativa();
            InitializeComboBox4();

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
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.Items.Add("");
            comboBox5.Items.Add("Los Angeles Lakers");
            comboBox5.Items.Add("Boston Celtics");
            comboBox5.Items.Add("Golden State Warriors");
            comboBox5.Items.Add("New York Knicks");
            comboBox5.Items.Add("Chicago Bulls");
            comboBox5.Items.Add("Miami Heat");
            comboBox5.Items.Add("Dallas Mavericks");
            comboBox5.Items.Add("San Antonio Spurs");
            comboBox5.Items.Add("Houston Rockets");
            comboBox5.Items.Add("Portland Trail Blazers");

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
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.Items.Add("");
            comboBox3.Items.Add("Los Angeles Lakers");
            comboBox3.Items.Add("Boston Celtics");
            comboBox3.Items.Add("Golden State Warriors");
            comboBox3.Items.Add("New York Knicks");
            comboBox3.Items.Add("Chicago Bulls");
            comboBox3.Items.Add("Miami Heat");
            comboBox3.Items.Add("Dallas Mavericks");
            comboBox3.Items.Add("San Antonio Spurs");
            comboBox3.Items.Add("Houston Rockets");
            comboBox3.Items.Add("Portland Trail Blazers");

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

        private void InitializeTabela_Classificativa()
        {
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
                textBox5.Text = selectedTeam.Conference;
                textBox7.Text = selectedTeam.FoundYear;

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
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    TeamStats teamStats = new TeamStats();
                    teamStats.Points = reader1["AveragePoints"].ToString();
                    teamStats.Assists = reader1["AverageAssists"].ToString();
                    teamStats.Rebounds = reader1["AverageRebounds"].ToString();
                    teamStats.Blocks = reader1["AverageBlocks"].ToString();
                    teamStats.Steals = reader1["AverageSteals"].ToString();
                    teamStats.FG = reader1["AverageFGP"].ToString();
                    teamStats.PT3 = reader1["Average3PTP"].ToString();
                    richTextBox3.Text = teamStats.ToString();
                }
                reader1.Close();

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
                game.StadiumName = reader["StadiumName"].ToString();

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
            if (selectedGame != null)
            {
                textBox15.Text = selectedGame.HomeTeamName;
                textBox6.Text = selectedGame.AwayTeamName;
                textBox21.Text = selectedGame.StadiumName;
                textBox12.Text = selectedGame.Time;
                textBox13.Text = selectedGame.HomeScore;
                textBox11.Text = selectedGame.AwayScore;
                //DateTime data = new DateTime(int.Parse(selectedGame.Date.Substring(0,4)), int.Parse(selectedGame.Date.Substring(5, 2)), int.Parse(selectedGame.Date.Substring(8, 2)));
                //dateTimePicker1.Value = data;

                if (selectedGame.HomeScore != "")
                {
                    Bilhetes_Jogo.Items.Add("Bilhetes indisponíveis, pois jogo já aconteceu");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from NBA.GetGameTickets(" + "'" + selectedGame.ID + "')", cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ticket ticket = new Ticket();
                        ticket.Type = reader["Type"].ToString();
                        ticket.Price = reader["Price"].ToString();
                        ticket.Restantes = reader["Restantes"].ToString();

                        Bilhetes_Jogo.Items.Add(ticket);
                        totalItems++;
                    }
                    reader.Close();

                    this.Bilhetes_Jogo.SelectedIndexChanged +=
                        new System.EventHandler(Bilhetes_Jogo_SelectedIndexChanged);
                }

                button21.Visible = true;
                button22.Visible = true;
            }
        }

        private void Bilhetes_Jogo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ListBox = (ListBox)sender;
            Ticket selectedTicket = (Ticket)Bilhetes_Jogo.SelectedItem;
            if (selectedTicket != null)
            {
                button28.Visible = true;
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
                team.TeamName = reader["TeamName"].ToString();
                team.City = reader["City"].ToString();
                team.Conference = reader["Conference"].ToString();
                team.FoundYear = reader["Found_Year"].ToString();
                team.CoachName = reader["CoachName"].ToString();
                team.OwnerName = reader["OwnerName"].ToString();

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
                game.StadiumName = reader["StadiumName"].ToString();

                totalItems++;
                Lista_Jogos.Items.Add(game);

            }
            label26.Text = "Total de jogos: " + totalItems.ToString();
            reader.Close();
        }

        private void barraPesquisa(string nome, string tabela)
        {
            //Console.WriteLine("exec NBA.pesquisarPorNome @nome = " + "'" + nome + "'" + ", @esquema = 'NBA', @tabela = " + "'" + tabela + "'");
            SqlCommand cmd = new SqlCommand("exec NBA.pesquisarPorNome @nome = " + "'" + nome + "'" + ", @esquema = 'NBA', @tabela = " + "'" + tabela + "'", cn);

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

                    totalItems++;
                    Lista_Equipas.Items.Add(team);

                }
                label11.Text = "Total de equipas: " + totalItems.ToString();
                reader.Close();
            }
        }

        private void clear(string janela, string botao)
        {
            if (janela == "jogadores")
            {
                if (botao != "filtro")
                {
                    comboBox2.Enabled = true;
                    comboBox2.SelectedIndex = -1;
                    FiltroEquipa_Jogadores.Enabled = true;
                    FiltroEquipa_Jogadores.SelectedIndex = -1;
                    comboBox1.Enabled = true;
                    comboBox1.SelectedIndex = -1;
                }

                if (botao != "pesquisar")
                {
                    Search_Jogadores.Text = "";
                }
                
                button9.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;

                NumeroCC_Jogadores.Text = "";
                Name_Jogadores.Text = "";
                Altura_Jogadores.Text = "";
                Peso_Jogadores.Text = "";
                Posicao_Jogadores.Text = "";
                Idade_Jogadores.Text = "";
                IDEquipa_Jogadores.Text = "";
                IDContrato_Jogadores.Text = "";
                Estatistica_Jogador.Text = "";
                Contrato_Jogador.Text = "";

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
                    comboBox6.SelectedIndex = -1;
                }

                if (botao != "pesquisar")
                {
                    textBox17.Text = "";
                }

                button6.Visible = false;
                button5.Visible = false;
                button4.Visible = false;
                button2.Visible = false;

                textBox17.Text = "";
                textBox16.Text = "";
                textBox20.Text = "";
                textBox14.Text = "";
                textBox1.Text = "";
                richTextBox2.Text = "";

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
                    comboBox7.SelectedIndex = -1;
                }

                if (botao != "pesquisar")
                {
                    textBox10.Text = "";
                }

                button16.Visible = false;
                button15.Visible = false;
                button14.Visible = false;
                button3.Visible = false;

                textBox10.Text = "";
                textBox9.Text = "";
                textBox4.Text = "";
                textBox2.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox5.Text = "";
                richTextBox3.Text = "";
                Lista_Jogos_Equipa.Items.Clear();

                if (botao == "limpar")
                {
                    updateListaEquipas();
                }
            } else if (janela == "jogos")
            {
                if (botao != "filtro")
                {
                    comboBox5.Enabled = true;
                    comboBox5.SelectedIndex = -1;
                    comboBox3.Enabled = true;
                    comboBox3.SelectedIndex = -1;
                    comboBox4.Enabled = true;
                    comboBox4.SelectedIndex = -1;
                }

                button22.Visible = false;
                button21.Visible = false;
                button28.Visible = false;
                button29.Visible = false;
                button30.Visible = false;

                textBox15.Text = "";
                textBox6.Text = "";
                textBox21.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox11.Text = "";
                Bilhetes_Jogo.Items.Clear();

                if (botao == "limpar")
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


        private void button7_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = true;
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

        private void button17_Click(object sender, EventArgs e)
        {
            button14.Visible = true;
            button3.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            button3.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button14.Visible = false;
            button3.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button14.Visible = true;
            button3.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button14.Visible = true;
            button3.Visible = true;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            button20.Visible = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            button20.Visible = true;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            button20.Visible = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            button19.Visible = false;
            button20.Visible = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button19.Visible = false;
            button20.Visible = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button29.Visible = true;
            button30.Visible = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            button29.Visible = false;
            button30.Visible = false;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button29.Visible = false;
            button30.Visible = false;
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
    }
}