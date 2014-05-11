using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;
using AurelienRibon.Ui.SyntaxHighlightBox;
using AvalonDock.Layout;
using DiagramDesigner;
using Modeler.modeler;

namespace Modeler
{
    public partial class MainWindow
    {
        public Project _project;
        private readonly ModelCode _modelCode = new ModelCode();
        readonly DispatcherTimer _clearStatusBar = new DispatcherTimer();
        public DesignerCanvas MyDesignerCanvas;
        public string current_model;
    

        private void ClearStatus(object sender, EventArgs e)
        {
            ErrorsBlock.Text = "";
        }

        private void InitModeler()
        {
            ElementCode.CurrentHighlighter = HighlighterManager.Instance.Highlighters["RUBY"];
            MyDesigner.SelectionService.SelectedChanged += MyDesigner_SelectedChanged;
            ProjectTreeView.OpenModel += OpenModel;
            ProjectTreeView.GenerateCodeForElement += GenerateCodeEvent;
            LoadProject(@"C:/temp/ProjectModel.mdd");
            ProjectTreeView.LoadProject(_project);
            _clearStatusBar.Tick += ClearStatus;
            _clearStatusBar.Interval = new TimeSpan(0, 0, 20);
            _clearStatusBar.Start();
            MyDesignerCanvas = MyDesigner;
        }

        private void DataForSelectDiagramElement(string id, string fileName)
        {
            var elem = _modelCode.GetCodeForItem(id, fileName);
            ElementCode.Text = elem.Code;
            OutputPraram.Text = elem.OutputParam;
            InputParam.Text = GetInputParamForElement(id);
            if (OutputPraram.Text == "")
                OutputPraram.Text = InputParam.Text;
        }

        private void SelectDiagramElement(Connection item)
        {
            DataForSelectDiagramElement(item.ID.ToString(), ((LayoutDocument)MainDiagramsTabs.Children[0]).Description);
        }

