DECLARE @Date DATETIME 
SET @Date = GETDATE ()  

--ǰһ�죬�������ڵ�ǰһ��
SELECT DATEADD(DAY,-1,@Date) AS ǰһ��
--��һ�죬�������ڵĺ�һ�� 
SELECT DATEADD(DAY,1,@Date) AS ��һ��
GO

--��ѯĳ��һ��������
select day(dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,'2019-06-01')+1, 0)))	
select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,'2019-06-01')+1, 0))
select DATEADD(mm, DATEDIFF(m,0,'2019-06-30')+1, 0)
select DATEDIFF(m,0,'2019-06-20')+1

select day ('2019-06-20')
select datepart(dy,'2019-06-20')
select datepart(dd,'2019-06-20')

 

--�³�������������������µĵ�һ��
--�������ļ������ȼ��㵱ǰ���ڵ���1900-01-01����ʱ��������Ȼ������ӵ���1900-01-01�����������������ڣ�������ɿ�����---������ܶ಻ͬ�����ڡ�
DECLARE @Date  DATETIME
SET @Date=GETDATE()
SELECT DATEADD(MONTH,DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01) AS �����µĵ�һ��
--�����㷨������SQL Server��ʱ���ʾ��ʽ��֪��1900-01-01 ������0����
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0) AS �����µĵ�һ��
--���������㷨��ȷ���� ʱ�����Ϊ00:00:00.000
--�����㷨���Ա���ʱ����
--˼·���ø������ڼ�ȥ�µ�һ����������ڲ������
SELECT DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)
GO

--��ĩ������������������µ����һ��
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--˼·����ǰ�µ���һ��1���ڼ�1��
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1+DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01)) AS �����µ���һ��
SELECT DATEADD(MONTH,1+DATEDIFF(MONTH,1900-01-01,@Date),1900-01-01)-1 AS �����µ���һ��
--1900-01-01 ��0����
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1+DATEDIFF(MONTH,0,@Date),0)) AS �����µ���һ��
SELECT DATEADD(MONTH,1+DATEDIFF(MONTH,0,@Date),0)-1 AS �����µ���һ��
--˼·�����³�����˼·��ͬ
SELECT DATEADD(MONTH,DATEDIFF(MONTH,1989-12-31,@Date),1989-12-31) AS �����µ���һ��
--�����㷨��1989-12-31 ��-1����
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date),-1) AS �����µ���һ��
--����ʱ������㷨
SELECT DATEADD(DAY,-1,DATEADD(MONTH,1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)))
GO

--�����¼���

--����������������µ����µ�һ��
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--��ǰ�µ�һ���ȥһ����
SELECT DATEADD(MONTH,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS ���µ�һ��
--��
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)-1,0) AS ���µ�һ��
--��һ�ֵ�ǰ�µ�һ���㷨
SELECT DATEADD(MONTH,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) ���µ�һ��
GO

--����������������µ��������һ��
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--��ǰ�µ�һ���ȥһ��
SELECT DATEADD(DAY,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS �������һ��
--��һ�ֵ�ǰ�µ�һ���㷨
SELECT DATEADD(DAY,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) �������һ��
SELECT DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)-1 �������һ��
--��һ���㷨�������õ�ǰ�µ����һ���һ���£���Ϊ��ǰ�¿�����30�졣
--���� SELECT DATEADD(MONTH,1,2010-06-30) --�����2010-07-30������2010-07-31��
--��Ҳ����ĩ�㷨�������µ�һ���1������ԭ��
--���������������31�����޴�����
--���� SELECT DATEADD(MONTH,1,2010-05-31) --�����2010-06-30
--��������㷨����ȷ�ģ�-1 ��ʾ1899-12-31 00:00:00.000-- SELECT CONVERT(DATETIME,-1) 
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date)-1,-1)
--��һ�ֵ�ǰ���㷨
SELECT DATEADD(DAY,-1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) �������һ��
--��
SELECT DATEADD(DAY,0-DATEPART(DAY,@Date),@Date) �������һ��
GO

