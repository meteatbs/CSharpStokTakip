USE [DotnetMob]
GO 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImageAddOrEdit]
@ImageID INT,
@Title VARCHAR(50),
@Image IMAGE
AS BEGIN
IF(@ImageID=0)
BEGIN
INSERT INTO Image
(Title,Image)Values
(@Title,@Image)
END
ELSE
BEGIN
UPDATE Image
SET Title=@Title,Image=@Image WHERE ImageID=@ImageID
END
END