using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public class InventoryItem(string name, string type, string description, int number, double price, Manufacturer manufacturer) : Material(name, type, description)
{
	[Column(ColumnNames.Number)] public int Number { get; set; } = number;
	[Column(ColumnNames.Price)] public double Price { get; set; } = price;
	[Column(ColumnNames.TotalPrice)] public double TotalPrice { get; set; } = number * price;
	public Manufacturer Manufacturer { get; set; } = manufacturer;
	
	public override string GetInfoString() => 
		$"{Type}: {Name}, Manufacturer: {Manufacturer.Name}, Price per piece: {Price}$, Number: {Number}, Total price: {TotalPrice}$\n\nAdditional info:\n{Description}\n{Manufacturer.GetInfoString()}";
}