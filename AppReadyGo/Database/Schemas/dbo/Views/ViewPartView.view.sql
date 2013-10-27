
CREATE view [dbo].[ViewPartView]
as
select 
pv.ApplicationID,
v.PageViewID,
a.Description,
pv.Path,
pv.ScreenWidth,
pv.ClientHeight,
v.X,
v.Y,
v.Orientation,
v.StartDate,
v.FinishDate
from Applications a
join PageViews pv on pv.ApplicationID = a.ID
join ViewParts v on v.PageViewId = pv.ID