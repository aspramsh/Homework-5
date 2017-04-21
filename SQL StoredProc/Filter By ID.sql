CREATE PROCEDURE FilterByIDs
AS
Begin
if exists(select * from IDs)
Select * from FilteredByMessages where exists(select * from IDs where exists(select * from 
TimeRange Where FilteredByMessages.ID = ID and FilteredByMessages.LogTime > StartTime and 
FilteredByMessages.LogTime < EndTime))
else
select * from FilteredByMessages where exists(select * from TimeRange where 
FilteredByMessages.LogTime > StartTime and 
FilteredByMessages.LogTime < EndTime);
End;
Go