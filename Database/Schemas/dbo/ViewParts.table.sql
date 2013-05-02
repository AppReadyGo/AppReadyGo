
CREATE TABLE [dbo].[ViewParts] (
    [ID]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
	[StartDate]          DATETIME NULL,
    [FinishDate]         DATETIME NULL,
	[Orientation]        INT      NULL,
    [PageViewID] BIGINT   NOT NULL
);
GO

ALTER TABLE [dbo].[ViewParts]
ADD CONSTRAINT [PK_ViewParts] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[ViewParts]
    ADD CONSTRAINT [FK_ViewParts_PageView] FOREIGN KEY ([PageViewID]) REFERENCES [dbo].[PageViews] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;
GO

