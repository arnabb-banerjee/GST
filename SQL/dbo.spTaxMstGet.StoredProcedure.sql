USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spTaxMstGet]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spTaxMstGet] (
	@Mode					INT = 0,/*0 = MASTER, 1 =DROPDOWN*/
	@TaxDefinationID		BIGINT = 0
)
AS 
BEGIN 
	if @Mode = 0 begin
		SELECT A.TaxDefinationID, A.TaxName, A.DatauniqueID, 
			A.LastModifiedBy, A.LastModifiedOn
		FROM taxMstTaxMaster A
		WHERE A.TaxDefinationID = CASE WHEN(ISNULL(@TaxDefinationID, '') = '') Then A.TaxDefinationID Else @TaxDefinationID End
	END
END


GO
