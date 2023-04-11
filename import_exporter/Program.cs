using System;
using System.IO;
using UAssetAPI;
using UAssetAPI.UnrealTypes;

namespace import_exporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string engineVersionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "engineversion.txt");

            if (!File.Exists(engineVersionFilePath))
            {
                Console.WriteLine("Error: Engine version file not found at " + engineVersionFilePath);
                Console.WriteLine("Please create a file named engineversion.txt in the program directory with the desired engine version as its content.");
                return;
            }

            string engineVersion = File.ReadAllText(engineVersionFilePath).Trim();
            string inputFolder;

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: import_exporter <input_folder>");
                return;
            }
            else
            {
                inputFolder = args[0];
            }

            string outputCsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "import_data.csv");

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("File,Import Object Name");
                DumpImportMaps(inputFolder, writer, engineVersion);
            }

            Console.WriteLine("Import data dumped to " + outputCsv);
        }

        static void DumpImportMaps(string directory, StreamWriter writer, string engineVersion)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) == ".uasset")
                {
                    Console.WriteLine("Processing " + file);
                    UAsset asset = new UAsset(file, (EngineVersion)Enum.Parse(typeof(EngineVersion), engineVersion));
                    using (FileStream stream = new FileStream(file, FileMode.Open))
                    {
                        AssetBinaryReader reader = new AssetBinaryReader(stream);
                        asset.Read(reader);
                        bool isFirstImport = true;
                        foreach (var import in asset.Imports)
                        {
                            if (isFirstImport)
                            {
                                writer.Write(file + "," + import.ObjectName);
                                isFirstImport = false;
                            }
                            else
                            {
                                writer.Write("," + import.ObjectName);
                            }
                        }
                        writer.WriteLine();
                    }
                }
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                DumpImportMaps(subdirectory, writer, engineVersion);
            }
        }
    }
}
