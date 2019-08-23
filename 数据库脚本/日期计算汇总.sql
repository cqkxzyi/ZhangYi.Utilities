DECLARE @Date DATETIME 
SET @Date = GETDATE ()  

--前一天，给定日期的前一天
SELECT DATEADD(DAY,-1,@Date) AS 前一天
--后一天，给定日期的后一天 
SELECT DATEADD(DAY,1,@Date) AS 后一天
GO

--查询某月一共多少天
select day(dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,'2019-06-01')+1, 0)))	
select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,'2019-06-01')+1, 0))
select DATEADD(mm, DATEDIFF(m,0,'2019-06-30')+1, 0)
select DATEDIFF(m,0,'2019-06-20')+1

select day ('2019-06-20')
select datepart(dy,'2019-06-20')
select datepart(dd,'2019-06-20')

 

--月初，计算给定日期所在月的第一天
--这个计算的技巧是先计算当前日期到“1900-01-01”的时间间隔数，然后把它加到“1900-01-01”上来获得特殊的日期，这个技巧可以用---来计算很多不同的日期。
DECLARE @Date  DATETIME
SET @Date=GETDATE()
SELECT DATEADD(MONTH,DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01) AS 所在月的第一天
--精简算法，根据SQL Server的时间表示方式可知，1900-01-01 可以用0代替
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0) AS 所在月的第一天
--上面两种算法精确到天 时分秒均为00:00:00.000
--下面算法课以保留时分秒
--思路：用给定日期减去月第一天与给定日期差的天数
SELECT DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)
GO

--月末，计算给定日期所在月的最后一天
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--思路：当前月的下一月1号在减1天
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1+DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01)) AS 所在月的最一天
SELECT DATEADD(MONTH,1+DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01)-1 AS 所在月的最一天
--1900-01-01 用0代替
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1+DATEDIFF(MONTH,0,@Date),0)) AS 所在月的最一天
SELECT DATEADD(MONTH,1+DATEDIFF(MONTH,0,@Date),0)-1 AS 所在月的最一天
--思路：与月初计算思路相同
SELECT DATEADD(MONTH,DATEDIFF(MONTH,1989-12-31,@Date),1989-12-31) AS 所在月的最一天
--精简算法，1989-12-31 用-1代替
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date),-1) AS 所在月的最一天
--保留时分秒的算法
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)))
GO

--其他月计算

--计算给定日期所在月的上月第一天
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--当前月第一天减去一个月
SELECT DATEADD(MONTH,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS 上月第一天
--简化
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)-1,0) AS 上月第一天
--另一种当前月第一天算法
SELECT DATEADD(MONTH,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) 上月第一天
GO

--计算给定日期所在月的上月最后一天
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--当前月第一天减去一天
SELECT DATEADD(DAY,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS 上月最后一天
--另一种当前月第一天算法
SELECT DATEADD(DAY,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) 上月最后一天
SELECT DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)-1 上月最后一天
--另一种算法，不能用当前月的最后一天加一个月，因为当前月可能是30天。
--例如 SELECT DATEADD(MONTH,1,2010-06-30) --结果是2010-07-30而不是2010-07-31，
--这也是月末算法采用下月第一天减1天计算的原因
--但是如果计算月是31天择无此问题
--例如 SELECT DATEADD(MONTH,1,2010-05-31) --结果是2010-06-30
--因此下面算法是正确的，-1 表示1899-12-31 00:00:00.000-- SELECT CONVERT(DATETIME,-1) 
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date)-1,-1)
--另一种当前月算法
SELECT DATEADD(DAY,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) 上月最后一天
--简化
SELECT DATEADD(DAY,0-DATEPART(DAY,@Date),@Date) 上月最后一天
GO

--计算给定日期所在月的下月第一天
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--当前月第一天加一个月
SELECT DATEADD(MONTH,1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS 下月第一天
--简化
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+1,0) AS 下月第一天
--另一种当前月第一天算法
SELECT DATEADD(MONTH,1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) 下月第一天
GO

--计算给定日期所在月的下月最后一天
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--当前月第一天加2个月再减去1天
SELECT DATEADD(DAY,-1,DATEADD(MONTH,2,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0))) AS 下月最后一天
--简化
SELECT DATEADD(DAY,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+2,0)) AS 下月最后一天
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+2,0)-1 AS 下月最后一天
--另一种算法
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date)+1,-1) 下月最后一天
--另一种当前月第一天算法
SELECT DATEADD(DAY,-1,DATEADD(MONTH,2,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date))) 下月最后一天
GO

