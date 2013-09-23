CREATE TABLE [eco].[UserApplicationsTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserApplicationId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_UserApplicationsTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) 

GO

ALTER TABLE [eco].[UserApplicationsTask] ADD  CONSTRAINT [FK_Tasks] FOREIGN KEY([TaskId])
REFERENCES [eco].[Tasks] ([Id])
GO

ALTER TABLE [eco].[UserApplicationsTask] CHECK CONSTRAINT [FK_Tasks]
GO

ALTER TABLE [eco].[UserApplicationsTask]  ADD  CONSTRAINT [FK_UserAppsTask_UserApplication] FOREIGN KEY([UserApplicationId])
REFERENCES [eco].[UserApplication] ([Id])
GO

ALTER TABLE [eco].[UserApplicationsTask] CHECK CONSTRAINT [FK_UserAppsTask_UserApplication]
GO


