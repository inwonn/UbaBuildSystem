﻿using EpicGames.Core;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbaBuildSystem.Plugins.MSBuild.UnrealBuildTool
{
    class UnrealBuildAcceleratorConfig
    {
        /// <summary>
        /// When set to true, UBA will not use any remote help
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxDisableRemote")]
        //[CommandLine("-UBADisableRemote")]
        public bool bDisableRemote { get; set; } = false;

        /// <summary>
        /// When set to true, UBA will force all actions that can be built remotely to be built remotely. This will hang if there are no remote agents available
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxForceRemote")]
        //[CommandLine("-UBAForceRemote")]
        public bool bForceBuildAllRemote { get; set; } = false;

        /// <summary>
        /// When set to true, actions that fail locally with UBA will be retried without UBA.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxForcedRetry")]
        //[CommandLine("-UBAForcedRetry")]
        public bool bForcedRetry { get; set; } = false;

        /// <summary>
        /// When set to true, all errors and warnings from UBA will be output at the appropriate severity level to the log (rather than being output as 'information' and attempting to continue regardless).
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxStrict")]
        //[CommandLine("-UBAStrict")]
        public bool bStrict { get; set; } = false;

        /// <summary>
        /// If UBA should store cas compressed or raw
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxStoreRaw")]
        //[CommandLine("-UBAStoreRaw")]
        public bool bStoreRaw { get; set; } = false;

        /// <summary>
        /// If UBA should distribute linking to remote workers. This needs bandwidth but can be an optimization
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxLinkRemote")]
        //[CommandLine("-UBALinkRemote")]
        public bool bLinkRemote { get; set; } = false;

        /// <summary>
        /// The amount of gigabytes UBA is allowed to use to store workset and cached data. It is a good idea to have this >10gb
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxStoreCapacityGb")]
        //[CommandLine("-UBAStoreCapacityGb")]
        public int StoreCapacityGb { get; set; } = 40;

        /// <summary>
        /// Max number of worker threads that can handle messages from remotes. 
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxMaxWorkers")]
        //[CommandLine("-UBAMaxWorkers")]
        public int MaxWorkers { get; set; } = 192;

        /// <summary>
        /// Max size of each message sent from server to client
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxSendSize")]
        //[CommandLine("-UBASendSize")]
        public int SendSize { get; set; } = 256 * 1024;

        /// <summary>
        /// Which ip UBA server should listen to for connections
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxHost")]
        //[CommandLine("-UBAHost")]
        public string Host { get; set; } = String.Empty;

        /// <summary>
        /// Which port UBA server should listen to for connections.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxPort")]
        //[CommandLine("-UBAPort")]
        public int Port { get; set; } = 1345;

        /// <summary>
        /// Which directory to store files for UBA.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxRootDir")]
        //[CommandLine("-UBARootDir")]
        public string? RootDir { get; set; } = null;

        /// <summary>
        /// Use Quic protocol instead of Tcp (experimental)
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxQuic", Value = "true")]
        //[CommandLine("-UBAQuic", Value = "true")]
        public bool bUseQuic { get; set; } = false;

        /// <summary>
        /// Enable logging of UBA processes
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxLog", Value = "true")]
        //[CommandLine("-UBALog", Value = "true")]
        public bool bLogEnabled { get; set; } = false;

        /// <summary>
        /// Prints summary of UBA stats at end of build
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxPrintSummary", Value = "true")]
        //[CommandLine("-UBAPrintSummary", Value = "true")]
        public bool bPrintSummary { get; set; } = false;

        /// <summary>
        /// Launch visualizer application which shows build progress
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxVisualizer", Value = "true")]
        //[CommandLine("-UBAVisualizer", Value = "true")]
        public bool bLaunchVisualizer { get; set; } = false;

        /// <summary>
        /// Resets the cas cache
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxResetCas", Value = "true")]
        //[CommandLine("-UBAResetCas", Value = "true")]
        public bool bResetCas { get; set; } = false;

        /// <summary>
        /// Provide custom path for trace output file
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxTraceOutputFile")]
        //[CommandLine("-UBATraceOutputFile")]
        public string TraceFile { get; set; } = String.Empty;

        /// <summary>
        /// Add verbose details to the UBA trace
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxDetailedTrace", Value = "true")]
        //[CommandLine("-UBADetailedTrace", Value = "true")]
        public bool bDetailedTrace { get; set; }

        /// <summary>
        /// Disable UBA waiting on available memory before spawning new processes
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-UBADisableWaitOnMem", Value = "true")]
        public bool bDisableWaitOnMem { get; set; }

        /// <summary>
        /// Let UBA kill running processes when close to out of memory
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxAllowKillOnMem", Value = "true")]
        //[CommandLine("-UBAAllowKillOnMem", Value = "true")]
        public bool bAllowKillOnMem { get; set; }

        /// <summary>
        /// Threshold for when executor should output logging for the process. Defaults to never
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxOutputStatsThresholdMs")]
        //[CommandLine("-UBAOutputStatsThresholdMs")]
        public int OutputStatsThresholdMs { get; set; } = Int32.MaxValue;

        /// <summary>
        /// Skip writing intermediate and output files to disk. Useful for validation builds where we don't need the output
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxNoWrite", Value = "false")]
        //[CommandLine("-UBANoWrite", Value = "false")]
        public bool bWriteToDisk { get; set; } = true;

        /// <summary>
        /// Set to true to disable mimalloc and detouring of memory allocations.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxNoCustoMalloc", Value = "true")]
        //[CommandLine("-UBANoCustoMalloc", Value = "true")]
        public bool bDisableCustomAlloc { get; set; } = false;

        /// <summary>
        /// The zone to use for UBA.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxZone=")]
        //[CommandLine("-UBAZone=")]
        public string Zone { get; set; } = String.Empty;

        /// <summary>
        /// Set to true to enable encryption when transfering files over the network.
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-BoxCrypto", Value = "true")]
        //[CommandLine("-UBACrypto", Value = "true")]
        public bool bUseCrypto { get; set; } = false;

        /// <summary>
        /// Set to true to provide known inputs to processes that are run remote. This is an experimental feature to speed up build times when ping is higher
        /// </summary>
        //[XmlConfigFile(Category = "UnrealBuildAccelerator")]
        //[CommandLine("-UBAUseKnownInputs", Value = "true")]
        public bool bUseKnownInputs { get; set; } = false;
    }
}
