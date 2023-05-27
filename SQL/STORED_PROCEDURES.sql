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

		set @query = '
			select *
			from ' + QUOTENAME(@esquema) + '.' + QUOTENAME(@tabela) +
			' where [Name] like ''%' + REPLACE(@nome, '''', '''''') + '%''';

		execute sp_executesql @query;
	end;
go

-- Procedure para adicionar jogador
drop procedure IF EXISTS NBA.adicionarJogador;
go
CREATE PROCEDURE NBA.adicionarJogador
    @CCNumber INT,
    @Name VARCHAR(50),
    @Age INT,
    @Number INT,
    @Height VARCHAR(5),
    @Weight FLOAT,
    @Position VARCHAR(20),
    @Team_ID INT = NULL,
    @Contract_ID INT = NULL
AS
BEGIN
    MERGE NBA.Person AS Target
    USING (SELECT @CCNumber, @Name, @Age, @Contract_ID) AS Source (CCNumber, [Name], Age, Contract_ID)
    ON Target.CCNumber = Source.CCNumber

    WHEN MATCHED THEN 
        UPDATE SET [Name] = Source.[Name],
                   Age = Source.Age,
                   Contract_ID = Source.Contract_ID
    WHEN NOT MATCHED THEN 
        INSERT (CCNumber, [Name], Age, Contract_ID)
        VALUES (Source.CCNumber, Source.[Name], Source.Age, Source.Contract_ID);

    MERGE NBA.Player AS Target
    USING (SELECT @CCNumber, @Number, @Height, @Weight, @Position, @Team_ID) AS Source (CCNumber, Number, Height, [Weight], Position, Team_ID)
    ON Target.CCNumber = Source.CCNumber

    WHEN MATCHED THEN 
        UPDATE SET Number = Source.Number,
                   Height = Source.Height,
                   [Weight] = Source.[Weight],
                   Position = Source.Position,
                   Team_ID = Source.Team_ID
    WHEN NOT MATCHED THEN 
        INSERT (CCNumber, Number, Height, [Weight], Position, Team_ID)
        VALUES (Source.CCNumber, Source.Number, Source.Height, Source.[Weight], Source.Position, Source.Team_ID);
END;
GO

-- Procedure para apagar jogador
drop procedure IF EXISTS NBA.apagarJogador;
go
CREATE PROCEDURE NBA.apagarJogador
	@CCNumber int
as
	delete from NBA.Player where CCNumber = @CCNumber;
	delete from NBA.Person where CCNumber = @CCNumber;
go