--����������������µ����µ�һ��
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--��ǰ�µ�һ���һ����
SELECT DATEADD(MONTH,1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0)) AS ���µ�һ��
--��
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+1,0) AS ���µ�һ��
--��һ�ֵ�ǰ�µ�һ���㷨
SELECT DATEADD(MONTH,1,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date)) ���µ�һ��
GO

--����������������µ��������һ��
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--��ǰ�µ�һ���2�����ټ�ȥ1��
SELECT DATEADD(DAY,-1,DATEADD(MONTH,2,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date),0))) AS �������һ��
--��
SELECT DATEADD(DAY,-1,DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+2,0)) AS �������һ��
SELECT DATEADD(MONTH,DATEDIFF(MONTH,0,@Date)+2,0)-1 AS �������һ��
--��һ���㷨
SELECT DATEADD(MONTH,DATEDIFF(MONTH,-1,@Date)+1,-1) �������һ��
--��һ�ֵ�ǰ�µ�һ���㷨
SELECT DATEADD(DAY,-1,DATEADD(MONTH,2,DATEADD(DAY,1-DATEPART(DAY,@Date),@Date))) �������һ��
GO

--�������ڵĵ�һ�죬������������������ڵĵ�1��(������Ϊ��һ��) 
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--��SQL Server���԰汾��ص��㷨
--˼·����ǰ����+������(ÿ�ܵĵ�1��)�뵱ǰ���ڵĲ������
--DATEPART(WEEKDAY,DATE)�ķ���ֵ��@@DATEFIRST���
SET DATEFIRST 7 -- ��������Ϊ����Ӣ��SET LANGUAGE us_english; (������Ϊ��һ��)
SELECT DATEADD(WEEKDAY,1-DATEPART(WEEKDAY,@Date),@Date) AS �������ڵĵ�һ��Ҳ����������
--�����գ���SQL Server���԰汾��@@DATEFIRST�޹�
--1989-12-31 �������գ�1989-12-31 �ټ���(��ǰ������1989-12-31���������)������
SELECT DATEADD(WEEK,DATEDIFF(WEEK,-1,@Date),-1) AS �������ڵ�������
--����
SELECT DATEADD(WEEK,DATEDIFF(WEEK,6,@Date),6) AS �������ڵ�������
GO


--�������ڵĵڶ��죬������������������ڵĵ�2��(������Ϊ��һ��)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--˼·����ǰ����+����һ(ÿ�ܵĵ�2��)�뵱ǰ���ڵĲ������
--DATEPART(WEEKDAY,DATE)�ķ���ֵ��@@DATEFIRST���
SET DATEFIRST 7 -- ��������Ϊ����Ӣ��SET LANGUAGE us_english; (������Ϊ��һ��)
SELECT DATEADD(DAY,2-DATEPART(WEEKDAY,@Date),@Date) AS �������ڵĵڶ���Ҳ��������һ
--����һ����SQL Server���԰汾��@@DATEFIRST�޹�
--1900-01-01 ������һ��1900-01-01 �ټ���(��ǰ������1900-01-01���������)������
SELECT DATEADD(WEEK,DATEDIFF(WEEK,0,@Date),0) AS �������ڵ�����һ
GO

