use p4g1;

-- Criação do trigger para adição ou alteração de jogos
drop trigger IF EXISTS GamesInsertOrUpdate;
go
create trigger GamesInsertOrUpdate on NBA.Game
after insert, update
as
    begin
        set nocount on;
        declare @HomeScore as int;
        declare @AwayScore as int;
        declare @HomeTeamID as int;
        declare @AwayTeamID as int;
        declare @StadiumID as int;
        declare @Date as date;
        declare @Time as time;
        select @HomeScore = Home_Score from inserted;
        select @AwayScore = Away_Score from inserted;
        select @HomeTeamID = Home_Team_ID from inserted;
        select @AwayTeamID = Away_Team_ID from inserted;
        select @StadiumID = Stadium_ID from inserted;
        select @Date = [Date] from inserted;
        select @Time = [Time] from inserted;

        -- Verifica se os campos obrigatórios estão preenchidos
        if (@Time is null or @Date is null or @HomeTeamID is null or @AwayTeamID is null or @StadiumID is null)
            begin
                raiserror('Time, date, home team, away team and stadium cannot be null', 16, 1);
                rollback transaction;
                return;
            end;

        -- Verifica se o jogo já aconteceu tem que ter resultado
        if ((@HomeScore is null or @AwayScore is null) and @Date < getdate())
            begin
                raiserror('Home score and away score cannot be null', 16, 1);
                rollback transaction;
                return;
            end;

        -- Verifica que o resultado não pode ser empate
        if (@HomeScore = @AwayScore)
            begin
                raiserror('Home score cannot be equal to away score', 16, 1);
                rollback transaction;
                return;
            end;
        
        -- Verifica que o resultado não pode ser negativo
        if (@HomeScore < 0 or @AwayScore < 0)
            begin
                raiserror('Home score and away score cannot be negative', 16, 1);
                rollback transaction;
                return;
            end;
        
        -- Verifica que o jogo é entre equipas diferentes
        if (@HomeTeamID = @AwayTeamID)
            begin
                raiserror('Home team cannot be equal to away team', 16, 1);
                rollback transaction;
                return;
            end;
        
        -- Verifica que o estádio é o mesmo que o da equipa da casa
        if (@StadiumID != (select Stadium_ID from NBA.Team where ID = @HomeTeamID))
            begin
                raiserror('Stadium must be the same as the home team', 16, 1);
                rollback transaction;
                return;
            end;

        -- Verifica que as equipas e o estádio existem
        if (not exists (select 1 from NBA.Team where ID = @HomeTeamID) or not exists (select 1 from NBA.Team where ID = @AwayTeamID) or not exists (select 1 from NBA.Stadium where ID = @StadiumID))
            BEGIN
                raiserror('Home team, away team and stadium must exist', 16, 1);
                rollback transaction;
                return;
            end;
    end;
go

-- Criação do trigger para adição ou alteração de jogadores
drop trigger IF EXISTS PlayersInsertOrUpdate;
go
create trigger PlayersInsertOrUpdate on NBA.Player 
after insert, update
as
    begin
        set nocount on;
        declare @CCNumber as int;
        declare @Number as int;
        declare @Height as varchar(5);
        declare @Weight as float;
        declare @Position as varchar(20);
        declare @TeamID as int;
        select @Name = [Name] from inserted;
        select @Number = [Number] from inserted;
        select @Height = Height from inserted;
        select @Weight = [Weight] from inserted;
        select @Position = Position from inserted;
        select @TeamID = Team_ID from inserted;

        -- Verifica se os campos são nulos
        if (@Number is null or @Height is null or @Weight is null or @Position is null or @TeamID is null)
            begin
                raiserror('CCNumber, Number, Height, Weight, Position and TeamID cannot be null', 16, 1);
                rollback transaction;
                return;
            end;
    
        -- Verifica se a equipa existe
        if (not exists (select 1 from NBA.Team where ID = @TeamID))
            begin
                raiserror('Team does not exist', 16, 1);
                rollback transaction;
                return;
            end;
    end;
go

-- Criação do trigger para adição ou alteração de treinadores
drop trigger IF EXISTS CoachesInsertOrUpdate;
go
create trigger CoachesInsertOrUpdate on NBA.Coach
after insert, update
as
    begin
        set nocount on;
        declare @CCNumber as int;
        select @CCNumber = CCNumber from inserted;

        -- Verifica se o CCNumber é nulo
        if (@CCNumber is null)
            begin
                raiserror('CCNumber cannot be null', 16, 1);
                rollback transaction;
                return;
            end;
        
        -- Verifica se o CCNumber já existe
        if (NBA.checkCCNumber(@CCNumber) > 0)
            BEGIN
                raiserror('CCNumber already exists', 16, 1);
                rollback transaction;
                return;
            end;
    end;
go

-- Criação do trigger para adição ou alteração de pessoas
drop trigger IF EXISTS PersonsInsertOrUpdate;
go
create trigger PersonsInsertOrUpdate on NBA.Person
after insert, update
as
    begin
        set nocount on;
        declare @Name as varchar(50);
        declare @Age as date;
        select @Name = [Name] from inserted;
        select @Age = Age from inserted;

        -- Verifica se os campos são nulos
        if (@Name is null or @Age is null)
            begin
                raiserror('CCNumber, Name, Age and ContractID cannot be null', 16, 1);
                rollback transaction;
                return;
            end;
    end;
go