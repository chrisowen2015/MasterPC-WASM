using Newtonsoft.Json;
using Shared.View_Models;
using Microsoft.AspNetCore.Components.Forms;

namespace MasterPC_WASM.Utils
{
    public class MemoryJSONParserVM
    {
        public string Name { get; set; } = null!;

        public double? Price { get; set; }

        public int[]? Speed { get; set; }

        public int[]? Modules { get; set; }

        public double? PricePerGB { get; set; }

        public string? Color { get; set; }

        public double? FirstWordLatency { get; set; }

        public int? CASLatency { get; set; }
    }
    public class FileUploadFunctions
    {
        public static async Task<List<PSUVM>> HandlePSUUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<PSUVM> psuList = JsonConvert.DeserializeObject<List<PSUVM>>(jsonContent);

            foreach (PSUVM psu in psuList)
            {
                string manufacturer = psu.Name.Substring(0, psu.Name.IndexOf(' '));

                if (DataInjestionConstants.powerSupplyManufacturers.Any(x => x.manufacturerShortName == manufacturer))
                {
                    manufacturer = DataInjestionConstants.powerSupplyManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
                }

                psu.Manufacturer = manufacturer;
            }

            return psuList;
        }

        public static async Task<List<GPUVM>> HandleGPUUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<GPUVM> gpuList = JsonConvert.DeserializeObject<List<GPUVM>>(jsonContent);

            foreach (GPUVM gpu in gpuList)
            {
                string manufacturer = gpu.Name.Substring(0, gpu.Name.IndexOf(' '));

                if (DataInjestionConstants.gpuManufacturers.Any(x => x.manufacturerShortName == manufacturer))
                {
                    manufacturer = DataInjestionConstants.gpuManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
                }
                gpu.Manufacturer = manufacturer;

                if (gpu.ImgUrl is not null && !gpu.ImgUrl.Contains("amazon.com"))
                {
                    gpu.ImgUrl = null;
                }
            }

