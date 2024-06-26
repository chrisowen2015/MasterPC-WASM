﻿namespace Shared.View_Models
{
    public class StorageVM
    {
        public string Id { get; set; } = null!;

        public string PCPId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImgUrl { get; set; }

        public string? Manufacturer { get; set; }

        public double? Price { get; set; }

        public string? Type { get; set; }

        public int? Capacity { get; set; }

        public double? PricePerGB { get; set; }

        public int? Cache { get; set; }

        public string? FormFactor { get; set; }

        public string? Interface { get; set; }

        public bool IsNVMe { get; set; }
    }
}
