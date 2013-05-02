CREATE TABLE [dbo].[Screens] (
    [ID]            INT       IDENTITY (1, 1) NOT NULL,
    [ApplicationID] INT          NOT NULL,
    [Path]          NVARCHAR (256) NOT NULL,
    [Width]         INT          NOT NULL,
    [Height]        INT          NOT NULL,
    [FileExtension] NVARCHAR (5) NOT NULL
);
GO

ALTER TABLE [dbo].[Screens]
ADD CONSTRAINT [PK_Screens] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

ALTER TABLE [dbo].[Screens]
    ADD CONSTRAINT [FK_Screens_Application] FOREIGN KEY ([ApplicationID]) REFERENCES [dbo].[Applications] ([ID]);
GO
