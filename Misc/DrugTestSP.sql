









WITH DriversCTE (UserName, FirstName, LastName, IsActive) AS
(
	select mpp_id as UserName, mpp_firstname as FirstName,
	LEFT(mpp_lastname, CASE WHEN charindex(' ', mpp_lastname) = 0 THEN LEN(mpp_lastname) 
							 ELSE charindex(' ', mpp_lastname) - 1 END) AS LastName,
	CASE WHEN mpp_terminationdt > GetDate() OR
		mpp_terminationdt IS NULL THEN 1 ELSE 0 END AS IsActive
	from [4COSQL02].TMWS_LIVE.dbo.manpowerprofile
)

INSERT INTO Drivers (UserName, FirstName, LastName, Active)

select * from DriversCTE
WHERE UserName NOT IN ('123456','UNKNOWN')
AND IsActive = 1