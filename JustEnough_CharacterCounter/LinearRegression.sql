
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cudf_Test_Calculate_Simple_Linear_Regression_Model_Without_The_Intercept_Term]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
      DROP FUNCTION dbo.cudf_Test_Calculate_Simple_Linear_Regression_Model_Without_The_Intercept_Term
GO

/**
 * The formula for the slope of the least square regression line is a multiplication between
 * the linear correlation coefficent and tue quotient of the standard deviation between two sets.
 *
 * This forumla can be simplied using algebra to the following:
 * (sum(xi * yi) - (sum(x) * sum(y)) / n) / (sum(xi^2)) - (((sum(x))^2) / n)
 * 
 * This is the formula i will be using to calculate the simple linear regression model without the
 * the intercept term.
 */

CREATE FUNCTION cudf_Test_Calculate_Simple_Linear_Regression_Model_Without_The_Intercept_Term
(
      @cXPoints VARCHAR(MAX)
      ,@cYPoints VARCHAR(MAX)
)

RETURNS FLOAT
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result FLOAT;
	SET @Result = 0.2;
      
    -- parse variables
	DECLARE @pos INT ,@nextpos INT ,@valuelen INT, @delimiter CHAR  = ',';
	
	-- formula variables
	DECLARE @countX INT, @countY INT
	DECLARE @sumX FLOAT, @sumY FLOAT, @sumXSquared FLOAT, @xSquaredSum FLOAT, @xySum FLOAT;	
	SELECT  @pos = 0 ,@nextpos = 1, @sumX = 0, @sumY = 0, @countX = 0, @countY = 0
	, @sumXSquared = 0, @xSquaredSum = 0, @xySum = 0;

/*
	-- Sum up the x points
	WHILE @nextpos > 0 
		BEGIN
			DECLARE @x FLOAT;
			SELECT  @nextpos = CHARINDEX(@delimiter,@cXPoints,@pos + 1)
            SELECT  @valuelen = CASE WHEN @nextpos > 0 THEN @nextpos
                                     ELSE LEN(@cXPoints) + 1
                                END - @pos - 1
			SELECT @x = CONVERT(FLOAT,SUBSTRING(@cXPoints,@pos + 1,@valuelen));
			
			-- sum x
            SELECT @sumX = @sumX + @x;
            
            -- sum squared x
            SELECT @xSquaredSum += (@x * @x);
            
            -- incrememnt counter
            SELECT  @pos = @nextpos
            SELECT @countX += 1
		END
	
	-- Sum up the y points
	SELECT  @pos = 0 ,@nextpos = 1;
	WHILE @nextpos > 0
	BEGIN
		SELECT  @nextpos = CHARINDEX(@delimiter,@cYPoints,@pos + 1)
		SELECT  @valuelen = CASE WHEN @nextpos > 0 THEN @nextpos
								 ELSE LEN(@cYPoints) + 1
							END - @pos - 1
		SELECT @sumY = @sumY + CONVERT(FLOAT,SUBSTRING(@cYPoints,@pos + 1,@valuelen))
		SELECT  @pos = @nextpos
		SELECT @countY += 1
	END
*/	
	-- Sum up the x and y points together
	DECLARE @posX INT, @nextposX INT, @posY INT, @nextPosY INT;
	SELECT @posX = 0, @nextposX = 1, @posY =0, @nextPosY = 1;
	WHILE @nextposX > 0
	BEGIN
		-- get x
		DECLARE @x FLOAT
		SELECT @nextposX = CHARINDEX(@delimiter, @cXPoints, @posX + 1)
		SELECT @valuelen = CASE WHEN @nextposX > 0 
								THEN @nextposX
								ELSE LEN(@cXPoints) + 1
							END - @posX - 1
		SELECT @x = CONVERT(FLOAT,SUBSTRING(@cXPoints,@posX + 1,@valuelen));
					
		-- sum x
        SELECT @sumX = @sumX + @x;
        
        -- sum squared x
        SELECT @xSquaredSum += (@x * @x);
		SELECT @posX = @nextposX
		SELECT @countX += 1
		
		-- get y
		DECLARE @y FLOAT
		SELECT @nextposY = CHARINDEX(@delimiter, @cYPoints, @posY + 1)
		SELECT @valuelen = CASE WHEN @nextposY > 0 
								THEN @nextposY
								ELSE LEN(@cYPoints) + 1
							END - @posY - 1
		SELECT @y = CONVERT(FLOAT, SUBSTRING(@cYPoints, @posY + 1, @valuelen));
		SELECT @sumY = @sumY + @y
		SELECT @posY = @nextposY
		
		-- sum
		SELECT @xySum += @x * @y
	END
	
	-- square the sum of the x list
	SELECT @sumXSquared = @sumX * @sumX;

	-- Return the result of the function
	SELECT @Result = (@xySum - ((@sumX * @sumY)/@countX)) / (@xSquaredSum - ((@sumXSquared) / @countX));
	RETURN @Result;
END
GO
