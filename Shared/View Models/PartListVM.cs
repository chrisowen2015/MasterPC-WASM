namespace Shared.View_Models
{
    public class PartListVM
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public DateTime? UpdatedDate { get; set; }
        
        public CPUVM? CPU { get; set; }
        
        public CPUCoolerVM? CPUCooler { get; set; }
        
        public MotherboardVM? Motherboard { get; set; }
        
        public List<RAMVM> RAMs { get; set; } = new List<RAMVM>();
        
        public List<StorageVM> Storages { get; set; } = new List<StorageVM>();

        public GPUVM? GPU { get; set; }
        
        public CaseVM? Case { get; set; }
        
        public PSUVM? PSU { get; set; }
    }
}
