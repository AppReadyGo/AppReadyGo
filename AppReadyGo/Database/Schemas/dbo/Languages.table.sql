CREATE TABLE [dbo].[Languages] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL
);
GO

ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([ID] ASC);
GO
