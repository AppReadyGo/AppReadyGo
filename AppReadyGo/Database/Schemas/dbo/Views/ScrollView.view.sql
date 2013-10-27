CREATE VIEW [dbo].[ScrollView]
AS
SELECT 
pv.ApplicationID,
s.PageViewId,
a.Description,
pv.Path,
pv.ScreenWidth,
pv.ClientHeight,
s.ID,
s.FirstTouchId,
s.LastTouchId
from Applications a
join PageViews pv on pv.ApplicationID = a.ID
join Scrolls s on s.PageViewId = pv.ID
