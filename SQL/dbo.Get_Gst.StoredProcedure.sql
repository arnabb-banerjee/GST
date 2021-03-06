USE [GST_DEV_V2]
GO
/****** Object:  StoredProcedure [dbo].[Get_Gst]    Script Date: 08-12-2019 17:51:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Get_Gst]
(
	@ProductId VARCHAR(50) = 0,
	@OrganizationCode VARCHAR(50) = '',
	@ShipStateID VARCHAR(50) = 0
)
AS
BEGIN 
	select * from (select ISNULL(SUM(cast(ct.Percentage as numeric(18,2))), 0) Percentage 
	from prMAPOrganizationproduct p
	left join prMstCategoryMaster c on c.CategoryId = p.CategoryId
	left join prMAPCategoryTaxDefination ct on c.CategoryId = ct.CategoryId
	left join prMstAllowedTaxDefinationCountryWise tc on tc.TaxDefinationID = ct.TaxDefinationID
	left join urMstOrganizationBranch b on b.Country = tc.CountryID

	WHERE (
		(tc.LocationRangeType = 'intra' and b.[State] = @ShipStateID)
		or (tc.LocationRangeType = 'inter' and b.[State] <> @ShipStateID)
		or (ISNULL(RTRIM(LTRIM(tc.LocationRangeType)), '') = '')
	)
	AND B.OrganizationID = (SELECT O.OrganizationID FROM urMstOrganizationMaster O WHERE O.OrganizationCode = @OrganizationCode)
	) a, 
	(select p.SalePrice, c.HSNCode, c.ServiceOrGoods
	from prMAPOrganizationproduct p
	left join prMstCategoryMaster c on c.CategoryId = p.CategoryId
	WHERE p.ProductId = @ProductId) b
END








GO
