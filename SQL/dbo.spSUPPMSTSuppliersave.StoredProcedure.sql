USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSUPPMSTSuppliersave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSUPPMSTSuppliersave] (
	@SuppID												BIGINT, 
	@Title												VARCHAR(50),
	@FirstName											VARCHAR(150),
	@MiddleName										VARCHAR(50),
	@LastName											VARCHAR(150),
	@Safix												VARCHAR(50),
	@Sex												VARCHAR(1),
	@DOB												DATE = NULL,

	@OrganizationCode									VARCHAR(50), 
	
	@EmailID											VARCHAR(500), 
	@Mobile												VARCHAR(500), 
	@Street1											VARCHAR(500), 
	@Street2											VARCHAR(500),
	@City												VARCHAR(50), 
	@State												BIGINT, 
	@Country											BIGINT,
	@Website											VARCHAR(50),
	
	@GSTRegistrationType								BIGINT, 
	@GSTIN												VARCHAR(50), 
	@IsSubSupplier										VARCHAR(1), 
	@ParentSuppID										BIGINT,
	@BillingWith										VARCHAR(50), 
	@Notes												VARCHAR(50), 
	@PrefferedPaymentMethod								VARCHAR(50),
	@PrefferedDeliveryMethod							VARCHAR(50),
	@TaxRegNo											VARCHAR(50), 
	@CSTRegNo											VARCHAR(50), 
	@PANNo												VARCHAR(50),
	@Terms												BIGINT,
	
	@OpeningBalance										NUMERIC(18, 2),
	@AsOfDate											DATE,

	@ShippingStreet1									VARCHAR(500), 
	@ShippingStreet2									VARCHAR(500),
	@ShippingCity										VARCHAR(50), 
	@ShippingState										BIGINT, 
	@ShippingCountry									BIGINT,

	@BillingStreet1									VARCHAR(500), 
	@BillingStreet2									VARCHAR(500),
	@BillingCity										VARCHAR(50), 
	@BillingState										BIGINT, 
	@BillingCountry									BIGINT,

	@IsActive  											VARCHAR(1),
	@UserID												BIGINT,
	@isOnlyDelete										VARCHAR(1),
	@NewSuppID 											BIGINT OUT,
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

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(SuppID) x FROM SuppMSTSupplierAddressBilling WHERE SuppID = @SuppID
			UNION
			SELECT COUNT(SuppID) x FROM SuppMSTSupplierAddressBillingAudit WHERE SuppID = @SuppID
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
			SELECT COUNT(A.SuppID) x FROM SuppMSTSupplierContact A, SuppMSTSupplierMaster B WHERE A.SuppID = B.SuppID AND B.OrganizationID = @OrganizationID AND EmailID = @EmailID AND ((@SuppID < 1) OR (@SuppID > 0 AND A.SuppID <> @SuppID))
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Email ID is already registered'
		END
		SELECT @IsExists = MAX(x) FROM (
			SELECT COUNT(A.SuppID) x FROM SuppMSTSupplierContact A, SuppMSTSupplierMaster B WHERE A.SuppID = B.SuppID AND B.OrganizationID = @OrganizationID AND Mobile = @Mobile AND ((@SuppID < 1) OR (@SuppID > 0 AND A.SuppID <> @SuppID))
		)A

		IF (@IsExists > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Mobile is already registered'
		END
			
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @SuppID < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO SuppMSTSupplierMaster (DatauniqueID, ActivityType, IsActive, Title, FirstName, MiddleName, LastName, Safix, DOB, Sex, 
					IsSubSupplier, ParentSuppID, BillingWith, OrganizationID, LastModifiedOn, LastModifiedBy)
			VALUES( @NewDatauniqueID, 'A', @IsActive, @Title, @FirstName, @MiddleName, @LastName, @Safix, @DOB, @Sex, 
					@IsSubSupplier, @ParentSuppID, @BillingWith, @OrganizationID, GETDATE(), @UserID)

			SELECT @NewSuppID = @@Identity

			INSERT INTO SuppMSTSupplierContact(SuppID, DatauniqueID, EmailID, Mobile, Street1, Street2, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
			VALUES(@NewDatauniqueID, @NewSuppID, @EmailID, @Mobile, @Street1, @Street2, @City, @State, @Country, @Website, GETDATE(), @UserID)

			INSERT INTO SuppMSTSupplierTaxRelated(SuppID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSuppID, @NewDatauniqueID, @GSTRegistrationType, @GSTIN, @TaxRegNo, @CSTRegNo, @PANNo, @Terms, GETDATE(), @UserID)
				
			INSERT INTO SuppMSTSupplierPreferedMethods (SuppID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
			VALUES(@NewSuppID, @NewDatauniqueID, @PrefferedPaymentMethod,@PrefferedDeliveryMethod, GETDATE(),@UserID)

			INSERT INTO SuppMSTSupplierNotes (SuppID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
			VALUES(@NewSuppID, @NewDatauniqueID, @Notes, GETDATE(),@UserID)

			INSERT INTO SuppMSTSupplierOpeningBalance (SuppID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@SuppID, @OpeningBalance, @AsOfDate, GETDATE(), @UserID)

			INSERT INTO SuppMSTSupplierOpeningBalance (SuppID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@SuppID, @OpeningBalance, @AsOfDate, GETDATE(), @UserID)

			INSERT INTO SuppMSTSupplierOpeningBalance (SuppID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@SuppID, @OpeningBalance, @AsOfDate, GETDATE(), @UserID)

			INSERT INTO SuppMSTSupplierAddressBilling (SuppID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewDatauniqueID, @NewSuppID, @BillingStreet1, @BillingStreet2, @BillingCity, @BillingState, @BillingCountry, GETDATE(), @UserID)

			INSERT INTO SuppMSTSupplierAddressShipping (SuppID, DatauniqueID, Street1, Street2, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewDatauniqueID, @NewSuppID, @ShippingStreet1, @ShippingStreet2, @ShippingCity, @ShippingState, @ShippingCountry, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @NewSuppID = @SuppID
			SELECT @OldDatauniqueID = DatauniqueID FROM SuppMSTSupplierMasterAudit WHERE SuppID = @SuppID
					
				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierMaster
						WHERE  ISNULL(Safix, '') = ISNULL(@Safix, '')
						AND ISNULL(Title, '') = ISNULL(@Title, '')
						AND ISNULL(FirstName, '') = ISNULL(@FirstName, '')
						AND ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
						AND ISNULL(LastName, '') = ISNULL(@LastName, '') 
						AND ISNULL(Sex, '') = ISNULL(@Sex, '')
		
						AND ISNULL(IsSubSupplier, '') = ISNULL(@IsSubSupplier, '') 
						AND ISNULL(ParentSuppID, 0) = ISNULL(@ParentSuppID, 0)
						AND ISNULL(BillingWith, '') = ISNULL(@BillingWith, '')
						AND ISNULL(IsActive, '') = ISNULL(@IsActive, '')
						AND SuppID = @SuppID
						AND ISNULL(RTRIM(LTRIM(@FirstName)), '') <> '' 
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierMaster WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierMasterAudit (
						SuppID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubSupplier,ParentSuppID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,ActivityType,IsActive,
						Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
						IsSubSupplier,ParentSuppID,BillingWith,
						OrganizationID,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierMaster
					WHERE SuppID = @SuppID

				  	UPDATE SuppMSTSupplierMaster SET 
				  		DatauniqueID = @NewDatauniqueID,
						Safix = @Safix, 
						Title = @Title,
						FirstName = @FirstName,
						MiddleName = @MiddleName, 
						LastName = @LastName, 
						Sex = @Sex,
		
						IsSubSupplier = @IsSubSupplier, 
						ParentSuppID = @ParentSuppID,
						BillingWith = @BillingWith,
				  			
						ActivityType = 'Modify', 
						IsActive = @IsActive, 
						LastModifiedOn = GETDATE(), 
						LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierContact
						WHERE  ISNULL(EmailID, '') = ISNULL(@EmailID, '')
						AND ISNULL(Mobile, '') = ISNULL(@Mobile, '')
						AND ISNULL(Website, '') = ISNULL(@Website, '')
						AND ISNULL(Street1, '') = ISNULL(@Street1, '') 
						AND ISNULL(Street2, '') = ISNULL(Street2, '') 
						AND ISNULL(City, '') = ISNULL(@City, '')
		
						AND ISNULL(State, 0) = ISNULL(@State, 0) 
						AND ISNULL(Country, 0) = ISNULL(@Country, 0)
						AND SuppID = @SuppID
						AND (ISNULL(RTRIM(LTRIM(@EmailID)), '') <> '' OR ISNULL(RTRIM(LTRIM(@Mobile)), '') <> '') 
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierContact WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierContactAudit(
						SuppID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierContact
					WHERE SuppID = @SuppID
				   	

				  	UPDATE SuppMSTSupplierContact SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		EmailID = @EmailID, 
				  		Mobile = @Mobile,
				  		Website = @Website, 
				  		Street1 = @Street1, 
				  		Street2 = @Street2,
				  		City = @City, 
				  		State = @State, 
				  		Country = @Country,
		
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierTaxRelated
						WHERE  ISNULL(GSTRegistrationType, 0) = ISNULL(GSTRegistrationType, 0)
						AND ISNULL(GSTIN, '') = ISNULL(@GSTIN, '')
						AND ISNULL(TaxRegNo, '') = ISNULL(@TaxRegNo, '')
						AND ISNULL(CSTRegNo, '') = ISNULL(@CSTRegNo, '') 
						AND ISNULL(PANNo, '') = ISNULL(@PANNo, '') 
						AND ISNULL(Terms, 0) = ISNULL(@Terms, 0) 
						AND SuppID = @SuppID
						AND (ISNULL(RTRIM(LTRIM(@GSTRegistrationType)), '') <> '' OR ISNULL(RTRIM(LTRIM(@GSTIN)), '') <> '')  
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierTaxRelated WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierTaxRelatedAudit(
						SuppID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,Terms,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,
						GSTRegistrationType,GSTIN,
						TaxRegNo,CSTRegNo,PANNo,@Terms,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierTaxRelated
					WHERE SuppID = @SuppID
 				  

				  	UPDATE SuppMSTSupplierTaxRelated SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		GSTRegistrationType = @GSTRegistrationType, 
				  		GSTIN = @GSTIN, 
				  		TaxRegNo = @TaxRegNo,
				  		CSTRegNo = @CSTRegNo,
				  		PANNo = @PANNo,
						Terms = @Terms,
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID

				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierPreferedMethods
						WHERE  ISNULL(PrefferedPaymentMethod, '') = ISNULL(@PrefferedPaymentMethod, '')
						AND ISNULL(PrefferedDeliveryMethod, '') = ISNULL(@PrefferedDeliveryMethod, '')
						AND SuppID = @SuppID
						AND (ISNULL(RTRIM(LTRIM(@PrefferedDeliveryMethod)), '') <> '' OR ISNULL(RTRIM(LTRIM(@PrefferedPaymentMethod)), '') <> '')  
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierPreferedMethods WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierPreferedMethodsAudit(
						SuppID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,
						PrefferedPaymentMethod,PrefferedDeliveryMethod,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierPreferedMethods
					WHERE SuppID = @SuppID

				  	UPDATE SuppMSTSupplierPreferedMethods SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		PrefferedPaymentMethod = @PrefferedPaymentMethod,
				  		PrefferedDeliveryMethod = @PrefferedDeliveryMethod,
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID

				END

				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierNotes
						WHERE  ISNULL(Notes, '') = ISNULL(@Notes, '')
						AND SuppID = @SuppID
						AND ISNULL(RTRIM(LTRIM(@Notes)), '') <> '' 
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierNotes WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierNotesAudit(
						SuppID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,Notes,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierNotes
					WHERE SuppID = @SuppID

				  	UPDATE SuppMSTSupplierNotes SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Notes = @Notes,
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END


				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierOpeningBalance
						WHERE  ISNULL(OpeningBalance, 0) = ISNULL(@OpeningBalance, 0)
						AND  ISNULL(@AsOfDate, convert(date, '01/01/1753', 103)) = ISNULL(@AsOfDate, convert(date, '01/01/1753', 103))
						AND SuppID = @SuppID
						AND ISNULL(@OpeningBalance, 0) <> 0 
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierNotes WHERE SuppID = @SuppID

					INSERT INTO SuppMSTSupplierOpeningBalanceAudit (
						SuppID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
					SELECT SuppID,DatauniqueID,OpeningBalance, AsOfDate,
						LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
					FROM SuppMSTSupplierOpeningBalance
					WHERE SuppID = @SuppID
						
					UPDATE SuppMSTSupplierOpeningBalance SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		OpeningBalance = @OpeningBalance, 
						AsOfDate = @AsOfDate, 
				  		LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END



				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierAddressShipping
						WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  		AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
				  		AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  		AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  		AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
						AND SuppID = @SuppID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierAddressShipping WHERE SuppID = @SuppID

					INSERT INTO [SuppMSTSupplierAddressShippingAudit] (
						[DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [SuppMSTSupplierAddressShipping]
					WHERE SuppID = @SuppID
						
					UPDATE [SuppMSTSupplierAddressShippingAudit] SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Street1 = @BillingStreet1, 
				  		Street2 = @BillingStreet1,
				  		City = @BillingCity, 
				  		State = @BillingState, 
				  		Country = @BillingCountry,
						LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END


				IF (NOT EXISTS(
						SELECT DatauniqueID
						FROM   SuppMSTSupplierAddressBilling
						WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  		AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
				  		AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  		AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  		AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
						AND SuppID = @SuppID
					)
				)
				BEGIN
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SuppMSTSupplierAddressBilling WHERE SuppID = @SuppID

					INSERT INTO [SuppMSTSupplierAddressBillingAudit] (
						[DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
					SELECT [DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
					FROM [SuppMSTSupplierAddressBilling]
					WHERE SuppID = @SuppID
						
					UPDATE [SuppMSTSupplierAddressBillingAudit] SET 
				  		DatauniqueID = @NewDatauniqueID,
				  		Street1 = @BillingStreet1, 
				  		Street2 = @BillingStreet1,
				  		City = @BillingCity, 
				  		State = @BillingState, 
				  		Country = @BillingCountry,
						LastModifiedOn = GETDATE(), 
				  		LastModifiedBy = @UserID
						WHERE SuppID = @SuppID
				END



			END 
			ELSE 
			BEGIN
				INSERT INTO SuppMSTSupplierContactAudit(
					SuppID,DatauniqueID, EmailID,Mobile,Street1,Street2,City,State,Country,Website,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,EmailID,Mobile,Street1,Street2,City,State,Country,Website,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierContact
				WHERE SuppID = @SuppID

				INSERT INTO SuppMSTSupplierMasterAudit (
					SuppID,DatauniqueID,ActivityType,IsActive,
					Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
					IsSubSupplier,ParentSuppID,BillingWith,
					OrganizationID,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,ActivityType,IsActive,
					Title,FirstName,MiddleName,LastName,Safix,DOB,Sex,
					IsSubSupplier,ParentSuppID,BillingWith,
					OrganizationID,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierMaster
				WHERE SuppID = @SuppID

				INSERT INTO SuppMSTSupplierTaxRelatedAudit(
					SuppID,DatauniqueID,
					GSTRegistrationType,GSTIN,
					TaxRegNo,CSTRegNo,PANNo,Terms,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,
					GSTRegistrationType,GSTIN,
					TaxRegNo,CSTRegNo,PANNo,@Terms,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierTaxRelated
				WHERE SuppID = @SuppID
 				  
				INSERT INTO SuppMSTSupplierPreferedMethodsAudit(
					SuppID,DatauniqueID,
					PrefferedPaymentMethod,PrefferedDeliveryMethod,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,
					PrefferedPaymentMethod,PrefferedDeliveryMethod,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierPreferedMethods
				WHERE SuppID = @SuppID

				INSERT INTO SuppMSTSupplierNotesAudit(
					SuppID,DatauniqueID,Notes,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,Notes,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierNotes
				WHERE SuppID = @SuppID

				INSERT INTO SuppMSTSupplierOpeningBalanceAudit (
					SuppID,DatauniqueID,OpeningBalance, AsOfDate,
					LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
				SELECT SuppID,DatauniqueID,OpeningBalance, AsOfDate,
					LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
				FROM SuppMSTSupplierOpeningBalance
				WHERE SuppID = @SuppID

				INSERT INTO [SuppMSTSupplierAddressBillingAudit] (
					[DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
				SELECT [DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
				FROM [SuppMSTSupplierAddressBilling]
				WHERE SuppID = @SuppID
					
 				INSERT INTO [SuppMSTSupplierAddressShippingAudit] (
					[DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], AuditOperationOn, AuditOperationBy)
				SELECT [DataUniqueID],[SuppID],[Street1],[Street2],[City],[State],[Country],[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID
				FROM [SuppMSTSupplierAddressShipping]
				WHERE SuppID = @SuppID
					
				DELETE FROM SuppMSTSupplierMaster WHERE SuppID = @SuppID
				DELETE FROM SuppMSTSupplierContact WHERE SuppID = @SuppID
				DELETE FROM SuppMSTSupplierTaxRelated WHERE SuppID = @SuppID
				DELETE FROM SuppMSTSupplierPreferedMethods WHERE SuppID = @SuppID
				DELETE FROM SuppMSTSupplierNotes WHERE SuppID = @SuppID
				DELETE FROM SuppMSTSupplierOpeningBalance WHERE SuppID = @SuppID
			END
		END 
	END
END 


GO
