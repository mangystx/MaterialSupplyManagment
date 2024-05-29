using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public class InventoryItem : Material
{
    private int _number;
    private double _price;
    private Manufacturer _manufacturer;
    
    [Column(ColumnNames.Number)]
    public int Number
    {
        get => _number; 
        set
        {
            _number = value;
            UpdateLastModified();
            UpdateTotalPrice();
        }
    }

    [Column(ColumnNames.Price)]
    public double Price
    {
        get => _price;
        set
        {
            _price = value;
            UpdateLastModified();
            UpdateTotalPrice();
        }
    }

    [Column(ColumnNames.TotalPrice)]
    public double TotalPrice { get; set; }

    public Manufacturer Manufacturer
    {
        get => _manufacturer; 
        set
        {
            _manufacturer = value;
            UpdateLastModified();
            UpdateTotalPrice();
        }
    }
    
    public InventoryItem()
    {
        UpdateLastModified();
        UpdateTotalPrice();
    }

    public InventoryItem(string name, string type, string description, int number, double price, Manufacturer manufacturer)
        : base(name, type, description)
    {
        Number = number;
        Price = price;
        TotalPrice = number * price;
        Manufacturer = manufacturer;
        LastUpdate = DateTimeOffset.UtcNow;
    }
    
    private void UpdateTotalPrice()
    {
        TotalPrice = Number * Price;
    }

    public override string GetInfoString() =>
        $"{Type}: {Name}, Manufacturer: {Manufacturer.Name}, Price per piece: {Price}$, Number: {Number}, Total price: {TotalPrice}$\n\nAdditional info:\n{Description}\n{Manufacturer.GetInfoString()}";
}