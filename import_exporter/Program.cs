using System;
using System.IO;
using UAssetAPI;

namespace import_exporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputCsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "import_data.csv");

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: import_exporter <input_folder>");
                return;
            }

            string inputFolder = args[0];

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("File,Import Object Name");
                DumpImportMaps(inputFolder, writer);
            }

            Console.WriteLine("Import data dumped to " + outputCsv);
        }

        static void DumpImportMaps(string directory, StreamWriter writer)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) == ".uasset")
                {
                    Console.WriteLine("Processing " + file);
                    UAsset asset = new UAsset(file, UAssetAPI.UnrealTypes.EngineVersion.VER_UE4_20);
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
                DumpImportMaps(subdirectory, writer);
            }
        }
    }
}
