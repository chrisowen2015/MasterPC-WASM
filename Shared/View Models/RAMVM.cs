namespace Shared.View_Models
{
    public class RAMVM
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Type { get; set; }

        public int? Speed { get; set; }

        public int? NumModules { get; set; }

        public int? Capacity { get; set; }

        public double? PricePerGB { get; set; }

        public string? Color { get; set; }

        public bool? RGB { get; set; }

        public double? FirstWordLatency { get; set; }

        public int? CASLatency { get; set; }

        public bool? HeatSpreader { get; set; }
    }
}
