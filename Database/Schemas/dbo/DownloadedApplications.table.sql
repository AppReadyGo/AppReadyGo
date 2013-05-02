CREATE TABLE [dbo].[DownloadedApplications] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationID]		INT NOT NULL,
    [UserID]		INT NOT NULL
);
GO

ALTER TABLE [dbo].[DownloadedApplications]
ADD CONSTRAINT [PK_DownloadedApplications] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[DownloadedApplications]
    ADD CONSTRAINT [FK_DownloadedApplications_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]);
GO

ALTER TABLE [dbo].[DownloadedApplications]
    ADD CONSTRAINT [FK_DownloadedApplications_User] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO

