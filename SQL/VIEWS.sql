use p4g1;

-- Jogadores com contrato
drop view IF EXISTS NBA.PlayersWithContract
go 
create view NBA.PlayersWithContract as 
	select *
	from ((NBA.Player as Pl join NBA.Person as Pe on Pl.ID = Pe.ID) join NBA.[Contract] as C on Pe.Contract_ID = C.ID)
	where C.End_Date > getdate();
go

-- Jogadores sem contrato
drop view IF EXISTS NBA.PlayersWithoutContract
go 
create view NBA.PlayersWithoutContract as 
	select *
	from ((NBA.Player as Pl join NBA.Person as Pe on Pl.ID = Pe.ID) join NBA.[Contract] as C on Pe.Contract_ID = C.ID)
	where C.End_Date < getdate();
go

-- Treinadores com contrato
drop view IF EXISTS NBA.CoachesWithContract
go 
create view NBA.CoachesWithContract as 
	select *
	from ((NBA.Coach as Pl join NBA.Person as Pe on Pl.ID = Pe.ID) join NBA.[Contract] as C on Pe.Contract_ID = C.ID)
	where C.End_Date > getdate();
go

-- Treinadores sem contrato
drop view IF EXISTS NBA.CoachesWithoutContract
go 
create view NBA.CoachesWithoutContract as 
	select *
	from ((NBA.Coach as Pl join NBA.Person as Pe on Pl.ID = Pe.ID) join NBA.[Contract] as C on Pe.Contract_ID = C.ID)
	where C.End_Date < getdate();
go

-- Jogos com resultado
drop view IF EXISTS NBA.GamesWithResult
go 
create view NBA.GamesWithResult as 
	select *
	from NBA.Game as G
    where G.Home_Score is not null and G.Away_Score is not null;
go

-- Jogos sem resultado
drop view IF EXISTS NBA.GamesWithoutResult
go 
create view NBA.GamesWithoutResult as 
	select *
    from NBA.Game as G
    where G.Home_Score is null and G.Away_Score is null;
go