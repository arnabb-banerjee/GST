USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSettingsSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
[spSettingsSave] '97421BF6-A62A-4C7E-8812-C539C277FCBE', 'C', '','','','','','','','','','','','','','','','','',''


*/


CREATE PROCEDURE [dbo].[spSettingsSave] (
	@OrganizationCode						VARCHAR(50) = '', 
	@infotype								VARCHAR(2), /*an=alert/notifiction, mc = multi curr, p=payment, c=company*/
	@mc_isAllowedMutyCurrency				VARCHAR(1),
	@mc_CurrencyList						VARCHAR(MAX) = '',
	@p_isAllowedOnlinePayment				VARCHAR(1) = '',
	@p_BankAccountNumber					VARCHAR(250) = '',
	@p_BankAccountHolder					VARCHAR(159) = '',
	@p_BankAccountIFSCCode					VARCHAR(20) = '',
	@p_BankAccountIMCRCode					VARCHAR(20) = '',
	@p_BankAccountIBranchName				VARCHAR(50) = '',
	@p_BankAccountIBankName					VARCHAR(50) = '',
	@p_PaypalAccountID						VARCHAR(500) = '',
	@c_isAllowedMultyLanguage				VARCHAR(1) = '',
	@an_isAllowedAlert_GSTDate				VARCHAR(1) = '',
	@an_isAllowedAlert_PaidMembership		VARCHAR(1) = '',
	@an_AlertText_GSTDate					VARCHAR(MAX) = '',
	@an_AlertText_PaidMembership			VARCHAR(MAX) = '', 
	@CompanyName							VARCHAR(250) = '',
    @Email									VARCHAR(250) = '',
    @Mobile									VARCHAR(50) = '',
    @Address								VARCHAR(500) = '',
    @City									VARCHAR(250) = '',
    @State									BIGINT = 0,
    @Country								BIGINT = 0,
    @Website								VARCHAR(250) = '',
    @CIN									VARCHAR(50) = '',
    @PAN									VARCHAR(50) = '',
    @DefaultEmail							VARCHAR(250) = '',
    @SMTP									VARCHAR(50) = '',
	@UserCode								[VARCHAR](50) = '',
	@NewDatauniqueID 						[BIGINT] OUT,
	@ErrorMessage 							[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	SELECT @OrganizationId = 0
	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END


	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	-- Modified on 31/07/2019
	IF @Email <> '' --OR @Email IS NOT NULL
	BEGIN
	SELECT @IsExists = MAX(x) FROM (
		SELECT COUNT(A.OrganizationId) x FROM OrganizationSettings A WHERE Email = @Email AND ((@OrganizationId < 1) OR (@OrganizationId > 0 AND A.OrganizationId <> @OrganizationId))
		)A
		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Company Email ID is already registered'
		END
	END

	IF @DefaultEmail <> '' --OR @DefaultEmail IS NOT NULL
	BEGIN
	SELECT @IsExists = MAX(x) FROM (
		SELECT COUNT(A.OrganizationId) x FROM OrganizationSettings A WHERE DefaultEmail = @DefaultEmail AND ((@OrganizationId < 1) OR (@OrganizationId > 0 AND A.OrganizationId <> @OrganizationId))
		)A
		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Default Email ID is already registered'
		END
	END
	-- End: Modified on 31/07/2019

	IF (@AllowSaveDeleteData = 'Y') -- Modified on 31/07/2019
	BEGIN

	IF (SELECT COUNT(*) FROM OrganizationSettings WHERE OrganizationId = @OrganizationId) < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO [dbo].[OrganizationSettings]([OrganizationId]
				,[DataUniqueId],[mc_isAllowedMutyCurrency]
				,[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
				,[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
				,[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
				,[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
				,[an_AlertText_GSTDate],[an_AlertText_PaidMembership], CompanyName, Email
				,[Mobile], [Address], City, [State], Country, Website, CIN, PAN, DefaultEmail, SMTP

				,[LastModifiedOn],[LastModifiedBy])

				VALUES
				(@OrganizationID, @NewDatauniqueID, @mc_isAllowedMutyCurrency
				,@mc_CurrencyList,@p_isAllowedOnlinePayment,@p_BankAccountNumber
				,@p_BankAccountHolder,@p_BankAccountIFSCCode,@p_BankAccountIMCRCode
				,@p_BankAccountIBranchName,@p_BankAccountIBankName,@p_PaypalAccountID
				,@c_isAllowedMultyLanguage,@an_isAllowedAlert_GSTDate,@an_isAllowedAlert_PaidMembership
				,@an_AlertText_GSTDate,@an_AlertText_PaidMembership, @CompanyName, @Email
				,@Mobile, @Address, @City, @State, @Country, @Website, @CIN, @PAN, @DefaultEmail, @SMTP
				,GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
				SELECT @OldDatauniqueID = DatauniqueID FROM OrganizationSettings WHERE OrganizationId = @OrganizationId
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM OrganizationSettings WHERE OrganizationId = @OrganizationId
					
				INSERT INTO [dbo].[OrganizationSettingsAudit]
					([id],[OrganizationId]
					,[DataUniqueId],[mc_isAllowedMutyCurrency]
					,[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
					,[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
					,[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
					,[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
					,[an_AlertText_GSTDate],[an_AlertText_PaidMembership], CompanyName, Email
					,[Mobile], [Address], City, [State], Country, Website, CIN, PAN, DefaultEmail, SMTP
					,[LastModifiedOn],[LastModifiedBy])
				SELECT [id],[OrganizationId]
					,[DataUniqueId],[mc_isAllowedMutyCurrency]
					,[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
					,[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
					,[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
					,[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
					,[an_AlertText_GSTDate],[an_AlertText_PaidMembership], CompanyName, Email
					,[Mobile], [Address], City, [State], Country, Website, CIN, PAN, DefaultEmail, SMTP
					,[LastModifiedOn],[LastModifiedBy]
				FROM [OrganizationSettings]
				WHERE OrganizationId = @OrganizationId
				   				  
				IF @infotype = 'AN'
				BEGIN
					UPDATE [OrganizationSettings] SET 
				  			DatauniqueID = @NewDatauniqueID
							,[an_isAllowedAlert_GSTDate] = @an_isAllowedAlert_GSTDate
							,[an_isAllowedAlert_PaidMembership] = @an_isAllowedAlert_PaidMembership
							,[an_AlertText_GSTDate] = @an_AlertText_GSTDate
							,[an_AlertText_PaidMembership] = @an_AlertText_PaidMembership
							,[LastModifiedOn] = GETDATE()
							,[LastModifiedBy] = @UserID
					
						WHERE OrganizationID = @OrganizationID
				END
				ELSE IF @infotype = 'MC'
				BEGIN
					UPDATE [OrganizationSettings] SET 
				  			DatauniqueID = @NewDatauniqueID
							,[mc_isAllowedMutyCurrency] = @mc_isAllowedMutyCurrency
							,[mc_CurrencyList] = @mc_CurrencyList
							,[LastModifiedOn] = GETDATE()
							,[LastModifiedBy] = @UserID
					
						WHERE OrganizationID = @OrganizationID
				END
				ELSE IF @infotype = 'P'
				BEGIN
					UPDATE [OrganizationSettings] SET 
				  			DatauniqueID = @NewDatauniqueID
							,[p_isAllowedOnlinePayment] = @p_isAllowedOnlinePayment
							,[p_BankAccountNumber] = @p_BankAccountNumber
							,[p_BankAccountHolder] = @p_BankAccountHolder
							,[p_BankAccountIFSCCode] = @p_BankAccountIFSCCode
							,[p_BankAccountIMCRCode] = @p_BankAccountIMCRCode
							,[p_BankAccountIBranchName] = @p_BankAccountIBranchName
							,[p_BankAccountIBankName] = @p_BankAccountIBankName
							,[p_PaypalAccountID] = @p_PaypalAccountID
							,[LastModifiedOn] = GETDATE()
							,[LastModifiedBy] = @UserID
					
						WHERE OrganizationID = @OrganizationID
				END
				ELSE IF @infotype = 'C'
				BEGIN
					UPDATE [OrganizationSettings] SET 
				  			DatauniqueID = @NewDatauniqueID
							,[c_isAllowedMultyLanguage] = @c_isAllowedMultyLanguage
							,[LastModifiedOn] = GETDATE()
							,[LastModifiedBy] = @UserID

							, CompanyName = @CompanyName
							, Email = @Email
							, [Mobile] = @Mobile
							, [Address] = @Address
							, City = @City
							, [State] = @State
							, Country = @Country
							, Website = @Website
							, CIN = @CIN
							, PAN = @PAN
							, DefaultEmail = @DefaultEmail
							, SMTP = @SMTP
					
						WHERE OrganizationID = @OrganizationID
				END
		END
	END 
END




GO
