CREATE PROCEDURE FilterByIDs
AS
Begin
if exists(select * from IDs)
Select * from FilteredByMessages where exists(select * from IDs where
FilteredByMessages.ID = ID)
else
select * from FilteredByMessages;
End;
Go