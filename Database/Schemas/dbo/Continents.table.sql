CREATE TABLE [dbo].[Continents] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    [Code] NVARCHAR (3) NOT NULL
);
GO

ALTER TABLE [dbo].[Continents]
ADD CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

