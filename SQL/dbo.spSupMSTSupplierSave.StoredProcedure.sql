USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSupMSTSupplierSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
[spSupMSTSupplierSave] 0,'','','','','','','','','','','','','','0','0','','0','','N','0','','','0','0','','','','','0.0','','','','','0','0','','','','0','0','','0','','','',''
[spSupMSTSupplierSave] 14,'','','','','','','','','','','','','','0','1','','0','','N','0','','','0','0','','','','','0.0','','','','','0','0','','','','0','0','','0','','','',''
*/

CREATE PROCEDURE [dbo].[spSupMSTSupplierSave] (
	@SupID												BIGINT, 
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
	@IsSubSupplier										VARCHAR(1) = NULL, 
	@ParentSupID										BIGINT = NULL,
	@BillingWith										VARCHAR(50) = NULL, 
	@Notes												VARCHAR(50) = NULL, 
	@PrefferedPaymentMethod								VARCHAR(50) = NULL,
	@PrefferedDeliveryMethod							VARCHAR(50) = NULL,
	@TaxRegNo											VARCHAR(50) = NULL, 
	@CSTRegNo											VARCHAR(50) = NULL, 
	@PANNo												VARCHAR(50) = NULL,
	@Terms												BIGINT = NULL,
	
	@OpeningBalance										NUMERIC(18, 2) = NULL,
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
	@NewSupID 											BIGINT OUT,
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
			SELECT COUNT(SupID) x FROM SupMSTSupplierAddressBilling WHERE SupID = @SupID
			UNION
			SELECT COUNT(SupID) x FROM SupMSTSupplierAddressBillingAudit WHERE SupID = @SupID
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Supplier can not be deleted as the same Supplier is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(A.SupID) x FROM SupMSTSupplierContact A, SupMSTSupplierMaster B WHERE A.SupID = B.SupID AND B.OrganizationID = @OrganizationID AND EmailID = @EmailID AND ((@SupID < 1) OR (@SupID > 0 AND A.SupID <> @SupID))
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Email ID is already registered'
		END
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(A.SupID) x FROM SupMSTSupplierContact A, SupMSTSupplierMaster B WHERE A.SupID = B.SupID AND B.OrganizationID = @OrganizationID AND Mobile = @Mobile AND ((@SupID < 1) OR (@SupID > 0 AND A.SupID <> @SupID))
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
		 		
		IF @SupID < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO SupMSTSupplierMaster (DatauniqueID, ActivityType, IsActive, Title, FirstName, MiddleName, LastName, Safix, DOB, Sex, 
					IsSubSupplier, ParentSupID, BillingWith, OrganizationID, LastModifiedOn, LastModifiedBy)
			VALUES( @NewDatauniqueID, 'A', NULLIF(@IsActive, ''), NULLIF(@Title, ''), NULLIF(@FirstName, ''), NULLIF(@MiddleName, ''), NULLIF(@LastName, ''), NULLIF(@Safix, ''), @DOB, NULLIF(@Sex, ''), 
					NULLIF(@IsSubSupplier, ''), NULLIF(@ParentSupID, '0'), NULLIF(@BillingWith, ''), NULLIF(@OrganizationID, ''), GETDATE(), @UserID)

			SELECT @NewSupID = @@Identity

			INSERT INTO SupMSTSupplierContact(SupID, DatauniqueID, EmailID, Mobile, Street1, Street2, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@EmailID, ''), NULLIF(@Mobile, ''), NULLIF(@Street1, ''), NULLIF(@Street2, ''), NULLIF(@City, ''), NULLIF(@State, '0'), NULLIF(@Country, '0'), NULLIF(@Website, ''), GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierTaxRelated(SupID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@GSTRegistrationType, '0'), NULLIF(@GSTIN, ''), NULLIF(@TaxRegNo, ''), NULLIF(@CSTRegNo, ''), NULLIF(@PANNo, ''), NULLIF(@Terms, '0'), GETDATE(), @UserID)
				
			INSERT INTO SupMSTSupplierPreferedMethods (SupID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@PrefferedPaymentMethod, '0'), NULLIF(@PrefferedDeliveryMethod, '0'), GETDATE(),@UserID)

			INSERT INTO SupMSTSupplierNotes (SupID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@Notes, ''), GETDATE(),@UserID)

			INSERT INTO SupMSTSupplierOpeningBalance (SupID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, NULLIF(@OpeningBalance, '0'), @AsOfDate, GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierAddressBilling (SupID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@BillingStreet1, ''), NULLIF(@BillingStreet2, ''), NULLIF(@BillingCity, ''), NULLIF(@BillingState, '0'), NULLIF(@BillingCountry, '0'), GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierAddressShipping (SupID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@ShippingStreet1, ''), NULLIF(@ShippingStreet2, ''), NULLIF(@ShippingCity, ''), NULLIF(@ShippingState, '0'), NULLIF(@ShippingCountry, '0'), GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @NewSupID = @SupID
			SELECT @OldDatauniqueID = DatauniqueID FROM SupMSTSupplierMasterAudit WHERE SupID = @SupID
				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierMaster
						WHERE  ISNULL(Safix, '') = ISNULL(@Safix, '')
						AND ISNULL(Title, '') = ISNULL(@Title, '')
						AND ISNULL(FirstName, '') = ISNULL(@FirstName, '')
						AND ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
						AND ISNULL(LastName, '') = ISNULL(@LastName, '') 
						AND ISNULL(Sex, '') = ISNULL(@Sex, '')
						AND ISNULL(DOB, '') = ISNULL(@DOB, '')
		
						AND ISNULL(IsSubSupplier, '') = ISNULL(@IsSubSupplier, '') 
						AND ISNULL(ParentSupID, 0) = ISNULL(@ParentSupID, 0)
						AND ISNULL(BillingWith, '') = ISNULL(@BillingWith, '')
						AND ISNULL(IsActive, '') = ISNULL(@IsActive, '')
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierMaster WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierMasterAudit (
						SupID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubSupplier,ParentSupID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubSupplier,ParentSupID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierMaster
					WHERE SupID = @SupID

				  	UPDATE SupMSTSupplierMaster SET 
				  		DatauniqueID = @NewDatauniqueID,
						Safix = NULLIF(@Safix, ''),  
						Title = NULLIF(@Title, ''),
						FirstName = NULLIF(@FirstName, ''),
						MiddleName = NULLIF(@MiddleName, ''),
						LastName = NULLIF(@LastName, ''),
						Sex = NULLIF(@Sex, ''),
						DOB = @DOB,
		
						IsSubSupplier = NULLIF(@IsSubSupplier, ''), 
						ParentSupID = NULLIF(@ParentSupID, ''),
						BillingWith = NULLIF(@BillingWith, ''),
				  			
						ActivityType = 'M', 
						IsActive = NULLIF(@IsActive, ''),
						LastModifiedOn = GETDATE(), 
						LastModifiedBy = @UserID
						WHERE SupID = @SupID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierContact
						WHERE  ISNULL(EmailID, '') = ISNULL(@EmailID, '')
						AND ISNULL(Mobile, '') = ISNULL(@Mobile, '')
						AND ISNULL(Website, '') = ISNULL(@Website, '')
						AND ISNULL(Street1, '') = ISNULL(@Street1, '') 
						AND ISNULL(Street2, '') = ISNULL(Street2, '') 
						AND ISNULL(City, '') = ISNULL(@City, '')
						AND ISNULL(State, 0) = ISNULL(@State, 0) 
						AND ISNULL(Country, 0) = ISNULL(@Country, 0)
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierContact WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierContactAudit(
						SupID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierContact
					WHERE SupID = @SupID
				   	

				  	UPDATE SupMSTSupplierContact SET 
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
						WHERE SupID = @SupID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierTaxRelated
						WHERE  ISNULL(GSTRegistrationType, 0) = ISNULL(GSTRegistrationType, 0)
						AND ISNULL(GSTIN, '') = ISNULL(@GSTIN, '')
						AND ISNULL(TaxRegNo, '') = ISNULL(@TaxRegNo, '')
						AND ISNULL(CSTRegNo, '') = ISNULL(@CSTRegNo, '') 
						AND ISNULL(PANNo, '') = ISNULL(@PANNo, '') 
						AND ISNULL(Terms, 0) = ISNULL(@Terms, 0) 
						AND SupID = @SupID
						AND (ISNULL(RTRIM(LTRIM(@GSTRegistrationType)), '') <> '' OR ISNULL(RTRIM(LTRIM(@GSTIN)), '') <> '')  
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierTaxRelated WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierTaxRelatedAudit(
						SupID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,Terms,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,@Terms,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierTaxRelated
					WHERE SupID = @SupID
 				  

				  	UPDATE SupMSTSupplierTaxRelated SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		GSTRegistrationType = NULLIF(@GSTRegistrationType, ''), 
				  		GSTIN = NULLIF(@GSTIN, ''),
				  		TaxRegNo = NULLIF(@TaxRegNo, ''),
				  		CSTRegNo = NULLIF(@CSTRegNo, ''),
				  		PANNo = NULLIF(@PANNo, ''),
						Terms = NULLIF(@Terms, ''),
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID

				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierPreferedMethods
						WHERE  ISNULL(PrefferedPaymentMethod, '') = ISNULL(@PrefferedPaymentMethod, '')
						AND ISNULL(PrefferedDeliveryMethod, '') = ISNULL(@PrefferedDeliveryMethod, '')
						AND SupID = @SupID
						AND (ISNULL(RTRIM(LTRIM(@PrefferedDeliveryMethod)), '') <> '' OR ISNULL(RTRIM(LTRIM(@PrefferedPaymentMethod)), '') <> '')  
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierPreferedMethods WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierPreferedMethodsAudit(
						SupID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierPreferedMethods
					WHERE SupID = @SupID

				  	UPDATE SupMSTSupplierPreferedMethods SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		PrefferedPaymentMethod = NULLIF(@PrefferedPaymentMethod, ''),
				  		PrefferedDeliveryMethod = NULLIF(@PrefferedDeliveryMethod, ''),
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID

				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierNotes
						WHERE  ISNULL(Notes, '') = ISNULL(@Notes, '')
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierNotes WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierNotesAudit(
						SupID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierNotes
					WHERE SupID = @SupID

				  	UPDATE SupMSTSupplierNotes SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Notes = NULLIF(@Notes, ''),
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierOpeningBalance
						WHERE  ISNULL(OpeningBalance, 0) = ISNULL(@OpeningBalance, 0)
						AND  ISNULL(@AsOfDate, '') = ISNULL(@AsOfDate, '')
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierNotes WHERE SupID = @SupID

					INSERT INTO SupMSTSupplierOpeningBalanceAudit (
						SupID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SupID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SupMSTSupplierOpeningBalance
					WHERE SupID = @SupID
						
					UPDATE SupMSTSupplierOpeningBalance SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		OpeningBalance = NULLIF(@OpeningBalance, '0'), 
						AsOfDate = @AsOfDate, 
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierAddressShipping
						WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  		AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
				  		AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  		AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  		AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierAddressShipping WHERE SupID = @SupID

					INSERT INTO [SupMSTSupplierAddressShippingAudit] (
						[DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [SupMSTSupplierAddressShipping]
					WHERE SupID = @SupID
						
					UPDATE [SupMSTSupplierAddressShippingAudit] SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Street1 = NULLIF(@BillingStreet1, ''), 
				  		Street2 = NULLIF(@BillingStreet1, ''),
				  		City = NULLIF(@BillingCity, ''),
				  		State = NULLIF(@BillingState, '0'),
				  		Country = NULLIF(@BillingCountry, '0'),
						LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SupMSTSupplierAddressBilling
						WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  		AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
				  		AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  		AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  		AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
						AND SupID = @SupID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierAddressBilling WHERE SupID = @SupID

					INSERT INTO [SupMSTSupplierAddressBillingAudit] (
						[DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [SupMSTSupplierAddressBilling]
					WHERE SupID = @SupID
						
					UPDATE [SupMSTSupplierAddressBillingAudit] SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Street1 = NULLIF(@BillingStreet1, ''),
				  		Street2 = NULLIF(@BillingStreet1, ''),
				  		City = NULLIF(@BillingCity, ''),
				  		State = NULLIF(@BillingState, '0'),
				  		Country = NULLIF(@BillingCountry, '0'),
						LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SupID = @SupID
				END
			END 
			ELSE 
			BEGIN
				INSERT INTO SupMSTSupplierContactAudit(
					SupID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierContact
				WHERE SupID = @SupID

				INSERT INTO SupMSTSupplierMasterAudit (
					SupID,DatauniqueID,ActivityType,IsActive,
					Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
					IsSubSupplier,ParentSupID,BillingWith,
					OrganizationID,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,ActivityType,IsActive,
					Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
					IsSubSupplier,ParentSupID,BillingWith,
					OrganizationID,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierMaster
				WHERE SupID = @SupID

				INSERT INTO SupMSTSupplierTaxRelatedAudit(
					SupID,DatauniqueID,
					GSTRegistrationType,GSTIN,
					TaxRegNo,CSTRegNo,PANNo,Terms,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,
					GSTRegistrationType,GSTIN,
					TaxRegNo,CSTRegNo,PANNo,@Terms,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierTaxRelated
				WHERE SupID = @SupID
 				  
				INSERT INTO SupMSTSupplierPreferedMethodsAudit(
					SupID,DatauniqueID,
					PrefferedPaymentMethod,PrefferedDeliveryMethod,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,
					PrefferedPaymentMethod,PrefferedDeliveryMethod,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierPreferedMethods
				WHERE SupID = @SupID

				INSERT INTO SupMSTSupplierNotesAudit(
					SupID,DatauniqueID,Notes,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,Notes,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierNotes
				WHERE SupID = @SupID

				INSERT INTO SupMSTSupplierOpeningBalanceAudit (
					SupID,DatauniqueID,OpeningBalance, AsOfDate,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SupID,DatauniqueID,OpeningBalance, AsOfDate,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SupMSTSupplierOpeningBalance
				WHERE SupID = @SupID

				INSERT INTO [SupMSTSupplierAddressBillingAudit] (
					[DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
				SELECT [DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
				FROM [SupMSTSupplierAddressBilling]
				WHERE SupID = @SupID
					
 				INSERT INTO [SupMSTSupplierAddressShippingAudit] (
					[DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
				SELECT [DataUniqueID],[SupID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
				FROM [SupMSTSupplierAddressShipping]
				WHERE SupID = @SupID
					
				DELETE FROM SupMSTSupplierMaster WHERE SupID = @SupID
				DELETE FROM SupMSTSupplierContact WHERE SupID = @SupID
				DELETE FROM SupMSTSupplierTaxRelated WHERE SupID = @SupID
				DELETE FROM SupMSTSupplierPreferedMethods WHERE SupID = @SupID
				DELETE FROM SupMSTSupplierNotes WHERE SupID = @SupID
				DELETE FROM SupMSTSupplierOpeningBalance WHERE SupID = @SupID
			END
		END 
	END
END 



GO
