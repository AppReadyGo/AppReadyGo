CREATE TABLE [dbo].[Browsers] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL
);
GO

ALTER TABLE [dbo].[Browsers]
ADD CONSTRAINT [PK_Browsers] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;
