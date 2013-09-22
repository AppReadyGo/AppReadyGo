CREATE PROCEDURE [utls].[CreateFakeData]
	-- Add the parameters for the stored procedure here
	@applicationId int,
	@clientHeight int = 800,
	@clientWidth int = 480,
	@path nvarchar(256),
	@x int,
	@y int,
	@offsetAroundThePoint int = 10,
	@numberOfPointsToCreate int = 50000
AS


BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @i INT
	DECLARE @pageViewId INT
	DECLARE @screenHeight INT
	DECLARE @vClientHeight INT
	DECLARE @date date
	DECLARE @rad int,@vX int ,@vY int, @vSign int
	SET @i = 1
	SET NOCOUNT ON;
	
	select @pageViewId = max (c.PageViewId) 
	  from dbo.[ClickView] c
	 where c.ApplicationId = @applicationId
	   and c.ClientWidth = @clientWidth
	   and c.ClientHeight = @clientHeight
	   and c.Path = @path; 
	print  @pageViewId
	if @pageViewId > 0 
	begin
	    select @screenHeight = p.ScreenHeight, @vClientHeight = p.ClientHeight, @date = p.Date
	      from PageViews p
	     where p.Id = @pageViewId; 
	     
		WHILE (@i <=@numberOfPointsToCreate)
		BEGIN
		
		SET @i  = @i + 1
		if rand() > 0.5 set @vSign = -1 else set @vSign = 1
	    set @vX = @offsetAroundThePoint*RAND()*@vSign; 
	    
	    if rand() > 0.5 set @vSign = -1 else set @vSign = 1
		set @vY = rand()* (sqrt(power(@offsetAroundThePoint,2) - power(@vX,2)))*@vSign
	
		PRINT 'x =' +  convert(varchar(4), @vX) + ' y= ' + convert(varchar(4), @vY)
		
		INSERT INTO [dbo].[Clicks]
			   ([Date]
			   ,[X]
			   ,[Y]
			   ,[PageViewId]
			   ,[Orientation])
		 VALUES
			   (@date
			   ,@x + @vX
			   ,@y + @vY
			   ,@pageViewId
			   ,0)
		  	   
	 INSERT INTO [dbo].[ViewParts]
           ([X]
           ,[Y]
           ,[StartDate]
           ,[FinishDate]
           ,[Orientation]
           ,[PageViewId])
     VALUES
           (0
           ,0
           ,@date
           ,@date
           ,0
           ,@pageViewId)
           
      --if the screen is longer then device, we create a some amount of "scrolls" by user
      -- to make the fake more real
      if   @screenHeight != @clientHeight and @i > @numberOfPointsToCreate*0.8
      begin
         INSERT INTO [dbo].[ViewParts]
           ([X]
           ,[Y]
           ,[StartDate]
           ,[FinishDate]
           ,[Orientation]
           ,[PageViewId])
         VALUES
           (0
           ,(@screenHeight - @vClientHeight) * RAND()
           ,@date
           ,@date
           ,0
           ,@pageViewId)
      end
	END 
    end
    else PRINT 'PageView was not found, nothing inserted'
	print N'Created ' +  + convert(varchar(9), @i)
END
