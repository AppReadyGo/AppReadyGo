
CREATE TABLE [dbo].[UserQuestionReply](
	[UserQuestionReplyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ApplicationVersionQuestionId] [int] NOT NULL,
	[Score] [int] NULL,
	[PolarScore] [int] NULL,
	[UserComment] [nvarchar](500) NULL,
CONSTRAINT [PK_UserQuestionReply] PRIMARY KEY CLUSTERED 
(
	[UserQuestionReplyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[UserQuestionReply]  ADD  CONSTRAINT [FK_UserQuestionReply_ApplicationVersionQuestion] FOREIGN KEY([ApplicationVersionQuestionId])
REFERENCES [dbo].[ApplicationVersionQuestion] ([ApplicationVersionQuestionyId])
GO

ALTER TABLE [dbo].[UserQuestionReply] CHECK CONSTRAINT [FK_UserQuestionReply_ApplicationVersionQuestion]
GO

ALTER TABLE [dbo].[UserQuestionReply]  ADD  CONSTRAINT [FK_UserQuestionReply_User] FOREIGN KEY([UserId])
REFERENCES [usr].[Users] ([ID])
GO

ALTER TABLE [dbo].[UserQuestionReply] CHECK CONSTRAINT [FK_UserQuestionReply_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK to User Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserQuestionReply', @level2type=N'CONSTRAINT',@level2name=N'FK_UserQuestionReply_User'
GO


