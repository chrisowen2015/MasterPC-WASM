namespace Api.Data.Entities
{
    public partial class Motherboard
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Socket { get; set; }

        public string? FormFactor { get; set; }

        public string? Chipset { get; set; }

        public string? IntegratedWifi { get; set; }

        public int? MaxMemory { get; set; }

        public int? MemorySlots { get; set; }

        public string? Color { get; set; }

        public bool? M2Slot { get; set; }
    }
}
