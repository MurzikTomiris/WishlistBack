CREATE TABLE Giftcards (
    id INT PRIMARY KEY IDENTITY,
    title NVARCHAR(1000) NOT NULL,
    description NVARCHAR(2000),
    price DECIMAL,
    link NVARCHAR(1000),
    image NVARCHAR(2000),
    wishlistId INT FOREIGN KEY REFERENCES Wishlists(id),
    isReserved INT DEFAULT 0
);

CREATE proc pCreateGiftcard --"flowers", "flowers", 10000, "google.com", "google.com", 1
    @title NVARCHAR(1000),
    @description NVARCHAR(2000),
    @price DECIMAL(10,2),
    @link NVARCHAR(1000),
    @image NVARCHAR(1000),
    @wishlistId INT
AS
    INSERT INTO Giftcards (title, description, price, link, image, wishlistId)
    VALUES (@title, @description, @price, @link, @image, @wishlistId)
    SELECT 'ok'


CREATE proc pGetAllGiftcards
AS
    SELECT id, title, description, price, link, image, wishlistId, isReserved
    FROM Giftcards

CREATE proc pGetGiftcardByWishlistsId --2
    @wishlistId INT
AS
    SELECT id, title, description, price, link, image, wishlistId, isReserved
    FROM Giftcards
    WHERE wishlistId = @wishlistId


CREATE proc pGetGiftcardById --1
    @id INT
AS
    SELECT id, title, description, price, link, image, wishlistId, isReserved
    FROM Giftcards
    WHERE id = @id

CREATE proc pGetGiftcardByLink --"1f5ec184c91f"
	@listLink NVARCHAR(2000)
AS
    SELECT g.id, g.title, g.description, g.price, g.link, g.image, g.wishlistId, g.isReserved
    FROM Giftcards as g
    INNER JOIN Wishlists as w ON w.id = g.wishlistId
    WHERE w.listLink = @listLink;

CREATE proc pUpdateGiftcard --2, "flowers", "someflowers", 10000, "google.com", "google.com", 1, 0
    @id INT,
    @title NVARCHAR(1000),
    @description NVARCHAR(2000),
    @price DECIMAL(10,2),
    @link NVARCHAR(1000),
    @image NVARCHAR(1000),
    @wishlistId INT,
    @isReserved INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Giftcards WHERE id = @id)
    BEGIN
        SELECT 'giftcard_not_found'
        RETURN
    END

    UPDATE Giftcards
    SET title = @title,
        description = @description,
        price = @price,
        link = @link,
        image = @image,
        wishlistId = @wishlistId,
        isReserved = @isReserved
    WHERE id = @id

    SELECT 'ok'
END


CREATE proc pDeleteGiftcard --6
    @id INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Giftcards WHERE id = @id)
    BEGIN
        SELECT 'giftcard_not_found'
        RETURN
    END

    DELETE FROM Giftcards WHERE id = @id

    SELECT 'ok'
END


CREATE proc pReserveGiftcard --3
    @id INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Giftcards WHERE id = @id)
    BEGIN
        SELECT 'giftcard_not_found'
        RETURN
    END

    UPDATE Giftcards
    SET isReserved = CASE WHEN isReserved = 1 THEN 0 ELSE 1 END
    WHERE id = @id

    SELECT 'ok'
END