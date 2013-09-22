CREATE PROCEDURE [utls].[CreateFakePageView]
	-- Add the parameters for the stored procedure here
	@applicationId int,
	@screenHeight int = 800,
	@screenWidth int = 480,
	@path nvarchar(256),
	@x int,
	@y int,
	@offsetY  int = 10,
	@offsetX  int = 0,
	@numberOfViewPartsToCreate int = 100,
	@numberOfRandomVPToCreate int = 30
AS


BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @i INT
	DECLARE @pageViewId INT
	DECLARE @vScreenHeight INT
	DECLARE @vClientHeight INT
	DECLARE @date date
	DECLARE @rad int,@vX int ,@vY int
	SET @i = 1
	SET NOCOUNT ON;
	
	select @pageViewId = max (c.PageViewId) 
	  from dbo.[ClickView] c
	 where c.ApplicationId = @applicationId
	   and c.ScreenWidth = @screenWidth
	   and c.ScreenHeight = @screenHeight
	   and c.Path = @path; 
	print  @pageViewId
	if @pageViewId > 0 
	begin
	    select @vScreenHeight = p.ScreenHeight, @vClientHeight = p.ClientHeight, @date = p.Date
	      from PageViews p
	     where p.Id = @pageViewId; 
	     
		WHILE (@i <=@numberOfViewPartsToCreate + @numberOfRandomVPToCreate)
		BEGIN
		
		 	SET @i  = @i + 1
			
			if @i <= @numberOfViewPartsToCreate 	 
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
					   ,0
					   ,@date
					   ,@date
					   ,0
					   ,@pageViewId)
             end
				
				  if  @i >  @numberOfViewPartsToCreate 	 
				  begin
				     set @vX = @offsetX *RAND(); 
		    
	   				 set @vY = @offsetY* rand();
				
					 PRINT 'x =' +  convert(varchar(4), @vX) + ' y= ' + convert(varchar(4), @vY)
					 
					 INSERT INTO [dbo].[ViewParts]
					   ([X]
					   ,[Y]
					   ,[StartDate]
					   ,[FinishDate]
					   ,[Orientation]
					   ,[PageViewId])
					 VALUES
					   (@vX
					   ,@vY
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

