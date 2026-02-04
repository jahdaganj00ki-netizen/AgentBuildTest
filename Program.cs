using System.Diagnostics;

namespace ToDoList
{
    internal static class Program
    {
        private static readonly TraceSource traceSource = new TraceSource("ToDoList");

        [STAThread]
        static void Main()
        {
            ConfigureTracing();
            
            traceSource.TraceEvent(TraceEventType.Information, 1000, "Application starting...");
            
            try
            {
                ApplicationConfiguration.Initialize();
                traceSource.TraceEvent(TraceEventType.Information, 1001, "Application configuration initialized");
                
                Application.Run(new MainForm());
                
                traceSource.TraceEvent(TraceEventType.Information, 1002, "Application closing normally");
            }
            catch (Exception ex)
            {
                traceSource.TraceEvent(TraceEventType.Error, 1003, $"Application error: {ex.Message}");
                throw;
            }
            finally
            {
                traceSource.Flush();
                traceSource.Close();
            }
        }

        private static void ConfigureTracing()
        {
            // Add console trace listener
            traceSource.Listeners.Add(new ConsoleTraceListener());
            
            // Add text file trace listener
            string logPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ToDoList",
                "trace.log"
            );
            
            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
            traceSource.Listeners.Add(new TextWriterTraceListener(logPath));
            
            // Set trace level to all
            traceSource.Switch = new SourceSwitch("ToDoListSwitch", "All");
            
            traceSource.TraceInformation($"Tracing configured. Log file: {logPath}");
        }
    }
}
