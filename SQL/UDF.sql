use p4g1;

/* OBTER A LISTA DE JOGADORES */
drop function IF EXISTS NBA.obterJogadores;
go
create function NBA.obterJogadores() returns table 
as 
	return (select * from NBA.Player)
go

/* OBTER A LISTA DE JOGADORES COM CONTRATO */
drop function IF EXISTS NBA.obterPlayersWithContract;
go
create function NBA.obterPlayersWithContract() returns table 
as 
	return (select * from NBA.PlayersWithContract)
go

/* OBTER A LISTA DE JOGADORES SEM CONTRATO */
drop function IF EXISTS NBA.obterPlayersWithoutContract;
go
create function NBA.obterPlayersWithoutContract() returns table
as 
    return (select * from NBA.PlayersWithoutContract)
go

/* OBTER A LISTA DE TREINADORES */
drop function IF EXISTS NBA.obterTreinadores;
go
create function NBA.obterTreinadores() returns table 
as 
	return (select * from NBA.Coach)
go

/* OBTER A LISTA DE TREINADORES COM CONTRATO */
drop function IF EXISTS NBA.obterCoachesWithContract;
go
create function NBA.obterCoachesWithContract() returns table 
as 
	return (select * from NBA.CoachesWithContract)
go

/* OBTER A LISTA DE TREINADORES SEM CONTRATO */
drop function IF EXISTS NBA.obterCoachesWithoutContract;
go
create function NBA.obterCoachesWithoutContract() returns table
as 
    return (select * from NBA.CoachesWithoutContract)
go

/* OBTER A LISTA DE EQUIPAS */
drop function IF EXISTS NBA.obterEquipas;
go
create function NBA.obterEquipas() returns table 
as 
	return (select * from NBA.Team)
go

/* OBTER A LISTA DE JOGOS */
drop function IF EXISTS NBA.obterJogos;
go
create function NBA.obterJogos() returns table
as 
    return (select * from NBA.Game)
go

/* OBTER A LISTA DE JOGOS COM RESULTADO */
drop function IF EXISTS NBA.obterGamesWithResult;
go
create function NBA.obterGamesWithResult() returns table
as 
    return (select * from NBA.GamesWithResult)
go

/* OBTER A LISTA DE JOGOS SEM RESULTADO */
drop function IF EXISTS NBA.obterGamesWithoutResult;
go
create function NBA.obterGamesWithoutResult() returns table
as 
    return (select * from NBA.GamesWithoutResult)
go

/* OBTER A LISTA DE PLAYERS GUARD */
drop function IF EXISTS NBA.obterPlayerGuard;
go
create function NBA.obterPlayerGuard() returns table
as 
	return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Guard')
go


/* OBTER A LISTA DE PLAYERS FORWARD */
drop function IF EXISTS NBA.obterPlayerForward;
go
create function NBA.obterPlayerForward() returns table
as 
    return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Forward')
go

/* OBTER A LISTA DE PLAYERS FORWARD CENTER */
drop function IF EXISTS NBA.obterPlayerForwardCenter;
go
create function NBA.obterPlayerForwardCenter() returns table
as 
    return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Forward-Center')  
go

/* OBTER A LISTA DE PLAYERS GUARD FORWARD */
drop function IF EXISTS NBA.obterPlayerGuardForward;
go
create function NBA.obterPlayerGuardForward() returns table
as 
    return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Guard-Forward') 
go

/* OBTER A LISTA DE PLAYERS CENTER */
drop function IF EXISTS NBA.obterPlayerCenter;
go
create function NBA.obterPlayerCenter() returns table
as 
    return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Center') 
go

/* OBTER A LISTA DE PLAYERS CENTER FORWARD */
drop function IF EXISTS NBA.obterPlayerCenterForward;
go
create function NBA.obterPlayerCenterForward() returns table
as 
    return (select * from NBA.PlayersWithContract
            union 
            select * from NBA.PlayersWithoutContract
            where Position = 'Center-Forward') 
go

-- Função com os filtro de equipa, contrato e posição dos jogadores
drop function IF EXISTS NBA.filtrarJogadoresPorEquipaEContratoEPosicao;
go
create function NBA.filtrarJogadoresPorEquipaEContratoEPosicao(@equipa varchar(50), @contrato bit, @posicao varchar(30)) returns table
as
return (
    select *
    from NBA.Players as P join NBA.Team as T on P.Team_ID = T.ID
    where 
        (T.Name = @equipa or @equipa is null) and (
            (@contrato = 1 and P.ID in (select ID from NBA.obterPlayersWithContract()))
            OR
            (@contrato = 0 and P.ID in (select ID from NBA.obterPlayersWithoutContract()))
            OR
            (@contrato is null) 
        ) and ( P.Position = @posicao or @posicao is null)
);
go

