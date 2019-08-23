
-----------inner join 去重---------------
select *,
(select top 1 IOUNo from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  IOUNo,
(select top 1 max(LoanDate) from BPMS_IOUInfo as iou where iou.[LoanNo]=loan.[No]) as  LoanDate
from BPMS_FormalLoan as loan  


-----------distinct 去重---------------
--只能单个字段
select distinct name from tableName

--保留其他字段，同时去重
select *, count(distinct name) from 表名 group by name