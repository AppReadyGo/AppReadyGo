-- =============================================
-- Script initilizate database with development data
-- =============================================


INSERT INTO [usr].[Users] ([UserTypeID], [Email], [Password], [PasswordSalt], [CreateDate], [Activated], [FirstName], [LastName],[Unsubscribed], [SpecialAccess],[MembershipID], [AcceptedTermsAndConditions])
VALUES (2/*Member*/, 'test@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==', '20120101', 1, 'Development', 'AppReadyGo', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1);

GO

INSERT INTO [dbo].[Applications] ([Name], [Description], [CreateDate] ,[Type] ,[UserId])
VALUES ('Demo app','Demo Application', '20120525', 3 /*3 stands for android, see EyeTracker.Domain.Model.ApplicationType*/, (SELECT ID FROM [usr].[Users] WHERE [Email] = 'test@appreadygo.com'));

GO
INSERT INTO [dbo].[OperationSystems] ([Name])
VALUES ('2.3.3');