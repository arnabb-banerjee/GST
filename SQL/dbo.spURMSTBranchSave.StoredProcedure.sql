USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spURMSTBranchSave]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
spbranchsave  -1, 'ABC', 1, 'Y', 'Park', '', 'Kolkata', 1, 1, 'Y', 1, 'Y', '', ''	
spbranchsave  5, 'ABC - modify', 1, 'Y', 'Park', '', 'Kolkata', 1, 1, 'Y', 1, '', '', ''	
spbranchsave  5, 'ABC - modify', 1, 'Y', 'Park', '', 'Kolkata', 1, 1, 'Y', 1, 'Y', '', ''	
select * from URMSTOrganizationBranch
select * from URMSTOrganizationBranchAudit

*/




CREATE PROCEDURE [dbo].[spURMSTBranchSave] (
	@BranchId				BIGINT = 0, 
	@BranchName				VARCHAR(5000),
	@OrganizationCode		VARCHAR(50), 
	@IsMainBranch			VARCHAR(1), 
	@Street1				VARCHAR(500), 
	@Street2				VARCHAR(500),
	@City					VARCHAR(50), 
	@StateID				BIGINT, 
	@CountryID				BIGINT,
	@IsActive  				VARCHAR(1),
	@UserID					BIGINT,
	@isOnlyDelete			VARCHAR(1),
	@NewDatauniqueID 		BIGINT OUT,
	@ErrorMessage 			VARCHAR(50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	select @AllowSaveDeleteData = 'Y'
	DECLARE @IsExists INT
	DECLARE @OrganizationID BIGINT

	if(isnull(rtrim(ltrim(@OrganizationCode)), '') <> '')
	begin
		SELECT @OrganizationId = OrganizationId FROM [dbo].[URMSTOrganizationMaster] WHERE [OrganizationCode] = @OrganizationCode
	end

	IF (@isOnlyDelete = 'Y')
	BEGIN 
		SELECT @IsExists = MAX(x) FROM 
		(
			SELECT COUNT(BranchId) x FROM URMAPOrganizationBranchUserRole WHERE BranchId = @BranchId
			UNION
			SELECT COUNT(BranchId) x FROM URMAPOrganizationBranchUserRoleAudit WHERE BranchId = @BranchId
		)A
			
		IF (@IsExists > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be deleted as the same is assigned to another module'
		END
	END 
	ELSE 
	BEGIN 
		IF ((SELECT COUNT(BranchId) x FROM URMSTOrganizationBranch WHERE OrganizationID = @OrganizationID AND BranchName = @BranchName AND ((@BranchId < 1) OR (@BranchId > 0 AND BranchId <> @BranchId))) > 0)
		BEGIN 
			SELECT @AllowSaveDeleteData = 'N'
			SELECT @ErrorMessage = 'Data can not be saved as the same name is assigned for another record'
		END
	END 
	
	IF (@AllowSaveDeleteData = 'Y')
	BEGIN	
		IF (@BranchId < 1)
		BEGIN 
			SELECT @NewDatauniqueID = 1
		
			INSERT INTO URMSTOrganizationBranch (BranchName, 
				OrganizationID, IsMainBranch, Street1, Street2,
				City, State, Country,
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy)
			VALUES(@BranchName, 
			@OrganizationID, @IsMainBranch, @Street1, @Street2,
				@City, @StateID, @CountryID,
				1, 'A', @IsActive, GETDATE(), @UserID)
		END
		ELSE
		BEGIN 
			SELECT @OldDatauniqueID = DatauniqueID FROM URMSTOrganizationBranch WHERE BranchId = @BranchId
					
			INSERT INTO URMSTOrganizationBranchAudit (BranchId, BranchName, 
				OrganizationID, IsMainBranch, Street1, Street2,
				City, State, Country,
				DatauniqueID, ActivityType, IsActive, LastModifiedOn, LastModifiedBy,
				AuditOperationOn, AuditOperationBy)
			SELECT BranchId, BranchName, 
				OrganizationID, IsMainBranch, Street1, Street2,
				City, State, Country,
				DatauniqueID, CASE WHEN(@isOnlyDelete <> 'Y') THEN ActivityType ELSE 'D' END, IsActive, LastModifiedOn, LastModifiedBy,
				GETDATE(), @UserID 
			FROM URMSTOrganizationBranch
			WHERE BranchId = @BranchId
				   				  
			IF @isOnlyDelete <> 'Y'
			BEGIN 
				SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM URMSTOrganizationBranch WHERE BranchId = @BranchId
				  
				UPDATE URMSTOrganizationBranch SET 
					BranchName = @BranchName, 
				  	DatauniqueID = @NewDatauniqueID, 
				  	OrganizationID = @OrganizationID, 
				  	IsMainBranch = @IsMainBranch, 
				  	Street1 = @Street1, 
				  	Street2 = @Street2,
				  	City = @City, 
				  	State = @StateID, 
				  	Country = @CountryID,
				  	ActivityType = 'M', 
				  	IsActive = @IsActive, 
				  	LastModifiedOn = GETDATE(), 
				  	LastModifiedBy = @UserID
				WHERE BranchId = @BranchId
			END 
			ELSE 
				DELETE FROM URMSTOrganizationBranch WHERE BranchId = @BranchId
		END 
	 END
END



GO
