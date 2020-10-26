UPDATE Record R
SET R.RealDate =  STR_TO_DATE(R.Date,'%m/%d/%Y');

UPDATE Data D
SET D.RealDate =  STR_TO_DATE(D.Date,'%m/%d/%Y');