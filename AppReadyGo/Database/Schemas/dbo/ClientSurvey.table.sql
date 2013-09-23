CREATE TABLE [dbo].[ClientSurvey](
	[ClientSurveyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[SurveyId] [int] NOT NULL,
CONSTRAINT [PK_ClientSurvey] PRIMARY KEY CLUSTERED 
(
	[ClientSurveyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 

GO

ALTER TABLE [dbo].[ClientSurvey]  ADD  CONSTRAINT [FK_ClientSurvey_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([SurveyId])
GO

ALTER TABLE [dbo].[ClientSurvey] CHECK CONSTRAINT [FK_ClientSurvey_Survey]
GO

ALTER TABLE [dbo].[ClientSurvey]  ADD  CONSTRAINT [FK_ClientSurvey_Users] FOREIGN KEY([UserId])
REFERENCES [usr].[Users] ([ID])
GO

ALTER TABLE [dbo].[ClientSurvey] CHECK CONSTRAINT [FK_ClientSurvey_Users]
GO