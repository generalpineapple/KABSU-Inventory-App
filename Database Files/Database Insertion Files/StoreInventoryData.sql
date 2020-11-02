CREATE DEFINER=`root`@`localhost` PROCEDURE `StoreInventoryData`(
IN Item VARCHAR(32),
IN Description VARCHAR(100),
IN Qty INT,
IN Rate INT,
IN Amount INT,
IN AnimalID VARCHAR(32))
BEGIN
INSERT kabsu.InventoryRecord(Item, Description, Qty, Rate, Amount, AnimalID)
VALUES(Item, Description, Qty, Rate, Amount, AnimalID);
END
