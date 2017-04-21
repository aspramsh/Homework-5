CREATE PROCEDURE FilterByMessages
AS
Begin
if exists(select * from Messages)
Select * from FilteredByIPs where exists(select * from Messages where
FilteredByIPs.Message = Message)
else
select * from FilteredByIPs;
End;
Go