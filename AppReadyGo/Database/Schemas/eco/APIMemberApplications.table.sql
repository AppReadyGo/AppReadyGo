CREATE TABLE [dbo].[APIMemberApplications] (
    [ApplicationID]			INT NOT NULL,
    [UserID]				INT NOT NULL
);
GO

ALTER TABLE [dbo].[APIMemberApplications]
ADD CONSTRAINT [PK_APIMemberApplications] PRIMARY KEY CLUSTERED ([ApplicationID], [UserID]);
GO;

ALTER TABLE [dbo].[APIMemberApplications]
    ADD CONSTRAINT [FK_APIMemberApplications_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]);
GO

ALTER TABLE [dbo].[APIMemberApplications]
    ADD CONSTRAINT [FK_APIMemberApplications_User] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO

