USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spGet_GstCategory]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*
[spGet_GstCategory] 22, '2D010D5E-C5F5-448D-931E-C0DBF731660A', 7

*/
CREATE procedure [dbo].[spGet_GstCategory]
(
	@CategoryId VARCHAR(50) = 0,
	@OrganizationCode VARCHAR(50) = '',
	@ShipStateID VARCHAR(50) = 0
)
AS
BEGIN 

	declare @SalesPrice bigint,
			@Percentage bigint

	select @Percentage = 0

	select @Percentage = ISNULL(SUM(cast(ct.Percentage as numeric(18,2))), 0)
			from taxMapTaxCountryCategory ct 
			left join taxMstTaxMaster t on t.TaxDefinationID = ct.TaxDefinationID
			left join urMstOrganizationBranch b on b.Country = ct.CountryID

			WHERE ct.CategoryId = @CategoryId
			and
			(
				(ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = 'intra' and b.[State] = @ShipStateID)
				or (ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = 'inter' and b.[State] <> @ShipStateID)
				or (ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = '')
			)
			--AND B.OrganizationID = (SELECT O.OrganizationID FROM urMstOrganizationMaster O WHERE O.OrganizationCode = @OrganizationCode)

	select  @Percentage Percentage 
END
GO