--所在星期的第一天，计算给定日期所在星期的第1天(星期日为第一天) 
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--与SQL Server语言版本相关的算法
--思路：当前日期+星期日(每周的第1天)与当前日期的差的天数
--DATEPART(WEEKDAY,DATE)的返回值与@@DATEFIRST相关
SET DATEFIRST 7 -- 或者设置为美国英语SET LANGUAGE us_english; (星期日为第一天)
SELECT DATEADD(WEEKDAY,1-DATEPART(WEEKDAY,@Date),@Date) AS 所在星期的第一天也就是星期日
--星期日，与SQL Server语言版本或@@DATEFIRST无关
--1989-12-31 是星期日，1989-12-31 再加上(当前日期与1989-12-31差的星期数)个星期
SELECT DATEADD(WEEK,DATEDIFF(WEEK,-1,@Date),-1) AS 所在星期的星期日
--或者
SELECT DATEADD(WEEK,DATEDIFF(WEEK,6,@Date),6) AS 所在星期的星期日
GO


--所在星期的第二天，计算给定日期所在星期的第2天(星期日为第一天)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--思路：当前日期+星期一(每周的第2天)与当前日期的差的天数
--DATEPART(WEEKDAY,DATE)的返回值与@@DATEFIRST相关
SET DATEFIRST 7 -- 或者设置为美国英语SET LANGUAGE us_english; (星期日为第一天)
SELECT DATEADD(DAY,2-DATEPART(WEEKDAY,@Date),@Date) AS 所在星期的第二天也就是星期一
--星期一，与SQL Server语言版本或@@DATEFIRST无关
--1900-01-01 是星期一，1900-01-01 再加上(当前日期与1900-01-01差的星期数)个星期
SELECT DATEADD(WEEK,DATEDIFF(WEEK,0,@Date),0) AS 所在星期的星期一
GO

--上个星期第一天，计算给定日期所在星期的上一个星期日(星期日为第一天)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--思路：当前日志所在星期的星期日再减1周
--DATEPART(WEEKDAY,DATE)的返回值与@@DATEFIRST相关
SET DATEFIRST 7 -- 或者设置为美国英语SET LANGUAGE us_english; (星期日为第一天)
SELECT DATEADD(WEEK,-1,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS 上个星期第一天也就是星期日
--一周等于7天
SELECT DATEADD(DAY,-7,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS 上个星期第一天也就是星期日
--简化
SELECT DATEADD(DAY,-6-DATEPART(WEEKDAY,@Date),@Date) AS 上个星期第一天也就是星期日
--上个星期日，与SQL Server语言版本或@@DATEFIRST无关
SELECT DATEADD(WEEK,-1+DATEDIFF(WEEK,-1,@Date),-1) AS 上个星期日
--或者
SELECT DATEADD(WEEK,DATEDIFF(WEEK,6,@Date),-1) AS 上个星期日
GO


--下个星期第一天，计算给定日期所在星期的下一个星期日(星期日为第一天)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--思路：当前日志所在星期的星期日再加1周
--DATEPART(WEEKDAY,DATE)的返回值与@@DATEFIRST相关
SET DATEFIRST 7 -- 或者设置为美国英语SET LANGUAGE us_english; (星期日为第一天)
SELECT DATEADD(WEEK,1,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS 下个星期第一天也就是星期日
--一周等于7天
SELECT DATEADD(DAY,7,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS 下个星期第一天也就是星期日
--简化
SELECT DATEADD(DAY,8-DATEPART(WEEKDAY,@Date),@Date) AS 下个星期第一天也就是星期日
--下个星期日，与SQL Server语言版本或@@DATEFIRST无关
SELECT DATEADD(WEEK,1+DATEDIFF(WEEK,-1,@Date),-1) AS 下个星期日
--或者
SELECT DATEADD(WEEK,DATEDIFF(WEEK,-1,@Date),6) AS 下个星期日
GO

--判断给定日期是星期几
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--DATEPART(WEEKDAY,DATE)的返回值与@@DATEFIRST相关
SET DATEFIRST 7 -- 或者设置为美国英语SET LANGUAGE us_english; (星期日为第一天)
SELECT DATEPART(WEEKDAY,@Date) --返回值 1-星期日，2-星期一，3-星期二......7-星期六
--上面算法与SQL 语言版本或 @@DATEFIRST 相关
--下面算法与SQL Server语言版本或@@DATEFIRST无关
SELECT DATENAME(WEEKDAY,@Date) 星期 
GO


--年度计算
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--年初，计算给定日期所在年的第一天
SELECT DATEADD(YEAR,DATEDIFF(YEAR,0,@Date),0) AS 所在年的第一天
--年末，计算给定日期所在年的最后一天
SELECT DATEADD(YEAR,DATEDIFF(YEAR,-1,@Date),-1) AS 所在年的最后一天
--上一年年初，计算给定日期所在年的上一年的第一天
SELECT DATEADD(YEAR,DATEDIFF(YEAR,-0,@Date)-1,0) AS 所在年的上一年的第一天
--上一年年末，计算给定日期所在年的上一年的最后一天
SELECT DATEADD(YEAR,DATEDIFF(YEAR,0,@Date),-1) AS 所在年的上一年的最后一天
--下一年年初，计算给定日期所在年的下一年的第一天
SELECT DATEADD(YEAR,1+DATEDIFF(YEAR,0,@Date),0) AS 所在年的下一年的第一天
--下一年年末，计算给定日期所在年的下一年的最后一天
SELECT DATEADD(YEAR,1+DATEDIFF(YEAR,-1,@Date),-1) AS 所在年的下一年的最后一天
GO

--季度计算
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--季度初，计算给定日期所在季度的第一天
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date),0) AS 当前季度的第一天
--季度末，计算给定日期所在季度的最后一天
SELECT DATEADD(QUARTER,1+DATEDIFF(QUARTER,0,@Date),-1) AS 当前季度的最后一天
--上个季度初
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date)-1,0) AS 当前季度的上个季度初
--上个季度末
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date),-1) AS 当前季度的上个季度末
--下个季度初
SELECT DATEADD(QUARTER,1+DATEDIFF(QUARTER,0,@Date),0) AS 当前季度的下个季度初
--下个季度末
SELECT DATEADD(QUARTER,2+DATEDIFF(QUARTER,0,@Date),-1) AS 当前季度的下个季度末
GO






