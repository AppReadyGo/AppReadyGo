CREATE TABLE [dbo].[SurveyQuestion](
	[SurveyQuestionId] [int] NOT NULL,
	[SurveyId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
CONSTRAINT [PK_SurveyQuestion] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SurveyId - FK to Survey Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SurveyQuestion', @level2type=N'COLUMN',@level2name=N'SurveyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'QuestionId - FK to Question table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SurveyQuestion', @level2type=N'COLUMN',@level2name=N'QuestionId'
GO

ALTER TABLE [dbo].[SurveyQuestion]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestion_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO

ALTER TABLE [dbo].[SurveyQuestion] CHECK CONSTRAINT [FK_SurveyQuestion_Question]
GO

ALTER TABLE [dbo].[SurveyQuestion]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestion_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([SurveyId])
GO

ALTER TABLE [dbo].[SurveyQuestion] CHECK CONSTRAINT [FK_SurveyQuestion_Survey]
GO