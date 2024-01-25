using UbaBuildSystem.Plugins.Core;

namespace UbaBuildSystem.Plugins.MSBuild
{
    public class RebuildCommand : ICommand
    {
        public string Name => "MSBuild.RebuildSolution";
        public string Description => "Rebuilds the solution.";

        public Task<bool> ExecuteAsync()
        {
            BuildService buildService = new BuildService();
            Console.WriteLine("Hello World");
            return Task.FromResult(true);
        }
    }
}