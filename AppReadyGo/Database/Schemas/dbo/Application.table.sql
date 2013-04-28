CREATE TABLE [dbo].[Application] (
    [ID]			INT				IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (50)	NOT NULL,
    [Description]	NVARCHAR (500)	NULL,
    [CreateDate]	DATETIME		NOT NULL,
    [Type]			INT				NOT NULL,
    [UserID]		INT				NOT NULL,
	[IconExt]		NVARCHAR (5)	NULL
);
GO;

ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[Application]
ADD CONSTRAINT [FK_Application_User] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO


