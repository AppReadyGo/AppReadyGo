CREATE TABLE [dbo].[Publishes] (
    [ID]			INT IDENTITY (1, 1) NOT NULL,
    [ApplicationID]	INT				NOT NULL,
    [CreatedDate]	DATETIME        NOT NULL,
	[Gender]		TINYINT			NULL,
	[AgeRange]		SMALLINT		NULL,
	[CountryID]		INT				NULL,
	[Zip]			NVARCHAR(5)		NULL
);
GO;

ALTER TABLE [dbo].[Publishes]
ADD CONSTRAINT [PK_Publishes] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;


