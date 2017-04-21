USE [LogDataBase]
GO
CREATE PROCEDURE FilterByPassword
AS
Begin
if exists(select * from Passwords)
Select * from FilteredByUsername where exists(select * from Passwords where
FilteredByUsername.Password like '%' + Password + '%')
else
select * from FilteredByUsername;
End;
Go
