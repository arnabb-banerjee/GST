USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spCUSMSTCustomerUpload]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCUSMSTCustomerUpload] (
	@CustomerData										CUSTOMERDATA ReadOnly, 
	@UserType											VARCHAR(1), 
	@OrganizationCode									VARCHAR(50), 
	@UserCode											VARCHAR(50) = '',
	@isOvereWrite										VARCHAR(1) = NULL,
	@ErrorMessage 										VARCHAR(50) OUT
)
AS 
BEGIN 
	DECLARE @OrganizationId BIGINT, @NewDatauniqueID BIGINT, @NewCusID BIGINT
	DECLARE @CompanyName VARCHAR(250), @FirstName VARCHAR(150), @MiddleName VARCHAR(150), @LastName VARCHAR(150), @EmailID VARCHAR(500), @Mobile VARCHAR(50)
	DECLARE @Street1 VARCHAR(500), @City VARCHAR(50), @State BIGINT, @Country BIGINT, @Website VARCHAR(50), @FAX VARCHAR(50)
	DECLARE @GSTRegistrationType BIGINT, @GSTIN VARCHAR(50), @ParentCusID BIGINT = NULL, @PrefferedPaymentMethod VARCHAR(50), @PrefferedDeliveryMethod VARCHAR(50)
	DECLARE @TaxRegNo VARCHAR(50), @CSTRegNo VARCHAR(50), @PANNo VARCHAR(50), @Terms BIGINT, @OpeningBalance varchar(50), @AsOfDate	VARCHAR(50)
	DECLARE @ShippingStreet1 VARCHAR(500), @ShippingCity VARCHAR(50), @ShippingState BIGINT, @ShippingCountry BIGINT
	DECLARE @BillingStreet1 VARCHAR(500), @BillingCity VARCHAR(50), @BillingState BIGINT, @BillingCountry BIGINT
	DECLARE @EffectiveDate varchar(50), @PIN VARCHAR(50), @ShippingPIN VARCHAR(50), @BillingPIN VARCHAR(50)
	DECLARE @UserID BIGINT
	SELECT	@UserID = 0

	if(isnull(rtrim(ltrim(@UserCode)), '') <> '')
	begin
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	end


	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end
	
	DECLARE vendorcursor CURSOR FOR
	SELECT FirstName, MiddleName, LastName, CompanyName, Email, Mobile, FAX, Street, City, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = [State])[State],	
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = Country)Country, PIN, 
	ParentCustomer, OpeningBalance, AsOfDate, GSTRegistrationType, GSTINNumber, TaxRegNo, CstRegNo, PANNumber,
	(select id from MSTTerms where Name = Terms)Terms, EffectiveDate, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStaticValues WHERE [Key] = 'DeliveryMethod')PreferedDeliveryMethod, (SELECT TOP 1 ISNULL(id, 0) FROM MSTStaticValues WHERE [Key] = 'PaymentMethod')PreferedPaymantMethod, 
	BillingStreet1, BillingCity, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = BillingState)BillingState, (SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = BillingCountry)BillingCountry, BillingPIN, ShippingStreet1, ShippingCity, 
	(SELECT TOP 1 ISNULL(id, 0) FROM MSTStates WHERE name = ShippingState)ShippingState, (SELECT TOP 1 ISNULL(id, 0) FROM MSTCountries WHERE name = ShippingCountry)ShippingCountry, ShippingPIN
	FROM @CustomerData


	OPEN vendorcursor;
	FETCH NEXT FROM vendorcursor 
	INTO @FirstName, @MiddleName, @LastName, @CompanyName, @EmailID, @Mobile, @FAX, @Street1, @City, @State,	
		@Country, @PIN, @ParentCusID, @OpeningBalance, @AsOfDate, @GSTRegistrationType, @GSTIN, 
		@TaxRegNo, @CSTRegNo, @PANNo, @Terms, @EffectiveDate, @PrefferedDeliveryMethod, @PrefferedPaymentMethod, 
		@BillingStreet1, @BillingCity, @BillingState, @BillingCountry, @BillingPIN, @ShippingStreet1, @ShippingCity, @ShippingState, 
		@ShippingCountry, @ShippingPIN

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @CusID BIGINT

	SELECT @CusID = MAX(x) FROM (
		SELECT COUNT(Y1.CusID) x FROM CUSMSTCustomerMaster Y1, CUSMSTCustomerContact Y2
		WHERE Y1.CusID = Y2.CusID
		AND ISNULL(CompanyName, '') = ISNULL(@CompanyName, '') 
		and ISNULL(FirstName, '') = ISNULL(@FirstName, '') 
		and ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
		and ISNULL(LastName, '') = ISNULL(@LastName, '') 
		and ISNULL(EmailID, '') = ISNULL(@EmailID, '') 
		and ISNULL(Mobile, '') = ISNULL(@Mobile, '')
	)A

	IF (ISNULL(@CusID, 0) > 0 and isnull(@isOvereWrite, '') = 'Y')
	BEGIN 
		SELECT @AllowSaveDeleteData = 'Y'
	END 
	ELSE 
		SELECT @AllowSaveDeleteData = 'Y'

	IF (@AllowSaveDeleteData = 'Y')
	BEGIN
		--SELECT @AsOfDate = CASE WHEN(@AsOfDate = '') THEN NULL ELSE CAST(dbo.ConvertToDate(@AsOfDate) as VARCHAR(50)) END
		SELECT @AsOfDate = CASE WHEN(@AsOfDate = '') THEN NULL ELSE @AsOfDate END
		 		
		IF @CusID < 1
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO CUSMSTCustomerMaster (DatauniqueID, ActivityType, IsActive, FirstName, MiddleName, LastName,  /*Safix, DOB, Sex,*/ 
					IsSubCustomer, ParentCusID, /*BillingWith,*/ OrganizationID, LastModifiedOn, LastModifiedBy)
			VALUES( @NewDatauniqueID, 'A', 'Y', NULLIF(@FirstName, ''), NULLIF(@MiddleName, ''), NULLIF(@LastName, ''),  /*NULLIF(@Safix, ''),@DOB, NULLIF(@Sex, ''),*/ 
					case when(isnull(@ParentCusID,'')<> '') then 'Y' else NULL end, NULLIF(@ParentCusID, '0'), /*NULLIF(@BillingWith, ''),*/ NULLIF(@OrganizationID, ''), GETDATE(), @UserID)

			SELECT @NewCusID = @@Identity

			INSERT INTO CUSMSTCustomerContact(CusID, DatauniqueID, EmailID, Mobile, Street1, City, State, Country, Website, LastModifiedOn, LastModifiedBy)
			VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@EmailID, ''), NULLIF(@Mobile, ''), NULLIF(@Street1, ''), NULLIF(@City, ''), NULLIF(@State, '0'), NULLIF(@Country, '0'), NULLIF(@Website, ''), GETDATE(), @UserID)

			INSERT INTO CUSMSTCustomerTaxRelated(CusID, DatauniqueID, GSTRegistrationType, GSTIN, TaxRegNo, CSTRegNo, PANNo, Terms, LastModifiedOn, LastModifiedBy)
			VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@GSTRegistrationType, '0'), NULLIF(@GSTIN, ''), NULLIF(@TaxRegNo, ''), NULLIF(@CSTRegNo, ''), NULLIF(@PANNo, ''), NULLIF(@Terms, '0'), GETDATE(), @UserID)
				
			INSERT INTO CUSMSTCustomerPreferedMethods (CusID, DatauniqueID, PrefferedPaymentMethod, PrefferedDeliveryMethod, LastModifiedOn,LastModifiedBy)
			VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@PrefferedPaymentMethod, '0'), NULLIF(@PrefferedDeliveryMethod, '0'), GETDATE(),@UserID)

			--INSERT INTO CUSMSTCustomerNotes (CusID, DatauniqueID, Notes, LastModifiedOn,LastModifiedBy)
			--VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@Notes, ''), GETDATE(),@UserID)

			INSERT INTO CUSMSTCustomerOpeningBalance (CusID, OpeningBalance, AsOfDate, LastModifiedOn, LastModifiedBy)
			VALUES(@NewCusID, NULLIF(@OpeningBalance, '0'), @AsOfDate, GETDATE(), @UserID)

			INSERT INTO CUSMSTCustomerAddressBilling (CusID, DatauniqueID, Street1, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@BillingStreet1, ''), NULLIF(@BillingCity, ''), NULLIF(@BillingState, '0'), NULLIF(@BillingCountry, '0'), GETDATE(), @UserID)

			INSERT INTO CUSMSTCustomerAddressShipping (CusID, DatauniqueID, Street1, City, State, Country, LastModifiedOn, LastModifiedBy)
			VALUES(@NewCusID, @NewDatauniqueID, NULLIF(@ShippingStreet1, ''), NULLIF(@ShippingCity, ''), NULLIF(@ShippingState, '0'), NULLIF(@ShippingCountry, '0'), GETDATE(), @UserID)
		
			INSERT INTO OrganizationCustSuppMapping (UserID, DatauniqueID, OrganizationID, UserType)
			VALUES(@NewCusID, @NewDatauniqueID, @OrganizationId, @UserType)

		END
		ELSE
		BEGIN 
			SELECT @NewCusID = @CusID
			SELECT @OldDatauniqueID = DatauniqueID FROM CUSMSTCustomerMasterAudit WHERE CusID = @CusID
				  
			IF (NOT EXISTS(
					SELECT DatauniqueID
					FROM   CUSMSTCustomerMaster
					WHERE ISNULL(FirstName, '') = ISNULL(@FirstName, '')
					AND ISNULL(MiddleName, '') = ISNULL(@MiddleName, '') 
					AND ISNULL(LastName, '') = ISNULL(@LastName, '') 
					--AND ISNULL(Sex, '') = ISNULL(@Sex, '')
					--AND ISNULL(DOB, '') = ISNULL(@DOB, '')
					--AND ISNULL(Safix, '') = ISNULL(@Safix, '')
					--AND ISNULL(Title, '') = ISNULL(@Title, '')
					AND ISNULL(ParentCusID, 0) = ISNULL(@ParentCusID, 0)
					AND ISNULL(IsActive, '') = 'Y'
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
					--Safix = NULLIF(@Safix, ''),  
					--Title = NULLIF(@Title, ''),
					FirstName = NULLIF(@FirstName, ''),
					MiddleName = NULLIF(@MiddleName, ''),
					LastName = NULLIF(@LastName, ''),
					--Sex = NULLIF(@Sex, ''),
					--DOB = @DOB,
		
					IsSubCustomer = case when(isnull(@ParentCusID,'')<> '') then 'Y' else '' end, 
					ParentCusID = NULLIF(@ParentCusID, ''),
					--BillingWith = NULLIF(@BillingWith, ''),
				  			
					ActivityType = 'M', 
					IsActive = 'Y',
					LastModifiedOn = GETDATE(), 
					LastModifiedBy = @UserID
					WHERE CusID = @CusID
			END

			IF (NOT EXISTS(
					SELECT DatauniqueID
					FROM   CUSMSTCustomerContact
					WHERE  ISNULL(EmailID, '') = ISNULL(@EmailID, '')
					AND ISNULL(Mobile, '') = ISNULL(@Mobile, '')
					--AND ISNULL(Website, '') = ISNULL(@Website, '')
					AND ISNULL(Street1, '') = ISNULL(@Street1, '') 
					--AND ISNULL(Street2, '') = ISNULL(Street2, '') 
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
				  	--Website = NULLIF(@Website, ''),
				  	Street1 = NULLIF(@Street1, ''),
				  	--Street2 = NULLIF(@Street2, ''),
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

			--IF (NOT EXISTS(
			--		SELECT DatauniqueID
			--		FROM   CUSMSTCustomerNotes
			--		WHERE  ISNULL(Notes, '') = ISNULL(@Notes, '')
			--		AND CusID = @CusID
			--	)
			--)
			--BEGIN
			--	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM CUSMSTCustomerNotes WHERE CusID = @CusID

			--	INSERT INTO CUSMSTCustomerNotesAudit(
			--		CusID,DatauniqueID,Notes,
			--		LastModifiedOn,LastModifiedBy, AuditOperationOn, AuditOperationBy)
			--	SELECT CusID,DatauniqueID,Notes,
			--		LastModifiedOn,LastModifiedBy, GETDATE(), @UserID
			--	FROM CUSMSTCustomerNotes
			--	WHERE CusID = @CusID

			--	UPDATE CUSMSTCustomerNotes SET 
			--	  	DatauniqueID = @NewDatauniqueID,
			--	  	Notes = NULLIF(@Notes, ''),
			--	  	LastModifiedOn = GETDATE(), 
			--	  	LastModifiedBy = @UserID
			--		WHERE CusID = @CusID
			--END

			IF (NOT EXISTS(
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
					WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  	--AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
				  	AND ISNULL(City, '') = ISNULL(@BillingCity, '')
				  	AND ISNULL(State, 0) = ISNULL(@BillingState, 0) 
				  	AND ISNULL(Country, 0) = ISNULL(@BillingCountry, 0)
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
						
				UPDATE [CUSMSTCustomerAddressShippingAudit] SET 
				  	DatauniqueID = @NewDatauniqueID,
				  	Street1 = NULLIF(@BillingStreet1, ''), 
				  	--Street2 = NULLIF(@BillingStreet1, ''),
				  	City = NULLIF(@BillingCity, ''),
				  	State = NULLIF(@BillingState, '0'),
				  	Country = NULLIF(@BillingCountry, '0'),
					LastModifiedOn = GETDATE(), 
				  	LastModifiedBy = @UserID
					WHERE CusID = @CusID
			END

			IF (NOT EXISTS(
					SELECT DatauniqueID
					FROM   CUSMSTCustomerAddressBilling
					WHERE  ISNULL(Street1, '') = ISNULL(@BillingStreet1, '') 
				  	--AND ISNULL(Street2, '') = ISNULL(@BillingStreet1, '')
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
						
				UPDATE [CUSMSTCustomerAddressBillingAudit] SET 
				  	DatauniqueID = @NewDatauniqueID,
				  	Street1 = NULLIF(@BillingStreet1, ''),
				  	--Street2 = NULLIF(@BillingStreet1, ''),
				  	City = NULLIF(@BillingCity, ''),
				  	State = NULLIF(@BillingState, '0'),
				  	Country = NULLIF(@BillingCountry, '0'),
					LastModifiedOn = GETDATE(), 
				  	LastModifiedBy = @UserID
					WHERE CusID = @CusID
			END
		END 
	END
END 



GO
