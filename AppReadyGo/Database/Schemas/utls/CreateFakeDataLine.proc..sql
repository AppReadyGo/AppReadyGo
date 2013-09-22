CREATE PROCEDURE [utls].[CreateFakeDataLine]
	-- Add the parameters for the stored procedure here
	@applicationId int,
	@clientHeight int = 800,
	@clientWidth int = 480,
	@path nvarchar(256),
	@x int,
	@y int,
	@offsetAroundThePoint int = 10,
	@numberOfPointsToCreate int = 50000,
	@interval int = 10,
	@lastX int = 420
AS


BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @i INT
	
	WHILE (@x < @lastX)
--	BEGIN
	BEGIN   PRINT @x
	EXEC	@i = [utls].[CreateFakeData]
	    @applicationId = @applicationId ,
		@clientHeight = @clientHeight,
		@clientWidth = @clientWidth,
		@path = @path,
		@x = @x,
		@y = @y ,
		@offsetAroundThePoint = @offsetAroundThePoint,
		@numberOfPointsToCreate = @numberOfPointsToCreate
		
	SET @x = @x + @interval
	
	END
	print N'Created ' +  + convert(varchar(9), @i)
END

