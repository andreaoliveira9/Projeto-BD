use p4g1;

-- Função com os filtro de equipa, contrato e posição dos jogadores
drop function IF EXISTS NBA.filtrarJogadoresPorEquipaEContratoEPosicao;
go
create function NBA.filtrarJogadoresPorEquipaEContratoEPosicao(@equipa varchar(50), @contrato varchar(3), @posicao varchar(30)) returns table
as
return (
    select P.CCNumber,P.[Name],P.Age,P.Contract_ID,P.Number,P.Height,P.[Weight],P.Position,P.Team_ID, T.[Name] as TeamName
    from NBA.PersonPlayer as P join NBA.Team as T on P.Team_ID = T.ID
    where 
        (T.[Name] = @equipa or @equipa is null) and (
            (@contrato = 'Sim' and P.CCNumber in (select CCNumber from NBA.PlayersWithContract))
            OR
            (@contrato = 'Nao' and P.CCNumber in (select CCNumber from NBA.PlayersWithoutContract))
            OR
            (@contrato is null) 
        ) and ( P.Position = @posicao or @posicao is null)
);
go

-- Função com filtro de contrato dos treinadores
drop function IF EXISTS NBA.filtrarTreinadoresPorContrato;
go
create function NBA.filtrarTreinadoresPorContrato(@contrato varchar(3)) returns table
as
return (
    select CCNumber, [Name], Age, Contract_ID
    from NBA.PersonCoach
    where 
        (@contrato = 'Sim' and CCNumber in (select CCNumber from NBA.CoachesWithContract))
        OR
        (@contrato = 'Nao' and CCNumber in (select CCNumber from NBA.CoachesWithoutContract))
        OR
        (@contrato is null) 
);
go

-- Função com o filtro de conferencia das equipas
drop function IF EXISTS NBA.filtrarEquipasPorConferencia;
go
create function NBA.filtrarEquipasPorConferencia(@conferencia varchar(10)) returns table
as
return (
    select *
    from NBA.TeamCoachOwner
    where (Conference = @conferencia or @conferencia is null)
);
go

-- Função com o filtro de equipa da casa e equipa visitante
drop function IF EXISTS NBA.filtrarJogosPorEquipaCasaEquipaForaESeAconteceu
go
create function NBA.filtrarJogosPorEquipaCasaEquipaForaESeAconteceu(@equipaCasa varchar(30), @equipaFora varchar(30), @aconteceu varchar(3)) returns table
as
return (
    select *
    from NBA.GamesTeamsStadium as G
    where (HomeTeamName = @equipaCasa or @equipaCasa is null) and (
            (@aconteceu = 'Sim' and G.ID in (select ID from NBA.GamesWithResult))
            OR
            (@aconteceu = 'Nao' and G.ID in (select ID from NBA.GamesWithoutResult))
            OR
            (@aconteceu is null) 
        ) and (AwayTeamName = @equipaFora or @equipaFora is null)
);
go

-- Função que retorna a média de estatísticaa de um dado jogador
drop function IF EXISTS NBA.GetPlayerStats
go
create function NBA.GetPlayerStats(@playerCC int) returns table
as
return (
    select Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%]
    from ((NBA.Average_Individual_Numbers as Stats inner join NBA.Player as Pl on Stats.Player_CCNumber = Pl.CCNumber) inner join NBA.Person as Pe on Pl.CCNumber = Pe.CCNumber)
    WHERE Pe.CCNumber = @playerCC
);
go

