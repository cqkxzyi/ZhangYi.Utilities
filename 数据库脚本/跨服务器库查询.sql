--1.�������ӷ�����,ͨ��@datasrcָ������Դ���ʺ����������������ݿ�

EXEC master.dbo.sp_addlinkedserver @server = N'Link',
                                   @srvproduct='ms', 
                                   @provider=N'SQLNCLI',                                    
                                   @datasrc=N'192.168.2.235'
 
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'Link',
                                    @useself=N'False',
                                    @locallogin=NULL,
                                    @rmtuser=N'sa',
                                    @rmtpassword='P@SSW0RD' --P@SSW0RD   QWEqwe123

--2.Ȼ������������Ĳ�ѯ,����:���ݿ�2��Զ�̷�������

update [OrderedGift] set SettlementPrice=temp2.SettlementPrice_decimal  from [OrderedGift] as temp1 inner join 
(
	select c.*,b.Id from [LiOrder] as a inner join [OrderedGift] as b on a.Id=b.OrderId 
	inner join 
	(
		select b.OrderNo_nvarchar,c.skuitemid_guid,a.SettlementPrice_decimal from [Link].[LI63O2ON].[dbo].[n_shop_shoppingcommodity] as a 
		left join [Link].[LI63O2ON].[dbo].[n_shopping] as b on a.shoppingid_bigint=b.Id_bigint
		left join [Link].[LI63O2ON].[dbo].[n_commodity] as c on a.commodityid_bigint=c.id_bigint
		where a.shoppingid_bigint in (select Id_bigint from [Link].[LI63O2ON].[dbo].[n_shopping] where createtime_datetime >='2017-02-13 12:00:00')
	) as c on c.OrderNo_nvarchar=a.SerialNo and c.SkuItemID_guid=b.GiftId
) as temp2 on temp1.Id=temp2.Id 



--ɾ�����ӷ�����
exec sp_dropserver 'Link','droplogins'



--�ڶ��ַ�ʽ------------
SELECT TOP 10 * FROM
opendatasource('SQLOLEDB','server=114.55.2.99;uid=lyzbzhangyi;pwd=lt35fz7nC5Q9;database=LI63IDSO').[LI63IDSO].[dbo].[Portal_UserAccount]