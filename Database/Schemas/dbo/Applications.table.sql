CREATE TABLE [dbo].[Applications] (
    [ID]			INT				IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (50)	NOT NULL,
    [Description]	NVARCHAR (500)	NULL,
    [CreateDate]	DATETIME		NOT NULL,
    [Type]			INT				NOT NULL,
    [UserID]		INT				NOT NULL,
	[IconExt]		NVARCHAR (5)	NULL, 
    [MarketUrl]		NVARCHAR(500) NULL
);
GO;

ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [FK_Applications_Users] FOREIGN KEY ([UserID]) REFERENCES [usr].[Users] ([ID]);
GO


