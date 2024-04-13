using UbaBuildSystem.Plugins.Core.Interfaces;

namespace UbaBuildSystem.Plugins.MSBuild.Commands
{
    public class BuildCommand : ICommand
    {
        public Task<bool> ExecuteAsync()
        {
            BuildService buildService = new BuildService();
            buildService.BuildAsync().Wait();
            return Task.FromResult(true);
        }
    }
}