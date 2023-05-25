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

