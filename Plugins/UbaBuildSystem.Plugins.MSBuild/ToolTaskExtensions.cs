using UbaBuildSystem.Plugins.MSBuild.Generated;

namespace UbaBuildSystem.Plugins.MSBuild
{
    internal static class ToolTaskExtensions
    {

        interface IToolTaskConverter
        {
            bool CanConvertFrom(ToolTask toolTask);
            object[]? ConvertFrom(ToolTask toolTask);
        }

        class DefaultToolTaskConverter : IToolTaskConverter
        {
            public bool CanConvertFrom(ToolTask toolTask) => true;
            public object[]? ConvertFrom(ToolTask toolTask)
            {
                return null;
            }
        }

        class CompileTaskConverter : IToolTaskConverter
        {
            public bool CanConvertFrom(ToolTask toolTask) => toolTask.CommandLine.Contains("cl.exe", StringComparison.OrdinalIgnoreCase);
            public object[]? ConvertFrom(ToolTask toolTask)
            {
                throw new NotImplementedException();
            }
        }

        class LibTaskConverter : IToolTaskConverter
        {
            public bool CanConvertFrom(ToolTask toolTask) => toolTask.CommandLine.Contains("cl.exe", StringComparison.OrdinalIgnoreCase);
            public object[]? ConvertFrom(ToolTask toolTask)
            {
                throw new NotImplementedException();
            }
        }

        class LinkTaskConverter : IToolTaskConverter
        {
            public bool CanConvertFrom(ToolTask toolTask) => toolTask.CommandLine.Contains("cl.exe", StringComparison.OrdinalIgnoreCase);
            public object[]? ConvertFrom(ToolTask toolTask)
            {
                throw new NotImplementedException();
            }
        }

        public static object[]? ToLinkedActions(this ToolTask toolTask)
        {
            return null;
        }
    }
}
