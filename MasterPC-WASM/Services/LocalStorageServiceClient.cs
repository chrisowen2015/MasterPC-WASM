using Shared.View_Models;
using Blazored.LocalStorage;

namespace MasterPC_WASM.Services
{
    public interface ILocalStorageServiceClient
    {
        public Task<PartListVM> GetPartListAsync();
        public Task<bool> SetPartListAsync(PartListVM partListVM);
        public Task<bool> AddCaseAsync(CaseVM caseVM);
        public Task<bool> AddCPUAsync(CPUVM cpu);
        public Task<bool> AddCPUCoolerAsync(CPUCoolerVM cpuCooler);
        public Task<bool> AddGPUAsync(GPUVM gpu);
        public Task<bool> AddMotherboardAsync(MotherboardVM motherboard);
        public Task<bool> AddPowerSupplyAsync(PSUVM psu);
        public Task<bool> AddRAMAsync(RAMVM ram);
        public Task<bool> AddStorageAsync(StorageVM storage);
    }
    public class LocalStorageServiceClient(ILocalStorageService localStorage) : ILocalStorageServiceClient
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        public async Task<PartListVM> GetPartListAsync()
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is null)
                {
                    return new PartListVM();
                }
                else
                {
                    return partListVM;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return new PartListVM();
            }
        }

        public async Task<bool> SetPartListAsync(PartListVM partListVM)
        {
            try
            {
                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddCaseAsync(CaseVM caseVM)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.Case = caseVM;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        Case = caseVM
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> AddCPUAsync(CPUVM cpu)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.CPU = cpu;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        CPU = cpu
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddCPUCoolerAsync(CPUCoolerVM cpuCooler)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.CPUCooler = cpuCooler;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        CPUCooler = cpuCooler
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddGPUAsync(GPUVM gpu)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.GPU = gpu;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        GPU = gpu
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddMotherboardAsync(MotherboardVM motherboard)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.Motherboard = motherboard;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        Motherboard = motherboard
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddPowerSupplyAsync(PSUVM psu)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.PSU = psu;
                }
                else
                {
                    partListVM = new PartListVM
                    {
                        PSU = psu
                    };
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddRAMAsync(RAMVM ram)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.RAMs.Add(ram);
                }
                else
                {
                    partListVM = new PartListVM();
                    partListVM.RAMs.Add(ram);
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }

        public async Task<bool> AddStorageAsync(StorageVM storage)
        {
            try
            {
                PartListVM? partListVM = await _localStorage.GetItemAsync<PartListVM>("PartList");

                if (partListVM is not null)
                {
                    partListVM.Storages.Add(storage);
                }
                else
                {
                    partListVM = new PartListVM();
                    partListVM.Storages.Add(storage);
                }

                await _localStorage.SetItemAsync("PartList", partListVM);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
        }
    }
}
