DROP TABLE IF EXISTS tempsample;

CREATE TABLE tempsample (
Valid BOOL NOT NULL,
CanNum VARCHAR(8) NOT NULL,
Code VARCHAR(32) NOT NULL,
CollDate VARCHAR(32),
NumUnits INT NOT NULL,
AnimalName VARCHAR(128) NOT NULL,
Breed VARCHAR(64) NOT NULL,
RegNum VARCHAR(32),
Notes VARCHAR(1000),
PersonName VARCHAR(100) NOT NULL,
Town VARCHAR(64) NOT NULL,
State VARCHAR(2) NOT NULL
);

LOAD DATA INFILE 'filepath'
INTO TABLE tempsample
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

INSERT INTO sample
(SELECT Valid, CanNum, TS.Code, CollDate, NumUnits, P.PersonID, Notes
FROM tempsample TS
	INNER JOIN person P ON P.PersonName EQUALS TS.PersonName AND P.Town EQUALS TS.Town
	INNER JOIN animal A ON A.AnimalID EQUALS TS.Code);

DROP TABLE tempsample;