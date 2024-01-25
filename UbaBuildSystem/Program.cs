using System.Reflection;
using UbaBuildSystem.Plugins.Core;
using PluginsPath = UbaBuildSystem.Plugins.Core.Path;
using SystemIOPath = System.IO.Path;

namespace UbaBuildSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1 && args[0] == "/d")
                {
                    Console.WriteLine("Waiting for any key...");
                    Console.ReadLine();
                }

                string[] pluginPaths = new string[]
                {
                    @"UbaBuildSystem.Plugins.MSBuild\\UbaBuildSystem.Plugins.MSBuild.dll",
                };

                IEnumerable<ICommand> commands = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateCommands(pluginAssembly);
                }).ToList();

                if (args.Length == 0)
                {
                    Console.WriteLine("Commands: ");
                    foreach (ICommand command in commands)
                    {
                        Console.WriteLine($"{command.Name}\t - {command.Description}");
                    }
                }
                else
                {
                    foreach (string commandName in args)
                    {
                        Console.WriteLine($"-- {commandName} --");

                        ICommand? command = commands.FirstOrDefault(c => c.Name == commandName);
                        if (command == null)
                        {
                            Console.WriteLine("No such command is known.");
                            return;
                        }

                        _ = command.ExecuteAsync();

                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = PluginsPath.GetPluginsRoot();

            string pluginLocation = SystemIOPath.GetFullPath(SystemIOPath.Combine(root, relativePath.Replace('\\', SystemIOPath.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(SystemIOPath.GetFileNameWithoutExtension(pluginLocation)));
        }

        static IEnumerable<ICommand> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ICommand).IsAssignableFrom(type))
                {
                    ICommand? result = Activator.CreateInstance(type) as ICommand;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}