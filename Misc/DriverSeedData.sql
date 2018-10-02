



select * from Drivers




INSERT INTO Drivers (UserName, FirstName, LastName, Active)
select DriverID, FirstName, LastName, Case when ActiveStatus = 'Y' then 1 else 0 end as ActiveStatus from OPENQUERY(SVRSQL, 'select * from Development.dbo.vwDriverProfile order by DriverID ASC')