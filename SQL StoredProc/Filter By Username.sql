USE [LogDataBase]
GO
CREATE PROCEDURE FilterByUsername
AS
if exists(select * from Usernames)
Begin
Select * from Logs where exists(select * from Usernames where
Logs.Username like '%' + Username + '%')
End;
else
select * from Logs;
go