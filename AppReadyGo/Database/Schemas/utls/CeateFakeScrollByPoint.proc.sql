CREATE PROCEDURE [utls].[CeateFakeScrollByPoint]
	-- Add the parameters for the stored procedure here
	(@applicationId int,
	@pageViewId int,
	@pointId int,
	@offset int)
/*returns INTEGER	*/
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @i INT
	DECLARE @returnValue as INTEGER
	DECLARE @vX int ,@vY int,@scrollPointId int
	
	SET NOCOUNT ON;
	print N'Starting' + + convert(varchar(9), @pointId)  
	begin
	select @vX = c.X, @vY = c.Y
	  from dbo.Clicks c
	 where c.id= @pointId; 
	end 
	print  @pageViewId
	
		begin
		   select @scrollPointId = c.ID
			from Clicks c
			where not exists (select null
								from scrolls  s
							  where s.FirstTouchId = c.ID) 
			 and not exists  (select null
								from scrolls  s
							  where s.LastTouchId = c.ID) 
			 and c.x between @vX - @offset and  @vX + @offset and c.y between @vY - @offset and @vY + @offset 
			 and c.pageviewid = @pageViewId
 			 and c.ID != @pointId;
 		end	
 		if @scrollPointId > 0
 		begin
 			   INSERT INTO [dbo].[Scrolls]
				   ([FirstTouchId]
				   ,[LastTouchId]
				   ,[PageViewId])
				VALUES
				   (@pointId
				   ,@scrollPointId
				   ,@pageViewId);
				   
			print N'Created, LAst point id is' + + convert(varchar(9), @scrollPointId)  
		end
		else print N'Nothing was created for' + + convert(varchar(9), @pointid)
end
