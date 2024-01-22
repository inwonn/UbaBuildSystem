using UbaBuildSystem.Plugins.Core;

namespace UbaBuildSystem.Plugins.MSBuild
{
    public class RebuildSolutionCommand : ICommand
    {
        public string Name => "RebuildSolution";
        public string Description => "Rebuilds the solution.";

        public Task<bool> ExecuteAsync()
        {
            Console.WriteLine("Hello World");
            return Task.FromResult(true);
        }
    }
}