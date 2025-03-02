using NodeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardView
{
    public partial class Form1 : Form
    {
        private NodeEditorPanel nodeEditor;

        public Form1()
        {
            InitializeComponent();

            InitializeNodeEditor();
        }


        private void InitializeNodeEditor()
        {
            // Create the node editor panel
            nodeEditor = new NodeEditorPanel();
            nodeEditor.Dock = DockStyle.Fill;
            this.Controls.Add(nodeEditor);

            // Create test functions
            var number1 = new NumberFunction();
            var number2 = new NumberFunction();
            var addFunction = new AddFunction();
            var multiplyFunction = new MultiplyFunction();
            var colorFunction = new ColorFunction();
            var textFunction = new TextFunction();
            var toStringFunction = new ToStringFunction();

            // Add functions to editor
            nodeEditor.AddFunction(number1);
            nodeEditor.AddFunction(number2);
            nodeEditor.AddFunction(addFunction);
            nodeEditor.AddFunction(multiplyFunction);
            nodeEditor.AddFunction(colorFunction);
            nodeEditor.AddFunction(textFunction);
            nodeEditor.AddFunction(toStringFunction);

            // Position the nodes
            foreach (Control control in nodeEditor.Controls)
            {
                if (control is FunctionNodeControl nodeControl)
                {
                    switch (nodeControl.Function.Name)
                    {
                        case "Number" when nodeControl.Function == number1:
                            nodeControl.Location = new Point(100, 20);
                            nodeControl.Controls.Add(number1.GetControl()); // Add UserControl
                            break;
                        case "Number" when nodeControl.Function == number2:
                            nodeControl.Location = new Point(100, 100);
                            nodeControl.Controls.Add(number2.GetControl()); // Add UserControl
                            break;
                        case "Add":
                            nodeControl.Location = new Point(300, 60);
                            break;
                        case "Multiply":
                            nodeControl.Location = new Point(300, 160);
                            break;
                        case "Color":
                            nodeControl.Location = new Point(100, 180);
                            nodeControl.Controls.Add(colorFunction.GetControl()); // Add UserControl
                            break;
                        case "Text Display":
                            nodeControl.Location = new Point(400, 60);
                            // Center the TextFunctionControl
                            var textControl = textFunction.GetControl();
                            textControl.Location = new Point(
                                (nodeControl.Width - textControl.Width) / 2,
                                (nodeControl.Height - textControl.Height) / 2
                            );
                            nodeControl.Controls.Add(textControl); // Add UserControl
                            break;
                        case "To String":
                            nodeControl.Location = new Point(300, 250);
                            break;
                    }
                }
            }
        }
    }
}
