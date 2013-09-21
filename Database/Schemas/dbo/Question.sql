
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] NOT NULL,
	[QuestionText] [nvarchar](500) NOT NULL,
	[QuestionTypeId] [int] NOT NULL,
	[MinScore] [int] NULL,
	[MaxScore] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'QuestionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'QuestionText' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Question', @level2type=N'COLUMN',@level2name=N'QuestionText'
GO


