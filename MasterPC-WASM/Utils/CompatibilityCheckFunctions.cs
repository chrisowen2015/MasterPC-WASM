using Shared.View_Models;

namespace MasterPC_WASM.Utils
{
    public static class CompatibilityCheckFunctions
    {
        public static async Task<List<string>> GetCompatibilityMessagesAsync(PartListVM partListVM)
        {
            List<string> results = new List<string>();

            // Check Case Compatibility

            if (partListVM.Case is not null)
            {
                if (partListVM.Motherboard is not null)
                {
                    Console.WriteLine("Case Type: " + partListVM.Case.Type + ", Motherboard Type: " + partListVM.Motherboard.FormFactor);

                    switch (partListVM.Motherboard.FormFactor)
                    {
                        case "EATX":
                            if (partListVM.Case.Type != "ATX" && partListVM.Case.Type != "EATX")
                            {
                                results.Add("The Case and Motherboard selected are not compatible.");
                            }
                            break;
                        case "ATX":
                            if (partListVM.Case.Type != "ATX" && partListVM.Case.Type != "EATX")
                            {
                                results.Add("The Case and Motherboard selected are not compatible.");
                            }
                            break;
                        case "Micro ATX":
                            if (partListVM.Case.Type != "Micro ATX")
                            {
                                results.Add("The Case and Motherboard selected are not compatible.");
                            }
                            break;
                        case "Mini ITX":
                            if (partListVM.Case.Type != "Mini ITX")
                            {
                                results.Add("The Case and Motherboard selected are not compatible.");
                            }
                            break;
                        default:
                            break;
                    }


                    if (partListVM.Case.Type != partListVM.Motherboard.FormFactor)
                    {
                        results.Add("The Case and Motherboard selected are not compatible.");
                    }
                }

                if (partListVM.PSU is not null)
                {
                    Console.WriteLine("Case Type: " + partListVM.Case.Type + ", PSU Type: " + partListVM.PSU.Type);

                    if (partListVM.Case.Type != partListVM.PSU.Type)
                    {
                        results.Add("The Case and PSU selected are not compatible.");
                    }
                }

                if (partListVM.Storages.Count > 0)
                {
                    Console.WriteLine("Case Type: " + partListVM.Case.Type + ", checking Storage...");

                    if (partListVM.Case.InternalBays < partListVM.Storages.Select(storage => storage.Type.Contains("RPM")).Count())
                    {
                        results.Add("The Case selected does not have enough space for the Storage selected.");
                    }
                }
            }

            // Check CPU Compatibility

            if (partListVM.CPU is not null)
            {
                if (partListVM.Motherboard is not null)
                {
                    Console.WriteLine("CPU Socket: " + partListVM.CPU.Socket + ", Motherboard Socket: " + partListVM.Motherboard.Socket);

                    if (partListVM.CPU.Socket != partListVM.Motherboard.Socket)
                    {
                        results.Add("The CPU and Motherboard selected are not compatible.");
                    }
                }

                if (partListVM.CPUCooler is not null)
                {
                    Console.WriteLine("CPU Socket: " + partListVM.CPU.Socket + ", checking CPU Coolers...");

                    if (!partListVM.CPUCooler.CompatibleSockets.Contains(partListVM.CPU.Socket))
                    {
                        results.Add("The CPU and CPU Cooler selected are not compatible.");
                    }
                }
            }

            // Check Motherboard Compatibility

            if (partListVM.Motherboard is not null)
            {
                if (partListVM.RAMs.Count > 0)
                {
                    // Check Motherboard Memory Type programmatically based on Motherboard Chipset

                    string ramType = partListVM.RAMs[0].Type!;
                    bool motherboardRAMCompatible = true;

                    switch (partListVM.Motherboard.Chipset)
                    {
                        case "B450":
                            motherboardRAMCompatible = ramType == "DDR4";
                            break;
                        default:
                            break;
                    }

                    if (motherboardRAMCompatible == false)
                    {
                        results.Add("The Motherboard and RAM selected are not compatible.");
                    }

                    // Check Motherboard Max Memory Slots and RAM Count

                    int totalRAMModules = 0;

                    foreach (RAMVM ram in partListVM.RAMs)
                    {
                        totalRAMModules += (int)ram.NumModules;
                    }

                    if (totalRAMModules > partListVM.Motherboard.MemorySlots)
                    {
                        results.Add("The Motherboard does not have enough memory slots for the RAM selected.");
                    }

                    // Check Motherboard Max Memory Capacity and RAM Capacity

                    int totalRAMCapacity = 0;

                    foreach (RAMVM ram in partListVM.RAMs)
                    {
                        totalRAMCapacity += (int)ram.Capacity;
                    }

                    if (totalRAMCapacity > partListVM.Motherboard.MaxMemory)
                    {
                        results.Add("The Motherboard does not have enough total memory capacity for the RAM selected.");
                    }

                    // Check if Motherboard has M.2 Slots

                    if (partListVM.Motherboard.M2Slot is not null && (bool)partListVM.Motherboard.M2Slot)
                    {
                        // Can't do any check for number of slots as we only capture a boolean value for M.2 Slot
                    }
                    else
                    {
                        if (partListVM.Storages.Count > 0 && partListVM.Storages.Select(s => s.IsNVMe == true).Count() > 0)
                        {
                            results.Add("The Motherboard does not have any M.2 slots for the Storage selected.");
                        }
                    }
                }
            }

            // Check RAM Compatibility

            if (partListVM.RAMs.Count > 0)
            {
                string? expectedRAMType = partListVM.RAMs[0].Type;

                foreach (RAMVM ram in partListVM.RAMs)
                {
                    if (ram.Type != expectedRAMType)
                    {
                        results.Add("The RAM selected are not all of the same type.");
                        break;
                    }
                }

                // Debating whether to check RAM Speed and CAS Latency
            }

            //  TODO: Check Power Supply Compatibility

            return results;
        }

        public static async Task CheckPowerCompatibilityAsync(PartListVM partListVM)
        {

        }

        public static async Task<List<string>> CheckCaseCompatibilityAsync(PartListVM partListVM)
        {



            throw new NotImplementedException();
        }

        public static async Task<List<string>> CheckCPUCompatibilityAsync(PartListVM partListVM)
        {
            throw new NotImplementedException();
        }

        public static async Task<List<string>> CheckGPUPowerCompatibilityAsync(PartListVM partListVM)
        {
            throw new NotImplementedException();
        }

        public static async Task<List<string>> CheckPSUCompatibilityAsync(PartListVM partListVM)
        {
            throw new NotImplementedException();
        }

        public static async Task<List<string>> CheckStorageCompatibilityAsync(PartListVM partListVM)
        {
            throw new NotImplementedException();
        }
    }
}
