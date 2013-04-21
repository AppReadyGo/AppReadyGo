
CREATE TABLE [dbo].[ViewPart] (
    [ID]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [X]          INT      NOT NULL,
    [Y]          INT      NOT NULL,
	[StartDate]          DATETIME NULL,
    [FinishDate]         DATETIME NULL,
	[Orientation]        INT      NULL,
    [PageViewID] BIGINT   NOT NULL
);

GO

ALTER TABLE [dbo].[ViewPart]
ADD CONSTRAINT [PK_ViewPart] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[ViewPart]
    ADD CONSTRAINT [FK_ViewPart_PageView] FOREIGN KEY ([PageViewID]) REFERENCES [dbo].[PageView] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE;


GO