        private void SelectDiagramElement(DesignerItem item)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                var block = item.Content as IBlockElementInterface;
                var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
                if (block != null)
                {
                    string name = Path.GetDirectoryName(doc.Description) + "\\" + block.TextValue;
                    if (Directory.Exists(name))
                    {
                        name += "\\Общая схема.model";
                    }
                    else
                    {
                        name += ".model";
                    }
                    OpenModelEditor(name);
                }
                return;
            }
            DataForSelectDiagramElement(item.ID.ToString(), ((LayoutDocument)MainDiagramsTabs.Children[0]).Description);
        }

        void LoadProject(string fileName)
        {
            _project = Project.Load(fileName);
            _project.CreatePaths();
            MyDesigner.Tag = this;
        }

        void CreateProject(string fileName)
        {
            _project = new Project();
            _project.Init(fileName);
            _project.Save();
            _project.CreatePaths();
        }

        private void NewProjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "ProjectModel",
                DefaultExt = ".mdd",
                Filter = "Mdd documents (.mdd)|*.mdd"
            };
            var result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                CreateProject(filename);
                ProjectTreeView.LoadProject(_project);
            }
        }

        private void OpenProjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".mdd", Filter = "Mdd documents (.mdd)|*.mdd" };
            var result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                LoadProject(filename);
                ProjectTreeView.LoadProject(_project);
            }
        }

        private void SaveModel()
        {
            var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
            try
            {
                MyDesigner.Save_Executed(doc.Description);
            }
            catch (Exception e)
            {
                ErrorsBlock.Text += e.Message;
            }
        }

        private string GetTitleForModel(string filename)
        {
            return filename.Substring(_project.FileModelPath.Length);
        }

        private void ClearDiagramCodes()
        {
            ElementCode.Text = "";
            OutputPraram.Text = "";
            InputParam.Text = "";
        }

        public void loadFromFile(string path, out List<DesignerItem> items, out List<Connection> conns)
        {
            items = new List<DesignerItem>();
            conns = new List<Connection>();
            var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
            var scrollViewer = doc.Content as ScrollViewer;
            if (scrollViewer == null) return;
            var canva = scrollViewer.Content as DesignerCanvas;
            canva.OpenFile(path, out items, out conns);
        }

        public void OpenModel(string fileName)
        {
            ClearDiagramCodes();
            var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
            var scrollViewer = doc.Content as ScrollViewer;
            if (scrollViewer == null) return;
            var canva = scrollViewer.Content as DesignerCanvas;
            try
            {
                canva.Children.Clear();
                doc.Title = GetTitleForModel(fileName);
                doc.Description = fileName;
                canva.Open_Executed(doc.Description);
                current_model = doc.Description;
                foreach (var item in canva.Children)
                {
                    try
                    {
                        var ds = (DesignerItem) item;
                        ((IBlockElementInterface)ds.Content).Tag = this;
                    }
                    catch (Exception e)
                    {
                        
                    }

                }
            }
            catch (Exception e)
            {
                ErrorsBlock.Text = e.Message;
            }
        }

        private void OpenModelEditor(string path)
        {
            /*foreach (LayoutDocument doc in MainDiagramsTabs.Children)
            {
                if (doc.Description == item.Tag.ToString())
                {
                    doc.IsActive = true;
                    return;
                }
            }
            var newDoc = new LayoutDocument
                {
                    Title = getTitleForTreeviewItem(item),
                    Description = item.Tag.ToString(),
                    IsActive = true
                };
            var canvas = new DesignerCanvas();
            canvas.initPalleters(Palleters);
            canvas.Focusable = true;
            canvas.Background =
                (Brush)
                (((MainDiagramsTabs.Children[0] as LayoutDocument).Content as ScrollViewer).Content as DesignerCanvas)
                    .Background;
            canvas.ContextMenu = (ContextMenu) Resources["DesignerCanvasContextMenu"];
            canvas.FocusVisualStyle = null;

            MainDiagramsTabs.Children.Add(newDoc);*/
            /*пока сделаем одну вкладку*/
            var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
            if (doc.Description != "")
            {
                SaveModel();
            }
            OpenModel(path);
        }

        private void OpenModel(object sender, EventArgs e)
        {
            _modelCode.FlushAll(MyDesigner.Children);
            OpenModelEditor(sender.ToString());
        }

        private void GenerateCodeEvent(object sender, EventArgs e)
        {
            var generator = new RubyCodeGenerator(_project);
            generator.Generate(sender.ToString());
            var cd = new CodeViewer(generator.GetCodeFileName(sender.ToString()));
            cd.ShowDialog();
        }

        private void ElementCode_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var doc = (LayoutDocument)MainDiagramsTabs.Children[0];
            var path = doc.Description;
            var selected = MyDesigner.SelectionService.CurrentSelection.OfType<DesignerItem>().ToList();
            if (selected.Count != 1)
            {
                var selectedConn = MyDesigner.SelectionService.CurrentSelection.OfType<Connection>().ToList();
                if (selectedConn.Count != 1 || selected.Count != 0) return;
                _modelCode.AddCode(path, selectedConn[0].ID.ToString(), ElementCode.Text, OutputPraram.Text);
            }
            else
            {
                _modelCode.AddCode(path, selected[0].ID.ToString(), ElementCode.Text, OutputPraram.Text);
            }
        }

        private void SaveCodeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _modelCode.FlushAll(MyDesigner.Children);
        }
        
        private string GetInputParamForElement(string id)
        {
            foreach (var elem in MyDesigner.Children)
            {
                var conn = elem as Connection;
                if (conn != null)
                {
                    if (conn.Sink.ParentDesignerItem.ID.ToString() == id || conn.ID.ToString() == id)
                    {
                        var item = _modelCode.GetCodeForItem(conn.Source.ParentDesignerItem.ID.ToString(), ((LayoutDocument)MainDiagramsTabs.Children[0]).Description);
                        return item.OutputParam;
                    }
                }
            }
            return "";
        }

        private void GenerateCode_OnClick(object sender, RoutedEventArgs e)
        {
            ICodeGenerator generator = new RubyCodeGenerator(_project);
            generator.Generate("");
            CodeViewer cd = new CodeViewer(generator.GetCodeFileName(""));
            cd.ShowDialog();
        }
    }
}
