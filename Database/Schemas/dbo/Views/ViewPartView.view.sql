
CREATE view [dbo].[ViewPartView]
as
select 
pv.ApplicationId,
v.PageViewId,
a.Description,
pv.path,
pv.ScreenWidth,
pv.ClientHeight,
v.X,
v.Y,
v.Orientation,
v.StartDate,
v.FinishDate
from applications a
join PageViews pv on pv.ApplicationId = a.id
join ViewParts v on v.PageViewId = pv.id