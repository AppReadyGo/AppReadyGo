CREATE TABLE [eco].[UserApplication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DownlaodDate] [datetime] NOT NULL,
CONSTRAINT [PK_UserApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 

GO

ALTER TABLE [eco].[UserApplication] ADD  CONSTRAINT [FK_UserApplication_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([Id])
GO

ALTER TABLE [eco].[UserApplication] CHECK CONSTRAINT [FK_UserApplication_Applications]
GO

ALTER TABLE [eco].[UserApplication] ADD  CONSTRAINT [FK_UserApplication_Users] FOREIGN KEY([UserId])
REFERENCES [usr].[Users] ([ID])
GO

ALTER TABLE [eco].[UserApplication] CHECK CONSTRAINT [FK_UserApplication_Users]
GO


