USE [GST_DEV_V2]
GO
/****** Object:  Table [dbo].[currency]    Script Date: 08-12-2019 17:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[currency](
	[name] [varchar](100) NULL,
	[code] [varchar](100) NULL,
	[symbol] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Leke', N'ALL', N'Lek')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'USD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Afghanis', N'AFN', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'ARS', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Guilders', N'AWG', N'ƒ')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'AUD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'New Manats', N'AZN', N'???')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'BSD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'BBD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rubles', N'BYR', N'p.')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Euro', N'EUR', N'€')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'BZD', N'BZ$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'BMD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Bolivianos', N'BOB', N'$b')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Convertible Marka', N'BAM', N'KM')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pula', N'BWP', N'P')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Leva', N'BGN', N'??')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Reais', N'BRL', N'R$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'GBP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'BND', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Riels', N'KHR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'CAD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'KYD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'CLP', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Yuan Renminbi', N'CNY', N'¥')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'COP', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Colón', N'CRC', N'¢')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Kuna', N'HRK', N'kn')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'CUP', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Koruny', N'CZK', N'Kc')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Kroner', N'DKK', N'kr')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'DOP ', N'RD$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'XCD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'EGP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Colones', N'SVC', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'FKP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'FJD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Cedis', N'GHC', N'¢')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'GIP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Quetzales', N'GTQ', N'Q')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'GGP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'GYD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Lempiras', N'HNL', N'L')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'HKD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Forint', N'HUF', N'Ft')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Kronur', N'ISK', N'kr')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'INR', N'Rp')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupiahs', N'IDR', N'Rp')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rials', N'IRR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'IMP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'New Shekels', N'ILS', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'JMD', N'J$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Yen', N'JPY', N'¥')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'JEP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Tenge', N'KZT', N'??')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Won', N'KPW', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Won', N'KRW', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Soms', N'KGS', N'??')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Kips', N'LAK', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Lati', N'LVL', N'Ls')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'LBP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'LRD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Switzerland Francs', N'CHF', N'CHF')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Litai', N'LTL', N'Lt')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Denars', N'MKD', N'???')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Ringgits', N'MYR', N'RM')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'MUR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'MXN', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Tugriks', N'MNT', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Meticais', N'MZN', N'MT')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'NAD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'NPR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Guilders', N'ANG', N'ƒ')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'NZD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Cordobas', N'NIO', N'C$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Nairas', N'NGN', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Krone', N'NOK', N'kr')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rials', N'OMR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'PKR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Balboa', N'PAB', N'B/.')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Guarani', N'PYG', N'Gs')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Nuevos Soles', N'PEN', N'S/.')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'PHP', N'Php')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Zlotych', N'PLN', N'zl')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rials', N'QAR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'New Lei', N'RON', N'lei')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rubles', N'RUB', N'???')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'SHP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Riyals', N'SAR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dinars', N'RSD', N'???.')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'SCR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'SGD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'SBD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Shillings', N'SOS', N'S')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rand', N'ZAR', N'R')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rupees', N'LKR', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Kronor', N'SEK', N'kr')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'SRD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pounds', N'SYP', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'New Dollars', N'TWD', N'NT$')
GO
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Baht', N'THB', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'TTD', N'TT$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Lira', N'TRY', N'TL')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Liras', N'TRL', N'£')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dollars', N'TVD', N'$')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Hryvnia', N'UAH', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Pesos', N'UYU', N'$U')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Sums', N'UZS', N'??')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Bolivares Fuertes', N'VEF', N'Bs')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Dong', N'VND', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Rials', N'YER', N'?')
INSERT [dbo].[currency] ([name], [code], [symbol]) VALUES (N'Zimbabwe Dollars', N'ZWD', N'Z$')
