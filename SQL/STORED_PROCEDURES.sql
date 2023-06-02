use p4g1;

-- Procedure para barra de pesquisa
drop procedure IF EXISTS NBA.pesquisarPorNome;
go
create procedure NBA.pesquisarPorNome
	@nome varchar(50),
	@esquema varchar(50),
    @tabela varchar(50)
as
	begin
		declare @query nvarchar(MAX);

		set @query = 'select * from ' + QUOTENAME(@esquema) + '.' + QUOTENAME(@tabela) + ' where [Name] like ''%' + REPLACE(@nome, '''', '''''') + '%''';

		execute sp_executesql @query;
	end;
go

-- Procedure para adicionar ou alterar jogador
drop procedure IF EXISTS NBA.adicionarAlterarJogador;
go
create procedure NBA.adicionarAlterarJogador
    @CCNumber int,
    @Name varchar(50),
    @Age int,
    @Number int,
    @Height varchar(5),
    @Weight float,
    @Position varchar(20),
    @Team_ID int,
    @Contract_ID int = null,
	@Command varchar(20),
	@NumberOrTeamIDChanged varchar(3),
	@Points float = null,
    @Assists float = null,
    @Rebounds float = null,
    @Blocks float = null,
    @Steals float = null,
    @FG float = null,
    @PT3 float = null
as
	begin
		declare @errorsCount as int = 0;

		if (@NumberOrTeamIDChanged = 'Sim')
			if (@Number is not null and exists(select 1 from (NBA.Team as T inner join NBA.Player as P on T.ID = P.Team_ID) where Team_ID = @Team_ID and Number = @Number))
				begin
					set @errorsCount = @errorsCount + 1;
					raiserror('Não foi possível adicionar/alterar jogador! Já existe um jogador da mesma equipa com o mesmo número de equipamento.', 16, 1);
				end

		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Person values(@CCNumber, @Name, @Age, @Contract_ID);
							insert into NBA.Player values(@CCNumber, @Number, @Height, @Weight, @Position, @Team_ID);
							insert into NBA.Average_Individual_Numbers values (@Points, @Assists, @Rebounds, @Blocks, @Steals, @FG ,@PT3, @CCNumber);
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogador não inserido! Algum dado está incorreto', 16, 1);
					end catch
				else if (@Command = 'alterar')
					begin try
						begin tran
							update NBA.Person
							set [Name] = @Name, Age = @Age, Contract_ID = @Contract_ID
							where CCNumber = @CCNumber;

							update NBA.Player
							set [Number] = @Number, Height = @Height, [Weight] = @Weight, Position = @Position, Team_ID = @Team_ID
							where CCNumber = @CCNumber;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogador não alterado! Algum dado está incorreto', 16, 1);
					end catch
					begin try
						begin tran
							update NBA.Average_Individual_Numbers
							set Points = @Points, Assists = @Assists, Rebounds = @Rebounds, Blocks = @Blocks, Steals = @Steals, [FG%] = @FG, [3PT%] = @PT3
							where Player_CCNumber = @CCNumber;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Estatística não alterads! Algum dado está incorreto', 16, 1);
					end catch
			end
	end
go

-- Procedure para apagar jogador
drop procedure IF EXISTS NBA.apagarJogador;
go
create procedure NBA.apagarJogador
	@CCNumber int
as
	delete from NBA.Player where CCNumber = @CCNumber;
	delete from NBA.Person where CCNumber = @CCNumber;
go

-- Procedure para adicionar ou alterar treinador
drop procedure IF EXISTS NBA.adicionarAlterarTreinador;
go
create procedure NBA.adicionarAlterarTreinador
    @CCNumber int,
    @Name varchar(50),
    @Age int,
    @Contract_ID int = null,
	@Command varchar(20)
as
	begin
		declare @errorsCount as int = 0;
	
		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Person values(@CCNumber, @Name, @Age, @Contract_ID);
							insert into NBA.Coach values(@CCNumber);
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogador não inserido! Algum dado está incorreto', 16, 1);
					end catch
				else if (@Command = 'alterar')
					begin try
						begin tran
							update NBA.Person
							set [Name] = @Name, Age = @Age, Contract_ID = @Contract_ID
							where CCNumber = @CCNumber;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogador não alterado! Algum dado está incorreto', 16, 1);
					end catch
			end
	end
go

-- Procedure para apagar treinador
drop procedure IF EXISTS NBA.apagarTreinador;
go
create procedure NBA.apagarTreinador
	@CCNumber int
as
	delete from NBA.Coach where CCNumber = @CCNumber;
	delete from NBA.Person where CCNumber = @CCNumber;
go

-- Procedure para adicionar ou alterar equipa
drop procedure IF EXISTS NBA.adicionarAlterarEquipa;
go
create procedure NBA.adicionarAlterarEquipa
	@ID int = null,
    @Name varchar(50),
	@City varchar(50),
	@Conference varchar(50),
    @FoundYear int,
    @CoachCCNumber int,
	@OwnerCCNumber int,
	@Command varchar(20),
	@CoachChanged varchar(3)
as
	begin
		declare @errorsCount as int = 0;
		declare @nextID as int = (select max(ID)+1 from NBA.Team);
		declare @nextIDStadium as int = (select max(ID)+1 from NBA.Stadium);
		declare @s as varchar(7) = 'Stadium';
		declare @nameStadium as varchar(50) = @Name+ ' ' + @s;
		declare @nextIDContract as int = (select max(ID)+1 from NBA.[Contract]);
		declare @c as varchar(8) = 'Contract';
		declare @descriptionContract as varchar(50) = @Name+ ' ' + @c;
		
		if ((@OwnerCCNumber is not null and exists (select 1 from NBA.Coach where CCNumber = @OwnerCCNumber)) or (@OwnerCCNumber is not null and exists (select 1 from NBA.Player where CCNumber = @OwnerCCNumber)))
			begin
				set @errorsCount = @errorsCount + 1;
				raiserror('Não foi possível adicionar/alterar equipa! O presidente iserido é um treinaodr/jogador.', 16, 1);
			end

		if (@CoachChanged = 'Sim')
			begin
				if (@CoachCCNumber is not null and exists (select 1 from NBA.Team where Coach_CCNumber = @CoachCCNumber) or @CoachCCNumber is not null and exists (select 1 from NBA.Person where CCNumber = @CoachCCNumber and Contract_ID is not null))
					begin
						set @errorsCount = @errorsCount + 1;
						raiserror('Não foi possível adicionar/alterar equipa! O treinador inserido já pertence a outra equipa ou já tem contrato.', 16, 1);
					end
			end

		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Team values(@nextID, @Name, @City, @Conference, @FoundYear, @OwnerCCNumber, @CoachCCNumber, 0);
							insert into NBA.Stadium values(@nextIDStadium, @nameStadium, @City, 20000, @nextID);
							insert into NBA.[Contract] values(@nextIDContract, @descriptionContract, 5000000, getdate(), dateadd(year, 5, getdate()));
							update NBA.Person set Contract_ID = @nextIDContract where CCNumber = @CoachCCNumber;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Equipa não inserida! Algum dado está incorreto', 16, 1);
					end catch
				else if (@Command = 'alterar')
					begin try
						begin tran
							update NBA.Team
							set [Name] = @Name, Conference = @Conference, Found_Year = @FoundYear, Owner_CCNumber = @OwnerCCNumber, Coach_CCNumber = @CoachCCNumber 
							where ID = @ID;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Equipa não alterado! Algum dado está incorreto', 16, 1);
					end catch
			end
	end
go

-- Procedure para apagar equipa
drop procedure IF EXISTS NBA.apagarEquipa;
go
create procedure NBA.apagarEquipa
	@ID int
as
	begin 
		declare @coachCCNumber as int = (select Coach_CCNumber from NBA.Team where ID = @ID);

		if exists(select * from NBA.Game where Home_Team_ID = @ID or Away_Team_ID = @ID)
			begin
				begin try
					begin tran
						update NBA.Team set disabled = 1 where ID = @ID;
						update NBA.Person set Contract_ID = null where CCNumber = @coachCCNumber;
					commit tran
				end try
				begin catch
					rollback tran
					raiserror('Erro! Equipa não desativada', 16, 1);
				end catch
				
			end
		else
			begin
				begin try
					begin tran
						delete from NBA.Team where ID = @ID;
						update NBA.Person set Contract_ID = null where CCNumber = @coachCCNumber;
					commit tran
				end try
				begin catch
					rollback tran
					raiserror('Erro! Equipa não apagada', 16, 1);
				end catch
			end
	end
go

-- Procedure para adicionar ou alterar jogo
drop procedure IF EXISTS NBA.adicionarAlterarJogo;
go
create procedure NBA.adicionarAlterarJogo
	@ID int = null,
    @Time time,
	@Date date,
	@HomeScore int = null,
    @AwayScore int = null,
    @HomeTeamID int,
	@AwayTeamID int,
	@StadiumID int,
	@Command varchar(30)
as
	begin
		declare @errorsCount as int = 0;
		declare @nextID as int = (select max(ID)+1 from NBA.Game);

		if ((@HomeScore is null and @AwayScore is not null) or (@HomeScore is not null and @AwayScore is null))
			begin
				set @errorsCount = @errorsCount + 1;
				raiserror('Não foi possível adicionar/alterar jogo! O resultado está incompleto.', 16, 1);
			end

		if (@HomeTeamID = @AwayTeamID)
			begin
				set @errorsCount = @errorsCount + 1;
				raiserror('Não foi possível adicionar/alterar jogo! O jogo tem de ser entre equipas diferentes.', 16, 1);
			end

		if (@HomeTeamID != @StadiumID and @AwayTeamID != @StadiumID)
			begin
				set @errorsCount = @errorsCount + 1;
				raiserror('Não foi possível adicionar/alterar jogo! A arena tem de pertencer a uma das equipas.', 16, 1);
			end

		if (@Date < getdate() and @HomeScore is null and @AwayScore is null)
			begin
				set @errorsCount = @errorsCount + 1;
				raiserror('Não foi possível adicionar jogo! Como o jogo já aconteceu tem de haver resultado.', 16, 1);
			end

		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Game values(@nextID, @Time, @Date, @HomeScore, @AwayScore, @HomeTeamID, @AwayTeamID, @StadiumID);
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogo não inserido! Algum dado está incorreto', 16, 1);
					end catch
				else if (@Command = 'alterar')
					begin try
						begin tran
							update NBA.Game
							set [Time]  = @Time, [Date] = @Date, Home_Score = @HomeScore, Away_Score = @AwayScore, Home_Team_ID = @HomeTeamID,  Away_Team_ID = @AwayTeamID, Stadium_ID = @StadiumID
							where ID = @ID;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Jogo não alterado! Algum dado está incorreto', 16, 1);
					end catch
			end
	end
go

-- Procedure para apagar jogo
drop procedure IF EXISTS NBA.apagarJogo;
go
create procedure NBA.apagarJogo
	@ID int
as
	delete from NBA.Ticket where Game_ID = @ID;
	delete from NBA.Game where ID = @ID;
go

-- Procedure para adicionar ou alterar bilhetes de jogos
drop procedure IF EXISTS NBA.adicionarAlterarBilhetes;
go
create procedure NBA.adicionarAlterarBilhetes
	@Type varchar(30),
    @Price float,
	@Restantes int,
	@Game_ID int,
	@Team_ID int,
	@Command varchar(30)
as
	begin
		declare @errorsCount as int = 0;

		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Ticket values(@Type, @Price, @Restantes, @Game_ID, @Team_ID);
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Bilhetes não inseridos! Algum dado está incorreto', 16, 1);
					end catch
				else if (@Command = 'alterar')
					begin try
						begin tran
							update NBA.Ticket
							set Price = @Price, Restantes = @Restantes
							where [Type] = @Type and Game_ID = @Game_ID;
						commit tran
					end try
					begin catch
						rollback tran
						raiserror('Bilhetes não alterados! Algum dado está incorreto', 16, 1);
					end catch
			end
	end
go