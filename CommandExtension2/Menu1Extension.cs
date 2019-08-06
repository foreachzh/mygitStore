using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Presentation;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Uml;
using Microsoft.VisualStudio.Modeling.ExtensionEnablement;
using Microsoft.VisualStudio.Uml.AuxiliaryConstructs;
using Microsoft.VisualStudio.Uml.Classes;
using Microsoft.VisualStudio.Uml.UseCases;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Extensibility;

using EnvDTE;
using EnvDTE80;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;
using System.Globalization;
using System.Reflection;

namespace CommandExtension2
{
    // DELETE any of these attributes if the command  
    // should not appear in some types of diagram.  
    [ClassDesignerExtension]
    [ActivityDesignerExtension]
    [ComponentDesignerExtension]
    [SequenceDesignerExtension]
    [UseCaseDesignerExtension]
    // [LayerDesignerExtension]  

    // All menu commands must export ICommandExtension:  
    [Export(typeof(ICommandExtension))]
    // CHANGE class name – determines order of appearance on menu:  
    public class Menu1 : ICommandExtension
    {
        [Import]
        public IDiagramContext DiagramContext { get; set; }

        public void QueryStatus(IMenuCommand command)
        { // Set command.Visible or command.Enabled to false  
            // to disable the menu command.  
            command.Visible = command.Enabled = true;
            // 只在uml用例图中显示菜单
            IUseCaseDiagram usercaseDiagram = DiagramContext.CurrentDiagram as IUseCaseDiagram;  
            command.Enabled = command.Visible =  usercaseDiagram != null ;  
        }

        public string Text
        {
            get { return "转到页面"; }
        }

        public void Execute(IMenuCommand command)
        {
            // A selection of starting points:  
            // IDiagram diagram = this.DiagramContext.CurrentDiagram;
            Microsoft.VisualStudio.ArchitectureTools.Extensibility.Presentation.IUseCaseDiagram usercaseDiagram = DiagramContext.CurrentDiagram as IUseCaseDiagram;
            var dslDiagram = DiagramContext.CurrentDiagram.GetObject<Microsoft.VisualStudio.Modeling.Diagrams.Diagram>();

            IEnumerable < IShape  < IUseCase > > shapelist = usercaseDiagram.GetSelectedShapes<IUseCase>();

            var selectedShapes = DiagramContext.CurrentDiagram.GetSelectedShapes<IClassifier>();
            if (selectedShapes.Count() > 0)
            {
                IClassifier firstElement = selectedShapes.First().Element;
            }


            foreach (IShape shape in shapelist)
            {
                string strPrint = string.Format("xpos:{0};ypos:{1};color:{2};width:{3};height:{4}",
                    shape.XPosition, shape.YPosition, shape.Color, shape.Width, shape.Height);
                // shape.GetObject<Shape>();
                // IElement element = shape.Element; 
                IShape<IUseCase> classShape = shape.ToIShape<IUseCase>();
                if (classShape != null)
                {
                    IUseCase aUseCase = classShape.Element;
                    //  如果节点没有子节点，说明是终节点，此处应该弹出菜单，跳转至目标位置
                    IEnumerable<IShape<IUseCase>> nodeChilds = shape.GetChildShapes<IUseCase>();
                    if (nodeChilds.Count() == 0)
                    {
                        SetHtmlSql(aUseCase);
                    }
                }

                // aClass.
                IElement ele = shape.GetElement();
             
                // string str = ele.OwnedComments.ToString();
                IShape<Microsoft.VisualStudio.Uml.UseCases.IUseCase> cc = shape.ToIShape<IUseCase>();

            }

            //foreach (IShape<IUseCase> shape in usercaseDiagram.GetChildShapes<IUseCase>())  
            //{
            //    IUseCase displayedElement = shape.Element;  
            //}  
            
            // IPackage linkedPackage = DiagramContext.CurrentDiagram..Element as IPackage;  


            IModelStore modelStore = usercaseDiagram.ModelStore;
            IModel model = modelStore.Root;
            //foreach (IElement element in modelStore.AllInstances<IClass>())
            //{ }
        }

