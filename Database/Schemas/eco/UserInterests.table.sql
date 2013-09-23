
CREATE TABLE [eco].[UserInterests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
CONSTRAINT [PK_UserInterets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 

GO

ALTER TABLE [eco].[UserInterests]   ADD  CONSTRAINT [FK_UserInterets_Interests] FOREIGN KEY([InterestId])
REFERENCES [eco].[Interests] ([Id])
GO

ALTER TABLE [eco].[UserInterets] CHECK CONSTRAINT [FK_UserInterets_Interests]
GO

ALTER TABLE [eco].[UserInterests]  ADD  CONSTRAINT [FK_UserInterets_Users] FOREIGN KEY([UserId])
REFERENCES [usr].[Users] ([ID])
GO

ALTER TABLE [eco].[UserInterets] CHECK CONSTRAINT [FK_UserInterets_Users]
GO


