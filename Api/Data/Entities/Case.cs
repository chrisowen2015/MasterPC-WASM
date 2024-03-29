namespace Api.Data.Entities
{
    public partial class Case
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Type { get; set; }

        public string? Color { get; set; }

        public string? Psu { get; set; }

        public string? SidePanel { get; set; }

        public double? ExternalVolume { get; set; }

        public int? InternalBays { get; set; }
    }
}
