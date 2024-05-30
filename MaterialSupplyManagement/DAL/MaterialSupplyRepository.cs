using Npgsql;
using RepoDb;
using Serilog;

namespace MaterialSupplyManagement.DAL;

public class MaterialSupplyRepository
{
	private readonly string _connectionString;
    private readonly ILogger _logger;

    public MaterialSupplyRepository(DBSettings settings, ILogger logger)
    {
        _connectionString = settings.ConnectionString;
        _logger = logger;
        GlobalConfiguration.Setup().UsePostgreSql();
        
        CreateDataStore().Wait();
    }

    public async Task CreateDataStore()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        
        await connection.ExecuteQueryAsync(SqlQueries.CreateInventoryItemTable + SqlQueries.CreateRawMaterialsTable);
    }
    
    public async Task<List<RawMaterial>> GetAllRawMaterials()
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            return (await connection.QueryAllAsync<RawMaterial>(SqlQueries.RawMaterialsTableName)).ToList();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"{ex}");
            throw;
        }
    }

    public async Task<RawMaterial?> GetRawMaterial(string name)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            return (await connection.QueryAsync<RawMaterial>(SqlQueries.RawMaterialsTableName, m => m.Name == name)).FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error fetching raw material with name {name}");
            throw;
        }
    }

    public async Task AddRawMaterial(RawMaterial rawMaterial)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.InsertAsync(SqlQueries.RawMaterialsTableName, rawMaterial);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error adding raw material {rawMaterial.GetInfoString()}");
            throw;
        }
    }

    public async Task UpdateRawMaterial(RawMaterial rawMaterial)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.UpdateAsync(SqlQueries.RawMaterialsTableName, rawMaterial, m => m.Name == rawMaterial.Name);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error updating raw material {rawMaterial.GetInfoString()}");
            throw;
        }
    }

    public async Task DeleteRawMaterial(RawMaterial rawMaterial)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.DeleteAsync(SqlQueries.RawMaterialsTableName, rawMaterial);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error deleting raw material with name {rawMaterial.GetInfoString()}");
            throw;
        }
    }

    public async Task<List<InventoryItem>> GetAllInventoryItems()
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            return (await connection.QueryAllAsync<InventoryItemDbRecord>(SqlQueries.InventoryItemTableName))
                .Select(DbRecordToInventoryItem).ToList();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"{ex}");
            throw;
        }
    }

    public async Task<InventoryItem?> GetInventoryItemByName(string name)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            return DbRecordToInventoryItem((await connection.QueryAsync<InventoryItemDbRecord>(SqlQueries.InventoryItemTableName,
                m => m.Name == name)).First());
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error fetching inventory item with name {name}");
            throw;
        }
    }

    public async Task AddInventoryItem(InventoryItem inventoryItem)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.InsertAsync(SqlQueries.InventoryItemTableName, InventoryItemToDbRecord(inventoryItem));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error adding inventory item {inventoryItem.GetInfoString()}");
            throw;
        }
    }

    public async Task UpdateInventoryItem(InventoryItem inventoryItem)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.UpdateAsync(SqlQueries.InventoryItemTableName, InventoryItemToDbRecord(inventoryItem), 
                e => e.Name == inventoryItem.Name);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error updating inventory item {inventoryItem.GetInfoString()}");
            throw;
        }
    }

    public async Task DeleteInventoryItem(InventoryItem inventoryItem)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            
            await connection.DeleteAsync(SqlQueries.InventoryItemTableName, InventoryItemToDbRecord(inventoryItem));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error deleting inventory item with name {inventoryItem.GetInfoString()}");
            throw;
        }
    }
    
    private InventoryItemDbRecord InventoryItemToDbRecord(InventoryItem inventoryItem)
    {
        return new InventoryItemDbRecord
        {
            Name = inventoryItem.Name,
            Type = inventoryItem.Type,
            Description = inventoryItem.Description,
            LastUpdate = inventoryItem.LastUpdate,
            Number = inventoryItem.Number,
            Price = inventoryItem.Price,
            TotalPrice = inventoryItem.TotalPrice,
            ManufacturerName = inventoryItem.Manufacturer.Name,
            ManufacturerAddress = inventoryItem.Manufacturer.Address
        };
    }
    
    private InventoryItem DbRecordToInventoryItem(InventoryItemDbRecord dbRecord)
    {
        return new InventoryItem(dbRecord.Name, dbRecord.Type, dbRecord.Description, dbRecord.Number, 
            dbRecord.Price, new Manufacturer(dbRecord.ManufacturerName, dbRecord.ManufacturerAddress));
    }
}