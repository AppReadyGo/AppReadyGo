CREATE view [dbo].[ScrollView]
as
select 
pv.ApplicationId,
s.PageViewId,
a.Description,
pv.path,
pv.ScreenWidth,
pv.ClientHeight,
s.Id,
s.FirstTouchId,
s.LastTouchId
from applications a
join PageViews pv on pv.ApplicationId = a.id
join scrolls s on s.PageViewId = pv.id
