USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spCUSMSTCustomerSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCUSMSTCustomerSave] (
	@UserType											VARCHAR(1) = NULL, 
	@CusID												BIGINT, 
	@Title												VARCHAR(50) = NULL,
	@FirstName											VARCHAR(150) = NULL,
	@MiddleName											VARCHAR(50) = NULL,
	@LastName											VARCHAR(150) = NULL,
	@Safix												VARCHAR(50) = NULL,
	@Sex												VARCHAR(1) = NULL,
	@DOB												VARCHAR(50) = NULL,

	@OrganizationCode									VARCHAR(50) = NULL, 
	
	@EmailID											VARCHAR(500) = NULL, 
	@Mobile												VARCHAR(500) = NULL, 
	@Street1											VARCHAR(500) = NULL, 
	@Street2											VARCHAR(500) = NULL,
	@City												VARCHAR(50) = NULL, 
	@State												BIGINT = NULL, 
	@Country											BIGINT = NULL,
	@Website											VARCHAR(50) = NULL,
	
	@GSTRegistrationType								BIGINT = NULL, 
	@GSTIN												VARCHAR(50) = NULL, 
	@IsSubCustomer										VARCHAR(1) = NULL, 
	@ParentCusID										BIGINT = NULL,
	@BillingWith										VARCHAR(50) = NULL, 
	@Notes												VARCHAR(50) = NULL, 
	@PrefferedPaymentMethod								VARCHAR(50) = NULL,
	@PrefferedDeliveryMethod							VARCHAR(50) = NULL,
	@TaxRegNo											VARCHAR(50) = NULL, 
	@CSTRegNo											VARCHAR(50) = NULL, 
	@PANNo												VARCHAR(50) = NULL,
	@Terms												BIGINT = NULL,
	
	@OpeningBalance										NUMERIC(18,2) = NULL,
	@AsOfDate											VARCHAR(50) = NULL,

	@ShippingStreet1									VARCHAR(500) = NULL, 
	@ShippingStreet2									VARCHAR(500) = NULL,
	@ShippingCity										VARCHAR(50) = NULL, 
	@ShippingState										BIGINT = NULL, 
	@ShippingCountry									BIGINT = NULL,

	@BillingStreet1										VARCHAR(500) = NULL, 
	@BillingStreet2										VARCHAR(500) = NULL,
	@BillingCity										VARCHAR(50) = NULL, 
	@BillingState										BIGINT = NULL, 
	@BillingCountry										BIGINT = NULL,

	@IsActive  											VARCHAR(1) = NULL,
	@UserCode											VARCHAR(50) = '',
	@isOnlyDelete										VARCHAR(1) = NULL,
	@NewCusID 											BIGINT OUT,
	@NewDatauniqueID 									BIGINT OUT,
	@ErrorMessage 										VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	DECLARE @UserID BIGINT
	SELECT	@UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END


	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(CusID) x FROM CUSMSTCustomerAddressBilling WHERE CusID = @CusID
			UNION
			SELECT COUNT(CusID) x FROM CUSMSTCustomerAddressBillingAudit WHERE CusID = @CusID
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Customer can not be deleted as the same Customer is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(A.CusID) x FROM CUSMSTCustomerContact A, CUSMSTCustomerMaster B WHERE A.CusID = B.CusID AND B.OrganizationID = @OrganizationID AND EmailID = @EmailID AND ((@CusID < 1) OR (@CusID > 0 AND A.CusID <> @CusID))
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Email ID is already registered'
		END
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(A.CusID) x FROM CUSMSTCustomerContact A, CUSMSTCustomerMaster B WHERE A.CusID = B.CusID AND B.OrganizationID = @OrganizationID AND Mobile = @Mobile AND ((@CusID < 1) OR (@CusID > 0 AND A.CusID <> @CusID))
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Mobile is already registered'
		END
			
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN
		SELECT @DOB = CASE WHEN(@DOB = '') THEN NULL ELSE CAST(dbo.ConvertToDate(@DOB) as VARCHAR(50)) END
		SELECT @AsOfDate = CASE WHEN(@AsOfDate = '') THEN NULL ELSE CAST(dbo.ConvertToDate(@AsOfDate) as VARCHAR(50)) END
		 		
		IF @CusID < 1
		BEGIN 
			BEGIN TRANSACTION T1;
			BEGIN TRY

				SELECT @NewDatauniqueID = 1
		
				INSERT INTO CUSMSTCustomerMaster (DatauniqueID, ActivityType, IsActive, Title, FirstName, MiddleName, LastName, Safix, DOB, Sex, 
						IsSubCustomer, ParentCusID, BillingWith, LastModifiedOn, LastModifiedBy)
				VALUES( @NewDatauniqueID, 'A', NULLIF(@IsActive, ''), NULLIF(@Title, ''), NULLIF(@FirstName, ''), NULLIF(@MiddleName, ''), NULLIF(@LastName, ''), NULLIF(@Safix, ''), @DOB, NULLIF(@Sex, ''), 
						NULLIF(@IsSubCustomer, ''), NULLIF(@ParentCusID, '0'), NULLIF(@BillingWith, ''), GETDATE(), @UserID)

				SELECT @NewCusID = @@Identity

				INSERT INTO CUSMSTCustomerContact(CusID, DatauniqueID, EmailID, Mobile, Street1, Street2, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@EmailID, ''), NULLIF(@Mobile, ''), NULLIF(@Street1, ''), NULLIF(@Street2, ''), NULLIF(@City, ''), NULLIF(@State, '0'), NULLIF(@Country, '0'), NULLIF(@Website, ''), GETDATE(), @UserID)

				INSERT INTO CUSMSTCustomerTaxRelated(CusID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@GSTRegistrationType, '0'), NULLIF(@GSTIN, ''), NULLIF(@TaxRegNo, ''), NULLIF(@CSTRegNo, ''), NULLIF(@PANNo, ''), NULLIF(@Terms, '0'), GETDATE(), @UserID)
				
				INSERT INTO CUSMSTCustomerPreferedMethods (CusID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@PrefferedPaymentMethod, '0'), NULLIF(@PrefferedDeliveryMethod, '0'), GETDATE(),@UserID)

				INSERT INTO CUSMSTCustomerNotes (CusID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@Notes, ''), GETDATE(),@UserID)

				INSERT INTO CUSMSTCustomerOpeningBalance (CusID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
				VALUES(@NewCusID, NULLIF(@OpeningBalance, '0'), @AsOfDate, GETDATE(), @UserID)

				INSERT INTO CUSMSTCustomerAddressBilling (CusID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@BillingStreet1, ''), NULLIF(@BillingStreet2, ''), NULLIF(@BillingCity, ''), NULLIF(@BillingState, '0'), NULLIF(@BillingCountry, '0'), GETDATE(), @UserID)

				INSERT INTO CUSMSTCustomerAddressShipping (CusID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
				VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@ShippingStreet1, ''), NULLIF(@ShippingStreet2, ''), NULLIF(@ShippingCity, ''), NULLIF(@ShippingState, '0'), NULLIF(@ShippingCountry, '0'), GETDATE(), @UserID)
		
				INSERT INTO OrganizationCustSuppMapping (UserID, DatauniqueID, OrganizationID, UserType)
				VALUES(@NewCusID, @NewDatauniqueID, @OrganizationId, @UserType)

				IF @@TRANCOUNT > 0
				COMMIT;
			END TRY
			BEGIN CATCH
				SELECT @ErrorMessage = ERROR_MESSAGE()

				IF @@TRANCOUNT > 0
					ROLLBACK;
				throw
			END CATCH
		END
		ELSE
		BEGIN 
			SELECT @NewCusID = @CusID
			SELECT @OldDatauniqueID = DatauniqueID FROM CUSMSTCustomerMasterAudit WHERE CusID = @CusID
				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				BEGIN TRANSACTION T1;
				BEGIN TRY
					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerMaster
							WHERE  ISNULL(Safix, '') = ISNULL(@Safix, '')
							AND ISNULL(Title, '') = ISNULL(@Title, '')
							AND ISNULL(FirstName, '') = ISNULL(@FirstName, '')
							AND ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
							AND ISNULL(LastName, '') = ISNULL(@LastName, '') 
							AND ISNULL(Sex, '') = ISNULL(@Sex, '')
							AND ISNULL(DOB, '') = ISNULL(@DOB, '')
		
							AND ISNULL(IsSubCustomer, '') = ISNULL(@IsSubCustomer, '') 
							AND ISNULL(ParentCusID, 0) = ISNULL(@ParentCusID, 0)
							AND ISNULL(BillingWith, '') = ISNULL(@BillingWith, '')
							AND ISNULL(IsActive, '') = ISNULL(@IsActive, '')
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerMaster WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerMasterAudit (
							CusID,DatauniqueID,ActivityType,IsActive,
							Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
							IsSubCustomer,ParentCusID,BillingWith,
							OrganizationID,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,ActivityType,IsActive,
							Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
							IsSubCustomer,ParentCusID,BillingWith,
							OrganizationID,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerMaster
						WHERE CusID = @CusID

				  		UPDATE CUSMSTCustomerMaster SET 
				  			DatauniqueID = @NewDatauniqueID,
							Safix = NULLIF(@Safix, ''),  
							Title = NULLIF(@Title, ''),
							FirstName = NULLIF(@FirstName, ''),
							MiddleName = NULLIF(@MiddleName, ''),
							LastName = NULLIF(@LastName, ''),
							Sex = NULLIF(@Sex, ''),
							DOB = @DOB,
		
							IsSubCustomer = NULLIF(@IsSubCustomer, ''), 
							ParentCusID = NULLIF(@ParentCusID, ''),
							BillingWith = NULLIF(@BillingWith, ''),
				  			
							ActivityType = 'M', 
							IsActive = NULLIF(@IsActive, ''),
							LastModifiedOn = GETDATE(), 
							LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerContact
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerContact(CusID, DatauniqueID, EmailID, Mobile, Street1, Street2, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@EmailID, ''), NULLIF(@Mobile, ''), NULLIF(@Street1, ''), NULLIF(@Street2, ''), NULLIF(@City, ''), NULLIF(@State, '0'), NULLIF(@Country, '0'), NULLIF(@Website, ''), GETDATE(), @UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerContact
							WHERE  ISNULL(EmailID, '') = ISNULL(@EmailID, '')
							AND ISNULL(Mobile, '') = ISNULL(@Mobile, '')
							AND ISNULL(Website, '') = ISNULL(@Website, '')
							AND ISNULL(Street1, '') = ISNULL(@Street1, '') 
							AND ISNULL(Street2, '') = ISNULL(Street2, '') 
							AND ISNULL(City, '') = ISNULL(@City, '')
							AND ISNULL(State, 0) = ISNULL(@State, 0) 
							AND ISNULL(Country, 0) = ISNULL(@Country, 0)
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerContact WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerContactAudit(
							CusID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerContact
						WHERE CusID = @CusID
				   	

				  		UPDATE CUSMSTCustomerContact SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			EmailID = NULLIF(@EmailID, ''), 
				  			Mobile = NULLIF(@Mobile, ''),
				  			Website = NULLIF(@Website, ''),
				  			Street1 = NULLIF(@Street1, ''),
				  			Street2 = NULLIF(@Street2, ''),
				  			City = NULLIF(@City, ''),
				  			State = NULLIF(@State, ''),
				  			Country = NULLIF(@Country, ''),
		
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerTaxRelated
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerTaxRelated(CusID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@GSTRegistrationType, '0'), NULLIF(@GSTIN, ''), NULLIF(@TaxRegNo, ''), NULLIF(@CSTRegNo, ''), NULLIF(@PANNo, ''), NULLIF(@Terms, '0'), GETDATE(), @UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerTaxRelated
							WHERE  ISNULL(GSTRegistrationType, 0) = ISNULL(GSTRegistrationType, 0)
							AND ISNULL(GSTIN, '') = ISNULL(@GSTIN, '')
							AND ISNULL(TaxRegNo, '') = ISNULL(@TaxRegNo, '')
							AND ISNULL(CSTRegNo, '') = ISNULL(@CSTRegNo, '') 
							AND ISNULL(PANNo, '') = ISNULL(@PANNo, '') 
							AND ISNULL(Terms, 0) = ISNULL(@Terms, 0) 
							AND CusID = @CusID
							AND (ISNULL(RTRIM(LTRIM(@GSTRegistrationType)), '') <> '' OR ISNULL(RTRIM(LTRIM(@GSTIN)), '') <> '')  
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerTaxRelated WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerTaxRelatedAudit(
							CusID,DatauniqueID,
							GSTRegistrationType,GSTIN,
							TaxRegNo,CSTRegNo,PANNo,Terms,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,
							GSTRegistrationType,GSTIN,
							TaxRegNo,CSTRegNo,PANNo,@Terms,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerTaxRelated
						WHERE CusID = @CusID
 				  

				  		UPDATE CUSMSTCustomerTaxRelated SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			GSTRegistrationType = NULLIF(@GSTRegistrationType, ''), 
				  			GSTIN = NULLIF(@GSTIN, ''),
				  			TaxRegNo = NULLIF(@TaxRegNo, ''),
				  			CSTRegNo = NULLIF(@CSTRegNo, ''),
				  			PANNo = NULLIF(@PANNo, ''),
							Terms = NULLIF(@Terms, ''),
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID

					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerPreferedMethods
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerPreferedMethods (CusID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@PrefferedPaymentMethod, '0'), NULLIF(@PrefferedDeliveryMethod, '0'), GETDATE(),@UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerPreferedMethods
							WHERE  ISNULL(PrefferedPaymentMethod, '') = ISNULL(@PrefferedPaymentMethod, '')
							AND ISNULL(PrefferedDeliveryMethod, '') = ISNULL(@PrefferedDeliveryMethod, '')
							AND CusID = @CusID
							AND (ISNULL(RTRIM(LTRIM(@PrefferedDeliveryMethod)), '') <> '' OR ISNULL(RTRIM(LTRIM(@PrefferedPaymentMethod)), '') <> '')  
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerPreferedMethods WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerPreferedMethodsAudit(
							CusID,DatauniqueID,
							PrefferedPaymentMethod,PrefferedDeliveryMethod,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,
							PrefferedPaymentMethod,PrefferedDeliveryMethod,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerPreferedMethods
						WHERE CusID = @CusID

				  		UPDATE CUSMSTCustomerPreferedMethods SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			PrefferedPaymentMethod = NULLIF(@PrefferedPaymentMethod, ''),
				  			PrefferedDeliveryMethod = NULLIF(@PrefferedDeliveryMethod, ''),
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID

					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerNotes
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerNotes (CusID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@Notes, ''), GETDATE(),@UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerNotes
							WHERE  ISNULL(Notes, '') = ISNULL(@Notes, '')
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerNotes WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerNotesAudit(
							CusID,DatauniqueID,Notes,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,Notes,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerNotes
						WHERE CusID = @CusID

				  		UPDATE CUSMSTCustomerNotes SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			Notes = NULLIF(@Notes, ''),
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerOpeningBalance
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerOpeningBalance (CusID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
						VALUES(@NewCusID, NULLIF(@OpeningBalance, '0'), @AsOfDate, GETDATE(), @UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerOpeningBalance
							WHERE  ISNULL(OpeningBalance, 0) = ISNULL(@OpeningBalance, 0)
							AND  ISNULL(@AsOfDate, '') = ISNULL(@AsOfDate, '')
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerNotes WHERE CusID = @CusID

						INSERT INTO CUSMSTCustomerOpeningBalanceAudit (
							CusID,DatauniqueID,OpeningBalance, AsOfDate,
							LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
						SELECT CusID,DatauniqueID,OpeningBalance, AsOfDate,
							LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
						FROM CUSMSTCustomerOpeningBalance
						WHERE CusID = @CusID
						
						UPDATE CUSMSTCustomerOpeningBalance SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			OpeningBalance = NULLIF(@OpeningBalance, '0'), 
							AsOfDate = @AsOfDate, 
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerAddressShipping
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerAddressShipping (CusID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@ShippingStreet1, ''), NULLIF(@ShippingStreet2, ''), NULLIF(@ShippingCity, ''), NULLIF(@ShippingState, '0'), NULLIF(@ShippingCountry, '0'), GETDATE(), @UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerAddressShipping
							WHERE  ISNULL(Street1, '') = ISNULL(@ShippingStreet1, '') 
				  			AND ISNULL(Street2, '') = ISNULL(@ShippingStreet2, '')
				  			AND ISNULL(City, '') = ISNULL(@ShippingCity, '')
				  			AND ISNULL(State, 0) = ISNULL(@ShippingState, 0) 
				  			AND ISNULL(Country, 0) = ISNULL(@ShippingCountry, 0)
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerAddressShipping WHERE CusID = @CusID

						INSERT INTO [CUSMSTCustomerAddressShippingAudit] (
							[DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
						SELECT [DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
						FROM [CUSMSTCustomerAddressShipping]
						WHERE CusID = @CusID
						
						UPDATE [CUSMSTCustomerAddressShipping] SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			Street1 = NULLIF(@ShippingStreet1, ''), 
				  			Street2 = NULLIF(@ShippingStreet2, ''),
				  			City = NULLIF(@ShippingCity, ''),
				  			State = NULLIF(@ShippingState, '0'),
				  			Country = NULLIF(@ShippingCountry, '0'),
							LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerAddressBilling
							WHERE  CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = 1

						INSERT INTO CUSMSTCustomerAddressBilling (CusID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
						VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@BillingStreet1, ''), NULLIF(@BillingStreet2, ''), NULLIF(@BillingCity, ''), NULLIF(@BillingState, '0'), NULLIF(@BillingCountry, '0'), GETDATE(), @UserID)
					END
					ELSE IF (NOT EXISTS(
							SELECT DatauniqueID
							FROM   CUSMSTCustomerAddressBilling
							WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  			AND ISNULL(Street2, '') = ISNULL(@BillingStreet2, '')
				  			AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  			AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  			AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
							AND CusID = @CusID
						)
					)
					BEGIN
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerAddressBilling WHERE CusID = @CusID

						INSERT INTO [CUSMSTCustomerAddressBillingAudit] (
							[DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
						SELECT [DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
						FROM [CUSMSTCustomerAddressBilling]
						WHERE CusID = @CusID
						
						UPDATE [CUSMSTCustomerAddressBilling] SET 
				  			DatauniqueID = @NewDatauniqueID,
				  			Street1 = NULLIF(@BillingStreet1, ''),
				  			Street2 = NULLIF(@BillingStreet2, ''),
				  			City = NULLIF(@BillingCity, ''),
				  			State = NULLIF(@BillingState, '0'),
				  			Country = NULLIF(@BillingCountry, '0'),
							LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
							WHERE CusID = @CusID
					END

					IF @@TRANCOUNT > 0
					COMMIT;
				END TRY
				BEGIN CATCH
					SELECT @ErrorMessage = ERROR_MESSAGE()

					IF @@TRANCOUNT > 0
						ROLLBACK;
					
					RAISERROR(@ErrorMessage, 16, 1)

					--throw
				END CATCH
			END 
			ELSE 
			BEGIN
				BEGIN TRANSACTION T1;
				BEGIN TRY
					INSERT INTO CUSMSTCustomerContactAudit(
						CusID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerContact
					WHERE CusID = @CusID

					INSERT INTO CUSMSTCustomerMasterAudit (
						CusID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubCustomer,ParentCusID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubCustomer,ParentCusID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerMaster
					WHERE CusID = @CusID

					INSERT INTO CUSMSTCustomerTaxRelatedAudit(
						CusID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,Terms,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,@Terms,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerTaxRelated
					WHERE CusID = @CusID
 				  
					INSERT INTO CUSMSTCustomerPreferedMethodsAudit(
						CusID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerPreferedMethods
					WHERE CusID = @CusID

					INSERT INTO CUSMSTCustomerNotesAudit(
						CusID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerNotes
					WHERE CusID = @CusID

					INSERT INTO CUSMSTCustomerOpeningBalanceAudit (
						CusID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT CusID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM CUSMSTCustomerOpeningBalance
					WHERE CusID = @CusID

					INSERT INTO [CUSMSTCustomerAddressBillingAudit] (
						[DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [CUSMSTCustomerAddressBilling]
					WHERE CusID = @CusID
					
 					INSERT INTO [CUSMSTCustomerAddressShippingAudit] (
						[DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[CusID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [CUSMSTCustomerAddressShipping]
					WHERE CusID = @CusID
					
					DELETE FROM CUSMSTCustomerMaster WHERE CusID = @CusID
					DELETE FROM CUSMSTCustomerContact WHERE CusID = @CusID
					DELETE FROM CUSMSTCustomerTaxRelated WHERE CusID = @CusID
					DELETE FROM CUSMSTCustomerPreferedMethods WHERE CusID = @CusID
					DELETE FROM CUSMSTCustomerNotes WHERE CusID = @CusID
					DELETE FROM CUSMSTCustomerOpeningBalance WHERE CusID = @CusID
					
					IF @@TRANCOUNT > 0
					COMMIT;
				END TRY
				BEGIN CATCH
					SELECT @ErrorMessage = ERROR_MESSAGE()

					IF @@TRANCOUNT > 0
						ROLLBACK;
					throw
				END CATCH
			END
		END 
	END
END 



GO
