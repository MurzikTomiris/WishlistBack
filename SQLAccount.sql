CREATE TABLE Accounts (
    id int primary key identity,
    [login] NVARCHAR(1000) NOT NULL,
    [password] NVARCHAR(1000) NOT NULL,
    [name] NVARCHAR(1000),
    email NVARCHAR(1000)
);


create proc pCreateAccount --"user", "qwerty", "user", "user@gmail.com"
    @login NVARCHAR(1000),
    @password NVARCHAR(1000),
    @name NVARCHAR(1000),
    @email NVARCHAR(1000)
AS
IF EXISTS(
		select 1 from Accounts
		WHERE [login] = @login)
		BEGIN
		SELECT 'exists'
		RETURN
		END


INSERT INTO Accounts
           ([login]
		   ,[password]
		   ,[name]
           ,email)
     VALUES
           (@login,
		   @password,
		   @name,
		   @email
		   )
		   SELECT 'ok'


create proc pGetAllAccounts 
AS
    SELECT id
	       ,[login]
		   ,[password]
		   ,[name]
           ,email
    FROM Accounts;


create proc pGetAccountById --1
@id int
as
select id
	   ,[login]
	   ,[password]
	   ,[name]
       ,email
from Accounts
where id=@id


create proc pUpdateAccount --1, "user2", "qwerty", "user", "user@gmail.com"
@id int,
@login NVARCHAR(1000),
@password NVARCHAR(1000),
@name NVARCHAR(1000),
@email NVARCHAR(1000)
AS
UPDATE Accounts
   SET 
   [login] = @login,
   [password] = @password,
   [name] = @name, 
   email = @email
 WHERE id = @id


alter proc pDeleteAccount --1
@id int
as
BEGIN
    SET NOCOUNT ON;

    -- Проверяем, существует ли аккаунт
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE id = @id)
    BEGIN
        SELECT 'account_not_found' AS status;
        RETURN;
    END

    -- Удаляем все giftcards, связанные с вишлистами аккаунта
    DELETE FROM Giftcards 
    WHERE wishlistId IN (SELECT id FROM Wishlists WHERE accountId = @id);

    -- Удаляем все вишлисты аккаунта
    DELETE FROM Wishlists WHERE accountId = @id;

    -- Удаляем сам аккаунт
    DELETE FROM Accounts WHERE id = @id;

    SELECT 'ok' AS status;
END;

create proc pLogin --"user2", "qwerty"
@login NVARCHAR(1000),
@password NVARCHAR(1000)
as
select *from Accounts
where [login]=@login
and [password]=@password