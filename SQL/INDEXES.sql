use p4g1;

drop index IF EXISTS seacrhContractStartDate on NBA.[Contract];
drop index IF EXISTS searchContractEndDate on NBA.[Contract];
drop index IF EXISTS searchPersonName on NBA.Person;
drop index IF EXISTS seacrhTeamName on NBA.Team;
drop index IF EXISTS seacrhTeamID on NBA.Team;
drop index IF EXISTS seacrhPlayerTeam on NBA.Player;
drop index IF EXISTS seacrhGameHomeTeamScore on NBA.Game;
drop index IF EXISTS seacrhGameAwayTeamScore on NBA.Game;


-- Criação de indexes na tabela NBA.[Contract]
create index seacrhContractStartDate on NBA.[Contract] ([Start_Date]);
create index searchContractEndDate on NBA.[Contract] ([End_Date]);

-- Criação de indexes na tabela NBA.Person
create index searchPersonName on NBA.Person ([Name]);

-- Criação de indexes na tabela NBA.Team
create index seacrhTeamName on NBA.Team ([Name]);
create index seacrhTeamID on NBA.Team (ID);

-- Criação de indexes na tabela NBA.Player
create index seacrhPlayerTeam on NBA.Player (Team_ID);

-- Criação de indexes na tabela NBA.Game
create index seacrhGameHomeTeamScore on NBA.Game (Home_Score);
create index seacrhGameAwayTeamScore on NBA.Game (Away_Score);

