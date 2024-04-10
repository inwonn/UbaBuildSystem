namespace UbaBuildSystem.Plugins.Core.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }

        Task<bool> ExecuteAsync();
    }
}