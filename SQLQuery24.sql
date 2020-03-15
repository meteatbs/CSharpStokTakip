CREATE PROCEDURE [dbo].[UpdateItem] 
@UrunID INT,
@Birimi NVARCHAR(50),
@UrunAdi nvarchar(150),
@TeslimZamani nvarchar(50),
@StokYeri nvarchar(50),
@Marka nvarchar(50),
@UreticiFirma nvarchar(50),
@UrunFiyati int,
@Photo image


AS


BEGIN


UPDATE [dbo].[Urun]
   SET [Birimi] = @Birimi
      ,[UrunAdi] = @UrunAdi
      ,[Teslimzamani] = @TeslimZamani
      ,[Stokyeri] = @StokYeri
      ,[Marka] = @Marka
      ,[UreticiFirma] = @UreticiFirma
      ,[UrunFiyati] = @UrunFiyati
      ,[Photo] = @Photo
 WHERE [UrunID]=@UrunID




END