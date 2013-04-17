CREATE TABLE [api].[Countries] (
    [ID] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL
);
GO;

ALTER TABLE [api].[Countries]
ADD PRIMARY KEY CLUSTERED ([ID] ASC);
GO;



