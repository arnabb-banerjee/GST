USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spCUSMSTCustomerImageSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

[spCUSMSTCustomerImageSave] 0, 0, NULL, 'd2a67601-9a2e-4fd3-bebf-089f333b57a8.bmp', 'image/bmp', 0, 'N', 'N', '1C0E80ED-A137-49EC-9152-2D3FBAF0C42A', 'N', '', ''
*/


CREATE PROCEDURE [dbo].[spCUSMSTCustomerImageSave] (
	@ImageId					BIGINT,
	@CustomerId					BIGINT = '', 
	@FileData					IMAGE = null, 
	@FileName					VARCHAR(200) = '', 
	@FileType					VARCHAR(200) = '', 
	@Seq  						INT = 0,
	@IsMain  					VARCHAR(1) = '',
	@IsActive  					VARCHAR(1) = '',
	@UserCode					VARCHAR(50),
	@isOnlyDelete				VARCHAR(1),
	@NewDatauniqueID 			BIGINT OUT,
	@ErrorMessage 				VARCHAR(50) OUT
)

AS 
BEGIN 
	SELECT @NewDataUniqueID = 1
	declare @UserId bigint

	select @UserId = isnull(UserId, 0) from urMstUserRegisteredMaster where UserCode = @UserCode

	IF(ISNULL(RTRIM(LTRIM(@isOnlyDelete)), '') = 'Y' AND ISNULL(RTRIM(LTRIM(@ImageId)), 0) > 0)
	BEGIN
		INSERT INTO [cusMstcustomerImage_Audit] ([ImageId], [CustomerId], [OrganizationID],
				[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], [LastModifiedOn], [LastModifiedBy])
		SELECT [ImageId], [CustomerId], [OrganizationID],
			[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], [LastModifiedOn], [LastModifiedBy]
		FROM [cusMstcustomerImage]
		WHERE ImageId = @ImageId

		DELETE FROM [cusMstcustomerImage] WHERE ImageId = @ImageId
	END
	ELSE
	BEGIN	
		IF(SELECT COUNT(*) FROM [cusMstcustomerImage] WHERE [CustomerId] = @CustomerId) > 0
		BEGIN
			SELECT @NewDatauniqueID = (MAX(DatauniqueID) + 1) FROM [cusMstcustomerImage] WHERE [CustomerId] = @CustomerId
			
			IF(ISNULL(@Seq, 0) = 0)
			BEGIN
				SELECT @Seq = MAX(SEQ) + 1 FROM [cusMstcustomerImage] WHERE [CustomerId] = @CustomerId
			END
		END
		ELSE
		BEGIN
			SELECT @NewDatauniqueID = 1
			
			IF(ISNULL(@Seq, 0) = 0)
			BEGIN
				SELECT @Seq = 1
			END
		END

		INSERT INTO [cusMstcustomerImage] ([CustomerId], [OrganizationID],
				[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], [LastModifiedOn], [LastModifiedBy])
				VALUES(@CustomerId, NULL, @NewDatauniqueID, @FileData, @FileName, @FileType, @Seq, @isMain, @IsActive, GETDATE(), @UserId)
	END
END




GO
