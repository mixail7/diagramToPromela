using System;
using System.IO;
using System.Windows;
using AvalonDock.Layout;
using DiagramDesigner;

namespace Modeler.modeler
{
    public class FunctionCode
    {
        public string functionExecSource;
        public string functionSource;
        public FunctionCode(string execSource, string source)
        {
            functionSource = source;
            functionExecSource = execSource;
        }
    }
    public class RubyCodeGenerator : ICodeGenerator
    {
        private Project _project;
        private ModelCode _modelCode = new ModelCode();

        public RubyCodeGenerator(Project project)
        {
            _project = project;
        }
        private string GetValidFileName(string baseFileName)
        {
            if (baseFileName == "" || baseFileName == _project.FileName)
                baseFileName = _project.FileModelPath + "\\Общая схема.model";
            if (Directory.Exists(baseFileName))
                baseFileName += "\\Общая схема.model";
            return baseFileName;
        }
        
        public string GetCodeFileName(string modelFileName)
        {
            modelFileName = GetValidFileName(modelFileName);
            return modelFileName.Replace(_project.FileModelPath, _project.FileCodePath);
        }
        private DesignerItem getFirst(DesignerCanvas canva)
        {
            foreach (var elem in canva.Children)
            {
                var item = elem as DesignerItem;
                if (item != null)
                {
                    if (item.ParentID == Guid.Empty)
                        return item;
                }
            }
            return null;
        }
        
        private DesignerItem getNext(DesignerItem item, DesignerCanvas canva)
        {
            string id = item.ID.ToString();
            foreach (var elem in canva.Children)
            {
                var conn = elem as Connection;
                if (conn != null)
                {
                    if (conn.Source.ParentDesignerItem.ID.ToString() == id || conn.ID.ToString() == id)
                    {
                        return conn.Sink.ParentDesignerItem;
                    }
                }
            }
            return null;
        }

        private CodeElement getCode(DesignerItem item, string fileName)
        {
            return _modelCode.GetCodeForItem(item.ID.ToString(), fileName);
        }

        private string getOptNameForElement(DesignerItem item)
        {
            var block = item.Content as IBlockElementInterface;
            return block == null ? "" : block.TextValue.Replace(" ", "_");
        }

        private string GetInputParamForElement(string id, DesignerCanvas canva, string fileName)
        {
            foreach (var elem in canva.Children)
            {
                var conn = elem as Connection;
                if (conn != null)
                {
                    if (conn.Sink.ParentDesignerItem.ID.ToString() == id || conn.ID.ToString() == id)
                    {
                        var item = _modelCode.GetCodeForItem(conn.Source.ParentDesignerItem.ID.ToString(), fileName);
                        return item.OutputParam;
                    }
                }
            }
            return "";
        }

        private FunctionCode getFunctionCode(CodeElement elem, DesignerItem item, string fileName, DesignerCanvas canva)
        {
            string buf = "";
            string funcSourceExec = "def " + getOptNameForElement(item) + " (" +
                                    GetInputParamForElement(item.ID.ToString(), canva, fileName) + ")";
            buf += funcSourceExec + "\r\n" + elem.Code + "\r\nend";
            return new FunctionCode(funcSourceExec, buf);
        }

        public void Generate(string baseFileName)
        {
            string fileName = GetValidFileName(baseFileName);
            //MessageBox.Show(fileName);
            //MessageBox.Show(GetCodeFileName(fileName));
            try
            {
                DesignerCanvas canva = new DesignerCanvas();
                canva.Open_Executed(fileName);
                DesignerItem item = getFirst(canva);
                string bufferFunctions = "", bufferFunctionsSource = "";
                while (item != null)
                {
                    CodeElement elem = getCode(item, fileName);
                    FunctionCode code = getFunctionCode(elem, item, fileName, canva);
                    bufferFunctions += "\r\n" + code.functionExecSource;
                    bufferFunctionsSource += "\r\n" + code.functionSource;
                    item = getNext(item, canva);
                }
                //canva.Children
                File.WriteAllText(GetCodeFileName(fileName), (bufferFunctionsSource + "\r\n#function exec\r\n" + bufferFunctions).Trim());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
