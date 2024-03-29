USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spCUSMSTCustomerImageGet]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

[dbo].[spMAPOrganizationproductImageGet] 0, 0, 0, '', 'Y'

*/

CREATE PROCEDURE [dbo].[spCUSMSTCustomerImageGet] (
	@ImageId					BIGINT,
	@CustomerId					BIGINT,
	@IsActive					VARCHAR(1),
	@IsMain						VARCHAR(1) = ''
)
AS 
BEGIN 
	
	if(@CustomerId > 0)
	begin
		IF(ISNULL(RTRIM(LTRIM(@IsMain)), '') = 'Y' AND ISNULL(@ImageId, 0) = 0)
		BEGIN
			if(select count(imageid) from [cusMstcustomerImage] where CustomerId = @CustomerId AND ISNULL(RTRIM(LTRIM(IsMain)), '') = 'Y') > 0
			begin
				SELECT TOP 1 [ImageId], [CustomerId],
						[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], 
						[LastModifiedOn], [LastModifiedBy]
				FROM [cusMstcustomerImage] B 
				WHERE ISNULL(RTRIM(LTRIM(@IsMain)), '') = 'Y'
				AND B.CustomerId = @CustomerId
				AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') THEN B.IsActive ELSE @IsActive END
				ORDER BY [LastModifiedOn] DESC
			end
			ELSE
			BEGIN
				SELECT TOP 1 [ImageId], [CustomerId],
						[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], 
						[LastModifiedOn], [LastModifiedBy]
				FROM [cusMstcustomerImage] B 
				WHERE B.CustomerId = @CustomerId
				AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') THEN B.IsActive ELSE @IsActive END
				ORDER BY [LastModifiedOn] ASC
			END
		END
		ELSE
		BEGIN
			SELECT [ImageId], [CustomerId],
						[DatauniqueID], [FileData], [FileName], [FileType], [SEQ], [isMain], [IsActive], 
						[LastModifiedOn], [LastModifiedBy]
			FROM [cusMstcustomerImage] B 
			WHERE B.ImageId = CASE WHEN(ISNULL(@ImageId, 0) = 0) THEN B.ImageId ELSE @ImageId END
			AND B.CustomerId = @CustomerId
			AND B.IsActive = CASE WHEN(ISNULL(@IsActive, '') = '') THEN B.IsActive ELSE @IsActive END
			--AND B.isMain = CASE WHEN(ISNULL(@IsMain, '') = '') THEN B.isMain ELSE @IsMain END
			ORDER BY B.SEQ ASC
		END
	end
END


GO
