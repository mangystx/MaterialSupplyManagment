using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public class RawMaterial(string name, string type, string description, double pricePerKg, int weightTons) : Material(name, type, description)
{
	[Column(ColumnNames.PricePerKg)] public double PricePerKg { get; set; } = pricePerKg;
	[Column(ColumnNames.WeightTons)] public int WeightTons { get; set; } = weightTons;
	[Column(ColumnNames.TotalPrice)] public double TotalPrice { get; set; } = pricePerKg * weightTons * 1000;

	public override string GetInfoString() => 
		$"Raw Material: {Name}, Price per kilogram: {PricePerKg}$, Total weight in tons: {WeightTons}, Total price: {TotalPrice}$\n\nAdditional info:\n{Description}";
}