CREATE TABLE Wishlists (
    id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(1000) NOT NULL,
    description NVARCHAR(2000),
    listLink NVARCHAR(2000),
    accountId INT FOREIGN KEY REFERENCES Accounts(id),
    isActive INT DEFAULT 1
);

CREATE proc pCreateWishlist --"Birthday", "what i want for birthday", "gfdsaasdfg", 2
    @name NVARCHAR(1000),
    @description NVARCHAR(2000),
    @listLink NVARCHAR(1000),
    @accountId INT
AS
    INSERT INTO Wishlists (name, description, listLink, accountId)
    VALUES (@name, @description, @listLink, @accountId)
    SELECT 'ok'


CREATE proc pGetAllWishlists
AS
    SELECT id, name, description, listLink, accountId, isActive
    FROM Wishlists

CREATE proc pGetWishlistByAccountId --2
    @accountId INT
AS
    SELECT id, name, description, listLink, accountId, isActive
    FROM Wishlists
    WHERE accountId = @accountId

CREATE proc pGetWishlistById --1
    @id INT
AS
    SELECT id, name, description, listLink, accountId, isActive
    FROM Wishlists
    WHERE id = @id


CREATE proc pUpdateWishlist --1, "Birthday", "what i want for birthday", "lkjhgfd", 2, 1
    @id INT,
    @name NVARCHAR(1000),
    @description NVARCHAR(2000),
    @listLink NVARCHAR(1000),
    @accountId INT,
    @isActive INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Wishlists WHERE id = @id)
    BEGIN
        SELECT 'wishlist_not_found'
        RETURN
    END

    UPDATE Wishlists
    SET name = @name,
        description = @description,
        listLink = @listLink,
        accountId = @accountId,
        isActive = @isActive
    WHERE id = @id
    SELECT 'ok'
END

alter proc pDeleteWishlist --2
    @id INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Wishlists WHERE id = @id)
    BEGIN
        SELECT 'wishlist_not_found'
        RETURN
    END

	
	DELETE FROM Giftcards WHERE wishlistId = @id;
    DELETE FROM Wishlists WHERE id = @id

    SELECT 'ok'
END

CREATE PROCEDURE pDeactivateWishlist --1
    @id INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Wishlists WHERE id = @id)
    BEGIN
        SELECT 'wishlist_not_found'
        RETURN
    END

    UPDATE Wishlists
    SET isActive = CASE WHEN isActive = 1 THEN 0 ELSE 1 END
    WHERE id = @id

    SELECT 'ok'
END