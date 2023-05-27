use p4g1;

-- Trigger para adição de pessoas
drop trigger IF EXISTS NBA.CheckPersonInsertOrUpdate;
go
create trigger NBA.CheckPersonInsertOrUpdate on NBA.Person after insert
as
	begin
		declare @InsertedContractID int;
		select @InsertedContractID = Contract_ID from inserted;

		if (@InsertedContractID is not null and not exists(select 1 from NBA.[Contract] where ID = @InsertedContractID))
		begin
			raiserror ('No contract exists with the provided Contract_ID', 16, 1);
			rollback transaction;
			return;
		end
	end
GO

-- Trigger para adição de jogadores
drop trigger IF EXISTS NBA.CheckPlayerInsertOrUpdate;
go
create trigger NBA.CheckPlayerInsertOrUpdate on NBA.Player after insert, update
as
	begin
		declare @InsertedTeamID int;
		select @InsertedTeamID = Team_ID from inserted;

		if @InsertedTeamID IS NOT NULL AND NOT EXISTS(select 1 from NBA.Team where ID = @InsertedTeamID)
		begin
			raiserror ('No team exists with the provided Team_ID', 16, 1);
			rollback transaction;
			return;
		end
	end
go