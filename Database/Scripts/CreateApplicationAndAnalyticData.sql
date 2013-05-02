

DECLARE @UserID INT = 0, @ApiUserID INT, @AppID INT, @PageViewID INT;


INSERT INTO usr.Users(UserTypeID, Email, CreateDate, Activated, AcceptedTermsAndConditions, GenderID, AgeRange, FirstName, LastName, Password, PasswordSalt)
VALUES(3, 'apiuser@test.com', GETDATE(), 1, 1, 1, 1, 'Test API', 'User', 'Password', 'jdksfhksdjhfksdla');

SELECT @ApiUserID = SCOPE_IDENTITY();
GO


INSERT INTO dbo.Applications([Name], [Description], [CreateDate], [Type], [UserID], [IconExt])
VALUES('Test application', 'The application created for test purposes.', GETDATE(), 1, @UserID, NULL)

SELECT @AppID = SCOPE_IDENTITY();
GO


INSERT INTO PageViews([Date], [Path], [Ip], [LanguageId], [CountryId], [City], [OperationSystemId], [BrowserId], [ScreenWidth], [ScreenHeight], [ClientWidth], [ClientHeight], [ApplicationId])
VALUES(GETDATE(),'MainScreen','192.169.0.1', NULL, NULL, NULL, NULL, NULL, 480, 320, 480, 320, @AppID)

SELECT @PageViewID = SCOPE_IDENTITY();
GO


INSERT INTO Clicks([Date], [X], [Y], [PageViewID], [Orientation])
VALUES(GETDATE(), 200, 300, @PageViewID)
GO


INSERT INTO Publishes([ApplicationID], [CreatedDate], [Gender], [AgeRange], [CountryID], [Zip])
VALUES(@AppID, GETDATE(), 1, 1, NULL, NULL)
GO

INSERT INTO DownloadedApplications(ApplicationID, UserID)
VALUES(@AppID, @ApiUserID)
GO