        public void SetHtmlSql(IUseCase aUseCase)
        {
            // 获取解决方案所在路径
            // ServiceProvider sp = new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte);
            DTE dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE;
            DTE2 dte2 =  (DTE2)dte;
            if (dte == null)
                return;

            string solutionName = dte.Solution.FileName;
            Solution sln = dte.Solution;
            string solutionPath = Path.GetDirectoryName(solutionName);
            string solutionName2 = Path.GetFileNameWithoutExtension(solutionName);
            string projectName = Path.GetFileNameWithoutExtension(solutionName);

            string sqlpath = solutionPath +"\\"+ projectName + "\\App_Data\\SqlCode\\";
            string htmlpath = solutionPath + "\\" + projectName + "\\Job\\";

            string tablename = string.Empty;
            string nodeSqlpath = string.Empty;
            string nodeHtmlpath = string.Empty;
            string titleName = aUseCase.Name;
            bool bJson = false;
            bool bCloseBtn = false;
            bool bDetailList = false;
            // 弹出对话框，设置html页面以及 对应sql文件 
             
            IEnumerable<IReference> refer = aUseCase.GetReferences("tablename");
            if (refer.Count() > 0)
            {
                string value = refer.First().Value;
                tablename = value;
            }
            else
                SetValue(aUseCase, sqlpath, htmlpath, ref tablename, ref nodeSqlpath, ref nodeHtmlpath, ref bJson, ref bCloseBtn, ref bDetailList);

            IEnumerable<IReference> refer1 = aUseCase.GetReferences("sql.path");
            if (refer1.Count() > 0)
            {
                string value = refer1.First().Value;
                nodeSqlpath = value;
            }
            else
                SetValue(aUseCase, sqlpath, htmlpath, ref tablename, ref nodeSqlpath, ref nodeHtmlpath, ref bJson, ref bCloseBtn, ref bDetailList);


            refer1 = aUseCase.GetReferences("html.path");
            if (refer1.Count() > 0)
            {
                string value = refer1.First().Value;
                nodeHtmlpath = value;
            }
            else
                SetValue(aUseCase, sqlpath, htmlpath, ref tablename, ref nodeSqlpath, ref nodeHtmlpath, ref bJson, ref bCloseBtn, ref bDetailList);

            string originalSqlpath = nodeSqlpath;
            string originalHtmlpath = nodeHtmlpath;

            //if (AppSetting.GetAppConfig("bShowDlg") != "1")
            //    AppSetting.UpdateAppConfig("bShowDlg", "1");
            //else
            //    AppSetting.UpdateAppConfig("bShowDlg", "0");
            if(AppSetting.GetAppConfig("bShowDlg") == "1")
                SetValue(aUseCase, sqlpath, htmlpath, ref tablename, ref nodeSqlpath, ref nodeHtmlpath, ref bJson, ref bCloseBtn, ref bDetailList);


            if (originalSqlpath != nodeSqlpath)
            {
                bool bok = EnvDTEHelper.MoveFile(dte, projectName, originalSqlpath, nodeSqlpath);
                bok = EnvDTEHelper.MoveFile(dte, projectName, originalHtmlpath, nodeHtmlpath);
            }

            // 定位到文件 没有的话创建
            string sqlFileName = Path.GetFileName(nodeSqlpath);
            if (!string.IsNullOrEmpty(nodeSqlpath))
            {
                // string sqlFileName = Path.GetFileName(nodeSqlpath);
                if (!File.Exists(nodeSqlpath))
                {
                    // 手动创建文件并加入到项目中
                    string sql = "select * from " + tablename + "\r\n";
                    File.WriteAllText(nodeSqlpath, sql);

                    EnvDTEHelper.AddFilesToProject(dte, solutionName2, nodeSqlpath);
                }

                dte.ItemOperations.OpenFile(nodeSqlpath);
            }

            if (!string.IsNullOrEmpty(nodeHtmlpath))
            {
                string htmlFileName = Path.GetFileName(nodeHtmlpath);
                string procName = Path.GetFileNameWithoutExtension(nodeSqlpath);
                if (!File.Exists(nodeHtmlpath))
                {
                    string codeStr=string.Empty;
                    bool bSuccess = FormatHtml.FormatCode(titleName, procName, tablename, ref codeStr, bJson, bCloseBtn, bDetailList);
                    if (bSuccess)
                    {
                        // 手动创建文件并加入到项目中
                        File.WriteAllText(nodeHtmlpath, codeStr);

                        EnvDTEHelper.AddFilesToProject(dte, solutionName2, nodeHtmlpath);
                    }
                    else
                    {
                        Debug.WriteLine("使用tt模板创建文件失败！Reason="+codeStr);
                        EnvDTEHelper.AddFile2Project(dte, solutionName2, "Job", nodeHtmlpath, ".html");
                    }

                    if (bSuccess)
                    {
                        string theHtmlpath = nodeHtmlpath ;
                        theHtmlpath = theHtmlpath.Replace(solutionPath+"\\"+projectName,"");
                        WebStageInterface.Instance().AddItem2Menu(titleName, theHtmlpath);
                    }
                }

                dte.ItemOperations.OpenFile(nodeHtmlpath);
            }

            /*
              ProjectItem item = _dte.SelectedItems.Item(1).ProjectItem;        
                item.Properties.Item("CustomTool").Value = nameof(MinifyCodeGenerator);
             */
        }

