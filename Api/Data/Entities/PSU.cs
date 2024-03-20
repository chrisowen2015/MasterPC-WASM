namespace Api.Data.Entities
{
    public partial class PSU
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Type { get; set; }

        public string? Efficiency { get; set; }

        public int? Wattage { get; set; }

        public string? Modular { get; set; }

        public string? Color { get; set; }
    }
}
