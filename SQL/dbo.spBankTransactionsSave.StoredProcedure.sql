USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spBankTransactionsSave]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spBankTransactionsSave] (
	@isOvereWrite					[VARCHAR](1) = 'N',
	@TransactionID					[VARCHAR](50) = '', 
	@TransactionDate				[VARCHAR](50) = '', 
	@ChqNo							[VARCHAR](50) = '',
	@Particulars					[VARCHAR](50) = '',
	@Debit							[VARCHAR](50) = '',
	@Credit							[VARCHAR](50) = '',
	@Balance						[VARCHAR](50) = '',
	@InitBr							[VARCHAR](50) = '',
	@BankName						[VARCHAR](50) = '',
	@OrganizationCode				[VARCHAR](50) = '',
	@UserCode						[VARCHAR](50) = '',
	@isOnlyDelete					[VARCHAR](1),
	@NewDatauniqueID 				[BIGINT] OUT,
	@ErrorMessage 					[VARCHAR](50) OUT
)
AS 
BEGIN 

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @IsExists INT
	DECLARE @OrganizationId BIGINT
	select @OrganizationId = 0

	DECLARE @UserID BIGINT
	SELECT @UserID = 0

	IF(ISNULL(RTRIM(LTRIM(@UserCode)), '') <> '')
	BEGIN
		SELECT @UserID = UserId FROM [urMstUserRegisteredMaster] WHERE [UserCode] = @UserCode
	END
	IF(ISNULL(RTRIM(LTRIM(@OrganizationCode)), '') <> '')
	BEGIN
		SELECT @OrganizationId = OrganizationId FROM [urMstOrganizationMaster] WHERE [organizationCode] = @OrganizationCode
	END
			   				  
	IF ISNULL(RTRIM(LTRIM(@isOvereWrite)), '') <> 'Y'
	begin
		IF ((select count(*) from [mstBankTransactions] where BankName = @BankName and OrganizationID = @OrganizationId and TransactionID = @TransactionID) > 0)
		begin
			RAISERROR('Transaction ID already exists', 16, 1)
		end

		INSERT INTO [mstBankTransactions]([DatauniqueID],TransactionID,[ActivityType],[TransactionDate],
			[ChqNo],[Particulars],[Debit],[Credit],[Balance],[InitBr],[IsActive],[BankName],
			[LastModifiedOn],[LastModifiedBy],[OrganizationID],[CreatedOn],[CreatedBy])
		VALUES( 1, @TransactionID, 'A', @TransactionDate,
			@ChqNo,@Particulars,@Debit,@Credit,@Balance,@InitBr,'Y',@BankName,
			GETDATE(), @UserID,@OrganizationId,GETDATE(), @UserID)
	
	end
	ELSE
	begin
		INSERT INTO [mstBankTransactionsAudit]([DatauniqueID],TransactionID,[ActivityType],[TransactionDate],
			[ChqNo],[Particulars],[Debit],[Credit],[Balance],[InitBr],[IsActive],[BankName],
			[LastModifiedOn],[LastModifiedBy],[OrganizationID],[CreatedOn],[CreatedBy])
		SELECT [DatauniqueID],TransactionID,[ActivityType],[TransactionDate],
			[ChqNo],[Particulars],[Debit],[Credit],[Balance],[InitBr],[IsActive],[BankName],
			GETDATE(),@UserID,[OrganizationID],[CreatedOn],[CreatedBy]
			FROM [mstBankTransactions]
			WHERE TransactionID = @TransactionID
		
		UPDATE [mstBankTransactions] SET 
			[DatauniqueID] = (SELECT MAX([DatauniqueID]) + 1 FROM [mstBankTransactions] WHERE BankName = @BankName and OrganizationID = @OrganizationId and TransactionID = @TransactionID),
			[TransactionDate] = @TransactionDate,
			[ChqNo] = @ChqNo,
			[Particulars] = @Particulars,
			[Debit] = @Debit,
			[Credit] = @Credit,
			[Balance] = @Balance,
			[InitBr] = @InitBr,
			[IsActive] = 'Y',
			[BankName] = @BankName,
			[LastModifiedOn] = GETDATE(),
			[LastModifiedBy] = @UserID,
			[OrganizationID] = @OrganizationId
		WHERE TransactionID = @TransactionID
	END
END





GO
