namespace Shared.View_Models
{
    public class GPUVM
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Chipset { get; set; }

        public double? Memory { get; set; }

        public int? CoreClock { get; set; }

        public int? BoostClock { get; set; }

        public string? Color { get; set; }

        public int? Length { get; set; }

        public int? Tdp { get; set; }
    }
}
