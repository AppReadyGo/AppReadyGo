CREATE TABLE [dbo].[OperationSystems] (
    [ID]   INT    IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL
);
GO

ALTER TABLE [dbo].[OperationSystems]
ADD CONSTRAINT [PK_OperationSystems] PRIMARY KEY CLUSTERED ([ID] ASC);
GO;

