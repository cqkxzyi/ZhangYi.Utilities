
-----------inner join ȥ��---------------
select *,
(select top 1 IOUNo from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  IOUNo,
(select top 1 max(LoanDate) from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  LoanDate
from BPMS_FormalLoan as loan  


-----------distinct ȥ��---------------
--ֻ�ܵ����ֶ�
select distinct name from tableName

--���������ֶΣ�ͬʱȥ��
select *, count(distinct name) from ���� group by name