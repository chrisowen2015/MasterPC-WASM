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

    public class CPUCoolerJSONParserVM : CPUCoolerVM
    {
        public string? RPM { get; set; }

        public string? Noise { get; set; }

        public string Sockets { get; set; } = null!;
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

                if (psu.ImgUrl is not null && !psu.ImgUrl.Contains("amazon.com"))
                {
                    psu.ImgUrl = null;
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

                if (motherboard.ImgUrl is not null && !motherboard.ImgUrl.Contains("amazon.com"))
                {
                    motherboard.ImgUrl = null;
                }
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

            List<RAMVM> parsedMemories = JsonConvert.DeserializeObject<List<RAMVM>>(jsonContent);

            foreach (RAMVM memory in parsedMemories)
            {
                memory.Manufacturer = GetMemoryManufacturer(memory.Name);
                memory.RGB = memory.Name.ToUpper().Contains("RGB") ? true : false;

                if (memory.ImgUrl is not null && !memory.ImgUrl.Contains("amazon.com"))
                {
                    memory.ImgUrl = null;
                }

                if (memory.Price is not null && memory.Capacity is not null)
                {
                    memory.PricePerGB = memory.Price / memory.Capacity;
                }

                RAMVMs.Add(memory);
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

                if (cpu.ImgUrl is not null && !cpu.ImgUrl.Contains("amazon.com"))
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
                }
                else
                {
                    caseVM.Manufacturer = manufacturer;
                }

                if (caseVM.ImgUrl is not null && !caseVM.ImgUrl.Contains("amazon.com"))
                {
                    caseVM.ImgUrl = null;
                }

                if (caseVM.Psu == "None")
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
                }
                else
                {
                    storageVM.Manufacturer = manufacturer;
                }

                if (storageVM.ImgUrl is not null && !storageVM.ImgUrl.Contains("amazon.com"))
                {
                    storageVM.ImgUrl = null;
                }

                if (storageVM.Price is not null && storageVM.Capacity is not null)
                {
                    storageVM.PricePerGB = storageVM.Price / storageVM.Capacity;
                }

                storageVMs.Add(storageVM);
            }

            return storageVMs;
        }


        public static async Task<List<CPUCoolerVM>> HandleCPUCoolerUpload(InputFileChangeEventArgs e)
        {
            List<CPUCoolerVM> cpuCoolerVMs = new List<CPUCoolerVM>();
            var file = e.File;
            var buffer = new byte[file.Size];

            await file.OpenReadStream().ReadAsync(buffer);

            var jsonContent = System.Text.Encoding.UTF8.GetString(buffer);

            List<CPUCoolerJSONParserVM> cPUCoolerJSONParserVMs = JsonConvert.DeserializeObject<List<CPUCoolerJSONParserVM>>(jsonContent);

            foreach (CPUCoolerJSONParserVM coolerJSONParserVM in cPUCoolerJSONParserVMs)
            {
                CPUCoolerVM cPUCoolerVM = new CPUCoolerVM
                {
                    Name = coolerJSONParserVM.Name,
                    PCPId = coolerJSONParserVM.PCPId,
                    ImgUrl = coolerJSONParserVM.ImgUrl,
                    Color = coolerJSONParserVM.Color,
                    Price = coolerJSONParserVM.Price,
                    RadiatorSize = coolerJSONParserVM.RadiatorSize,
                };

                if (coolerJSONParserVM.RPM is not null && !String.IsNullOrWhiteSpace(coolerJSONParserVM.RPM))
                {
                    if (!coolerJSONParserVM.RPM.Contains('-'))
                    {
                        cPUCoolerVM.MinRPM = int.Parse(coolerJSONParserVM.RPM);
                    }
                    else
                    {
                        string[] RPMs = coolerJSONParserVM.RPM.Split('-');
                        cPUCoolerVM.MinRPM = int.Parse(RPMs[0]);
                        cPUCoolerVM.MaxRPM = int.Parse(RPMs[1]);
                    }
                }

                if (coolerJSONParserVM.Noise is not null && !String.IsNullOrWhiteSpace(coolerJSONParserVM.Noise))
                {
                    if (!coolerJSONParserVM.Noise.Contains('-'))
                    {
                        cPUCoolerVM.MinNoise = double.Parse(coolerJSONParserVM.Noise);
                    }
                    else
                    {
                        string[] noises = coolerJSONParserVM.Noise.Split('-');
                        cPUCoolerVM.MinNoise = double.Parse(noises[0]);
                        cPUCoolerVM.MaxNoise = double.Parse(noises[1]);
                    }
                }

                if (coolerJSONParserVM.Sockets is not null && coolerJSONParserVM.Sockets.Contains(','))
                {
                    cPUCoolerVM.CompatibleSockets = coolerJSONParserVM.Sockets.Split(',').ToList();
                }
                else
                {
                    cPUCoolerVM.CompatibleSockets = new List<string>();
                }


                string manufacturer = cPUCoolerVM.Name.Substring(0, cPUCoolerVM.Name.IndexOf(' '));

                if (DataInjestionConstants.cpuCoolerManufacturers.Any(x => x.manufacturerShortName == manufacturer))
                {
                    cPUCoolerVM.Manufacturer = DataInjestionConstants.cpuCoolerManufacturers.First(x => x.manufacturerShortName == manufacturer).manufacturerFullName;
                }
                else
                {
                    cPUCoolerVM.Manufacturer = manufacturer;
                }

                if (cPUCoolerVM.ImgUrl is not null && !cPUCoolerVM.ImgUrl.Contains("amazon.com"))
                {
                    cPUCoolerVM.ImgUrl = null;
                }

                cpuCoolerVMs.Add(cPUCoolerVM);
            }

            return cpuCoolerVMs;
        }
    }
}
