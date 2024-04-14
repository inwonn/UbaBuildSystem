using System.Reflection;
using System.IO;
using UbaBuildSystem.Plugins.Core.Core;
using PluginStatics = UbaBuildSystem.Plugins.Core.Statics.Plugin;

namespace UbaBuildSystem.Plugins.MSBuild
{
    public class VsSolution
    {
        private Assembly? _cppTaskAsm;
        private Assembly? _buildAsm;
        private string _solutionPath;
        private string? _solutionDir;

        public VsSolution(string solutionPath)
        {
            PluginBase? plugin = PluginStatics.GetPlugin("UbaBuildSystem.Plugins.MSBuild");
            if (plugin == null)
            {
                throw new Exception("Plugin not found.");
            }

            _cppTaskAsm = plugin.LoadAssembly("Microsoft.Build.CPPTasks.Common");
            _buildAsm = plugin.LoadAssembly("Microsoft.Build");
            _solutionPath = solutionPath;
            _solutionDir = Path.GetDirectoryName(solutionPath);
        }

        public void Load()
        {
            Type? solutionFileType = _buildAsm?.GetType("Microsoft.Build.Construction.SolutionFile");
            Type? projectInSolutionType = _buildAsm?.GetType("Microsoft.Build.Construction.ProjectInSolution");
            if (solutionFileType == null || projectInSolutionType == null)
            {
                throw new Exception("Type not found.");
            }

            var parseMethod = solutionFileType.GetMethod("Parse");
            var projects = parseMethod?.Invoke(null, new object[] { _solutionPath });
            var projectsInOrder = projects?.GetType()?.GetProperty("ProjectsInOrder")?.GetValue(projects) as IEnumerable<object?>;
            foreach (object? project in projectsInOrder ?? new List<object?>())
            {
                LoadVcxproj(project);
            }
        }

        private void LoadVcxproj(object? project)
        {
            if (project == null)
            {
                return;
            }

            Type? projectType = _buildAsm?.GetType("Microsoft.Build.Evaluation.Project");
            
            var projectPath = project.GetType().GetProperty("AbsolutePath")?.GetValue(project);
            //var test = Activator.CreateInstance(projectType!);

            //var root = openMethod?.Invoke(null, new object?[] { projectPath });
        }
    }
}
