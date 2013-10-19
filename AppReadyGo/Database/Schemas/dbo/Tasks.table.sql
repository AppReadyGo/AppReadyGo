CREATE TABLE [dbo].[Tasks] (
    [ID]			INT IDENTITY (1, 1) NOT NULL,
    [DescriptionID]	INT				NOT NULL,
    [ApplicationID]	INT				NOT NULL,
    [CreatedDate]	DATETIME        NOT NULL,
	[Gender]		TINYINT			NULL,
	[AgeRange]		SMALLINT		NULL,
	[CountryID]		INT				NULL,
	[Zip]			NVARCHAR(10)	NULL,
	[Published]		BIT				NOT NULL
);
GO;

ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;