-- Função que retorna a média de estatísticas de uma equipa
drop function IF EXISTS NBA.getTeamAverageStats
go
create function NBA.getTeamAverageStats(@InputTeamID INT) returns @TeamAverageStats table (
    TeamID INT,
    TeamName varchar(50),
    AveragePoints decimal(10, 2),
    AverageAssists decimal(10, 2),
    AverageRebounds decimal(10, 2),
    AverageBlocks decimal(10, 2),
    AverageSteals decimal(10, 2),
    AverageFGP decimal(10, 2),
    Average3PTP decimal(10, 2)
)
as
    begin
        declare @TeamID as int;
        declare @TeamName varchar(50);
        declare @PlayersStats table
        (
            Points int,
            Assists int,
            Rebounds int,
            Blocks int,
            Steals int,
            [FG%] float,
            [3PT%] float
        );

        declare teamCursor cursor for select ID, [Name] from NBA.Team where ID = @InputTeamID;
        open teamCursor;

        fetch next from teamCursor into @TeamID, @TeamName;

        while @@FETCH_STATUS = 0
            begin
                insert into @PlayersStats (Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%])
                    select Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%]
                    from NBA.Average_Individual_Numbers as Stats inner join NBA.Player as Pl on Stats.Player_CCNumber = Pl.CCNumber 
                    where Pl.Team_ID = @TeamID;

                insert into @TeamAverageStats (TeamID, TeamName, AveragePoints, AverageAssists, AverageRebounds, AverageBlocks, AverageSteals, AverageFGP, Average3PTP)
                    select @TeamID, @TeamName, avg(Points), avg(Assists), avg(Rebounds), avg(Blocks), avg(Steals), avg([FG%]), avg([3PT%])
                    from @PlayersStats;

                delete from @PlayersStats;

                fetch next from teamCursor into @TeamID, @TeamName;
            end;

        close teamCursor;
        deallocate teamCursor;

        return;
    END;
go

-- Função para retornar a tabela de classificação
drop function IF EXISTS NBA.GetTeamStandings;
go
create function NBA.GetTeamStandings() returns @TeamStandings table (
    Team_ID int,
    Team_Name varchar(50),
	GamesPlayed int,
    Wins int,
    Losses int,
	[Win%] float
)
as
    begin
        -- Inserir os resultados dos jogos na tabela @GameWinners
        declare @GameWinners table (
            Game_ID int,
            Winner_ID int,
			Loser_ID int
        );

        -- Chamar a UDF anterior para obter os vencedores de cada jogo
        insert into @GameWinners (Game_ID, Winner_ID, Loser_ID)
            select ID,
                (case 
                    when Home_Score > Away_Score then Home_Team_ID
                    when Home_Score < Away_Score then Away_Team_ID
                end) as Winner_ID,
				(case 
                    when Home_Score > Away_Score then Away_Team_ID
                    when Home_Score < Away_Score then Home_Team_ID
                end) as Loser_ID
            from NBA.Game
			where Home_Score is not null and Away_Score is not null;

        -- Calcular o número de vitórias e derrotas para cada equipe
		insert into @TeamStandings (Team_ID, Team_Name, GamesPlayed, Wins, Losses, [Win%])
			select T.ID, T.[Name],
				sum(case when GW.Winner_ID = T.ID then 1 else 0 end)+sum(case when GW.Loser_ID = T.ID then 1 else 0 end) as GamesPlayed,
				sum(case when GW.Winner_ID = T.ID then 1 else 0 end) as Wins,
				sum(case when GW.Loser_ID = T.ID then 1 else 0 end) as Losses,
				round((sum(case when GW.Winner_ID = T.ID then 1 else 0 end) * 100.0) / count(*) , 4) as [Win%]
			from NBA.Team as T left join @GameWinners as GW on T.ID = GW.Winner_ID OR T.ID = GW.Loser_ID
			group by T.ID, T.[Name]
        return;
    end;
go

-- Função para retornar a tabela de jogos de uma dada equipa
drop function IF EXISTS NBA.GetTeamGames
go
create function NBA.GetTeamGames (@TeamID int) returns table
as
    return
    (
        select G.ID AS GameID, G.[Time], G.[Date], G.Home_Score, G.Away_Score, G.Home_Team_ID, G.Away_Team_ID
        from (NBA.Game G inner join NBA.Team T on T.ID = G.Home_Team_ID or T.ID = G.Away_Team_ID)
        where T.ID = @TeamID
    )
go

-- Função para retornar os bilhetes de um dado jogo
drop function IF EXISTS NBA.GetGameTickets
go
create function NBA.GetGameTickets (@GameID int) returns table
as
return
(
    select [Type], Price, Restantes
    from NBA.Ticket
    where Game_ID = @GameID
)
go

