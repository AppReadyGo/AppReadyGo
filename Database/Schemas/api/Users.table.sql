CREATE TABLE [api].[Users] (
    [ID]             INT			IDENTITY (1, 1) NOT NULL,
    [UserTypeID]     TINYINT		NOT NULL,
    [Email]          NVARCHAR (50)	NOT NULL,
    [Password]       NVARCHAR (50)	NOT NULL,
    [PasswordSalt]   NVARCHAR (50)	NOT NULL,
    [CreateDate]     DATETIME		NOT NULL,
    [LastAccessDate] DATETIME		NULL,
	[Activated]		 BIT			NOT NULL,
	[Gender]		 BIT			NOT NULL,
	[Age]			 SMALLINT		NOT NULL,
	[FirstName]		 NVARCHAR (100)	NULL,
	[LastName]		 NVARCHAR (100)	NULL,
	[MembershipID]	 SMALLINT		NOT NULL,
	[AcceptedTermsAndConditions] BIT NOT NULL,
	[CountryID]		INT				NULL
);
GO

ALTER TABLE [api].[Users]
ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [api].[Users]
ADD CONSTRAINT UC_Email UNIQUE ([Email])
GO

ALTER TABLE [api].[Users]
    ADD CONSTRAINT [FK_Users_Countries] FOREIGN KEY ([CountryID]) REFERENCES [api].[Countries] ([ID]);
GO
