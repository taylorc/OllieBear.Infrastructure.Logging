
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Craig Taylor
-- Create date: 22/05/2020
-- Description:	Stored procedure to support JobName log context
-- =============================================
CREATE PROCEDURE dbo.uspGetLogsByJobName
	-- Add the parameters for the stored procedure here
	@JobName nvarchar(256) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  	SELECT
  [Message], [TimeStamp], [Exception],
  [Properties].value('(//property[@key="JobName"]/node())[1]', 'nvarchar(max)') AS JobName
FROM [Logs]
WHERE
  (@jobName is NULL OR [Properties].value('(//property[@key="JobName"]/node())[1]', 'nvarchar(max)') = @JobName)
END
GO
