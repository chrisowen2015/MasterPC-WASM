namespace Api.Data.Entities
{
    public partial class CpuCooler
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public int? MinRPM { get; set; }

        public int? MaxRPM { get; set; }

        public double? MinNoise { get; set; }

        public double? MaxNoise { get; set;}

        public string? Color { get; set; }

        public int? RadiatorSize { get; set; }

        public string CompatibleSockets { get; set; } = null!;

        public double? Price { get; set; }
    }
}
