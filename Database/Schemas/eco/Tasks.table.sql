
CREATE TABLE [eco].[Tasks](
	[Id]	INT IDENTITY(1,1)		NOT NULL,
	[DescriptionID]	INT				NOT NULL,
    [ApplicationID]	INT				NOT NULL,
    [CreatedDate]	DATETIME        NOT NULL,
	[Gender]		TINYINT			NULL,
	[AgeRange]		SMALLINT		NULL,
	[CountryID]		INT				NULL,
	[Zip]			NVARCHAR(10)	NULL,
	[PublishDate]	DATETIME		NULL,
	[Audence] INT NOT NULL, 
    CONSTRAINT[PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 
GO

ALTER TABLE [eco].[Tasks]
ADD CONSTRAINT [FK_Tasks_Applications] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]);
GO

ALTER TABLE [eco].[Tasks]
ADD CONSTRAINT [FK_Tasks_Descriptions] FOREIGN KEY ([DescriptionID]) REFERENCES [eco].[TaskDescriptions] ([ID]);
GO

ALTER TABLE [eco].[Tasks]
ADD CONSTRAINT [FK_Tasks_Contries] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([GeoID]);
GO


