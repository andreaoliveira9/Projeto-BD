use p4g1;

-- Pessoa e jogador
drop view IF EXISTS NBA.PersonPlayer
go 
create view NBA.PersonPlayer as 
	select Pe.CCNumber, Pe.[Name], Pe.Age, Pe.Contract_ID, Pl.[Number], Pl.Height, Pl.[Weight], Pl.Position, Pl.Team_ID, T.[Name] as TeamName
	from ((NBA.Player as Pl join NBA.Person as Pe on Pl.CCNumber = Pe.CCNumber) inner join NBA.Team as T on Pl.Team_ID = T.ID);
go

-- Pessoa e treinador
drop view IF EXISTS NBA.PersonCoach
go 
create view NBA.PersonCoach as 
	select Pe.CCNumber, Pe.[Name], Pe.Age, Pe.Contract_ID 
	from (NBA.Coach as Co join NBA.Person as Pe on Co.CCNumber = Pe.CCNumber);
go

-- Jogadores com contrato
drop view IF EXISTS NBA.PlayersWithContract
go 
create view NBA.PlayersWithContract as 
	select Pl.CCNumber, Pl.[Name], Pl.Age, Pl.[Number], Pl.Height, Pl.[Weight], Pl.Position, Pl.Team_ID, C.ID, C.[Description], C.Salary,C.[Start_Date],C.End_Date
	from (NBA.PersonPlayer as Pl join NBA.[Contract] as C on Pl.Contract_ID = C.ID)
	where C.End_Date > getdate();
go

-- Jogadores sem contrato
drop view IF EXISTS NBA.PlayersWithoutContract
go 
create view NBA.PlayersWithoutContract as 
	select Pl.CCNumber, Pl.[Name], Pl.Age, Pl.[Number], Pl.Height, Pl.[Weight], Pl.Position, Pl.Team_ID, C.ID, C.[Description],C.Salary,C.[Start_Date],C.End_Date
	from (NBA.PersonPlayer as Pl join NBA.[Contract] as C on Pl.Contract_ID = C.ID)
	where C.End_Date < getdate();
go

-- Treinadores com contrato
drop view IF EXISTS NBA.CoachesWithContract
go 
create view NBA.CoachesWithContract as 
	select Co.CCNumber, Co.[Name], Co.Age, C.ID,C.[Description], C.Salary ,C.[Start_Date], C.End_Date
	from (NBA.PersonCoach as Co join NBA.[Contract] as C on Co.Contract_ID = C.ID)
	where C.End_Date > getdate();
go

-- Treinadores sem contrato
drop view IF EXISTS NBA.CoachesWithoutContract
go 
create view NBA.CoachesWithoutContract as 
	select Co.CCNumber, Co.[Name], Co.Age, C.ID ,C.[Description], C.Salary ,C.[Start_Date], C.End_Date
	from (NBA.PersonCoach as Co join NBA.[Contract] as C on Co.Contract_ID = C.ID)
	where C.End_Date < getdate();
go

-- Equipas
drop view IF EXISTS NBA.TeamCoachOwner
go 
create view NBA.TeamCoachOwner as 
	select T.ID, T.[Name], T.City, T.Conference, T.Found_Year, C.[Name] as CoachName, P.[Name] as OwnerName, C.CCNumber as CoachCCNumber, P.CCNumber as OwnerCCNumber
	from ((NBA.Team as T join NBA.PersonCoach as C on T.Coach_CCNumber = C.CCNumber) join NBA.Person as P on T.Owner_CCNumber = P.CCNumber);
go

-- Jogos com nome das equipas e pavlhão
drop view IF EXISTS NBA.GamesTeamsStadium
go 
create view NBA.GamesTeamsStadium as 
	select G.ID, G.[Time], G.[Date], G.Home_Score, G.Away_Score, T1.[Name] as HomeTeamName, T2.[Name] as AwayTeamName, S.[Name] as StadiumName
	from ((NBA.Game as G inner join NBA.Team as T1 on G.Home_Team_ID = T1.ID) inner join NBA.Team as T2 on G.Away_Team_ID = T2.ID) inner join NBA.Stadium as S on G.Stadium_ID = S.ID
go

-- Jogos com resultado
drop view IF EXISTS NBA.GamesWithResult
go 
create view NBA.GamesWithResult as 
	select G.ID, G.[Time], G.[Date], G.Home_Score, G.Away_Score, T1.[Name] as HomeTeamName, T2.[Name] as AwayTeamName, S.[Name] as StadiumName
	from ((NBA.Game as G inner join NBA.Team as T1 on G.Home_Team_ID = T1.ID) inner join NBA.Team as T2 on G.Away_Team_ID = T2.ID) inner join NBA.Stadium as S on G.Stadium_ID = S.ID
	where G.Home_Score is not null
go

-- Jogos com resultado
drop view IF EXISTS NBA.GamesWithoutResult
go 
create view NBA.GamesWithoutResult as 
	select G.ID, G.[Time], G.[Date], G.Home_Score, G.Away_Score, T1.[Name] as HomeTeamName, T2.[Name] as AwayTeamName, S.[Name] as StadiumName
	from ((NBA.Game as G inner join NBA.Team as T1 on G.Home_Team_ID = T1.ID) inner join NBA.Team as T2 on G.Away_Team_ID = T2.ID) inner join NBA.Stadium as S on G.Stadium_ID = S.ID
	where G.Home_Score is null
go