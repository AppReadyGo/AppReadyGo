CREATE view [dbo].[ClickView]
as
select 
pv.ApplicationID,
c.PageViewID,
a.Description,
pv.Path,
pv.ScreenWidth,
pv.ScreenHeight,
pv.ClientHeight,
pv.ClientWidth,
c.X,
c.Y,
c.Date,
c.Orientation
from Applications a
join PageViews pv on pv.ApplicationID = a.ID
join clicks c on c.PageViewId = pv.ID

