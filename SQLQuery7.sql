CREATE PROCEDURE dbo.UrunAra
@UrunAdi  nvarchar(150)
AS
SELECT * FROM Urun WHERE UrunAdi LIKE @UrunAdi+'%'