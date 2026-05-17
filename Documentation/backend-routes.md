# Backend API Routes (quick reference)

This file lists backend HTTP routes (controller/action) found in the repository and where to locate the controller implementation.

**Notes**
- Routes use the controller's route attribute. For `[Route("[controller]/[action]")]` the endpoint is `/ControllerName/ActionName`.
- Paths link to the source files in the workspace.

---

**AccountController** (/Account)
- POST /Account/BasicInfoHomeOwner — registers a homeowner. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L47)
- POST /Account/BasicInfo — registers a contractor/standard user. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L249)
- POST /Account/VerifyOTP — verify OTP during signup. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L333)
- POST /Account/SignIn — user sign-in. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L637)
- POST /Account/ForgotPasswordSendMail — sends forgot-password email. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L90)
- POST /Account/ForgotPassword — generate reset token. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L115)
- POST /Account/ForgotPasswordApp — app flow for forgot password. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L136)
- POST /Account/ResetPassword — reset password (via service). [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L156)
- GET  /Account/GetStates — returns states list. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L405)
- POST /Account/GetCitiesList — returns cities for a state. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L432)
- POST /Account/UpdateBasicInfo — update user basic info. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L852)
- POST /Account/UpdateBasicInfoHomeOwner — update homeowner basic info. [KopkeHome-FMRS_API/Controllers/AccountController.cs](KopkeHome-FMRS_API/Controllers/AccountController.cs#L973)

**AdminSeedController** (/api/seed)
- GET /api/seed/admin — seed an admin user via Identity. [KopkeHome-FMRS_API/Controllers/AdminSeedController.cs](KopkeHome-FMRS_API/Controllers/AdminSeedController.cs#L22)
- GET /api/seed/delete-admin — remove seeded admin. [KopkeHome-FMRS_API/Controllers/AdminSeedController.cs](KopkeHome-FMRS_API/Controllers/AdminSeedController.cs#L74)

**MembershipController** (/Membership)
- GET  /Membership/GetMembershipPlans — list membership plans. [KopkeHome-FMRS_API/Controllers/MembershipController.cs](KopkeHome-FMRS_API/Controllers/MembershipController.cs#L12)
- POST /Membership/SaveMembershipZipcodesAndCategories — save membership mappings. [KopkeHome-FMRS_API/Controllers/MembershipController.cs](KopkeHome-FMRS_API/Controllers/MembershipController.cs#L28)
- POST /Membership/SaveCustomZipcodeRequest — save custom plan request. [KopkeHome-FMRS_API/Controllers/MembershipController.cs](KopkeHome-FMRS_API/Controllers/MembershipController.cs#L46)
- POST /Membership/GetZipCodesByCityName — get zipcodes for city. [KopkeHome-FMRS_API/Controllers/MembershipController.cs](KopkeHome-FMRS_API/Controllers/MembershipController.cs#L70)
- POST /Membership/GetCustomPlanDetailsByUserId — get custom plan by user id. [KopkeHome-FMRS_API/Controllers/MembershipController.cs](KopkeHome-FMRS_API/Controllers/MembershipController.cs#L96)

**PaymentController** (/Payment)
- POST /Payment/AddPaymentTransactionDetails — add payment transaction. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L24)
- POST /Payment/UpdatePaymentTransactionInfo — update transaction info. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L40)
- POST /Payment/GetSubscriptionDetailByUserId — get subscription by user id. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L56)
- GET  /Payment/GetSubscriptionDetailByUserIdApp — app subscription detail. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L72)
- POST /Payment/CheckUserHaveSubscriptionOrNotByEmail — check subscription by email. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L100)
- POST /Payment/SubscribeToAPlanCustom — subscribe to a custom plan. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L153)
- POST /Payment/SubscribeToAPlan — subscribe to a plan. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L259)
- GET  /Payment/GetCustomerByStripCustomerId — get subscription by Stripe customer id. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L380)
- POST /Payment/PaymentSuccess — handle Stripe payment success. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L416)
- POST /Payment/UpgradeSubscription — upgrade subscription (create session). [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L491)
- POST /Payment/CancelSubscription — cancel subscription. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L537)
- POST /Payment/DowngradeSubscription — downgrade and issue link. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L607)
- POST /Payment/CreateCustomPriceSubscription — create custom Stripe price and email link. [KopkeHome-FMRS_API/Controllers/PaymentController.cs](KopkeHome-FMRS_API/Controllers/PaymentController.cs#L654)

**DashboardController** (/Dashboard) — requires Bearer auth
- POST /Dashboard/SearchContractorsList — search contractors. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L15)
- POST /Dashboard/GetContractorProfileDetails — contractor profile details. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L31)
- GET  /Dashboard/GetContractors — (placeholder) get contractors. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L67)
- POST /Dashboard/GetCategoriesList — get categories list. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L86)
- POST /Dashboard/GetZipCodeList — get zipcodes by prefix/user. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L112)
- POST /Dashboard/ContractorsReview — post a review/like. [KopkeHome-FMRS_API/Controllers/DashboardController.cs](KopkeHome-FMRS_API/Controllers/DashboardController.cs#L136)

**ContactController** (/Contact)
- POST /Contact/SendEmailToProhz — send email from contact form. [KopkeHome-FMRS_API/Controllers/ContactController.cs](KopkeHome-FMRS_API/Controllers/ContactController.cs#L8)

**BusinessProfileController** (/BusinessProfile)
- POST /BusinessProfile/BusinessProfileForContractor — create contractor profile. [KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs](KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs#L12)
- POST /BusinessProfile/UpdateBusinessProfileForContractor — update contractor profile. [KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs](KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs#L46)
- POST /BusinessProfile/BusinessProfileSubContractor — create sub-contractor profile. [KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs](KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs#L86)
- POST /BusinessProfile/UpdateBusinessProfileOfOtherContractors — update other contractor profile. [KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs](KopkeHome-FMRS_API/Controllers/BusinessProfileController.cs#L120)

**AdminController** (/Admin)
- GET  /Admin/CustomMembershipPlanRequestsList — list custom plan requests. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L12)
- GET  /Admin/GetDocumntsListForVerification — list docs for verification. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L32)
- POST /Admin/UpdateDocumentVerification — update document verification. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L56)
- POST /Admin/AddFAQ — add FAQ. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L76)
- POST /Admin/UpdateFAQ — update FAQ. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L92)
- POST /Admin/DeleteFAQ — delete FAQ. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L108)
- POST /Admin/GetCustomReqById — get custom request by id. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L124)
- POST /Admin/GetCustomReqByUserId — get custom req by user id. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L140)
- POST /Admin/GetFAQById — get FAQ by id. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L156)
- GET  /Admin/GetAllFAQ — list all FAQ. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L172)
- POST /Admin/GetPromoVideoById — get promo video by id. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L196)
- GET  /Admin/GetAllPromoVideos — list promo videos. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L216)
- POST /Admin/AddLegalFiles — add legal files. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L236)
- POST /Admin/AddPromoVideo — add promo video. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L256)
- POST /Admin/UpdatePromoVideo — update promo video. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L272)
- POST /Admin/DeletePromoVideo — delete promo video. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L292)
- GET  /Admin/GetDashboardStatus — admin dashboard. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L320)
- GET  /Admin/GetAllProhzLegalFiles — list legal files. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L344)
- GET  /Admin/GetUserList — list users. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L364)
- POST /Admin/GetCategoryById — get category by id. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L392)
- GET  /Admin/GetAllCategories — list categories. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L412)
- POST /Admin/AddCategory — add category. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L436)
- POST /Admin/UpdateCategory — update category. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L456)
- POST /Admin/DeleteCategory — delete category. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L476)
- GET  /Admin/GetAllProhzSalesPerson — list sales persons. [KopkeHome-FMRS_API/Controllers/AdminController.cs](KopkeHome-FMRS_API/Controllers/AdminController.cs#L506)

---

If you want, I can:
- Add MVC/webapp routes (controllers under `KopkeHome-FMRS/` WebApp) as a separate section
- Expand each endpoint with parameter details and response shapes (from service signatures)
- Generate a Postman collection / OpenAPI docs from these controllers

Which next step do you prefer?