--�ϸ����ڵ�һ�죬������������������ڵ���һ��������(������Ϊ��һ��)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--˼·����ǰ��־�������ڵ��������ټ�1��
--DATEPART(WEEKDAY,DATE)�ķ���ֵ��@@DATEFIRST���
SET DATEFIRST 7 -- ��������Ϊ����Ӣ��SET LANGUAGE us_english; (������Ϊ��һ��)
SELECT DATEADD(WEEK,-1,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS �ϸ����ڵ�һ��Ҳ����������
--һ�ܵ���7��
SELECT DATEADD(DAY,-7,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS �ϸ����ڵ�һ��Ҳ����������
--��
SELECT DATEADD(DAY,-6-DATEPART(WEEKDAY,@Date),@Date) AS �ϸ����ڵ�һ��Ҳ����������
--�ϸ������գ���SQL Server���԰汾��@@DATEFIRST�޹�
SELECT DATEADD(WEEK,-1+DATEDIFF(WEEK,-1,@Date),-1) AS �ϸ�������
--����
SELECT DATEADD(WEEK,DATEDIFF(WEEK,6,@Date),-1) AS �ϸ�������
GO


--�¸����ڵ�һ�죬������������������ڵ���һ��������(������Ϊ��һ��)
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--˼·����ǰ��־�������ڵ��������ټ�1��
--DATEPART(WEEKDAY,DATE)�ķ���ֵ��@@DATEFIRST���
SET DATEFIRST 7 -- ��������Ϊ����Ӣ��SET LANGUAGE us_english; (������Ϊ��һ��)
SELECT DATEADD(WEEK,1,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS �¸����ڵ�һ��Ҳ����������
--һ�ܵ���7��
SELECT DATEADD(DAY,7,DATEADD(DAY,1-DATEPART(WEEKDAY,@Date),@Date)) AS �¸����ڵ�һ��Ҳ����������
--��
SELECT DATEADD(DAY,8-DATEPART(WEEKDAY,@Date),@Date) AS �¸����ڵ�һ��Ҳ����������
--�¸������գ���SQL Server���԰汾��@@DATEFIRST�޹�
SELECT DATEADD(WEEK,1+DATEDIFF(WEEK,-1,@Date),-1) AS �¸�������
--����
SELECT DATEADD(WEEK,DATEDIFF(WEEK,-1,@Date),6) AS �¸�������
GO

--�жϸ������������ڼ�
DECLARE @Date  DATETIME
SET @Date= GETDATE()
--DATEPART(WEEKDAY,DATE)�ķ���ֵ��@@DATEFIRST���
SET DATEFIRST 7 -- ��������Ϊ����Ӣ��SET LANGUAGE us_english; (������Ϊ��һ��)
SELECT DATEPART(WEEKDAY,@Date) --����ֵ 1-�����գ�2-����һ��3-���ڶ�......7-������
--�����㷨��SQL ���԰汾�� @@DATEFIRST ���
--�����㷨��SQL Server���԰汾��@@DATEFIRST�޹�
SELECT DATENAME(WEEKDAY,@Date) ���� 
GO


--��ȼ���
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--����������������������ĵ�һ��
SELECT DATEADD(YEAR,DATEDIFF(YEAR,0,@Date),0) AS ������ĵ�һ��
--��ĩ�����������������������һ��
SELECT DATEADD(YEAR,DATEDIFF(YEAR,-1,@Date),-1) AS ����������һ��
--��һ���������������������������һ��ĵ�һ��
SELECT DATEADD(YEAR,DATEDIFF(YEAR,-0,@Date)-1,0) AS ���������һ��ĵ�һ��
--��һ����ĩ����������������������һ������һ��
SELECT DATEADD(YEAR,DATEDIFF(YEAR,0,@Date),-1) AS ���������һ������һ��
--��һ���������������������������һ��ĵ�һ��
SELECT DATEADD(YEAR,1+DATEDIFF(YEAR,0,@Date),0) AS ���������һ��ĵ�һ��
--��һ����ĩ����������������������һ������һ��
SELECT DATEADD(YEAR,1+DATEDIFF(YEAR,-1,@Date),-1) AS ���������һ������һ��
GO

--���ȼ���
DECLARE @Date  DATETIME
SET @Date=GETDATE()
--���ȳ�����������������ڼ��ȵĵ�һ��
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date),0) AS ��ǰ���ȵĵ�һ��
--����ĩ����������������ڼ��ȵ����һ��
SELECT DATEADD(QUARTER,1+DATEDIFF(QUARTER,0,@Date),-1) AS ��ǰ���ȵ����һ��
--�ϸ����ȳ�
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date)-1,0) AS ��ǰ���ȵ��ϸ����ȳ�
--�ϸ�����ĩ
SELECT DATEADD(QUARTER,DATEDIFF(QUARTER,0,@Date),-1) AS ��ǰ���ȵ��ϸ�����ĩ
--�¸����ȳ�
SELECT DATEADD(QUARTER,1+DATEDIFF(QUARTER,0,@Date),0) AS ��ǰ���ȵ��¸����ȳ�
--�¸�����ĩ
SELECT DATEADD(QUARTER,2+DATEDIFF(QUARTER,0,@Date),-1) AS ��ǰ���ȵ��¸�����ĩ
GO






