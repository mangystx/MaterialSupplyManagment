using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public class Manufacturer : IInfoProvider
{
	public Manufacturer() { }
	
	public Manufacturer(string name, string address)
	{
		Name = name;
		Address = address;
	}
	
	[Column(ColumnNames.ManufacturerName)] 
	[Required(ErrorMessage = "Manufacturer name is required")]
	public string Name { get; set; }
	
	[Column(ColumnNames.ManufacturerAddress)]
	[Required(ErrorMessage = "Manufacturer address is required")]
	public string Address { get; set; }

	public string GetInfoString() => $"Manufacturer: {Name}, Address: {Address}";
}