-- Função para barra de pesquuisa de nome de jogadores
drop function IF EXISTS NBA.pesquisarJogadoresPorNome;
go
create function NBA.pesquisarJogadoresPorNome(@nome varchar(50)) returns table
as
return (
    select *
    from NBA.Players
    where Name like '%' + @nome + '%'
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
create function NBA.getTeamAverageStats() returns @TeamAverageStats table (
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

        declare teamCursor cursor for select [Name] from NBA.Team;
        open teamCursor;

        fetch next from teamCursor into @TeamName;

        while @@FETCH_STATUS = 0
            begin
                insert into @PlayersStats (Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%])
                    select Points, Assists, Rebounds, Blocks, Steals, [FG%], [3PT%]
                    from (((NBA.Average_Individual_Numbers as States inner join NBA.Player as Pl on States.Player_CCNumber = Pl.CCNumber) inner join NBA.Team as T on Pl.Team_ID = T.ID) INNER JOIN NBA.Person AS Pe ON Pl.CCNumber = Pe.CCNumber)
                    WHERE T.[Name] = @TeamName;

                insert into @TeamAverageStats (TeamName, AveragePoints, AverageAssists, AverageRebounds, AverageBlocks, AverageSteals, AverageFGP, Average3PTP)
                    select @TeamName, avg(Points), avg(Assists), avg(Rebounds), avg(Blocks), avg(Steals), avg(FGPercentage), avg(ThreePTPercentage)
                    from @PlayerStats;

                delete from @PlayerStats;

                fetch next from teamCursor into @TeamName;
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
    Wins int,
    Losses int
)
as
    begin
        -- Inserir os resultados dos jogos na tabela @GameWinners
        declare @GameWinners table (
            Game_ID int,
            Winner_ID int
        );

        -- Chamar a UDF anterior para obter os vencedores de cada jogo
        insert into @GameWinners (Game_ID, Winner_ID)
            select Game_ID,
                (case 
                    when Home_Score > Away_Score then Home_Team_ID
                    when Home_Score < Away_Score then Away_Team_ID
                end) as Winner_ID
            from NBA.Game;

        -- Calcular o número de vitórias e derrotas para cada equipe
        insert into @TeamStandings (Team_ID, Team_Name, Wins, Losses)
            select T.Team_ID, T.Team_Name,
                sum(case when GW.Winner_ID = T.Team_ID then 1 else 0 end) as Wins,
                sum(case when GW.Winner_ID <> T.Team_ID then 1 else 0 end) as Losses
            from (NBA.Team as T left join @GameWinners as GW on T.Team_ID = GW.Winner_ID)
            group by T.Team_ID, T.Team_Name;
        return;
    end;
go

-- Função para retornar a tabela de jogos de uma dada equipa
drop function IF EXISTS NBA.GetTeamGames
go
create function NBA.GetTeamGames (@TeamID int) returns table
as
    RETURN
    (
        select G.ID AS GameID, G.[Time], G.[Date], G.Home_Score, G.Away_Score
        FROM (NBA.Game G inner join NBA.Team T on T.ID = G.Home_Team_ID or T.ID = G.Away_Team_ID)
        where T.ID = @TeamID
    )
go

-- Função para retornar os bilhetes de um dado jogo
drop function IF EXISTS NBA.GetGameTickets
go
create function GetGameTickets (@GameID int) returns table
as
return
(
    select [Type], Price, Restantes
    from NBA.Ticket
    where Game_ID = @GameID
)
go


-- Verificar se o CC já existe
drop function IF EXISTS NBA.checkCCNumber
go
create function NBA.checkCCNumber (@CC varchar(10)) returns int
as
	begin
		declare @counter int
		select @counter = COUNT(1) 
        from NBA.Person as P where P.CCNumber=@CC
		return @counter
	end
go

-- Verificar se o ID do jogo já existe
drop function IF EXISTS NBA.checkGameID
go
create function NBA.checkGameID (@ID int) returns int
as
    begin
        declare @counter int
        select @counter = COUNT(1) 
        from NBA.Game as G where G.ID=@ID
        return @counter
    end
go

-- Verificar se o ID do contrato já existe
drop function IF EXISTS NBA.checkContractID
go
create function NBA.checkContractID (@ID int) returns int
as
    begin
        declare @counter int
        select @counter = COUNT(1) 
        from NBA.[Contract] as C where C.ID=@ID
        return @counter
    end
go

/* Verificar o último ID de jogo adicionado */
drop function Mercado.nextIDJogo
go
create function Mercado.nextIDJogo() returns int
as
	begin
		declare @ID as int;
		select @ID = max(ID) + 1 from NBA.Game;
		return @ID;
	end
go