--====================================================================
DECLARE @createtime DATETIME 
SET @createtime = GETDATE ()  
select day(@createtime)     --取时间字段的天值
select month(@createtime)    --取时间字段的月值
select year(@createtime)     --取时间字段的年值

 

select datepart(yy,@createtime)      --取时间字段的年值
select datepart(qq,@createtime)      --取时间字段的季度值
select datepart(mm,@createtime)      --取时间字段的月值
select datepart(dy,@createtime)      --取时间字段是那年的第几天
select datepart(dd,@createtime)      --取时间字段的天值
select datepart(wk,@createtime)        --取时间字段是那年的第几个星期
select datepart(dw,@createtime)        --取时间字段是那年的那个星期的第几个工作日（工作日从星期日开算）
select datepart(hh,@createtime)      --取时间字段的小时值
select datepart(mi,@createtime)      --取时间字段的分钟值
select datepart(ss,@createtime)      --取时间字段的秒值
select datepart(ms,@createtime)      --取时间字段的毫秒值

 

select dateadd(yy,-1,@createtime)    ----取时间字段(年份被减1了)

select dateadd(mm,3,@createtime)    ----取时间字段(月份被加3了)

select dateadd(dd,1,@createtime)    ----取时间字段(日被加1了)

 

select DATEDIFF(yy,@createtime,getdate())  --与当前日期的年份差
select DATEDIFF(mm,@createtime,getdate())  --与当前日期的月份差
select DATEDIFF(dd,@createtime,getdate())  --与当前日期的日数差
select DATEDIFF(mi,@createtime,getdate())  --与当前日期的分钟数差

 

select datename(yy,@createtime)    --取时间字段的年值
select datename(mm,@createtime)    --取时间字段的月值
select datename(dd,@createtime)    --取时间字段的天值

 

select getdate()   --取当前时间


日期函数
--1、day(date_expression)
--返回date_expression中的日期值


--2、month(date_expression)
--返回date_expression中的月份值

 

--3、year(date_expression)
--返回date_expression中的年份值

 

--4、DATEADD()
--DATEADD (， ， )
--返回指定日期date 加上指定的额外日期间隔number 产生的新日期。参数“datepart” 取值如下：

 

--5、DATEDIFF()
--DATEDIFF (， ， )
--返回两个指定日期在datepart 方面的不同之处，即date2 超过date1的差距值，其结果值是一个带有正负号的整数值。

 

--、DATENAME()
--DATENAME (， )
--以字符串的形式返回日期的指定部分此部分。由datepart 来指定。

 

--7、DATEPART()

--DATEPART ( datepart , date )

--以整数值的形式返回日期的指定部分。此部分由datepart 来指定。

--DATEPART (dd， date) 等同于DAY (date)
--DATEPART (mm， date) 等同于MONTH (date)
--DATEPART (yy， date) 等同于YEAR (date)

 

--下表列出了 datepart 选项以及 SQL Server Compact Edition 所识别的缩写：
--日期部分        缩写 
--年份            yy、yyyy 
--季度            qq、q 
--月份            mm、m 
--每年的某一日    dy、y 
--日期            dd、d 
--星期            wk、ww 
--工作日*         dw
--小时            hh 
--分钟            mi、n 
--秒              ss、s 
--毫秒            ms