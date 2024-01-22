namespace UbaBuildSystem.Plugins.Core
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }

        Task<bool> ExecuteAsync();
    }
}