        public static void SetValue(IUseCase aUseCase, string sqlpath, string htmlpath, ref string tablename, ref string nodeSqlpath,ref string nodeHtmlpath, ref bool bJson, ref bool bCloseBtn, ref bool bDetail)
        {
            frmInput input = new frmInput();
            input.initSqlpath = sqlpath;
            input.initHtmlpath = htmlpath;
            input.txtTableName.Text = tablename;
            input.txtHtmlPath.Text = nodeHtmlpath;
            input.txtSqlPath.Text = nodeSqlpath;
            if (input.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nodeSqlpath = input.txtSqlPath.Text;
                nodeHtmlpath = input.txtHtmlPath.Text;
                tablename = input.txtTableName.Text;
                aUseCase.AddReference("tablename", input.txtTableName.Text,false);
                aUseCase.AddReference("sql.path", input.txtSqlPath.Text, false);
                aUseCase.AddReference("html.path", input.txtHtmlPath.Text, false);
                bJson = input.cbJson.Checked;
                bCloseBtn = input.cbClose.Checked;
                bDetail = input.cbDetail.Checked;
            }
        }
    }

    
    class EnvDTEHelper
    {
        public static string GetFullPath(ProjectItem proItem)
        {
            Property proper = proItem.Properties.Item("FullPath");
            string filepath = proper.Value.ToString();
            return filepath;
        }

        public static bool MoveFile(DTE dte, string projectName, string originalpath, string nowpath)
        {
            // 2.复制并创建文件
            if(!File.Exists(nowpath))
                File.Copy(originalpath, nowpath);
            // 1.删除原来的引用
            Project project = EnvDTEHelper.GetProjectbyName(dte, projectName);
            ProjectItem pItem = EnvDTEHelper.TravelGetItem(dte, projectName, originalpath);
            if (project == null)
                return false;

            if (pItem == null)
                return false;

            pItem.Remove();
            if(File.Exists(originalpath))
                File.Delete(originalpath);
            project.Save();
            // 3.添加引用
            EnvDTEHelper.AddFilesToProject(dte, projectName, nowpath);
            return true;
        }

        public static ProjectItem TravelGetItem(DTE dte, string projectName, string path)
        {
            // 提取项目路径，匹配只留下有效路径，分割并匹配，最终获得ProjectItem
            Project curPrj = GetProjectbyName(dte, projectName);
            if (curPrj == null)
                return null;

            IEnumerable<ProjectItem> htmlList = GetProjectItems(curPrj.ProjectItems);

            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Path.toFind= {0}", path));
            foreach (ProjectItem item in htmlList)
            {
                Property proper = item.Properties.Item("FullPath");
                string filepath = proper.Value.ToString();
                // Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Path= {0}", filepath));
                if (filepath == path)
                    return item;
            }
            return null;
        }

        public static String GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static IEnumerable<ProjectItem> GetProjectItems(EnvDTE.ProjectItems projectItems)
        {

            foreach (EnvDTE.ProjectItem item in projectItems)
            {
                yield return item;

                if (item.SubProject != null)
                {
                    foreach (EnvDTE.ProjectItem childItem in GetProjectItems(item.SubProject.ProjectItems))
                    {
                        string fullpath = GetFullPath(childItem);
                        Debug.WriteLine(fullpath);
                        //if (childItem.Kind == EnvDTE.Constants.vsProjectItemKindSolutionItems)
                            yield return childItem;
                    }
                }
                else
                {
                    foreach (EnvDTE.ProjectItem childItem in GetProjectItems(item.ProjectItems))
                    {
                        string fullpath = GetFullPath(childItem);
                        Debug.WriteLine(fullpath);
                        //if (childItem.Kind == EnvDTE.Constants.vsProjectItemKindSolutionItems)
                            yield return childItem;
                    }
                }
            }
        }

