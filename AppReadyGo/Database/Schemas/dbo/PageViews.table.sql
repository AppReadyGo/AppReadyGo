CREATE TABLE [dbo].[PageViews] (
    [ID]                BIGINT IDENTITY (1, 1) NOT NULL,
    [Date]              DATETIME       NOT NULL,
    [Path]              NVARCHAR (256) NOT NULL,
    [LanguageID]        INT            NULL,
    [CountryID]         INT            NULL,
    [City]              NVARCHAR (50)  NULL,
    [OperationSystemID] INT            NULL,
    [BrowserID]         INT            NULL,
    [ScreenWidth]       INT            NOT NULL,
    [ScreenHeight]      INT            NOT NULL,
    [ClientWidth]       INT            NOT NULL,
    [ClientHeight]      INT            NOT NULL,
    [ApplicationID]     INT            NOT NULL, 
    [TaskID] INT NULL, 
    [UserID] INT NULL
);
GO

ALTER TABLE [dbo].[PageViews]
ADD CONSTRAINT [PK_PageView] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;


ALTER TABLE [dbo].[PageViews]
ADD CONSTRAINT [FK_PageView_Browser] FOREIGN KEY ([BrowserID]) REFERENCES [dbo].[Browsers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageViews]
ADD CONSTRAINT [FK_PageView_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Languages] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageViews]
ADD CONSTRAINT [FK_PageView_OperationSystem] FOREIGN KEY ([OperationSystemID]) REFERENCES [dbo].[OperationSystems] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[PageViews]
    ADD CONSTRAINT [FK_PageView_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

ALTER TABLE [dbo].[PageViews]
ADD CONSTRAINT [FK_PageView_Countries] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([GeoID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

