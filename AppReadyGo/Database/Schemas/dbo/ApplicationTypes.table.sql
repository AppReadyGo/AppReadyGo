CREATE TABLE [dbo].[ApplicationTypes] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL
);
GO

ALTER TABLE [dbo].[ApplicationTypes]
ADD CONSTRAINT [PK_ApplicationTypes] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

