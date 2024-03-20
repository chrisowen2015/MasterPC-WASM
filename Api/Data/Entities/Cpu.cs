namespace Api.Data.Entities
{
    public partial class Cpu
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public string Socket { get; set; } = null!;

        public bool HasCooler { get; set; }

        public double? Price { get; set; }

        public int? CoreCount { get; set; }

        public double? CoreClock { get; set; }

        public double? BoostClock { get; set; }

        public int? Tdp { get; set; }

        public string? Graphics { get; set; }

        public bool? Smt { get; set; }
    }

}