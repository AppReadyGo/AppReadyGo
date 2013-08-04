-- =============================================
-- Script initilizate database with development data
-- =============================================


INSERT INTO [usr].[Users] ([UserTypeID], [Email], [Password], [PasswordSalt], [CreateDate], [Activated], [FirstName], [LastName],[Unsubscribed], [SpecialAccess],[MembershipID], [AcceptedTermsAndConditions],[ThirdParty])
VALUES (1/*Staff*/, 'yura.panshin@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==', GETDATE(), 1, 'Yura', 'Panshin', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1, 0),
(1/*Staff*/, 'michael.liagine@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Michael', 'Liagine', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1, 0),
(1/*Staff*/, 'michael.pilip@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Michael', 'Pilip', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1, 0),
(1/*Staff*/, 'pavel.mogilevsky@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Pavel', 'Mogilevsky', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1, 0),
(1/*Staff*/, 'philip.belder@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Philip', 'Belder', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1, 0);

GO
INSERT INTO [usr].[UserStaffRoles] ([RoleID], [UserID])
SELECT 1, ID FROM [usr].[Users] WHERE Email IN ('yura.panshin@appreadygo.com', 'michael.liagine@appreadygo.com', 'michael.pilip@appreadygo.com', 'pavel.mogilevsky@appreadygo.com', 'philip.belder@appreadygo.com')
GO


