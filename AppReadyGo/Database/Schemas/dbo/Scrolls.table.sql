CREATE TABLE [dbo].[Scrolls]
(
	[ID]                 BIGINT IDENTITY (1, 1) NOT NULL,
    [FirstTouchId]       BIGINT NOT NULL,
    [LastTouchId]        BIGINT NOT NULL,
    [PageViewId]         BIGINT NULL
);
GO

ALTER TABLE [dbo].[Scrolls]
ADD CONSTRAINT [PK_Scrolls] PRIMARY KEY CLUSTERED ([ID] ASC);
GO

ALTER TABLE [dbo].[Scrolls]
ADD CONSTRAINT [FK_Scroll_PageView] FOREIGN KEY ([PageViewId]) REFERENCES [dbo].[PageViews] ([ID]);
GO
