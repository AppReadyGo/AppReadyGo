-- =============================================
-- Script initilizate database with development data
-- =============================================


INSERT INTO [usr].[Users] ([UserTypeID], [Email], [Password], [PasswordSalt], [CreateDate], [Activated], [FirstName], [LastName],[Unsubscribed], [SpecialAccess],[MembershipID], [AcceptedTermsAndConditions])
VALUES (1/*Staff*/, 'yura.panshin@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==', GETDATE(), 1, 'Yura', 'Panshin', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1),
(1/*Staff*/, 'michael.liagine@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Michael', 'Liagine', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1),
(1/*Staff*/, 'michael.pilip@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Michael', 'Pilip', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1),
(1/*Staff*/, 'pavel.mogilevsky@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Pavel', 'Mogilevsky', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1),
(1/*Staff*/, 'philip.belder@appreadygo.com', 'XW0mA5DzfN4XL851H/i1xNFFbMOdtjVAL6fjBN5monE='/*111111*/, '/WCjbQ==',  GETDATE(), 1, 'Philip', 'Belder', 0, 1, (select [ID] from [usr].[Memberships] where Name = 'Pro'), 1);

GO


