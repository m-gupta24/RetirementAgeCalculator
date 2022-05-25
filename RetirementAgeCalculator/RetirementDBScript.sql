IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'RetirementDb')
  BEGIN
    CREATE DATABASE RetirementDb
  END
  GO
  USE [RetirementDb]
  GO
  SET ANSI_NULLS ON
 GO
	SET QUOTED_IDENTIFIER ON
	GO
	IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Retirement')
BEGIN
	CREATE TABLE [dbo].[Retirement](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[FullName] [nvarchar](255) NOT NULL,
		[Age] [int] NOT NULL,
		[Gender] [nvarchar](1) NOT NULL,
		[MonthlySavings] [BIGINT] NOT NULL,
		[TargetRetirementFunds] [BIGINT] NOT NULL,
	 CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	

 END
GO

BEGIN

INSERT INTO Retirement (FullName, Age, Gender, MonthlySavings,TargetRetirementFunds)
VALUES ('Ruskin Bond', 46, 'M', 45000, 60000000),
('Arundhati Roy', 30, 'F', 35000, 40000000),
('Vandana Singh', 40, 'F', 45000, 35000000),
('Gautam Bhatia', 43, 'M', 145000, 250000000),
('Amrita Pritam', 29, 'F', 95000, 90000000),
('Khushwant Singh', 35, 'M', 45000, 70000000),
('Anurag Anand', 38, 'M', 45000, 65000000);
END
GO




