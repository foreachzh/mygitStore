using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Presentation;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Uml;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Layer;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Layer.Internal;
using Microsoft.VisualStudio.Modeling.ExtensionEnablement;
using Microsoft.VisualStudio.Uml.Classes;
using Microsoft.VisualStudio.GraphModel;
using Microsoft.VisualStudio.GraphModel.Algorithms;
using Microsoft.VisualStudio.GraphModel.CodeSchema;
using Microsoft.VisualStudio.GraphModel.Schemas;
using Microsoft.VisualStudio.GraphModel.Styles;

namespace CommandExtension2
{
    /*
    // Custom context menu command extension
    // See http://msdn.microsoft.com/en-us/library/ee329481(VS.110).aspx
    [Export(typeof(ICommandExtension))]
    [ClassDesignerExtension] // TODO: Add other diagram types if needed
    [LayerDesignerExtension]
    [UseCaseDesignerExtension]
    [SequenceDesignerExtension]
    [ComponentDesignerExtension]
    [ActivityDesignerExtension]
    // [GraphDesignerExtension]
    class CommandExtension : ICommandExtension
    {
        [Import]
        IDiagramContext context { get; set; }

        /// <summary>
        /// 当用户单击菜单项（如果它可见并已启用）时调用。
        /// </summary>
        /// <param name="command"></param>
        public void Execute(IMenuCommand command)
        {
            // TODO: Add the logic for your command extension here

            // The following example creates a new class in the model store
            // and displays it on the current diagram.
            IClassDiagram diagram = context.CurrentDiagram as IClassDiagram;
            IModelStore store = diagram.ModelStore;
            IPackage rootPackage = store.Root;
            IClass newClass = rootPackage.CreateClass();
            newClass.Name = "CommandExtension2";
            diagram.Display(newClass);
        }
        /// <summary>
        /// 当用户在关系图中右击时调用。
        ///此方法不应更改模型。
        ///使用 DiagramContext.CurrentDiagram.SelectedShapes 确定是否显示并启用该命令。
        ///设置：
        ///- command.Visible 到true如果该命令必须出现在菜单中用户右键单击关系图中时
        ///- command.Enabled 到true如果用户可以单击菜单中的命令
        ///- command.Text 若要动态设置菜单标签
        /// </summary>
        /// <param name="command"></param>
        public void QueryStatus(IMenuCommand command)
        {
            if (context.CurrentDiagram != null &&
                context.CurrentDiagram.ChildShapes.Count() > 0)
            {
                command.Enabled = true;
            }
            else
            {
                command.Enabled = false;
            }
            // TODO: Add logic to control the display of your menu item

            // The following example will disable the command extension unless the user selected
            // a class shape.
            //
            //   IShape selshape = context.CurrentDiagram.SelectedShapes.FirstOrDefault();
            //   command.Enabled = selshape.Element is IClass;
            //
            // Note: Setting command.Visible=false can have unintended interactions with other extensions.
        }

        /// <summary>
        /// 返回菜单项的标签。
        /// </summary>
        public string Text
        {
            get { return "CommandExtension2"; }
        }
    }
    */
}
