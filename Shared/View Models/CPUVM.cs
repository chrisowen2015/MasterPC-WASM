﻿namespace Shared.View_Models
{
    public class CPUVM
    {
        public string? Id { get; set; }

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

        public int? TDP { get; set; }

        public string? Graphics { get; set; }

        public bool? SMT { get; set; }
    }
}
