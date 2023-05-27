use p4g1;

-- Trigger para adição de pessoas
drop trigger IF EXISTS NBA.ChecknPersonInsertOrUpdate;
go
CREATE TRIGGER NBA.ChecknPersonInsertOrUpdate
ON NBA.Person
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @InsertedContractID INT;
    SELECT @InsertedContractID = Contract_ID FROM inserted;

    IF @InsertedContractID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM NBA.[Contract] WHERE ID = @InsertedContractID)
    BEGIN
        RAISERROR ('No contract exists with the provided Contract_ID', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- Trigger para adição de jogadores
drop trigger IF EXISTS NBA.ChecknPlayerInsertOrUpdate;
go
CREATE TRIGGER NBA.ChecknPlayerInsertOrUpdate
ON NBA.Player
AFTER INSERT, UPDATE
AS
BEGIN
	declare @InsertedTeamID int;
	SELECT @InsertedTeamID = Team_ID FROM inserted;

	IF @InsertedTeamID IS NOT NULL AND NOT EXISTS(SELECT 1 FROM NBA.Team WHERE ID = @InsertedTeamID)
    BEGIN
        RAISERROR ('No team exists with the provided Team_ID', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO