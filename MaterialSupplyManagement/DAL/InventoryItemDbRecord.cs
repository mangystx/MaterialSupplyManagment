using System.ComponentModel.DataAnnotations.Schema;

namespace MaterialSupplyManagement.DAL;

public class InventoryItemDbRecord
{
    [Column(ColumnNames.Name)]
    public string Name { get; set; }
    
    [Column(ColumnNames.Type)]
    public string Type { get; set; }

    [Column(ColumnNames.Description)]
    public string Description { get; set; }
    
    [Column(ColumnNames.LastUpdate)]
    public DateTimeOffset LastUpdate { get; set; }
    
    [Column(ColumnNames.Number)]
    public int Number { get; set; }
    
    [Column(ColumnNames.Price)]
    public double Price { get; set; }
    
    [Column(ColumnNames.TotalPrice)]
    public double TotalPrice { get; set; }
    
    [Column(ColumnNames.ManufacturerName)]
    public string ManufacturerName { get; set; }
    
    [Column(ColumnNames.ManufacturerAddress)]
    public string ManufacturerAddress { get; set; }
}