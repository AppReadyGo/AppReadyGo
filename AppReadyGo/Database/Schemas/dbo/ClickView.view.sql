CREATE view [dbo].[ClickView]
as
select 
pv.ApplicationId,
c.PageViewId,
a.Description,
pv.path,
pv.ScreenWidth,
pv.ScreenHeight,
pv.ClientHeight,
pv.ClientWidth,
c.X,
c.Y,
c.Date,
c.Orientation
from applications a
join PageViews pv on pv.ApplicationId = a.id
join clicks c on c.PageViewId = pv.id

