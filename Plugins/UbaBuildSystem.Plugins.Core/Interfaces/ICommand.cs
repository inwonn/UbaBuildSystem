namespace UbaBuildSystem.Plugins.Core.Interfaces
{
    public interface ICommand
    {
        Task<bool> ExecuteAsync();
    }
}