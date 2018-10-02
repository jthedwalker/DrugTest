-- Procedure to pull in new drivers from the TMW database and update  
-- the status of existing drivers in the DrugTest database.
CREATE PROCEDURE SP_UpdateDrivers

AS
SET NOCOUNT ON;

WITH
    DriversCTE (UserName, FirstName, LastName, IsActive)
    AS
    (
        SELECT mpp_id AS UserName, mpp_firstname AS FirstName,
            LEFT(mpp_lastname, CASE WHEN charindex(' ', mpp_lastname) = 0 THEN LEN(mpp_lastname) 
							 ELSE charindex(' ', mpp_lastname) - 1 END) AS LastName,
            CASE WHEN mpp_terminationdt > GetDate() OR
                mpp_terminationdt IS NULL THEN 1 ELSE 0 END AS IsActive
        FROM [4COSQL02].TMWS_LIVE.dbo.manpowerprofile
    )

INSERT INTO Drivers
    (UserName, FirstName, LastName, Active)
SELECT *
FROM DriversCTE
WHERE UserName NOT IN ('123456','UNKNOWN')
    AND IsActive = 1
    AND UserName NOT IN (SELECT UserName
    FROM Drivers)

GO

WITH
    DriversCTE (UserName, FirstName, LastName, IsActive)
    AS
    (
        SELECT mpp_id AS UserName, mpp_firstname AS FirstName,
            LEFT(mpp_lastname, CASE WHEN charindex(' ', mpp_lastname) = 0 THEN LEN(mpp_lastname) 
							 ELSE charindex(' ', mpp_lastname) - 1 END) AS LastName,
            CASE WHEN mpp_terminationdt > GetDate() OR
                mpp_terminationdt IS NULL THEN 1 ELSE 0 END AS IsActive
        FROM [4COSQL02].TMWS_LIVE.dbo.manpowerprofile
    )

UPDATE Drivers

SET Active = TMW.IsActive

FROM DriversCTE TMW
    JOIN Drivers D ON TMW.UserName = D.UserName
WHERE Active <> TMW.IsActive

GO