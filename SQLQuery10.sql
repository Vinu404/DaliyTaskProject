alter proc Sp_cretaeddate
@cretaddate date
as
begin
select top 10 dtm.Id,dtm.JiraID,CONVERT(date,dtm.StartDate,106) as StartDate ,CONVERT(date,dtm.EndDate,106) as EndDate,
dtm.[Description],dtm.Remark,TS.TechStack,st.[Status],Md.ModuleName,dp.DeveloperName,dpt.DepartmentName
from Tbl_DailyTaskManagement as dtm inner join Tbl_TechStack as TS 
on dtm.TechstackID = ts.TechstackID inner join Tbl_Status as st on dtm.StatusID = st.StatusID inner join Tbl_Module as Md
on Dtm.Module_ID = Md.Module_ID inner join
Tbl_Developer as dp on Dtm.DeveloperID = dp.DeveloperID inner join
Tbl_Department as dpt on Dtm.DepartmentID = dpt.DepartmentID inner join Login_tbl as lg
on lg.ID = Dtm.UserID
where Dtm.CreatedDate = @cretaddate
order by dtm.Id desc 
end

exec Sp_cretaeddate '2023-04-05'