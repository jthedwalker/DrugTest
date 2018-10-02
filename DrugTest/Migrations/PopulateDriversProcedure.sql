-- Only used for development purposes to seed data to test with.

CREATE PROCEDURE dbo.populateDriverTable

AS

DBCC CHECKIDENT ('dbo.Drivers', RESEED, 0);
  
INSERT INTO Drivers
SELECT DriverID, FirstName, LastName, 
CASE WHEN ActiveYN = 'Y' THEN 1 ELSE 0 END AS ActiveYN FROM Development.dbo.vwDriverProfile ddp
WHERE ddp.DriverID not in (SELECT DriverID FROM Drivers)
ORDER BY 1

GO