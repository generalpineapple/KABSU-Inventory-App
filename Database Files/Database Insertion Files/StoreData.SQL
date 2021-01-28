CREATE DEFINER=`root`@`localhost` PROCEDURE `StoreData`(
IN ToFrom VARCHAR(100),
IN RealDate Date,
IN Date VARCHAR(32),
IN Received INT,
IN Shipped INT,
IN Balance INT,
IN AnimalID VARCHAR(32))
BEGIN
INSERT kabsu.Record(ToFrom, Date, NumReceived, NumShipped, Balance, AnimalID, RealDate)
VALUES(ToFrom, Date, Received, Shipped, Balance, AnimalID, RealDate);
END
