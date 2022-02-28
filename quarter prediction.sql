select 
qp.GameNo,
qp.QuarterNo,
qp.HomeTeam,
qp.AwayTeam,
qp.HomePoints,
qp.AwayPoints
from QuarterPredictions qp inner join GameTime gt on qp.GameNo=gt.GameNo where gt.GameDate>'2022-02-27 01:00:00.0000000'
order by gt.GameDate