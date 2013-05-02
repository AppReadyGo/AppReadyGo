CREATE TABLE [dbo].[Clicks] (
    [ID]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
    [PageViewID] BIGINT   NOT NULL,
	[Orientation] INT NOT NULL
);
GO
ALTER TABLE [dbo].[Clicks]
ADD CONSTRAINT [PK_Clicks] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
ALTER TABLE [dbo].[Clicks]
    ADD CONSTRAINT [FK_Click_PageView] FOREIGN KEY ([PageViewID]) REFERENCES [dbo].[PageViews] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO


