using System.Collections.Generic; // for IEnumerable  
using System.ComponentModel.Composition;
// for [Import], [Export]  
using System.Linq; // for IEnumerable extensions  
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Presentation;
// for IDiagramContext  
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Uml;
using Microsoft.VisualStudio.ArchitectureTools.Extensibility.Layer;
// for designer extension attributes  
using Microsoft.VisualStudio.Modeling.Diagrams;
// for ShapeElement  
using Microsoft.VisualStudio.Modeling.ExtensionEnablement;
// for IGestureExtension, ICommandExtension, ILinkedUndoContext  
using Microsoft.VisualStudio.Uml.Classes;
// for class diagrams, packages  

namespace CommandExtension23
{
    
    // Declare the class as an MEF component:  
    [Export(typeof(ICommandExtension))]
    [ClassDesignerExtension] // TODO: Add other diagram types if needed
    [LayerDesignerExtension]
    [UseCaseDesignerExtension]
    [SequenceDesignerExtension]
    [ComponentDesignerExtension]
    [ActivityDesignerExtension]
    // Add more ExportMetadata attributes to make  
    // the command appear on diagrams of other types.  
    public class NameSwapperExtension : ICommandExtension
    {
        // MEF required interfaces:  
        [Import]
        public IDiagramContext Context { get; set; }
        [Import]
        public ILinkedUndoContext LinkedUndoContext { get; set; }

        /// <summary>  
        /// Swap the names of the currently selected elements.  
        /// </summary>  
        /// <param name="command"></param>  
        public void Execute(IMenuCommand command)
        {
            // Get selected shapes that are IClassifiers -  
            // IClasses, IInterfaces, IEnumerators.  
            var selectedShapes = Context.CurrentDiagram
             .GetSelectedShapes<IClassifier>();
            if (selectedShapes.Count() < 2) return;

            // Get model elements displayed by shapes.  
            IClassifier firstElement = selectedShapes.First().Element;
            IClassifier lastElement = selectedShapes.Last().Element;

            // Do the swap in a transaction so that user  
            // cannot undo one change without the other.  
            using (ILinkedUndoTransaction transaction =
            LinkedUndoContext.BeginTransaction("Swap names"))
            {
                string firstName = firstElement.Name;
                firstElement.Name = lastElement.Name;
                lastElement.Name = firstName;
                transaction.Commit();
            }
        }

        /// <summary>  
        /// Called by Visual Studio to determine whether  
        /// menu item should be visible and enabled.  
        /// </summary>  
        public void QueryStatus(IMenuCommand command)
        {
            int selectedClassifiers = Context.CurrentDiagram
             .GetSelectedShapes<IClassifier>().Count();
            command.Visible = selectedClassifiers > 0;
            command.Enabled = selectedClassifiers == 2;
        }

        /// <summary>  
        /// Name of the menu command.  
        /// </summary>  
        public string Text
        {
            get { return "Swap Names"; }
        }
    }
}
