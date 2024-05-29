namespace MaterialSupplyManagement.DAL;

public static class SqlQueries
{
	public const string RawMaterialsTableName = "raw_materials";
	public const string InventoryItemTableName = "inventory_items";
	
	public static string CreateRawMaterialsTable => $@"
        CREATE TABLE IF NOT EXISTS ""{RawMaterialsTableName}""
        (            
            ""{ColumnNames.Name}"" TEXT NOT NULL,
            ""{ColumnNames.Type}"" TEXT NOT NULL,
            ""{ColumnNames.Description}"" TEXT,
            ""{ColumnNames.LastUpdate}"" TIMESTAMPTZ NOT NULL,
            ""{ColumnNames.PricePerKg}"" DOUBLE PRECISION,
            ""{ColumnNames.TotalPrice}"" DOUBLE PRECISION,
            ""{ColumnNames.WeightTons}"" INT
        );
        CREATE INDEX IF NOT EXISTS {ColumnNames.Type}_index ON {RawMaterialsTableName} ({ColumnNames.Type});
        CREATE INDEX IF NOT EXISTS {ColumnNames.Name}_index ON {RawMaterialsTableName} ({ColumnNames.Name});
	";
	
	public static string CreateInventoryItemTable => $@"
		CREATE TABLE IF NOT EXISTS ""{InventoryItemTableName}""
        (            
            ""{ColumnNames.Name}"" TEXT NOT NULL,
            ""{ColumnNames.Type}"" TEXT NOT NULL,
            ""{ColumnNames.Description}"" TEXT,
            ""{ColumnNames.LastUpdate}"" TIMESTAMPTZ NOT NULL,
            ""{ColumnNames.Number}"" INT,
            ""{ColumnNames.Price}"" DOUBLE PRECISION,
            ""{ColumnNames.TotalPrice}"" DOUBLE PRECISION,
            ""{ColumnNames.ManufacturerName}"" TEXT NOT NULL,
        	""{ColumnNames.ManufacturerAddress}"" TEXT NOT NULL
        );
        CREATE INDEX IF NOT EXISTS {ColumnNames.Type}_index ON {InventoryItemTableName} ({ColumnNames.Type});
        CREATE INDEX IF NOT EXISTS {ColumnNames.Name}_index ON {InventoryItemTableName} ({ColumnNames.Name});
	";
}