            return gpuList;
        }

        private static string GetMotherboardManufacturer(String model)
        {
            string manufacturer = model.Substring(0, model.IndexOf(' '));

            return manufacturer;
        }

        private static string GetMotherboardChipset(String model)
        {
            string chipset = "";

            foreach (string intelChipset in DataInjestionConstants.intelMotherboardChipsets)
            {
                if (model.Contains(intelChipset))
                {
                    chipset = "Intel " + intelChipset;
                    return chipset;
                }
            }

            foreach (string amdChipset in DataInjestionConstants.amdMotherboardChipsets)
            {
                if (model.Contains(amdChipset))
                {
                    chipset = "AMD " + amdChipset;
                    return chipset;
                }
            }

            if (model.Contains("9300"))
            {
                chipset = "NVIDIA GeForce 9300";
            }
            else if (model.Contains("8200"))
            {
                chipset = "NVIDIA GeForce 8200";
            }
            else if (model.Contains("7050"))
            {
                chipset = "NVIDIA GeForce 7050";
            }
            else if (model.Contains("7025"))
            {
                chipset = "NVIDIA GeForce 7025";
            }
            else if (model.Contains("6100"))
            {
                chipset = "NVIDIA GeForce 6100";
            }
            else if (model.Contains("6150SE"))
            {
                chipset = "NVIDIA GeForce 6150SE";
            }
            else if (model.Contains("6150"))
            {
                chipset = "NVIDIA GeForce 6150";
            }

            if (chipset == "")
            {
                if (model.Contains("430"))
                {
                    chipset = "NVIDIA nForce 430 MCP";
                }
                else if (model.Contains("590"))
                {
                    chipset = "NVIDIA nForce 590 SLI MCP";
                }
                else if (model.Contains("720"))
                {
                    chipset = "NVIDIA nForce 720D";
                }
                else if (model.Contains("750a"))
                {
                    chipset = "NVIDIA nForce 750a";
                }
                else if (model.Contains("750i"))
                {
                    chipset = "NVIDIA nForce 750i";
                }
                else if (model.Contains("980"))
                {
                    chipset = "NVIDIA nForce 980a SLI";
                }
                else if (model.Contains("ION"))
                {
                    chipset = "NVIDIA ION";
                }
            }

            if (chipset == "")
            {
                chipset = null;
            }

            return chipset;
        }

        public static async Task<List<MotherboardVM>> HandleMotherboardUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<MotherboardVM> motherboardList = JsonConvert.DeserializeObject<List<MotherboardVM>>(jsonContent);

            foreach (MotherboardVM motherboard in motherboardList)
            {
                motherboard.Manufacturer = GetMotherboardManufacturer(motherboard.Name);
                motherboard.Chipset = GetMotherboardChipset(motherboard.Name);
            }

            return motherboardList;
        }
        private static string GetMemoryManufacturer(String model)
        {
            string manufacturer = model.Substring(0, model.IndexOf(' '));

            if (DataInjestionConstants.memoryManufacturers.Any(x => x.manufacturerShortName == manufacturer))
            {
                manufacturer = DataInjestionConstants.memoryManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
            }

            return manufacturer;
        }
        public static async Task<List<RAMVM>> HandleRAMUpload(InputFileChangeEventArgs e)
        {
            List<RAMVM> RAMVMs = new List<RAMVM>();
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<MemoryJSONParserVM> parsedMemories = JsonConvert.DeserializeObject<List<MemoryJSONParserVM>>(jsonContent);

            foreach (MemoryJSONParserVM memory in parsedMemories)
            {
                RAMVM newMemory = new RAMVM
                {
                    Name = memory.Name,
                    Price = memory.Price,
                    PricePerGB = memory.PricePerGB,
                    Color = memory.Color,
                    FirstWordLatency = memory.FirstWordLatency,
                    CASLatency = memory.CASLatency
                };
                newMemory.Manufacturer = GetMemoryManufacturer(memory.Name);
                newMemory.NumModules = memory.Modules[0];
                newMemory.Capacity = memory.Modules[1];
                newMemory.Type = "DDR" + memory.Speed[0];
                newMemory.Speed = memory.Speed[1];
                newMemory.RGB = memory.Name.ToUpper().Contains("RGB") ? true : false;

                RAMVMs.Add(newMemory);
            }
            return RAMVMs;
        }

        public static async Task<List<CPUVM>> HandleCPUUpload(InputFileChangeEventArgs e)
        {
            List<CPUVM> cPUVMs = new List<CPUVM>();
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<CPUVM> cpuList = JsonConvert.DeserializeObject<List<CPUVM>>(jsonContent);
            foreach (CPUVM cpu in cpuList)
            {
                if (cpu.Name.ToUpper().Contains("AMD"))
                {
                    cpu.Manufacturer = "AMD";
                }
                else
                {
                    cpu.Manufacturer = "Intel";
                }

                if(cpu.ImgUrl is not null && !cpu.ImgUrl.Contains("amazon.com"))
                {
                    cpu.ImgUrl = null;
                }

                cPUVMs.Add(cpu);

            }
            return cPUVMs;
        }

        public static async Task<List<CaseVM>> HandleCaseUpload(InputFileChangeEventArgs e)
        {
            List<CaseVM> caseVMs = new List<CaseVM>();
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<CaseVM> caseList = JsonConvert.DeserializeObject<List<CaseVM>>(jsonContent);

            foreach (CaseVM caseVM in caseList)
            {
                string manufacturer = caseVM.Name.Substring(0, caseVM.Name.IndexOf(' '));

                if (DataInjestionConstants.caseManufacturers.Any(x => x.manufacturerShortName == manufacturer))
                {
                    caseVM.Manufacturer = DataInjestionConstants.caseManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
                } else
                {
                    caseVM.Manufacturer = manufacturer;
                }

                if (caseVM.ImgUrl is not null && !caseVM.ImgUrl.Contains("amazon.com"))
                {
                    caseVM.ImgUrl = null;
                }

                if(caseVM.Psu == "None")
                {
                    caseVM.Psu = null;
                }

                caseVMs.Add(caseVM);
            }

            return caseVMs;
        }
        public static async Task<List<StorageVM>> HandleStorageUpload(InputFileChangeEventArgs e)
        {
            List<StorageVM> storageVMs = new List<StorageVM>();
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<StorageVM> storageVMList = JsonConvert.DeserializeObject<List<StorageVM>>(jsonContent);

            foreach (StorageVM storageVM in storageVMList)
            {
                string manufacturer = storageVM.Name.Substring(0, storageVM.Name.IndexOf(' '));

                if (DataInjestionConstants.storageManufacturers.Any(x => x.manufacturerShortName == manufacturer))
                {
                    storageVM.Manufacturer = DataInjestionConstants.storageManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
                } else
                {
                    storageVM.Manufacturer = manufacturer;
                }

                storageVMs.Add(storageVM);
            }

            return storageVMs;
        }
    }
}
