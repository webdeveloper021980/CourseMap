IF OBJECT_ID('StudentBasicInformations') IS NOT NULL
DROP TABLE dbo.StudentBasicInformations
Go


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StudentBasicInformations](
	[StudentId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[FullName] [varchar](201) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[SchoolOrCollage] [varchar](10) NOT NULL,
	[SchoolOrCollageId] [bigint] NOT NULL,
	[Major] [varchar](50) NOT NULL,
	[Dob] [datetime] NOT NULL,
	[Address1] [varchar](500) NULL,
	[Address2] [varchar](500) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[ZipCode] [varchar](50) NULL,
 CONSTRAINT [PK_StudentBasicInformations] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


IF OBJECT_ID('States') IS NOT NULL
DROP TABLE dbo.States
Go


CREATE TABLE [dbo].[States](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[States] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[States]([Id], [StateName])
SELECT 1, N'Alabama' UNION ALL
SELECT 2, N'Alaska' UNION ALL
SELECT 3, N'Arizona' UNION ALL
SELECT 4, N'Arkansas' UNION ALL
SELECT 5, N'California' UNION ALL
SELECT 6, N'Colorado' UNION ALL
SELECT 7, N'Connecticut' UNION ALL
SELECT 8, N'Delaware' UNION ALL
SELECT 9, N'Florida' UNION ALL
SELECT 10, N'Georgia' UNION ALL
SELECT 11, N'Hawaii' UNION ALL
SELECT 12, N'Idaho' UNION ALL
SELECT 13, N'Illinois' UNION ALL
SELECT 14, N'Indiana' UNION ALL
SELECT 15, N'Iowa' UNION ALL
SELECT 16, N'Kansas' UNION ALL
SELECT 17, N'Kentucky' UNION ALL
SELECT 18, N'Louisiana' UNION ALL
SELECT 19, N'Maine' UNION ALL
SELECT 20, N'Massachusetts' UNION ALL
SELECT 21, N'Michigan' UNION ALL
SELECT 22, N'Minnesota' UNION ALL
SELECT 23, N'Mississippi' UNION ALL
SELECT 24, N'Missouri' UNION ALL
SELECT 25, N'Montana' UNION ALL
SELECT 26, N'Nebraska' UNION ALL
SELECT 27, N'Nevada' UNION ALL
SELECT 28, N'New Hampshire' UNION ALL
SELECT 29, N'New Jersey' UNION ALL
SELECT 30, N'New Mexico' UNION ALL
SELECT 31, N'New York' UNION ALL
SELECT 32, N'North Carolina' UNION ALL
SELECT 33, N'North Dakota' UNION ALL
SELECT 34, N'Ohio' UNION ALL
SELECT 35, N'Oklahoma' UNION ALL
SELECT 36, N'Oregon' UNION ALL
SELECT 37, N'Pennsylvania' UNION ALL
SELECT 38, N'Rhode Island' UNION ALL
SELECT 39, N'South Carolina' UNION ALL
SELECT 40, N'South Dakota' UNION ALL
SELECT 41, N'Tennessee' UNION ALL
SELECT 42, N'Texas' UNION ALL
SELECT 43, N'Utah' UNION ALL
SELECT 44, N'Vermont' UNION ALL
SELECT 45, N'Virginia' UNION ALL
SELECT 46, N'Washington' UNION ALL
SELECT 47, N'West Vifginia' UNION ALL
SELECT 48, N'Wisconsin' UNION ALL
SELECT 49, N'Wyoming'
COMMIT;
RAISERROR (N'[dbo].[States]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[States] OFF;



-----03122015----------------------------------------------------
IF OBJECT_ID('fn_ElectiveInformation') IS  NOT Null
DROP FUNCTION  fn_ElectiveInformation

Go

 GO

 
CREATE FUNCTION fn_ElectiveInformation(@CourseId BIGINT)

RETURNS  VARCHAR(MAX)
AS
BEGIN	
	DECLARE @ElectiveName VARCHAR(max)
	
	SELECT    
		@ElectiveName  = 	COALESCE(@ElectiveName+', ','') + ElectiveName
    FROM dbo.ElectiveInformations     
    WHERE CourseId = @CourseId   
         
    RETURN @ElectiveName
END

GO

IF OBJECT_ID('Vw_StudentCourseInformation') IS  NOT Null
DROP VIEW Vw_StudentCourseInformation

Go
CREATE VIEW Vw_StudentCourseInformation
As
Select cr.Id AS CourseId,cr.CourseName,cr.PassingGrade,cr.Unit,st.GradeRecieved,st.CourseStatus,
	dbo.fn_ElectiveInformation(cr.Id) AS ElectiveName
,st.StudentId,mj.Id AS MajorId
 From CourseInformations cr INNER JOIN dbo.MajorInformations mj ON cr.MajorId = mj.Id
  LEFT OUTER JOIN dbo.StudentCourseInformations st ON st.CourseId = cr.Id
 
 GO
 