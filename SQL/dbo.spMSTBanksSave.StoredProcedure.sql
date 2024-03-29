USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spMSTBanksSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[spMSTBanksSave] 0, 'Ss','','sSsA', 'ADADASDA', 'ASDADAD', 'ADSADADA', 'Y', '1', '', 0, '' 

CREATE PROCEDURE [dbo].[spMSTBanksSave] (
	@BankID							[BIGINT], 
	@Name							[VARCHAR](50), 
	@OrganizationCode				[VARCHAR](50) = '', 
	@CorpID							[varchar](50) = NULL,
	@Address						[varchar](500) = NULL,
	@IFSCCode						[varchar](50) = NULL,
	@MCRCode						[varchar](50) = NULL,
	@IsActive  						[VARCHAR](1),
	@UserCode						[varchar](50) = '',
	@isOnlyDelete					[VARCHAR](1),
	@NewDatauniqueID 				[BIGINT] OUT,
	@ErrorMessage 					[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	select @OrganizationId = 0

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
		IF ((SELECT COUNT(Id) x FROM [MSTBanks] WHERE Name = @Name AND ((@BankID < 1) OR (@BankID > 0 AND Id <> @BankID AND OrganizationId = @OrganizationId))
			) > 0)
		BEGIN 
				SELECT @AllowSaveDeleteData = 'N'
				SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN		
		IF @BankID < 1
		BEGIN 
				SELECT @NewDatauniqueID = 1
		
			INSERT INTO [dbo].[MSTBanks]
				([DatauniqueID],[ActivityType],[Name],[OrganizationID],[IsActive]
				,[CorpID],[Address],[IFSCCode],[MCRCode]
				,[LastModifiedOn],[LastModifiedBy])
				VALUES
				(@NewDatauniqueID,'A', @Name, @OrganizationID, @IsActive, 
				@CorpID, @Address, @IFSCCode, @MCRCode, 
				GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
				SELECT @OldDatauniqueID = DatauniqueID FROM [MSTBanks] WHERE Id = @BankID
					
				INSERT INTO [dbo].[MSTBanksAudit]
					([Id],[DatauniqueID],[ActivityType],[Name],[OrganizationID],[IsActive]
					,[CorpID],[Address],[IFSCCode],[MCRCode]
					,[LastModifiedOn],[LastModifiedBy],[AuditOperationOn],[AuditOperationBy])
				SELECT [Id],[DatauniqueID], CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END,[Name],[OrganizationID],[IsActive]
					,[CorpID],[Address],[IFSCCode],[MCRCode]
					,[LastModifiedOn],[LastModifiedBy], GETDATE(), @UserID 
				FROM [MSTBanks]
				WHERE Id = @BankID
				   				  
				IF @isOnlyDelete <> 'Y'
				BEGIN 
				  	SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [MSTBanks] WHERE Id = @BankID
		
				  	UPDATE [MSTBanks] SET 
				  			DatauniqueID = @NewDatauniqueID,
							Name = @Name, 
							OrganizationID = @OrganizationID, 
							[CorpID] = @CorpID,
							[Address] = @Address,
							[IFSCCode] = @IFSCCode,
							[MCRCode] = @MCRCode,
				  			ActivityType = 'M', 
				  			IsActive = @IsActive, 
				  			LastModifiedOn = GETDATE(), 
				  			LastModifiedBy = @UserID
					WHERE Id = @BankID
				END 
				ELSE 
				  	DELETE FROM [MSTBanks] WHERE Id = @BankID

		END 
	END
END




GO
