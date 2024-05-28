using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public class Manufacturer(string name, string address) : IInfoProvider
{
	[Column(ColumnNames.ManufacturerName)] public string Name { get; private set; } = name;
	[Column(ColumnNames.ManufacturerAddress)] public string Address { get; private set; } = address;

	public string GetInfoString() => $"Manufacturer: {Name}, Address: {Address}";
}