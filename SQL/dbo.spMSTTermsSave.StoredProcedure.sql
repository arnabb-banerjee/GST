USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTTermsSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMSTTermsSave] (
	@TermsId							BIGINT, 
	@Name							VARCHAR(50), 
	@OrganizationCode				VARCHAR(50), 
	@DueInFixedNumberDays			numeric(3,0), 
	@DueInCertainDayOfMonth			numeric(3,0), 
	@DueInNextMonth					numeric(3,0), 
	@Discount						numeric(3,3),
	
	@IsActive  						VARCHAR(1),
	@UserCode						VARCHAR(50),
	@isOnlyDelete					VARCHAR(1),
	@NewDatauniqueID 				BIGINT OUT,
	@ErrorMessage 					VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
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

	IF (@isOnlyDelete = 'Y')
	BEGIN 
			select 1 
	END 
	ELSE 
	BEGIN 
			IF ((SELECT COUNT(Id) x FROM [MSTTerms] WHERE Name = @Name AND ((@TermsId < 1) OR (@TermsId > 0 AND Id <> @TermsId AND OrganizationId = @OrganizationId))
				) > 0)
			BEGIN 
					SELECT @AllowSaveDeleteData = 'N'
					SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
			END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
			IF @TermsId < -1
			BEGIN 
					SELECT @NewDatauniqueID = 1
		
			  	INSERT INTO [dbo].[MSTTerms]
				   ([DatauniqueID],[ActivityType],[Name],[OrganizationID],[IsActive]
				   ,[DueInFixedNumberDays],[DueInCertainDayOfMonth],[DueInNextMonth],[Discount]
				   ,[LastModifiedOn],[LastModifiedBy])
				 VALUES
					(@NewDatauniqueID,'A', @Name, @OrganizationID, @IsActive, 
					@DueInFixedNumberDays, @DueInCertainDayOfMonth, @DueInNextMonth, @Discount, 
					GETDATE(), @UserID)
			END
			ELSE
			BEGIN 
					SELECT @OldDatauniqueID = DatauniqueID FROM [MSTTerms] WHERE Id = @TermsId
					
					INSERT INTO [dbo].[MSTTermsAudit]
					   ([Id],[DatauniqueID],[ActivityType],[Name],[OrganizationID],[IsActive]
					   ,[DueInFixedNumberDays],[DueInCertainDayOfMonth],[DueInNextMonth],[Discount]
					   ,[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy])
					SELECT [Id],[DatauniqueID],(CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END),[Name],[OrganizationID],[IsActive]
					   ,[DueInFixedNumberDays],[DueInCertainDayOfMonth],[DueInNextMonth],[Discount]
					   ,[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID 
					FROM [MSTTerms]
				   WHERE Id = @TermsId
				   				  
				  IF @isOnlyDelete <> 'Y'
				  BEGIN 
				  		SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [MSTTerms] WHERE Id = @TermsId
		
				  		UPDATE [MSTTerms] SET 
				  				DatauniqueID = @NewDatauniqueID,
								Name = @Name, 
								OrganizationID = @OrganizationID, 
								DueInFixedNumberDays = @DueInFixedNumberDays, 
								DueInCertainDayOfMonth = @DueInCertainDayOfMonth, 
								DueInNextMonth = @DueInNextMonth, 
								Discount = @Discount, 
				  				ActivityType = 'M', 
				  				IsActive = @IsActive, 
				  				LastModifiedOn = GETDATE(), 
				  				LastModifiedBy = @UserID
						WHERE Id = @TermsId
				  END 
				  ELSE 
				  		DELETE FROM [MSTTerms] WHERE Id = @TermsId

			END 
	END
END




GO
