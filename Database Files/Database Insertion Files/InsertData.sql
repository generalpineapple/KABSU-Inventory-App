CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertPerson`(
IN PersonID INT,
IN Name VARCHAR(100),
IN City VARCHAR(64),
IN State VARCHAR(2),
IN Country VARCHAR(3))
BEGIN
INSERT kabsu.person(PersonID, Name, City, State, Country)
VALUES(PersonID, Name, City, State, Country);
END

CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertAnimal`(
IN AnimalID VARCHAR(32),
IN Name VARCHAR(128),
IN Breed VARCHAR(64),
IN Species VARCHAR(16),
IN RegNum VARCHAR(32),
IN PersonID INT)
BEGIN 
IF NOT EXISTS (SELECT * FROM kabsu.animal a
				WHERE a.Name = Name
				AND a.RegNum = RegNum
				AND a.PersonID = PersonID)
	BEGIN
	INSERT kabsu.animal(AnimalID, Name, Breed, Species, RegNum, PersonID)
	VALUES(AnimalID, Name, Breed, Species, RegNum, PersonID);
	END
END

CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertSample`(
IN Valid BOOL,
IN CanNum VARCHAR(8),
IN AnimalID VARCHAR(32),
IN CollDate VARCHAR(32),
IN NumUnits INT,
IN PersonID INT,
IN Notes VARCHAR(1000))
BEGIN
INSERT kabsu.sample(Valid, CanNum, AnimalID, CollDate, NumUnits, PersonID, Notes)
VALUES(Valid, CanNum, AnimalID, CollDate, NumUnits, PersonID, Notes);
END
