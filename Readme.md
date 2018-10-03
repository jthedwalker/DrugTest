
# Drug Testing

This application was designed in house to pull a random selection of drivers for drug and alcohol testing. The testing of truck drivers is a federal requirement. This application serves as a tracking mechanism to that end.

## Requirements

1. Produce a selection of drivers based on app settings. Easily configurable through IIS.
2. Automation of the selection process based on a schedule. Configurable through HangFire.
3. Ability to track driver notification status. Status is color coded for visibility.
4. Tracking of historical test results. Tests grouped by batch number.

## Project Status

The project is in the first initial release. Testing from the user base will be required to validate the design concept and usability.

## Design Concept

As this application was designed in house for a specific use case, there were certain design concessions made for the application to function as desired. The company had an existing application that tracked all of the driver's information. It was requested that the drivers be populated from this application. To avoid connecting to a database this application didn't "own", a separate stored procedure and SQL job facilitate the population of the driver's table. Further, if the company decides to transition to a different application, porting the new information will be simple.

Because the drivers are populated from an external source, constraints were not created. If a driver is deleted or a username is changed, this application does not update that information. In the case of a username change, the old username remains and the new username is created. This was done to insure data integrity of the drug and alcohol test records.   

### System Requirements

* Microsoft Server
* Microsoft IIS
* Microsoft SQL Server
* .NET Framework 4.6.1

### Limitations

Current regulations require a portion of drug test selectees to also be alcohol tested. The batch generation was designed with this in mind. Alcohol test selectees will not exceed drug test selectees. If the alcohol percentage is greater than the drug test percentage, all selectees will be selected for both drug and alcohol testing.

***If the current regulation changes, this method will need to be updated.***