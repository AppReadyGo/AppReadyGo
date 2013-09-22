CREATE PROCEDURE [utls].[CreateFakeScrolls]
	(@applicationId int,
	@pageViewId int,
	@offset int,
	@numberOfPointsToCreatePerDate int,
	@date date)
as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @return_value INT
	DECLARE @i INT
	declare @pointId int
	DECLARE @currentDate date
	SET @i = 1
	set @currentDate = @date
	
	Declare points CURSOR READ_ONLY
	FOR
	select top 20 id
	from Clicks s
	where s.Date =  @currentDate
	  and s.PageViewId = @pageViewId;
	
	
	
	SET NOCOUNT ON;
	
	print N'STAART'  
	    
	    open points
	    fetch next from points 
	    into @pointId
	  
	    print N'PointId ' +  + convert(varchar(9),  @pointId)  
		
		BEGIN
		WHILE @@FETCH_STATUS = 0
		BEGIN
		  EXEC	@return_value = [utls].[CeateFakeScrollByPoint]
				@applicationId = @applicationId,
				@pageViewId = @pageViewId,
				@pointId = @pointId,
				@offset = @offset
				
		  FETCH NEXT FROM points
		  INTO @pointId
		  print N'PointId inside ' +  + convert(varchar(9),  @pointId)  
		END
		CLOSE points
		DEALLOCATE points
		
		
	    SET @currentDate  = DATEADD(day,1,@currentDate) 
		
---	end 	

           
     
	END 
	print N'Created ' +  + convert(varchar(9), @i)
END
