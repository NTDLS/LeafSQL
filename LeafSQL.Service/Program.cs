using LeafSQL.Service.Properties;
using System;

namespace LeafSQL.Service
{
    public static class Program
    {
        public static Engine.Core Core;

        static void Main(string[] args)
        {
            //System.Diagnostics.Process.Start("CMD.exe", "/C rd C:\\LeafSQL /S /Q");

            var settings = new Library.Payloads.Models.ServerSettings()
            {
                Name = Settings.Default.Name,
                BaseAddress = Settings.Default.BaseAddress,
                DataRootPath = Settings.Default.DataRootPath.TrimEnd(new char[] { '/', '\\' }),
                TransactionDataPath = Settings.Default.TransactionDataPath.TrimEnd(new char[] { '/', '\\' }),
                LogDirectory = Settings.Default.LogDirectory.TrimEnd(new char[] { '/', '\\' }),
                FlushLog = Settings.Default.FlushLog,
                EnableIOCaching = Settings.Default.EnableIOCaching,
                EnableDeferredIO = Settings.Default.EnableDeferredIO,
                WriteTraceData = Settings.Default.WriteTraceData,
                CacheScavengeBuffer = Settings.Default.CacheScavengeBuffer,
                CacheScavengeRate = Settings.Default.CacheScavengeRate,
                MaxCacheMemory = Settings.Default.MaxCacheMemory,
                RecordInstanceHealth = Settings.Default.RecordInstanceHealth
            };

            Core = new Engine.Core(settings);

            Core.Start();

            var owinServices = new OWIN.Services();
            owinServices.Start(settings.BaseAddress);

            Core.Log.Write(String.Format("Listening on {0}", settings.BaseAddress));

            Console.ReadLine(); //Continue running.

            Core.Shutdown();
        }
    }
}
