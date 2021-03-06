USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spSupMSTSupplierUpload]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSupMSTSupplierUpload] (
	@SupplierData										SupplierDATA ReadOnly, 
	@OrganizationCode									VARCHAR(50), 
	@UserCode											VARCHAR(50) = '',
	@isOvereWrite										VARCHAR(1) = NULL,
	@ErrorMessage 										VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT, @NewDatauniqueID BIGINT, @NewSupID BIGINT
	DECLARE @CompanyName VARCHAR(250), @FirstName VARCHAR(150), @MiddleName VARCHAR(150), @LastName VARCHAR(150), @EmailID VARCHAR(500), @Mobile VARCHAR(50)
	DECLARE @Street1 VARCHAR(500), @City VARCHAR(50), @State BIGINT, @Country BIGINT, @Website VARCHAR(50), @FAX VARCHAR(50)
	DECLARE @GSTRegistrationType BIGINT, @GSTIN VARCHAR(50), @ParentSupID BIGINT = NULL, @PrefferedPaymentMethod VARCHAR(50), @PrefferedDeliveryMethod VARCHAR(50)
	DECLARE @TaxRegNo VARCHAR(50), @CSTRegNo VARCHAR(50), @PANNo VARCHAR(50), @Terms BIGINT, @OpeningBalance NUMERIC(18, 2), @AsOfDate	VARCHAR(50)
	DECLARE @ShippingStreet1 VARCHAR(500), @ShippingCity VARCHAR(50), @ShippingState BIGINT, @ShippingCountry BIGINT
	DECLARE @BillingStreet1 VARCHAR(500), @BillingCity VARCHAR(50), @BillingState BIGINT, @BillingCountry BIGINT
	DECLARE @EffectiveDate varchar(50), @PIN VARCHAR(50), @ShippingPIN VARCHAR(50), @BillingPIN VARCHAR(50)
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
	
	DECLARE vendorcursor CURSOR FOR
	SELECT FirstName, MiddleName, LastName, CompanyName, Email, Mobile, FAX, Street, City, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = [State])[State],	
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = Country)Country, PIN, 
	ParentSupplier, OpeningBalance, AsOfDate, GSTRegistrationType, GSTINNumber, TaxRegNo, CstRegNo, PANNumber,
	(select id from MSTTerms where Name = Terms)Terms, EffectiveDate, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStaticValues WHERE [Key] = 'DeliveryMethod')PreferedDeliveryMethod, (SELECT TOP 1 ISNULL(id, 0) FROM MSTStaticValues WHERE [Key] = 'PaymentMethod')PreferedPaymantMethod, 
	BillingStreet1, BillingCity, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = BillingState)BillingState, (SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = BillingCountry)BillingCountry, BillingPIN, ShippingStreet1, ShippingCity, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = ShippingState)ShippingState, (SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = ShippingCountry)ShippingCountry, ShippingPIN
	FROM @SupplierData


	OPEN vendorcursor;
	FETCH NEXT FROM vendorcursor 
	INTO @FirstName, @MiddleName, @LastName, @CompanyName, @EmailID, @Mobile, @FAX, @Street1, @City, @State,	
		@Country, @PIN, @ParentSupID, @OpeningBalance, @AsOfDate, @GSTRegistrationType, @GSTIN, 
		@TaxRegNo, @CSTRegNo, @PANNo, @Terms, @EffectiveDate, @PrefferedDeliveryMethod, @PrefferedPaymentMethod, 
		@BillingStreet1, @BillingCity, @BillingState, @BillingCountry, @BillingPIN, @ShippingStreet1, @ShippingCity, @ShippingState, 
		@ShippingCountry, @ShippingPIN

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @SupID BIGINT

	SELECT @SupID = MAX(x) FROM (
		SELECT COUNT(Y1.SupID) x FROM SupMSTSupplierMaster Y1, SupMSTSupplierContact Y2
		WHERE Y1.SupID = Y2.SupID
		AND ISNULL(CompanyName, '') = ISNULL(@CompanyName, '') 
		and ISNULL(FirstName, '') = ISNULL(@FirstName, '') 
		and ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
		and ISNULL(LastName, '') = ISNULL(@LastName, '') 
		and ISNULL(EmailID, '') = ISNULL(@EmailID, '') 
		and ISNULL(Mobile, '') = ISNULL(@Mobile, '')
	)A

	IF (ISNULL(@SupID, 0) > 0 and isnull(@isOvereWrite, '') = 'Y')
	BEGIN 
		SELECT @AllowSaveDeleteData = 'Y'
	END 
	ELSE 
		SELECT @AllowSaveDeleteData = 'Y'

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN
		SELECT @AsOfDate = CASE WHEN(@AsOfDate = '') THEN NULL ELSE CAST(dbo.ConvertToDate(@AsOfDate) as VARCHAR(50)) END
		 		
		IF @SupID < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO SupMSTSupplierMaster (DatauniqueID, ActivityType, IsActive, FirstName, MiddleName, LastName,  /*Safix, DOB, Sex,*/ 
					IsSubSupplier, ParentSupID, /*BillingWith,*/ OrganizationID, LastModifiedOn, LastModifiedBy)
			VALUES( @NewDatauniqueID, 'A', 'Y', NULLIF(@FirstName, ''), NULLIF(@MiddleName, ''), NULLIF(@LastName, ''),  /*NULLIF(@Safix, ''),@DOB, NULLIF(@Sex, ''),*/ 
					case when(isnull(@ParentSupID,'')<> '') then 'Y' else NULL end, NULLIF(@ParentSupID, '0'), /*NULLIF(@BillingWith, ''),*/ NULLIF(@OrganizationID, ''), GETDATE(), @UserID)

			SELECT @NewSupID = @@Identity

			INSERT INTO SupMSTSupplierContact(SupID, DatauniqueID, EmailID, Mobile, Street1, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@EmailID, ''), NULLIF(@Mobile, ''), NULLIF(@Street1, ''), NULLIF(@City, ''), NULLIF(@State, '0'), NULLIF(@Country, '0'), NULLIF(@Website, ''), GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierTaxRelated(SupID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@GSTRegistrationType, '0'), NULLIF(@GSTIN, ''), NULLIF(@TaxRegNo, ''), NULLIF(@CSTRegNo, ''), NULLIF(@PANNo, ''), NULLIF(@Terms, '0'), GETDATE(), @UserID)
				
			INSERT INTO SupMSTSupplierPreferedMethods (SupID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@PrefferedPaymentMethod, '0'), NULLIF(@PrefferedDeliveryMethod, '0'), GETDATE(),@UserID)

			--INSERT INTO SupMSTSupplierNotes (SupID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
			--VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@Notes, ''), GETDATE(),@UserID)

			INSERT INTO SupMSTSupplierOpeningBalance (SupID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, NULLIF(@OpeningBalance, '0'), @AsOfDate, GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierAddressBilling (SupID, DatauniqueID, Street1, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@BillingStreet1, ''), NULLIF(@BillingCity, ''), NULLIF(@BillingState, '0'), NULLIF(@BillingCountry, '0'), GETDATE(), @UserID)

			INSERT INTO SupMSTSupplierAddressShipping (SupID, DatauniqueID, Street1, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewSupID, @NewDatauniqueID, NULLIF(@ShippingStreet1, ''), NULLIF(@ShippingCity, ''), NULLIF(@ShippingState, '0'), NULLIF(@ShippingCountry, '0'), GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @NewSupID = @SupID
			SELECT @OldDatauniqueID = DatauniqueID FROM SupMSTSupplierMasterAudit WHERE SupID = @SupID
				  
			IF (NOT EXISTS(
					SELECT DatauniqueID
					FROM   SupMSTSupplierMaster
					WHERE ISNULL(FirstName, '') = ISNULL(@FirstName, '')
					AND ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
					AND ISNULL(LastName, '') = ISNULL(@LastName, '') 
					--AND ISNULL(Sex, '') = ISNULL(@Sex, '')
					--AND ISNULL(DOB, '') = ISNULL(@DOB, '')
					--AND ISNULL(Safix, '') = ISNULL(@Safix, '')
					--AND ISNULL(Title, '') = ISNULL(@Title, '')
					AND ISNULL(ParentSupID, 0) = ISNULL(@ParentSupID, 0)
					AND ISNULL(IsActive, '') = 'Y'
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
					--Safix = NULLIF(@Safix, ''),  
					--Title = NULLIF(@Title, ''),
					FirstName = NULLIF(@FirstName, ''),
					MiddleName = NULLIF(@MiddleName, ''),
					LastName = NULLIF(@LastName, ''),
					--Sex = NULLIF(@Sex, ''),
					--DOB = @DOB,
		
					IsSubSupplier = case when(isnull(@ParentSupID,'')<> '') then 'Y' else '' end, 
					ParentSupID = NULLIF(@ParentSupID, ''),
					--BillingWith = NULLIF(@BillingWith, ''),
				  			
					ActivityType = 'M', 
					IsActive = 'Y',
					LastModifiedOn = GETDATE(), 
					LastModifiedBy = @UserID
					WHERE SupID = @SupID
			END

			IF (NOT EXISTS(
					SELECT DatauniqueID
					FROM   SupMSTSupplierContact
					WHERE  ISNULL(EmailID, '') = ISNULL(@EmailID, '')
					AND ISNULL(Mobile, '') = ISNULL(@Mobile, '')
					--AND ISNULL(Website, '') = ISNULL(@Website, '')
					AND ISNULL(Street1, '') = ISNULL(@Street1, '') 
					--AND ISNULL(Street2, '') = ISNULL(Street2, '') 
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
				  	--Website = NULLIF(@Website, ''),
				  	Street1 = NULLIF(@Street1, ''),
				  	--Street2 = NULLIF(@Street2, ''),
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

			--IF (NOT EXISTS(
			--		SELECT DatauniqueID
			--		FROM   SupMSTSupplierNotes
			--		WHERE  ISNULL(Notes, '') = ISNULL(@Notes, '')
			--		AND SupID = @SupID
			--	)
			--)
			--BEGIN
			--	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM SupMSTSupplierNotes WHERE SupID = @SupID

			--	INSERT INTO SupMSTSupplierNotesAudit(
			--		SupID,DatauniqueID,Notes,
			--		LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
			--	SELECT SupID,DatauniqueID,Notes,
			--		LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
			--	FROM SupMSTSupplierNotes
			--	WHERE SupID = @SupID

			--	UPDATE SupMSTSupplierNotes SET 
			--	  	DatauniqueID = @NewDatauniqueID,
			--	  	Notes = NULLIF(@Notes, ''),
			--	  	LastModifiedOn = GETDATE(), 
			--	  	LastModifiedBy = @UserID
			--		WHERE SupID = @SupID
			--END

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
				  	--AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
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
				  	--Street2 = NULLIF(@BillingStreet1, ''),
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
				  	--AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
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
				  	--Street2 = NULLIF(@BillingStreet1, ''),
				  	City = NULLIF(@BillingCity, ''),
				  	State = NULLIF(@BillingState, '0'),
				  	Country = NULLIF(@BillingCountry, '0'),
					LastModifiedOn = GETDATE(), 
				  	LastModifiedBy = @UserID
					WHERE SupID = @SupID
			END
		END 
	END
END 


GO
