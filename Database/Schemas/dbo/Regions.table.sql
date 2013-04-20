CREATE TABLE [dbo].[Regions] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
	[CountryID]	INT	NOT NULL
);
GO

ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [FK_Regions_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([GeoID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
