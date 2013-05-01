

DECLARE @UserID INT = 0;


[ID]             INT           IDENTITY (1, 1) NOT NULL,
    [UserTypeID]     TINYINT       NOT NULL,
    [Email]          NVARCHAR (50) NOT NULL,
    [Password]       NVARCHAR (50) NOT NULL,
    [PasswordSalt]   NVARCHAR (50) NOT NULL,
    [CreateDate]     DATETIME      NOT NULL,
    [LastAccessDate] DATETIME      NULL,
	[Activated]		BIT			   NOT NULL,
	[Unsubscribed]  BIT			   NOT NULL,
	[FirstName]		NVARCHAR (100) NULL,
	[LastName]		NVARCHAR (100) NULL,
	[SpecialAccess] BIT			   NOT NULL,
	[MembershipID]	SMALLINT	   NOT NULL,
	[AcceptedTermsAndConditions] BIT NOT NULL,
	[GenderID]		TINYINT			NULL,
	[AgeRange]			SMALLINT		NULL,
	[CountryID]		INT				NULL

INSERT INTO usr.Users(UserTypeID, Email, CreateDate, Activated, AcceptedTermsAndConditions, GenderID, AgeRange, FirstName, LastName, Password, PasswordSalt)
VALUES(3, 'someemail@test.com', GETDATE(), )