        public static void AddFilesToProject(DTE dte, string projectName, List<string> files)
        {
            try
            {
                // DTE dte = GetIntegrityServiceInstance();
                if (dte != null)
                {
                    foreach (EnvDTE.Project item in dte.Solution.Projects)
                    {
                        if (item.Name.Contains(projectName))
                        {
                            foreach (string file in files)
                                item.ProjectItems.AddFromFile(file);
                            item.Save();
                        }
                    }
                }
            }
            finally
            {
            }
        }
        // 添加已存在的文件到项目

        public static void AddFilesToProject(DTE dte, string projectName, string file)
        {
            try
            {
                // DTE dte = GetIntegrityServiceInstance();
                if (dte != null)
                {
                    foreach (EnvDTE.Project item in dte.Solution.Projects)
                    {
                        if (item.Name.Contains(projectName))
                        {
                            item.ProjectItems.AddFromFile(file);
                            item.Save();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strPrint = ex.Message;
            }
            finally
            {
            }
        }
        /// <summary>
        /// 可以使用文件夹的ProjectItems集合将组件添加到项目中的文件夹中。此代码使用先前检索到的ProjectItems集合来检索名为GenerateCode的文件夹，并使用集合的AddTemplate方法将类添加到文件夹：
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="projectName"></param>
        /// <param name="destFolder"></param>
        /// <param name="filename"></param>
        public static void AddFile2Project(DTE dte, string projectName, string destFolder, string filename, string extentionstr)
        {
            DTE2 dte2 = (DTE2)dte;
            Project prj = dte.Solution.Projects.Item(1);
            Solution2 sln2 = (Solution2)dte2.Solution;
            string templatepath = string.Empty;
            string htmlFileName = Path.GetFileName(filename);

            if(extentionstr == ".html")
                templatepath = sln2.GetProjectItemTemplate("aries.module.page.zip", "CSharp");
            //else
            //    templatepath = 
             //item.GetProjectItemTemplate("Interface.zip", "CSharp");
            // itemPath = dte.Solution.get_TemplatePath();// .GetProjectItemTemplate("Class.zip", "vbproj"); 
            if (dte != null)
            {
                foreach (EnvDTE.Project item in dte.Solution.Projects)
                {
                    if (item.Name.Contains(projectName))
                    {
                        ProjectItem fileInfo = item.ProjectItems.AddFromTemplate(templatepath, htmlFileName);
                        foreach (ProjectItem proItem in item.ProjectItems)
                        {//fileInfo != null
                            if (proItem.Name == htmlFileName)
                            {
                                Property proper = proItem.Properties.Item("FullPath");
                                string filepath = proper.Value.ToString() ;
                                // 复制一份到目标文件夹
                                File.Copy(filepath, filename);
                                // 复制文件添加到项目
                                AddFilesToProject(dte, projectName, filename);

                                proItem.Remove();
                                item.Save();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static Project GetProjectbyName(DTE dte, string projectName)
        {
            if (dte != null)
            {
                foreach (EnvDTE.Project item in dte.Solution.Projects)
                {
                    if (item.Name.Contains(projectName))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public static void GetSt(DTE dte)
        { 
            DTE2 dte2 =(DTE2)dte;
            Project proj = dte2.Solution.AddFromTemplate(@"C:\Program Files\Microsoft Visual Studio .NET\Vb7\VBWizards\ConsoleApplication\Templates\1033\ConsoleApplication.vbproj", "c:\temp2", "My New Project", true);
        }
        /* 
         string ItemTemplatePath = sln.GetProjectItemTemplate(“WebForm.zip”,  @”Web\VisualBasic”);
        ProjectItem pji = pjis.AddFromTemplate(ItemTemplatePath, “MyPage.aspx.vb”);
         * 
         可以使用文件夹的ProjectItems集合将组件添加到项目中的文件夹中。此代码使用先前检索到的ProjectItems集合来检索名为GenerateCode的文件夹，并使用集合的AddTemplate方法将类添加到文件夹：
         * 
        ProjectItem pji = pjis.Item(“GeneratedCode”).ProjectItems.AddFromTemplate(ItemTemplatePath,@”ConnectionManager.cs”);
         */
    }
}
