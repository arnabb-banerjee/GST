USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[spGet_Gst]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
spGet_Gst 22, '2D010D5E-C5F5-448D-931E-C0DBF731660A', 7

*/
CREATE procedure [dbo].[spGet_Gst]
(
	@ProductId VARCHAR(50) = 0,
	@OrganizationCode VARCHAR(50) = '',
	@ShipStateID VARCHAR(50) = 0
)
AS
BEGIN 

	declare @SalesPrice bigint = 0,
			@Percentage bigint = 0,
			@CategoryId bigint = 0

	select @CategoryId = CategoryId from prMAPOrganizationproduct where OrganizationproductId = @ProductId
	
	if(isnull(@CategoryId, 0) < 1)
	begin
		select @CategoryId = CategoryId from prMstproductMaster where ProductId = @ProductId
	end

	/*select @CategoryId*/

	select @Percentage = ISNULL(SUM(cast(ct.Percentage as numeric(18,2))), 0)
			from taxMapTaxCountryCategory ct 
			left join taxMstTaxMaster t on t.TaxDefinationID = ct.TaxDefinationID
			left join urMstOrganizationBranch b on b.Country = ct.CountryID

			WHERE ct.CategoryId = @CategoryId
			and
			(
				(ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = 'intra' and b.[State] = @ShipStateID)
				or (ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = 'inter' and b.[State] <> @ShipStateID)
				or (ISNULL(RTRIM(LTRIM(ct.ApplicableType)), '') = 'all')
			)
			--AND B.OrganizationID = (SELECT O.OrganizationID FROM urMstOrganizationMaster O WHERE O.OrganizationCode = @OrganizationCode)

	select @SalesPrice =  p.SalePrice
		from prMAPOrganizationproduct p
		inner join prMstCategoryMaster c on c.CategoryId = p.CategoryId
		WHERE p.OrganizationproductId = @ProductId

	select  @SalesPrice SalePrice, @Percentage Percentage 
END


GO
