CREATE PROCEDURE FilterByIPs
AS
Begin
if exists(select * from IPs)
Select * from FilteredBySeverities where exists(select * from IPs where
FilteredBySeverities.IP = IP)
else
select * from FilteredBySeverities;
End;
Go