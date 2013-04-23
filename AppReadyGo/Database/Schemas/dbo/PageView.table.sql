﻿CREATE TABLE [dbo].[PageView] (
    [ID]                BIGINT IDENTITY (1, 1) NOT NULL,
    [Date]              DATETIME       NOT NULL,
    [Path]              NVARCHAR (256) NOT NULL,
    [Ip]                NVARCHAR (15)  NULL,-- TODO: Remove
    [LanguageId]        INT            NULL,
    [CountryId]         INT            NULL,
    [City]              NVARCHAR (50)  NULL,
    [OperationSystemId] INT            NULL,
    [BrowserId]         INT            NULL,
    [ScreenWidth]       INT            NOT NULL,
    [ScreenHeight]      INT            NOT NULL,
    [ClientWidth]       INT            NOT NULL,
    [ClientHeight]      INT            NOT NULL,
    [ApplicationId]     INT            NOT NULL
);
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [PK_PageView] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;


ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Browser] FOREIGN KEY ([BrowserId]) REFERENCES [dbo].[Browser] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_OperationSystem] FOREIGN KEY ([OperationSystemId]) REFERENCES [dbo].[OperationSystem] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageView]
    ADD CONSTRAINT [FK_PageView_Application] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Application] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO

ALTER TABLE [dbo].[PageView]
ADD CONSTRAINT [FK_PageView_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([GeoID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
