CREATE DEFINER=`root`@`localhost` PROCEDURE `RetrieveInventoryRecords`(
IN AnimalID VARCHAR(32))
BEGIN
SELECT IR.Item, IR.Description, IR.Qty, IR.Rate, IR.Amount
FROM InventoryRecord IR
WHERE R.AnimalID = AnimalID;
END