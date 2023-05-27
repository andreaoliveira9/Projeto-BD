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
	@NumberOrTeamIDChanged varchar(3)
as
	begin
		declare @errorsCount as int = 0;
		if (@Command = 'adicionar')
			if (@CCNumber is not null and exists(select 1 from NBA.Person where CCNumber = @CCNumber))
				begin
					set @errorsCount = @errorsCount + 1;
					raiserror('Não foi possível adicionar jogador! O número de cartão de cidadão inserido já se encontra registado.', 16, 1);
				end

		if (@NumberOrTeamIDChanged = 'Sim')
			if (@Number is not null and exists(select 1 from (NBA.Team as T inner join NBA.Player as P on T.ID = P.Team_ID) where Team_ID = @Team_ID and Number = @Number))
				begin
					set @errorsCount = @errorsCount + 1;
					raiserror('Não foi possível adicionar jogador! Já existe um jogador da mesma equipa com o mesmo número de equipamento.', 16, 1);
				end

		if (@errorsCount = 0)
			begin
				if (@Command = 'adicionar')
					begin try
						begin tran
							insert into NBA.Person values(@CCNumber, @Name, @Age, @Contract_ID);
							insert into NBA.Player values(@CCNumber, @Number, @Height, @Weight, @Position, @Team_ID);
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
