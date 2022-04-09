CREATE VIEW [dbo].[vwOrigins_Get]
	AS SELECT TOP (100) PERCENT Id, Region, Country
			FROM [dbo].[Origin]
			ORDER BY Country, Region