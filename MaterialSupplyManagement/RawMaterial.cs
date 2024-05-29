using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement
{
    public class RawMaterial : Material
    {
        private double _pricePerKg;
        private int _weightTons;

        [Column(ColumnNames.TotalPrice)]
        public double TotalPrice { get; set; }

        [Column(ColumnNames.PricePerKg)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price per kilogram")]
        [Required(ErrorMessage = "Price per kilogram is required")]
        public double PricePerKg
        {
            get => _pricePerKg;
            set
            {
                _pricePerKg = value;
                UpdateLastModified();
                UpdateTotalPrice();
            }
        }

        [Column(ColumnNames.WeightTons)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid weight in tons")]
        [Required(ErrorMessage = "Weight in tons is required")]
        public int WeightTons
        {
            get => _weightTons;
            set
            {
                _weightTons = value;
                UpdateLastModified();
                UpdateTotalPrice();
            }
        }

        public RawMaterial()
        {
            UpdateLastModified();
            UpdateTotalPrice();
        }

        public RawMaterial(string name, string type, string description, double pricePerKg, int weightTons)
            : base(name, type, description)
        {
            PricePerKg = pricePerKg;
            WeightTons = weightTons;

            UpdateLastModified();
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = PricePerKg * WeightTons * 1000;
        }

        public override string GetInfoString() =>
            $"Raw Material: {Name}, Price per kilogram: {PricePerKg}$, Total weight in tons: {WeightTons}, Total price: {TotalPrice}$\n\nAdditional info:\n{Description}";
    }
}