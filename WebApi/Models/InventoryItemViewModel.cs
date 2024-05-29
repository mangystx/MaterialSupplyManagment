using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class InventoryItemViewModel
{
	[Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Number is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
    public int Number { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Manufacturer name is required")]
    public string ManufacturerName { get; set; }

    [Required(ErrorMessage = "Manufacturer address is required")]
    public string ManufacturerAddress { get; set; }
}