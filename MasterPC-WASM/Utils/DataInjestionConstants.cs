namespace MasterPC_WASM.Utils
{
    public class PowerSupplyManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }
    public class GPUManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }
    public class MemoryManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }
    public class CaseManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }
    public class StorageManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }

    public class CPUCoolerManufacturers
    {
        public string manufacturerShortName { get; set; } = null!;
        public string manufacturerFullName { get; set; } = null!;
    }
    public class DataInjestionConstants
    {
        public static PowerSupplyManufacturers[] powerSupplyManufacturers =>
            [
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Athena",
                    manufacturerFullName = "Athena Power"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "be",
                    manufacturerFullName = "be quiet!"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Broadway",
                    manufacturerFullName = "Broadway Com Corp"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Cooler",
                    manufacturerFullName = "Cooler Master"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Fractal",
                    manufacturerFullName = "Fractal Design"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "FSP",
                    manufacturerFullName = "FSP Group"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "In",
                    manufacturerFullName = "In Win"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Lian",
                    manufacturerFullName = "Lian Li"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Mars",
                    manufacturerFullName = "Mars Gaming"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "PC",
                    manufacturerFullName = "PC Power & Cooling"
                },
                new PowerSupplyManufacturers
                {
                    manufacturerShortName = "Replace",
                    manufacturerFullName = "Replace Power"
                },
            ];

        public static GPUManufacturers[] gpuManufacturers =>
            [
                new GPUManufacturers
                {
                    manufacturerShortName = "Club",
                    manufacturerFullName = "Club 3D"
                },
                new GPUManufacturers
                {
                    manufacturerShortName = "SONNET",
                    manufacturerFullName = "SONNET Technologies"
                },
            ];

        public static string[] amdMotherboardChipsets = [
        "690G",
            "740G",
            "760G",
            "770",
            "780G",
            "780L",
            "785G",
            "790FX",
            "790GX",
            "790X",
            "870",
            "880G",
            "880GX",
            "890FX",
            "890GX",
            "970",
            "990FX",
            "990X",
            "A320",
            "A520",
            "A55",
            "A58",
            "A620",
            "A68H",
            "A68M",
            "A70M",
            "A75",
            "A78",
            "A85X",
            "A88X",
            "AM1",
            "B350",
            "B450",
            "B550",
            "B650",
            "B650E",
            "Hudson D1",
            "Hudson M1",
            "SR5690",
            "TRX40",
            "X370",
            "X399",
            "X470",
            "X570",
            "X670",
            "X670E",
        ];

        public static string[] intelMotherboardChipsets = [
            "5520",
            "B150",
            "B250",
            "B360",
            "B365",
            "B43",
            "B460",
            "B560",
            "B65",
            "B660",
            "B75",
            "B760",
            "B85",
            "C204",
            "C206",
            "C222",
            "C224",
            "C226",
            "C232",
            "C236",
            "C242",
            "C246",
            "C602",
            "C602J",
            "C606",
            "C612",
            "G31",
            "G33",
            "G41",
            "G43",
            "G45",
            "H110",
            "H170",
            "H270",
            "H310",
            "H370",
            "H410",
            "H470",
            "H510",
            "H55",
            "H57",
            "H570",
            "H61",
            "H610",
            "H67",
            "H670",
            "H77",
            "H770",
            "H81",
            "H87",
            "H97",
            "ICH8M",
            "NM10",
            "NM70",
            "P35",
            "P43",
            "P45",
            "P55",
            "P67",
            "Q170",
            "Q270",
            "Q370",
            "Q45",
            "Q470",
            "Q57",
            "Q570",
            "Q67",
            "Q670",
            "Q77",
            "Q87",
            "X299",
            "X58",
            "X79",
            "X99",
            "Z170",
            "Z270",
            "Z370",
            "Z390",
            "Z490",
            "Z590",
            "Z68",
            "Z690",
            "Z75",
            "Z77",
            "Z790",
            "Z87",
            "Z97",
        ];
        public static MemoryManufacturers[] memoryManufacturers =>
            [
                new MemoryManufacturers
                {
                    manufacturerShortName = "Neo",
                    manufacturerFullName = "Neo Forza"
                },
                new MemoryManufacturers
                {
                    manufacturerShortName = "Silicon",
                    manufacturerFullName = "Silicon Power"
                },
                new MemoryManufacturers
                {
                    manufacturerShortName = "SK",
                    manufacturerFullName = "SK Hynix"
                },
                new MemoryManufacturers
                {
                    manufacturerShortName = "Super",
                    manufacturerFullName = "Super Talent"
                },
            ];
        public static CaseManufacturers[] caseManufacturers =>
            [
                new CaseManufacturers
                {
                    manufacturerShortName = "Athena",
                    manufacturerFullName = "Athena Power"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "be",
                    manufacturerFullName = "be quiet!"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Broadway",
                    manufacturerFullName = "Broadway Com Corp"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Cooler",
                    manufacturerFullName = "Cooler Master"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "DAN",
                    manufacturerFullName = "DAN Cases"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Deco",
                    manufacturerFullName = "Deco Gear"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Element",
                    manufacturerFullName = "Element Gaming"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Empire",
                    manufacturerFullName = "Empire Gaming"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Fractal",
                    manufacturerFullName = "Fractal Design"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "FSP",
                    manufacturerFullName = "FSP Group"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Geometric",
                    manufacturerFullName = "Geometric Future"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Golden",
                    manufacturerFullName = "Golden Field"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "HG",
                    manufacturerFullName = "HG Computers"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "In",
                    manufacturerFullName = "In Win"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Lian",
                    manufacturerFullName = "Lian Li"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Mars",
                    manufacturerFullName = "Mars Gaming"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "Parvum",
                    manufacturerFullName = "Parvum Systems"
                },
                new CaseManufacturers
                {
                    manufacturerShortName = "teenage",
                    manufacturerFullName = "teenage engineering"
                }
            ];
        public static StorageManufacturers[] storageManufacturers =>
            [
                new StorageManufacturers
                {
                    manufacturerShortName = "AITC",
                    manufacturerFullName = "AITC Kingsman"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Edge",
                    manufacturerFullName = "Edge Tech"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Fantom",
                    manufacturerFullName = "Fantom Drives"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Hyundai",
                    manufacturerFullName = "Hyundai Technology"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "NEMIX",
                    manufacturerFullName = "NEMIX RAM"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Neo",
                    manufacturerFullName = "Neo Forza"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Oyen",
                    manufacturerFullName = "Oyen Digital"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Silicon",
                    manufacturerFullName = "Silicon Power"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "SK",
                    manufacturerFullName = "SK Hynix"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Super",
                    manufacturerFullName = "Super Talent"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Titanium",
                    manufacturerFullName = "Titanium Micro"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Water",
                    manufacturerFullName = "Water Panther"
                },
                new StorageManufacturers
                {
                    manufacturerShortName = "Western",
                    manufacturerFullName = "Western Digital"
                },
            ];

        public static CPUCoolerManufacturers[] cpuCoolerManufacturers =>
            [
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Cooler",
                    manufacturerFullName = "Cooler Master"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "be",
                    manufacturerFullName = "be quiet!"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Fractal",
                    manufacturerFullName = "Fractal Design"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "FSP",
                    manufacturerFullName = "FSP Group"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Gelid",
                    manufacturerFullName = "Gelid Solutions"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Geometric",
                    manufacturerFullName = "Geometric Future"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Iceberg",
                    manufacturerFullName = "Iceberg Thermal"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "In",
                    manufacturerFullName = "In Win"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Lian",
                    manufacturerFullName = "Lian Li"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Mars",
                    manufacturerFullName = "Mars Gaming"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "PC",
                    manufacturerFullName = "PC Cooler"
                },
                new CPUCoolerManufacturers
                {
                    manufacturerShortName = "Zero",
                    manufacturerFullName = "Zero Infinity"
                }
            ];
    }
}
