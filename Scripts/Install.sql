CREATE LOGIN [HomeUser] WITH PASSWORD = 'noPass1234';
EXEC sp_addsrvrolemember 'HomeUser', 'sysadmin';
