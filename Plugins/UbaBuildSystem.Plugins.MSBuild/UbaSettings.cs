using EpicGames.Core;
using Google.Protobuf.WellKnownTypes;
using System.Xml.Linq;
using UbaBuildSystem.Plugins.MSBuild.UnrealBuildTool;

namespace UbaBuildSystem.Plugins.MSBuild
{


    internal class UbaSettings
    {
        public UnrealBuildAcceleratorConfig UbaConfig { get; set; } = new UnrealBuildAcceleratorConfig();
        public UnrealBuildAcceleratorHordeConfig UbaHordeConfig { get; set; } = new UnrealBuildAcceleratorHordeConfig();
    }
}
