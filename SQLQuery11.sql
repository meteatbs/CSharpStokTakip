CREATE PROCEDURE dbo.UrunSil
@UrunID int
AS
DELETE Urun WHERE UrunID=@UrunID 