USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[SPURMSTUserRegistration]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
	[SPURMSTUserRegistration] '', 'M', 'Mr', 'LAKDSASJSLSDA', 'ASDASD', 'FDFSF', '', NULL, '', 'Y', '', '59774360607', 'False', 
		'DAKDJAHK@AKJDSAKH.com', '', NULL, 'LKDJALDKA', NULL, 'LSKjlada', 1989, 111, NULL, NULL, '1,0,0#2,0,0', NULL, -1, -1, -1, NULL

	
*/
--SET XACT_ABORT ON;
CREATE PROCEDURE [dbo].[SPURMSTUserRegistration]
(
	@IsOnlyDelete		VARCHAR(1),
	@UserCode			VARCHAR(50),
	@UserType			VARCHAR(1),
	@Title				VARCHAR(50),
	@Safix				VARCHAR(50) = '',
	@FirstName			VARCHAR(50) = '',
	@MiddleName			VARCHAR(50) = '',
	@LastName			VARCHAR(50) = '',
	@DOB				VARCHAR(13) = '',
	@Sex				VARCHAR(1) = '1',
	@AccessAllowed		VARCHAR(1) = '',
	@IsApproved			VARCHAR(1) = '',
	@IsActive			VARCHAR(1) = '',
	@DONEBY				VARCHAR(50) = '',

	@Notes				VARCHAR(max) = '',

	@Mobile				VARCHAR(500) = '',
	@IsMobileActive		CHAR(1) = '',
	@EmailID			VARCHAR(500) = '',
	@EmailVerificationKey	VARCHAR(550)= NULL,
	@IsEmailActive		CHAR(1) = '',
	
	@Password			VARCHAR(500) = '',
	
	@Street1			VARCHAR(500) = '',
	@Street2			VARCHAR(500) = NULL,
	@City				VARCHAR(10) = '',
	@State				BIGINT = 0,
	@Country			BIGINT = 0,
	@PIN				VARCHAR(10)= NULL,

	@OrganizationCode1	VARCHAR(250) = NULL,
	@OrganizationName	VARCHAR(250) = NULL,

	@Functions			VARCHAR(5000) = '',

	@NewUserCode		VARCHAR(50) OUT,
	@BranchId			BIGINT OUT,
	@OrganizationCode 	VARCHAR(50) OUT,
	@NewDatauniqueID 	BIGINT OUT,
	@ErrorMessage 		VARCHAR(max) OUT
)
AS 
BEGIN 
	DECLARE @UserID bigint
	DECLARE @DoneByID BIGINT 
	DECLARE @OrganizationId BIGINT
	DECLARE @items VARCHAR(40)
	DECLARE @item INT, @function INT, @branch INT

	SELECT @DoneByID = ISNULL(UserID, 0) FROM urMstUserRegisteredMaster WHERE UserCode = @DONEBY
	SELECT @OrganizationCode = ''
	
	IF(ISNULL(@UserCode, '') = '')
	BEGIN
		IF isnull(rtrim(ltrim(@UserType)), '') = ''
			select @ErrorMessage = 'Internal error related to type of the new joinee'
		ELSE IF isnull(rtrim(ltrim(@UserType)), '') <> 'R' and ISNULL(RTRIM(LTRIM(@FirstName)), '') = ''
			select @ErrorMessage = 'First Name is mandatory for a new user record'
		ELSE IF isnull(rtrim(ltrim(@UserType)), '') <> 'R' and ISNULL(RTRIM(LTRIM(@LastName)), '') = ''
			select @ErrorMessage = 'Last Name is mandatory for a new user record'
		ELSE IF ISNULL(RTRIM(LTRIM(@EmailID)), '') = ''
			select @ErrorMessage = 'Email Address is mandatory for a new user record'
		ELSE IF (SELECT COUNT(EmailID) from urMstUserRegisteredEmail WHERE EmailID = @EmailID) > 0 
			select @ErrorMessage = 'Email Address is already used by another user. Please use different.'
		ELSE IF (SELECT COUNT(EmailID) from urMstUserRegisteredEmailAudit WHERE EmailID = @EmailID) > 0 
			select @ErrorMessage = 'Email Address is already used by another user. Please use different.'
		ELSE IF ISNULL(RTRIM(LTRIM(@Mobile)), '') = ''
			select @ErrorMessage = 'Mobile Number is mandatory for a new user record'
		ELSE IF (SELECT COUNT(Mobile) from urMstUserRegisteredMobile WHERE Mobile = @Mobile) > 0 
			select @ErrorMessage = 'Mobile Number is already used by another user. Please use different.'
		ELSE IF (SELECT COUNT(Mobile) from urMstUserRegisteredMobileAudit WHERE Mobile = @Mobile) > 0 
			select @ErrorMessage = 'Mobile Number is already used by another user. Please use different.'
		--ELSE IF ISNULL(RTRIM(LTRIM(@Functions)), '') = ''
		--	select @ErrorMessage = 'At least one function is mandatory for a new user record'
		ELSE IF ISNULL(RTRIM(LTRIM(@Password)), '') = ''
			select @ErrorMessage = 'Password is mandatory for a new user record'
		ELSE IF @UserType = 'R' AND (SELECT COUNT(*) FROM URMSTOrganizationMaster WHERE OrganizationName = @OrganizationName) > 0
			select @ErrorMessage = 'Organization Name is already used by another account'
		ELSE
		BEGIN
			SELECT @NewUserCode = NEWID()
			SELECT @NewDatauniqueID = 1
			
			BEGIN TRANSACTION T1;

			BEGIN TRY
				INSERT INTO urMstUserRegisteredMaster
					(DatauniqueID, ActivityType, 
					Title, Safix, FirstName, MiddleName, LastName, DOB,
					Sex, AccessAllowed, IsApproved, IsActive, LastModifiedOn,
					LastModifiedBy,Usercode,UserType)
				VALUES (@NewDatauniqueID, 'A', NULLIF(@Title, ''), NULLIF(@Safix, ''), @FirstName, @MiddleName,
						@LastName, CASE WHEN(ISNULL(@DOB, '') <> '') THEN CONVERT(datetime, '22/12/2001', 103) ELSE NULL END, NULLIF(@Sex, ''), NULLIF(@AccessAllowed, ''), NULLIF(@IsApproved, ''), NULLIF(@IsActive, ''), GETDATE(),
						@DoneByID, @NewUserCode, @UserType)

				SELECT @UserID = @@Identity

				SELECT @DoneByID = CASE WHEN(ISNULL(RTRIM(LTRIM(@DONEBY)), '') = '') THEN @UserID ELSE @DoneByID END
				UPDATE urMstUserRegisteredMaster SET LastModifiedBy = @DoneByID WHERE UserID = @UserID
	
				INSERT INTO urMstUserRegisteredMobile
		  			(Mobile, DatauniqueID, UserID,
	 					IsMobileConfirmationSent,MobileConfirmationSentOn ,
		   			IsMobileVerified, MobileVerifiedOn, IsActive, 
		   			LastModifiedOn, LastModifiedBy)
       
				VALUES (@Mobile, @NewDatauniqueID,  @UserID,NULL, NULL,NULL, NULL,@IsMobileActive,GETDATE(), @DoneByID)

				INSERT INTO urMstUserRegisteredPassword
					(Password, UserID ,IsAutoGenerated, AutoGeneratedOn, AutoGeneratedValidityTill,
					AutoGeneratedSubmittedOn, NumberOfWrongPassword,
						IsPasswordLocked,
					PasswordValidTill , LastModifiedOn , 						LastModifiedBy)
				VALUES	(@Password, @UserID, 'N', NULL,NULL,NULL,NULL,NULL,NULL,
						GETDATE(), @DoneByID) 

				INSERT INTO urMstUserRegisteredEmail 
					(EmailID, DatauniqueID, UserID, VerificationKey,
					IsEmailConfirmationSend,EmailConfirmationSentOn,
					IsEmailVerified, EmailVerifiedOn, IsActive,
					LastModifiedOn , LastModifiedBy)
				VALUES (@EmailID, @NewDatauniqueID, @UserID, @EmailVerificationKey,
					NULL, NULL,NULL,NULL, NULL,
					GETDATE(), @DoneByID) 

				INSERT INTO urMstUserRegisteredContact
				(UserID, DatauniqueID, Street1, Street2,
				City, State, Country, IsApproved, IsActive,
				LastModifiedOn, LastModifiedBy,ActivityType )

				VALUES (@UserID, @NewDatauniqueID, @Street1, @Street2,
						@City, @State, @Country, 'Y','Y',
						GETDATE(), @DoneByID, 'A')

				IF @UserType = 'M'
				BEGIN
					INSERT INTO urMapRegisteredUserFunction (DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
					SELECT 1, @UserID, FunctionId, NULL, NULL, @DoneByID, GETDATE() 
						FROM urMstFunctions where LOWER(RTRIM(LTRIM(IsDefaultForRegisteredUser))) = LOWER(RTRIM(LTRIM(@UserType)))
				END
				ELSE IF (@UserType = 'R' OR @UserType = 'E' OR @UserType = 'A' )
				BEGIN
					SELECT TOP 1 @OrganizationID = ISNULL(OrganizationID, 0) FROM (
						SELECT OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationName
						UNION
						SELECT OrganizationID FROM urMstOrganizationMaster WHERE OrganizationName = @OrganizationName
					) A
					
					IF(@OrganizationID > 0)
					BEGIN
						SELECT @OrganizationCode = OrganizationCode FROM urMstOrganizationMaster WHERE OrganizationID = @OrganizationId
					END
					ELSE
					BEGIN
						SELECT @OrganizationCode = NEWID()
						INSERT INTO URMSTOrganizationMaster (OrganizationCode, DatauniqueID, ActivityType, OrganizationName, IsActive, LastModifiedOn)
						values (@OrganizationCode, @NewDatauniqueID, 'A', @OrganizationName, 'Y', GETDATE())
						SELECT @OrganizationId = @@IDENTITY
					END

					INSERT INTO MSTBranch (Name, DatauniqueID, Street1, Street2, City, [State], Country, IsActive, IsMainBranch, PIN, OrganizationID)
					VALUES (@OrganizationName, @NewDatauniqueID, @Street1, @Street2, @City, @State, @Country, 'Y', 'Y', @PIN, @OrganizationId)

					SELECT @BranchId = @@IDENTITY

					INSERT INTO urMapRegisteredUserFunction (DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
					SELECT 1, @UserID, FunctionId, @BranchId, @OrganizationId, @DoneByID, GETDATE() 
						FROM urMstFunctions where LOWER(RTRIM(LTRIM(IsDefaultForRegisteredUser))) = 'Y' --LOWER(RTRIM(LTRIM(@UserType)))
				END

				DECLARE emp_cursor CURSOR FOR     
				SELECT items
				FROM dbo.Split(@Functions, '#');    
  
				OPEN emp_cursor    
  
				FETCH NEXT FROM emp_cursor     
				INTO @items
  
				WHILE @@FETCH_STATUS = 0    
				BEGIN    
					/*select @function = substring('1,5', 1, CHARINDEX(',', '1,5')-1)
					select @Branch = substring('1,5', CHARINDEX(',', '1,5')+1, len('1,5'))*/

					select @function = substring(@items, 1, CHARINDEX(',', @items)-1)
					select @Branch = substring(@items, CHARINDEX(',', @items)+1, len(@items))


					if(@function = 0)
						select @ErrorMessage = 'Problem is the permission is comming blank'
					else if(@branch = 0 and upper(rtrim(ltrim(@UserType))) <> 'M')
					begin
						if @BranchId = 0
							select @ErrorMessage = 'Problem is the branch is comming blank'
						else	
							select @branch = @BranchId 
					end 

					INSERT INTO urMapRegisteredUserFunction (DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
					VALUES(@NewDatauniqueID, @UserID, @function, (CASE WHEN(@BranchId > 0) THEN @BranchId ELSE NULL END), (CASE WHEN(@OrganizationId > 0) THEN @OrganizationId ELSE NULL END), @DoneByID, getdate())

					FETCH NEXT FROM emp_cursor INTO @items
   
				END     
				CLOSE emp_cursor;    
				DEALLOCATE emp_cursor; 
				
				COMMIT;
				RETURN
			END TRY
			BEGIN CATCH
				ROLLBACK;
				SELECT @ErrorMessage = ERROR_MESSAGE()
				SELECT @ErrorMessage = XACT_STATE()
				RETURN
			END CATCH;
		END -- End of Insertion
	END
	ELSE
	BEGIN
		SELECT @UserID = UserID FROM urMstUserRegisteredMaster WHERE UserCode = @UserCode

		IF ISNULL(RTRIM(LTRIM(@EmailID)), '') = ''
			select @ErrorMessage = 'Email Address is mandatory for a new user record'
		ELSE IF (SELECT COUNT(EmailID) from urMstUserRegisteredEmail WHERE EmailID = @EmailID AND UserID <> @UserID) > 0 
			select @ErrorMessage = 'Email Address is already used by another user. Please use different.'
		ELSE IF (SELECT COUNT(EmailID) from urMstUserRegisteredEmailAudit WHERE EmailID = @EmailID AND UserID <> @UserID) > 0 
			select @ErrorMessage = 'Email Address is already used by another user. Please use different.'
		ELSE IF ISNULL(RTRIM(LTRIM(@Mobile)), '') = ''
			select @ErrorMessage = 'Mobile Number is mandatory for a new user record'
		ELSE IF (SELECT COUNT(Mobile) from urMstUserRegisteredMobile WHERE Mobile = @Mobile AND UserID <> @UserID) > 0 
			select @ErrorMessage = 'Mobile Number is already used by another user. Please use different.'
		ELSE IF (SELECT COUNT(Mobile) from urMstUserRegisteredMobileAudit WHERE Mobile = @Mobile AND UserID <> @UserID) > 0 
			select @ErrorMessage = 'Mobile Number is already used by another user. Please use different.'
		ELSE
		BEGIN
		
			SELECT TOP 1 @OrganizationID = ISNULL(OrganizationID, 0) FROM (
				SELECT OrganizationID FROM urMstOrganizationMaster WHERE OrganizationCode = @OrganizationName
				UNION
				SELECT OrganizationID FROM urMstOrganizationMaster WHERE OrganizationName = @OrganizationName
			) A
					
			IF(@OrganizationID > 0)
			BEGIN
				SELECT @OrganizationCode = OrganizationCode FROM urMstOrganizationMaster WHERE OrganizationID = @OrganizationId
			END
			ELSE
			BEGIN
				SELECT @OrganizationCode = NEWID()
				INSERT INTO URMSTOrganizationMaster (OrganizationCode, DatauniqueID, ActivityType, OrganizationName, IsActive, LastModifiedOn)
				values (@OrganizationCode, @NewDatauniqueID, 'A', @OrganizationName, 'Y', GETDATE())
				SELECT @OrganizationId = @@IDENTITY
			END

			--select @OrganizationId
			--select @OrganizationCode

			--BEGIN TRY
				INSERT INTO urMstUserRegisteredMasterAudit
					(UserID, DatauniqueID, ActivityType, UserName,
					Title, Safix, FirstName, MiddleName, LastName, DOB,
					Sex, AccessAllowed, IsApproved, LastModifiedOn,
					LastModifiedBy,AuditOperationOn,AuditOperationBy,	UserCode)
				SELECT UserID, DatauniqueID, CASE WHEN((ISNULL(@isOnlyDelete,'') <> 'Y')) THEN ActivityType ELSE 'D' END,
						UserName, Title, Safix, FirstName, MiddleName, LastName, DOB,
						Sex, AccessAllowed, IsApproved, LastModifiedOn,
						LastModifiedBy,GETDATE(), @DONEBY, UserCode
    				FROM urMstUserRegisteredMaster
					WHERE	UserID = @UserID
	
				INSERT INTO urMstUserRegisteredContactAudit
						(UserID, DatauniqueID, Street1, Street2,
						City, State, Country, IsApproved, IsActive,
						LastModifiedOn, LastModifiedBy, AuditOperationOn, 
						AuditOperationBy,ActivityType)
					SELECT UserID, DatauniqueID, Street1, Street2,
							City, State, Country, IsApproved, IsActive,
							LastModifiedOn, LastModifiedBy, GETDATE(),@DONEBY,
							CASE WHEN((ISNULL(@isOnlyDelete,'') <> 'Y')) THEN ActivityType ELSE 'D' END
	 				FROM urMstUserRegisteredContact
					 WHERE UserID = @UserID

				INSERT INTO [urMstUserRegisteredNotesAudit]
					([UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy],
					[AuditOperationOn], [AuditOperationBy], [CreatedOn], [CreatedBy])

					SELECT [UserID], [DatauniqueID], [Notes], [LastModifiedOn], [LastModifiedBy],
					GETDATE(), @DoneByID, [CreatedOn], [CreatedBy]
					FROM [urMstUserRegisteredNotes]
					WHERE UserID = @UserID

				--IF (ISNULL(@IsOnlyDelete,'') <> 'Y')
				--	BEGIN
	
				SELECT @NewDatauniqueID = (MAX(DatauniqueID)+1)
	  			FROM urMstUserRegisteredMaster 
	 			WHERE UserID = @UserID

				SELECT @ErrorMessage = @NewDatauniqueID

				UPDATE urMstUserRegisteredMaster
	  			SET	DatauniqueID = @NewDatauniqueID,
		 				ActivityType = 'M',
		  				Title = @Title,
		  				Safix = @Safix,
						FirstName = @FirstName,
						MiddleName = @MiddleName,
						LastName = @LastName,
						DOB = convert(datetime, @DOB, 103),
		  				Sex = @Sex,
						AccessAllowed = @AccessAllowed,
						IsApproved = @IsApproved,
						IsActive = @IsActive,
						LastModifiedOn = GETDATE(),
		  				LastModifiedBy = @DONEBY
	   			WHERE UserID = @UserID

				UPDATE	urMstUserRegisteredContact
	  				SET	DatauniqueID = @NewDatauniqueID,
			 			Street1 = @Street1,
			 			Street2 = @Street2,
			 			City = @City,
			 			State = @State,
			 			Country = @Country,
						LastModifiedOn = GETDATE(),
			 			LastModifiedBy = @DONEBY,
						ActivityType = 'M'			 
	  			WHERE	UserID = @UserID

				if (select count(userid) from urMstUserRegisteredNotes where UserID = @UserID) > 0 
				begin
					UPDATE [urMstUserRegisteredNotes]
						SET [DatauniqueID] = @NewDatauniqueID, 
							[Notes] = @Notes, 
							[LastModifiedOn] = GETDATE(), 
							[LastModifiedBy]  = @DoneByID
	  				WHERE	UserID = @UserID
				end
				else 
				begin
					insert into [urMstUserRegisteredNotes] ([DatauniqueID], [Notes], UserID, [LastModifiedOn], [LastModifiedBy])
							values (@NewDatauniqueID, @Notes, @UserID, GETDATE(), @DoneByID)
				end

				INSERT INTO urMapRegisteredUserFunctionAudit(
						DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
				SELECT	DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn
				FROM urMapRegisteredUserFunction WHERE UserID = @UserID

				DELETE urMapRegisteredUserFunction WHERE UserID = @UserID

				select @NewDatauniqueID = (isnull(max(DataUniqueID), 0) + 1) from urMapRegisteredUserFunction where UserID = @UserID
				
				INSERT INTO urMapRegisteredUserFunction (DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
				SELECT @NewDatauniqueID, @UserID, FunctionId, @BranchId, @OrganizationId, @DoneByID, GETDATE() 
					FROM urMstFunctions where LOWER(RTRIM(LTRIM(IsDefaultForRegisteredUser))) = 'Y' --LOWER(RTRIM(LTRIM(@UserType)))

				DECLARE emp_cursor CURSOR FOR
				SELECT items
				FROM dbo.Split(@Functions, '#');    
  
				OPEN emp_cursor    
  
				FETCH NEXT FROM emp_cursor     
				INTO @items
  
				WHILE @@FETCH_STATUS = 0    
				BEGIN    
					/*select @function = substring('1,5', 1, CHARINDEX(',', '1,5')-1)
					select @Branch = substring('1,5', CHARINDEX(',', '1,5')+1, len('1,5'))*/

					select @function = substring(@items, 1, CHARINDEX(',', @items)-1)
					select @Branch = substring(@items, CHARINDEX(',', @items)+1, len(@items))

					if(@function = 0)
						select @ErrorMessage = 'Problem is the permission is comming blank'
					else if(@branch = 0 and upper(rtrim(ltrim(@UserType))) <> 'M')
					begin
						if @BranchId = 0
							select @ErrorMessage = 'Problem is the branch is comming blank'
						else	
							select @branch = @BranchId 
					end 

					if (select count(UserID) from urMapRegisteredUserFunction where UserID = @UserID and FunctionID = @function and isnull(BranchID, '') = isnull(@BranchId, '') and isnull(OrganizationID, '') = isnull(@OrganizationId, '')) = 0
					begin
						INSERT INTO urMapRegisteredUserFunction (DataUniqueID, UserID, FunctionID, BranchID, OrganizationID, LastModifiedBy, LastModifiedOn)
						VALUES(@NewDatauniqueID, @UserID, @function, (CASE WHEN(@BranchId > 0) THEN @BranchId ELSE NULL END), (CASE WHEN(@OrganizationId > 0) THEN @OrganizationId ELSE NULL END), @DoneByID, getdate())
					end 

					FETCH NEXT FROM emp_cursor INTO @items
   
				END     
				CLOSE emp_cursor;    
				DEALLOCATE emp_cursor; 


				--END
				--ELSE
				--BEGIN
				--	DELETE FROM urMstUserRegisteredMaster WHERE UserID = @UserID
	
				--	DELETE FROM urMstUserRegisteredContact WHERE UserID = @UserID

				--	DELETE FROM urMstUserRegisteredNotes WHERE UserID = @UserID
				--END

			--	COMMIT;
			--	RETURN
			--END TRY
			--BEGIN CATCH
			--	ROLLBACK;
			--	SELECT @ErrorMessage = ERROR_MESSAGE()
			--	SELECT @ErrorMessage = XACT_STATE()
			--	--SELECT @ErrorMessage
			--	RETURN
			--END CATCH;
		END
	END
	SELECT @ErrorMessage
END



GO
