CREATE TABLE [dbo].[APIMemberApplications] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationID]			INT NOT NULL,
    [UserID]				INT NOT NULL,
	[Used]					BIT NULL,
	[Review]				NVARCHAR(500) NULL
);
GO

ALTER TABLE [dbo].[APIMemberApplications]
ADD CONSTRAINT [PK_APIMemberApplications] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[APIMemberApplications]
    ADD CONSTRAINT [FK_APIMemberApplications_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]);
GO

ALTER TABLE [dbo].[APIMemberApplications]
    ADD CONSTRAINT [FK_APIMemberApplications_User] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO

