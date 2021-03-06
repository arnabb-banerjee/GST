USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spBankTransactionsUpload]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[spBankTransactionsUpload] (
	@typeBankTransactions								[typeBankTransactions] ReadOnly, 
	@OrganizationCode									VARCHAR(50) = NULL,
	@UserCode											VARCHAR(50) = NULL,
	@ErrorMessage 										VARCHAR(500) OUT
)
AS 
BEGIN 
	DECLARE @NewDatauniqueID BIGINT, @NewCusID BIGINT
	DECLARE @UserID BIGINT
	DECLARE @OrganizaionId BIGINT

	DECLARE @TransactionID VARCHAR(50)
	DECLARE @TransactionDate VARCHAR(30)
	DECLARE @ChqNo [varchar](50)
	DECLARE @Particulars [varchar](500)
	DECLARE @Debit	VARCHAR(21)
	DECLARE @Credit VARCHAR(21)
	DECLARE @Balance VARCHAR(21)
	DECLARE @InitBr VARCHAR(21)

	SELECT @UserID = (SELECT UserID from urMstUserRegisteredMaster where UserCode = @UserCode) 

	SELECT @OrganizaionId = OrganizationID FROM urMstOrganizationMaster WHERE [OrganizationCode] = @OrganizationCode

	DECLARE vendorcursor CURSOR FOR
	SELECT TransactionID, TransactionDate, ChqNo, Particulars, Debit, Credit, Balance, InitBr
	FROM @typeBankTransactions

	DECLARE @OldDatauniqueID BIGINT
	DECLARE @AllowSaveDeleteData VARCHAR(1)
	SELECT	@AllowSaveDeleteData = 'N'
	DECLARE @IsExists INT
	
	BEGIN TRANSACTION T1;
	BEGIN TRY
		OPEN vendorcursor;
		FETCH NEXT FROM vendorcursor 
		INTO @TransactionID, @TransactionDate, @ChqNo, @Particulars, @Debit, @Credit, @Balance, @InitBr

		WHILE @@FETCH_STATUS = 0  
		BEGIN
			
			if(select count(*) from [mstBankTransactions] where TransactionID = @TransactionID) > 0
			begin
				RAISERROR('Transaction ID already exists', 16, 1)
			end
				
			INSERT INTO [mstBankTransactions]([DatauniqueID],TransactionID,[ActivityType],[TransactionDate],
				[ChqNo],[Particulars],[Debit],[Credit],[Balance],[InitBr],[IsActive],
				[LastModifiedOn],[LastModifiedBy],[OrganizationID],[CreatedOn],[CreatedBy])
			VALUES( 1, @TransactionID, 'A', @TransactionDate,
				@ChqNo,@Particulars,@Debit,@Credit,@Balance,@InitBr,'Y',
				GETDATE(), @UserID,@OrganizaionId,GETDATE(), @UserID)

			FETCH NEXT FROM vendorcursor 
			INTO @TransactionID, @TransactionDate, @ChqNo, @Particulars, @Debit, @Credit, @Balance, @InitBr
		END

		IF @@TRANCOUNT > 0
			COMMIT;

	END TRY
	BEGIN CATCH
		SELECT @ErrorMessage = ERROR_MESSAGE()

		IF @@TRANCOUNT > 0
			ROLLBACK;
		throw
	END CATCH

END 



GO
