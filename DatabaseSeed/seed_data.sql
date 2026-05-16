-- SQL Server seed script for KopkeHome database
-- Note: AspNetUsers password hashes should be created via the application (SeedData) to ensure valid hashed passwords.
-- Insert order follows FK dependencies. Run in this order: AspNetRoles -> State -> Categories -> City -> ZipCode -> MembershipBenefitsPlan -> Users (optional via app) -> other tables referencing users -> Membership tables.

SET IDENTITY_INSERT [dbo].[AspNetRoles] ON;
INSERT INTO [dbo].[AspNetRoles] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (1, 'Admin', 'ADMIN', NEWID());
INSERT INTO [dbo].[AspNetRoles] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (2, 'Contractor', 'CONTRACTOR', NEWID());
INSERT INTO [dbo].[AspNetRoles] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (3, 'Homeowner', 'HOMEOWNER', NEWID());
INSERT INTO [dbo].[AspNetRoles] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (4, 'Sales', 'SALES', NEWID());
INSERT INTO [dbo].[AspNetRoles] (Id, [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (5, 'Support', 'SUPPORT', NEWID());
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF;

-- States (master)
SET IDENTITY_INSERT [dbo].[State] ON;
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (1, 'California', 1, 'CA');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (2, 'Texas', 1, 'TX');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (3, 'New York', 1, 'NY');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (4, 'Florida', 1, 'FL');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (5, 'Illinois', 1, 'IL');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (6, 'Pennsylvania', 1, 'PA');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (7, 'Ohio', 1, 'OH');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (8, 'Georgia', 1, 'GA');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (9, 'North Carolina', 1, 'NC');
INSERT INTO [dbo].[State] (StateId, StateName, CountryId, USAStateCode) VALUES (10, 'Michigan', 1, 'MI');
SET IDENTITY_INSERT [dbo].[State] OFF;

-- Categories (master)
SET IDENTITY_INSERT [dbo].[Categories] ON;
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (1, 'Plumbing');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (2, 'Electrical');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (3, 'Roofing');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (4, 'Painting');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (5, 'HVAC');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (6, 'Landscaping');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (7, 'Flooring');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (8, 'Carpentry');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (9, 'Masonry');
INSERT INTO [dbo].[Categories] (Id, Name) VALUES (10, 'Cleaning');
SET IDENTITY_INSERT [dbo].[Categories] OFF;

-- Cities
SET IDENTITY_INSERT [dbo].[City] ON;
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (1, 'Los Angeles', 1);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (2, 'San Francisco', 1);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (3, 'Houston', 2);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (4, 'Dallas', 2);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (5, 'New York', 3);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (6, 'Miami', 4);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (7, 'Chicago', 5);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (8, 'Philadelphia', 6);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (9, 'Columbus', 7);
INSERT INTO [dbo].[City] (Id, CityName, StateId) VALUES (10, 'Atlanta', 8);
SET IDENTITY_INSERT [dbo].[City] OFF;

-- ZipCodes
SET IDENTITY_INSERT [dbo].[ZipCode] ON;
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (1,1,'90001');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (2,2,'94102');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (3,3,'77001');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (4,4,'75201');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (5,5,'10001');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (6,6,'33101');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (7,7,'60601');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (8,8,'19101');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (9,9,'43085');
INSERT INTO [dbo].[ZipCode] (Id, CityId, Zipcode) VALUES (10,10,'30301');
SET IDENTITY_INSERT [dbo].[ZipCode] OFF;

-- Membership Plans
SET IDENTITY_INSERT [dbo].[MembershipBenifitsPlan] ON;
INSERT INTO [dbo].[MembershipBenifitsPlan] (Id, Title, RoleId, Categories, ZipCodes, PricePerMonth, PricePerYear, Website, PhoneApp, MonthlyStripePriceId, AnnuallyStripePriceId) 
VALUES (1,'Basic Contractor',2,'1,2','1,3',29.99,299.99,1,0,NULL,NULL);
INSERT INTO [dbo].[MembershipBenifitsPlan] (Id, Title, RoleId, Categories, ZipCodes, PricePerMonth, PricePerYear, Website, PhoneApp, MonthlyStripePriceId, AnnuallyStripePriceId) 
VALUES (2,'Pro Contractor',2,'1,2,3','1,2,3',59.99,599.99,1,1,NULL,NULL);
SET IDENTITY_INSERT [dbo].[MembershipBenifitsPlan] OFF;

-- DocumentsVerificationStatus
SET IDENTITY_INSERT [dbo].[DocumentsVerificationStatus] ON;
INSERT INTO [dbo].[DocumentsVerificationStatus] (Id, Status) VALUES (1,'Pending');
INSERT INTO [dbo].[DocumentsVerificationStatus] (Id, Status) VALUES (2,'Verified');
INSERT INTO [dbo].[DocumentsVerificationStatus] (Id, Status) VALUES (3,'Rejected');
SET IDENTITY_INSERT [dbo].[DocumentsVerificationStatus] OFF;

-- FAQ
SET IDENTITY_INSERT [dbo].[FAQ] ON;
INSERT INTO [dbo].[FAQ] (Id, Question, Answer, IsActive) VALUES (1,'How to register?','Click Register and fill details.',1);
INSERT INTO [dbo].[FAQ] (Id, Question, Answer, IsActive) VALUES (2,'How to contact support?','Use the Contact form or email support@prohz.example.',1);
SET IDENTITY_INSERT [dbo].[FAQ] OFF;

-- NOTE: AspNetUsers should be created via application user manager to ensure valid password hashes.
-- If you still want to insert AspNetUsers via SQL, generate password hashes from an Identity instance and place them in the PasswordHash column.

-- Example AspNetUserRoles mapping (after creating users via app and knowing their Ids):
-- INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId) VALUES (1,1); -- assign admin role to user id 1

-- Remaining tables (BusinessProfileDataModel, BusinessProfileOtherContractors, WorkGallery, ContractorsReview, ProhzReferral, ProhzSalesAssciates, ProhzLegalFiles, UniqueMemberId, VerifyOTP, UserMembershipSubscriptions, UserMembershipCategories, UserMembershipZipcodes, CustomMembershipPlanRequest, MembershipBenefits) can be inserted after users exist.

-- Example insert for Categories mapping table (MembershipBenefits) after users exist:
-- INSERT INTO [dbo].[UsersZipcodesAndCategories] (Id, UserId, ZipCodeId, CategoriesId, PlanId) VALUES (1, 2, 1, 1, 1);

GO
