CREATE TABLE [dbo].[Screenshots] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [FileExtension] NVARCHAR (10) NOT NULL,
	[ApplicationID]		INT	NOT NULL
);
GO

ALTER TABLE [dbo].[Screenshots]
ADD CONSTRAINT [PK_Screenshots] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[Screenshots]
ADD CONSTRAINT [FK_Screenshots_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Application] ([ID]);
GO
