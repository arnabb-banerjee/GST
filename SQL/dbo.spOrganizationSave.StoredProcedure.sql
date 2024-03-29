USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spOrganizationSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spOrganizationSave] (
	@OrganizationCode						VARCHAR(50) = '', 
	@infotype								VARCHAR(2), /*an=alert/notifiction, mc = multi curr, p=payment, c=company*/
	@mc_isAllowedMutyCurrency				VARCHAR(1),
	@mc_CurrencyList						VARCHAR(MAX),
	@p_isAllowedOnlinePayment				VARCHAR(1),
	@p_BankAccountNumber					VARCHAR(250),
	@p_BankAccountHolder					VARCHAR(159),
	@p_BankAccountIFSCCode					VARCHAR(20),
	@p_BankAccountIMCRCode					VARCHAR(20),
	@p_BankAccountIBranchName				VARCHAR(50),
	@p_BankAccountIBankName					VARCHAR(50),
	@p_PaypalAccountID						VARCHAR(500),
	@c_isAllowedMultyLanguage				VARCHAR(1),
	@an_isAllowedAlert_GSTDate				VARCHAR(1),
	@an_isAllowedAlert_PaidMembership		VARCHAR(1),
	@an_AlertText_GSTDate					VARCHAR(MAX),
	@an_AlertText_PaidMembership			VARCHAR(MAX), 
	@UserCode								[VARCHAR](50),
	@NewDatauniqueID 						[BIGINT] OUT,
	@ErrorMessage 							[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
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

	IF (SELECT COUNT(*) FROM OrganizationSettings WHERE OrganizationId = @OrganizationId) < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO [dbo].[OrganizationSettings]([OrganizationId]
				,[DataUniqueId],[mc_isAllowedMutyCurrency]
				,[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
				,[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
				,[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
				,[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
				,[an_AlertText_GSTDate],[an_AlertText_PaidMembership]
				,[LastModifiedOn],[LastModifiedBy])

				VALUES
				(@OrganizationID, @NewDatauniqueID, @mc_isAllowedMutyCurrency
				,@mc_CurrencyList,@p_isAllowedOnlinePayment,@p_BankAccountNumber
				,@p_BankAccountHolder,@p_BankAccountIFSCCode,@p_BankAccountIMCRCode
				,@p_BankAccountIBranchName,@p_BankAccountIBankName,@p_PaypalAccountID
				,@c_isAllowedMultyLanguage,@an_isAllowedAlert_GSTDate,@an_isAllowedAlert_PaidMembership
				,@an_AlertText_GSTDate,@an_AlertText_PaidMembership, 
				GETDATE(), @UserID)
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
					,[an_AlertText_GSTDate],[an_AlertText_PaidMembership]
					,[LastModifiedOn],[LastModifiedBy])
				SELECT [id],[OrganizationId]
					,[DataUniqueId],[mc_isAllowedMutyCurrency]
					,[mc_CurrencyList],[p_isAllowedOnlinePayment],[p_BankAccountNumber]
					,[p_BankAccountHolder],[p_BankAccountIFSCCode],[p_BankAccountIMCRCode]
					,[p_BankAccountIBranchName],[p_BankAccountIBankName],[p_PaypalAccountID]
					,[c_isAllowedMultyLanguage],[an_isAllowedAlert_GSTDate],[an_isAllowedAlert_PaidMembership]
					,[an_AlertText_GSTDate],[an_AlertText_PaidMembership]
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
					
						WHERE OrganizationID = @OrganizationID
				END
		END 
END



GO
