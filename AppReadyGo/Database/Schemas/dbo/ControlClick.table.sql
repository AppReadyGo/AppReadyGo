CREATE TABLE [dbo].[ControlClick](
	[Id] BIGINT IDENTITY (1, 1) NOT NULL,
	[Tag] [nvarchar](70) NOT NULL,
	[Date] [datetime] NOT NULL,
	[PageViewId] [bigint] NOT NULL
CONSTRAINT [PK_ControlClick] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
) 

GO

ALTER TABLE [dbo].[ControlClick]  ADD  CONSTRAINT [FK_ControlClick_PageView] FOREIGN KEY([PageViewId]) REFERENCES [dbo].[PageViews] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ControlClick] CHECK CONSTRAINT [FK_ControlClick_PageView]
GO


