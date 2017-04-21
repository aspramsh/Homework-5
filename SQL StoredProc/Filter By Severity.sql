CREATE PROCEDURE FilterBySeverities
AS
Begin
if exists(select * from Severities)
Select * from FilteredByPassword where exists(select * from Severities where
FilteredByPassword.Severity = Severity)
else
select * from FilteredByPassword;
End;
Go