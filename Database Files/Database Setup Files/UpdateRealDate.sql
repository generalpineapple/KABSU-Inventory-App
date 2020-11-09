--this code is to update the date string to an actual datetime
--to run this, copy it into the phpmyadmin server
--apparently they formated the date to include the canNum after a '#' thus looking like '%m/%d/%Y#CollectionCode'
UPDATE Record R
SET R.RealDate =  STR_TO_DATE(SUBSTRING_INDEX(R.Date,'#',1),'%m/%d/%Y')


--this takes the CollectionCode part of the date if it exists and adds it to the corresponding column
--to run this, copy it into the phpmyadmin server
UPDATE Data
SET D.CollCode =  SUBSTRING_INDEX(SUBSTRING_INDEX(R.Date,'#',2), '#', -1)
WHERE NOT SUBSTRING_INDEX(SUBSTRING_INDEX(R.Date,'#',2), '#', -1) = R.Date
                             
--just like the other code above, this coverts the string into a datetime
--to run this, copy it into the phpmyadmin server 
UPDATE Data D
SET D.RealDate =  STR_TO_DATE(SUBSTRING_INDEX(D.Date,'#',1),'%m/%d/%Y')