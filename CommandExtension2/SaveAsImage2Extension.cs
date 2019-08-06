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

using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Uml.AuxiliaryConstructs;  

namespace CommandExtension21
{
    [Export(typeof(ICommandExtension))]
    [ClassDesignerExtension] // TODO: Add other diagram types if needed
    [LayerDesignerExtension]
    [UseCaseDesignerExtension]
    [SequenceDesignerExtension]
    [ComponentDesignerExtension]
    [ActivityDesignerExtension]
    public class SaveAsImage2Extension : ICommandExtension
    {
        [Import(typeof(IDiagramContext))]
        public IDiagramContext context { get; set; }
        public ILinkedUndoContext LinkedUndoContext { get; set; }


        /// <summary>
        /// 返回菜单项的标签。
        /// </summary>
        public string Text
        {
            get
            {
                return "Save as image";
            }
        }

        /// <summary>
        /// 当用户单击菜单项（如果它可见并已启用）时调用。
        /// </summary>
        /// <param name="command"></param>
        public void Execute(IMenuCommand command)
        {
            var dslDiagram = context.CurrentDiagram.GetObject<Microsoft.VisualStudio.Modeling.Diagrams.Diagram>();

            if (dslDiagram != null)
            {
                var dialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = "image.bmp",
                    Filter = "Bitmap ( *.bmp )|*.bmp|" +
                                          "JPEG File ( *.jpg )|*.jpg|" +
                                          "Enhanced Metafile (*.emf )|*.emf|" +
                                          "Portable Network Graphic ( *.png )|*.png",
                    FilterIndex = 1,
                    Title = "Save Diagram to Image"
                };

                if (dialog.ShowDialog() == DialogResult.OK &&
                    !string.IsNullOrEmpty(dialog.FileName))
                {
                    var bitmap = dslDiagram.CreateBitmap(dslDiagram.NestedChildShapes,
                                 Diagram.CreateBitmapPreference.FavorClarityOverSmallSize);
                    bitmap.Save(dialog.FileName, GetImageType(dialog.FilterIndex));
                }

                IDiagram diagram = this.context.CurrentDiagram;
                //foreach (IShape<IElement> shape in diagram.GetSelectedShapes<IElement>())
                //{ 
                //    IElement element = shape.Element; 
                //}
                IModelStore modelStore = diagram.ModelStore;
                IModel model = modelStore.Root;
                IEnumerable<IElement> eList = model.OwnedElements;
                foreach (IElement element in model.OwnedElements) {
                    //string str = element.Shapes();
                }  
                //foreach (IElement element in modelStore.AllInstances<IClass>())
                //{

                //}  
            }
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
        }

        private static ImageFormat GetImageType(int filterIndex)
        {
            var result = ImageFormat.Bmp;

            switch (filterIndex)
            {
                case 2:
                    result = ImageFormat.Jpeg;
                    break;
                case 3:
                    result = ImageFormat.Emf;
                    break;
                case 4:
                    result = ImageFormat.Png;
                    break;
            }
            return result;
        }
    }
}
