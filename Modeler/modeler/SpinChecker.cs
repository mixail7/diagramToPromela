using System.Collections.Generic;
using System.IO;
using DiagramDesigner;

namespace Modeler.modeler
{
    internal class SpinChecker
    {
        private const string SpinPath = @"C:\MinGW\bin\spin.exe";
        private List<string> initFunctions = new List<string>();
        private string codebuffer;
        
        private List<DesignerItem> getNextNodes(List<DesignerItem> items, List<Connection> conns, DesignerItem ds)
        {
            List<DesignerItem> ditms = new List<DesignerItem>();
            foreach (var cn in conns)
            {
                /*if (cn.)
                {
                    
                }*/
                
            }
            return ditms;
        }

        private void getPromelaCode(List<DesignerItem> items, List<Connection> conns, string main_thread_name)
        {
           // initFunctions.Add();
           // return "";
        }

        public bool runAnalyze(string path, MainWindow mv)
        {
            codebuffer = "";
            initFunctions.Clear();
            var items = new List<DesignerItem>();
            var conns = new List<Connection>();
            var entries = Directory.GetFileSystemEntries(path);
            foreach (var entrie in entries)
            {
                if (Directory.Exists(entrie))
                {
                    continue;
                }
                mv.loadFromFile(entrie, out items, out conns);
            }
            return true;
        }
    }
}
