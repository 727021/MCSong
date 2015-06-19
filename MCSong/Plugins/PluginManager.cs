using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace MCSong
{


    internal class PluginManager
    {
        private static CSharpCodeProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
        private static CompilerParameters parameters = new CompilerParameters(new string[] { "mscorlib.dll", "System.Core.dll" });
        //private static CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");
        //private static CompilerParameters parameters = new CompilerParameters();
        private static CompilerResults results;

        public static void Create(string plugin)
        {
            if (!Directory.Exists("extra/plugins/source"))
                Directory.CreateDirectory("extra/plugins/source");
            if (!Directory.Exists("extra/plugins/source/" + plugin))
                Directory.CreateDirectory("extra/plugins/source/" + plugin);
            StreamWriter sw = new StreamWriter(File.Create(String.Format("extra/plugins/source/{0}/{0}.cs", plugin)));
            sw.Write(
                "/*" + Environment.NewLine +
                "\tAuto-generated plugin skeleton class." + Environment.NewLine +
                Environment.NewLine +
                "\tUse this as a basis for plugins implemented using the MCSong plugin system." + Environment.NewLine +
                Environment.NewLine +
                "\tAs a note, MCSong is designed for .NET 4.5." + Environment.NewLine +
                "*/" + Environment.NewLine +
                "using System;" + Environment.NewLine +
                Environment.NewLine +
                "namespace MCSong" + Environment.NewLine +
                "{" + Environment.NewLine +
                "\tpublic class " + plugin + " : Plugin" + Environment.NewLine +
                "\t {" + Environment.NewLine +
                "\t\t// The plugins name. This will be used in commands and displayed with plugin info, and must match this file's name." + Environment.NewLine +
                "\t\tpublic override string Name { get { return \"" + plugin + "\"; } }" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// The person (or people) who wrote the plugin. Displayed in plugin info. This can be blank." + Environment.NewLine +
                "\t\tpublic override string Author { get { return \"\"; } }" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// A description of the plugin. Displayed in plugin info. This can be blank." + Environment.NewLine +
                "\t\tpublic override string Description { get { return \"\"; } }" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// The version of the plugin. There is no set format for this." + Environment.NewLine +
                "\t\tpublic override string Version { get { return \"1.0.0.0\"; } }" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// The version of MCSong the plugin is designed for. It will only work with this version." + Environment.NewLine +
                "\t\tpublic override string SongVersion { get { return \"" + Server.Version + "\"; } }" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// This is called right after the plugin is loaded. Put events, etc. here." + Environment.NewLine +
                "\t\tpublic override void OnLoad()" + Environment.NewLine +
                "\t\t{" + Environment.NewLine +
                "\t\t\t" + Environment.NewLine +
                "\t\t}" + Environment.NewLine +
                Environment.NewLine +
                "\t\t// This is called just before the plugin is unloaded. Here you can save data, etc." + Environment.NewLine +
                "\t\tpublic override void OnUnload()" + Environment.NewLine +
                "\t\t{" + Environment.NewLine +
                "\t\t\t" + Environment.NewLine +
                "\t\t}" + Environment.NewLine +
                "\t}" + Environment.NewLine +
                "}"
                );
            sw.Dispose();
        }

        public static bool Compile(string plugin)
        {
            string divider = new string('-', 25);
            if (!File.Exists(string.Format("extra/plugins/source/{0}.cs", plugin)))
            {
                bool check = File.Exists("logs/errors/compiler.log");
                StreamWriter sw = new StreamWriter("logs/errors/compiler.log", check);
                if (check)
                {
                    sw.WriteLine();
                    sw.WriteLine(divider);
                    sw.WriteLine();
                }
                sw.WriteLine("File not found: " + plugin + ".cs");
                sw.Dispose();
                return false;
            }
            if (!Directory.Exists("extra/plugins/dll"))
                Directory.CreateDirectory("extra/plugins/dll");
            parameters.GenerateExecutable = false;
            parameters.MainClass = plugin;
            parameters.OutputAssembly = "extra/plugins/dll/" + plugin + ".dll";
            parameters.ReferencedAssemblies.Add("MCSong_.dll");

            List<string> sources = new List<string>();

            DirectoryInfo di = new DirectoryInfo("extra/plugins/source/" + plugin);
            foreach (FileInfo file in di.GetFiles())
            {
                Server.s.Log("Compiling... " + file.Name);
                sources.Add(Path.GetFullPath(file.Name));
            }

            results = compiler.CompileAssemblyFromFile(parameters, sources.ToArray());

            switch (results.Errors.Count)
            {
                case 0:
                    return true;
                case 1:
                    CompilerError error = results.Errors[0];
                    bool check = File.Exists("logs/errors/compiler.log");
                    StreamWriter sw = new StreamWriter("logs/errors/compiler.log", check);
                    if (check)
                    {
                        sw.WriteLine();
                        sw.WriteLine(divider);
                        sw.WriteLine();
                    }
                    sw.WriteLine("Error #" + error.ErrorNumber);
                    sw.WriteLine("Message: " + error.ErrorText);
                    sw.WriteLine("Line: " + error.Line);
                    sw.Dispose();
                    return false;
                default:
                    check = File.Exists("logs/errors/compiler.log");
                    sw = new StreamWriter("logs/errors/compiler.log", check);
                    bool start = true;
                    if (check)
                    {
                        sw.WriteLine();
                        sw.WriteLine(divider);
                        sw.WriteLine();
                    }
                    foreach (CompilerError err in results.Errors)
                    {
                        if (!start)
                        {
                            sw.WriteLine();
                            sw.WriteLine(divider);
                            sw.WriteLine();
                        }
                        sw.WriteLine("Error #" + err.ErrorNumber);
                        sw.WriteLine("Message: " + err.ErrorText);
                        sw.WriteLine("Line: " + err.Line);
                        if (start) start = false;
                    }
                    sw.Dispose();
                    return false;
            }
        }

        public static void Load(string plugin)
        {
            if (Loaded(plugin))
                throw new Exception("Plugin is already loaded.");
            try
            {
                Assembly asm = Assembly.LoadFile(Path.GetFullPath(String.Format("extra/plugins/dll/{0}.dll", plugin)));
                Plugin p = null;
                foreach (Type type in asm.GetTypes())
                {
                    Server.s.Log(asm.GetType().Name);
                    if (type == typeof(Plugin))
                    {
                        p = (Plugin)Activator.CreateInstance(type);
                        break;
                    }
                }
                    
                if (plugin == null)
                    throw new Exception("Could not find child class of Plugin in DLL.");
                string[] version = p.SongVersion.Split('.');
                if (version.Length != 4)
                    throw new Exception("Invalid MCSong version specified by plugin.");
                int i = 0;
                if (!int.TryParse(p.SongVersion.Replace(".", ""), out i))
                    throw new Exception("Invalid MCSong version specified by plugin.");
                if (p.SongVersion != Server.Version)
                    throw new Exception("Plugin is incompatible with current MCSong version.");

                loaded.Add(p);
                p.OnLoad();
            }
            catch (FileNotFoundException e)
            {
                Server.ErrorLog(e);
                throw new Exception(plugin + ".dll does not exist in the DLL folder, or is missing a dependency. Details in the error log.", e);
            }
            catch (BadImageFormatException e)
            {
                Server.ErrorLog(e);
                throw new Exception(plugin + ".dll is not a valid assembly, or has an invalid dependency. Details in the error log.", e);
            }
            catch (PathTooLongException e)
            {
                throw new Exception("Class name is too long.", e);
            }
            catch (FileLoadException e)
            {
                Server.ErrorLog(e);
                throw new Exception(plugin + ".dll or one of its dependencies could not be loaded. Details in the error log.", e);
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                throw new Exception("An unknown error occured and has been logged.", e);
            }
        }

        public static void Unload(Plugin p)
        {
            p.OnUnload();
            if (!loaded.Remove(p))
                throw new Exception("Failed to remove plugin. Is the name correct?");
            Server.s.Log("Unloaded plugin: " + p.Name);
            
            
        }

        public static bool Loaded(Plugin p)
        {
            return (loaded.Contains(p) || loaded.Contains(p.Name)) ;
        }
        public static bool Loaded(string plugin)
        {
            return (loaded.Find(plugin) != null);
        }

        public static void AutoLoad()
        {
            if (!File.Exists("text/pluginautoload.txt"))
                File.Create("text/pluginautoload.txt").Close();
            foreach (string s in File.ReadAllLines("text/pluginautoload.txt"))
                if (s.TrimEnd() != "" && s != null)
                    Load(s);
        }

        public static PluginList loaded = new PluginList();
    }
}