--====================================================================
DECLARE @createtime DATETIME 
SET @createtime = GETDATE ()  
select day(@createtime)     --ȡʱ���ֶε���ֵ
select month(@createtime)    --ȡʱ���ֶε���ֵ
select year(@createtime)     --ȡʱ���ֶε���ֵ

 

select datepart(yy,@createtime)      --ȡʱ���ֶε���ֵ
select datepart(qq,@createtime)      --ȡʱ���ֶεļ���ֵ
select datepart(mm,@createtime)      --ȡʱ���ֶε���ֵ
select datepart(dy,@createtime)      --ȡʱ���ֶ�������ĵڼ���
select datepart(dd,@createtime)      --ȡʱ���ֶε���ֵ
select datepart(wk,@createtime)        --ȡʱ���ֶ�������ĵڼ�������
select datepart(dw,@createtime)        --ȡʱ���ֶ���������Ǹ����ڵĵڼ��������գ������մ������տ��㣩
select datepart(hh,@createtime)      --ȡʱ���ֶε�Сʱֵ
select datepart(mi,@createtime)      --ȡʱ���ֶεķ���ֵ
select datepart(ss,@createtime)      --ȡʱ���ֶε���ֵ
select datepart(ms,@createtime)      --ȡʱ���ֶεĺ���ֵ

 

select dateadd(yy,-1,@createtime)    ----ȡʱ���ֶ�(��ݱ���1��)

select dateadd(mm,3,@createtime)    ----ȡʱ���ֶ�(�·ݱ���3��)

select dateadd(dd,1,@createtime)    ----ȡʱ���ֶ�(�ձ���1��)

 

select DATEDIFF(yy,@createtime,getdate())  --�뵱ǰ���ڵ���ݲ�
select DATEDIFF(mm,@createtime,getdate())  --�뵱ǰ���ڵ��·ݲ�
select DATEDIFF(dd,@createtime,getdate())  --�뵱ǰ���ڵ�������
select DATEDIFF(mi,@createtime,getdate())  --�뵱ǰ���ڵķ�������

 

select datename(yy,@createtime)    --ȡʱ���ֶε���ֵ
select datename(mm,@createtime)    --ȡʱ���ֶε���ֵ
select datename(dd,@createtime)    --ȡʱ���ֶε���ֵ

 

select getdate()   --ȡ��ǰʱ��


���ں���
--1��day(date_expression)
--����date_expression�е�����ֵ


--2��month(date_expression)
--����date_expression�е��·�ֵ

 

--3��year(date_expression)
--����date_expression�е����ֵ

 

--4��DATEADD()
--DATEADD (�� �� )
--����ָ������date ����ָ���Ķ������ڼ��number �����������ڡ�������datepart�� ȡֵ���£�

 

--5��DATEDIFF()
--DATEDIFF (�� �� )
--��������ָ��������datepart ����Ĳ�֮ͬ������date2 ����date1�Ĳ��ֵ������ֵ��һ�����������ŵ�����ֵ��

 

--��DATENAME()
--DATENAME (�� )
--���ַ�������ʽ�������ڵ�ָ�����ִ˲��֡���datepart ��ָ����

 

--7��DATEPART()

--DATEPART ( datepart , date )

--������ֵ����ʽ�������ڵ�ָ�����֡��˲�����datepart ��ָ����

--DATEPART (dd�� date) ��ͬ��DAY (date)
--DATEPART (mm�� date) ��ͬ��MONTH (date)
--DATEPART (yy�� date) ��ͬ��YEAR (date)

 

--�±��г��� datepart ѡ���Լ� SQL Server Compact Edition ��ʶ�����д��
--���ڲ���        ��д 
--���            yy��yyyy 
--����            qq��q 
--�·�            mm��m 
--ÿ���ĳһ��    dy��y 
--����            dd��d 
--����            wk��ww 
--������*         dw
--Сʱ            hh 
--����            mi��n 
--��              ss��s 
--����            ms