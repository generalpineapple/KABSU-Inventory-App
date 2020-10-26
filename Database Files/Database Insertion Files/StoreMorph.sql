CREATE DEFINER=`root`@`localhost` PROCEDURE `StoreMorph`(
IN Notes VARCHAR(256),
IN Date VARCHAR(32),
IN RealDate Date,
IN Vigor INT,
IN Mot INT,
IN Morph INT,
IN Code INT,
IN Units INT,
IN ID VARCHAR(32))
BEGIN
DELETE FROM `Data` WHERE `AnimalID` = ID;
INSERT kabsu.Data(Notes, Date, RealDate, Vigor, Mot, Morph, Code, Units, AnimalID)
VALUES(Notes, Date, RealDate, Vigor, Mot, Morph, Code, Units, ID);
END
