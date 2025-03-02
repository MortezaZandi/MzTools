using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace NodeEditor
{
    public class NodeEditorPanel : Panel
    {
        private List<FunctionNodeControl> nodes = new List<FunctionNodeControl>();
        private List<ConnectionInfo> connections = new List<ConnectionInfo>();
        private IFunctionPort currentPort;
        private Point currentMousePos;
        private IFunctionPort disconnectingPort;
        private ConnectionInfo connectionToRemove;

        public NodeEditorPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint, true);
            
            BackColor = Color.FromArgb(40, 40, 40);
        }

        public void AddFunction(IFunction function)
        {
            var nodeControl = new FunctionNodeControl(function);
            nodeControl.Location = new Point(50, 50);
            nodeControl.PortClicked += NodeControl_PortClicked;
            nodes.Add(nodeControl);
            Controls.Add(nodeControl);
        }

        private void NodeControl_PortClicked(object sender, IFunctionPort port)
        {
            if (currentPort == null)
            {
                if (port.Connections.Any() && !port.IsInput)
                {
                    disconnectingPort = port;
                    currentPort = null;
                }
                else
                {
                    currentPort = port;
                    disconnectingPort = null;
                }
                return;
            }

            if (currentPort != port && CanConnect(currentPort, port))
            {
                if (port.IsInput && port.Connections.Any())
                {
                    RemoveConnection(port);
                }
                Connect(currentPort, port);
            }

            currentPort = null;
            disconnectingPort = null;
            Invalidate();
        }

        private bool CanConnect(IFunctionPort port1, IFunctionPort port2)
        {
            // Allow connection if both ports are of the same type
            if (port1.DataType == port2.DataType)
            {
                return port1.IsInput != port2.IsInput && port1.ParentFunction != port2.ParentFunction;
            }

            // Allow connection if one port is of type object
            if (port1.DataType == typeof(object) || port2.DataType == typeof(object))
            {
                return port1.IsInput != port2.IsInput && port1.ParentFunction != port2.ParentFunction;
            }

            // Allow connection between compatible numeric types
            if ((port1.DataType == typeof(int) && port2.DataType == typeof(long)) ||
                (port1.DataType == typeof(long) && port2.DataType == typeof(int)) ||
                (port1.DataType == typeof(float) && port2.DataType == typeof(double)) ||
                (port1.DataType == typeof(double) && port2.DataType == typeof(float)))
            {
                return port1.IsInput != port2.IsInput && port1.ParentFunction != port2.ParentFunction;
            }

            return false; // Default case: disallow connection
        }

        private void Connect(IFunctionPort port1, IFunctionPort port2)
        {
            var input = port1.IsInput ? port1 : port2;
            var output = port1.IsInput ? port2 : port1;

            input.Connections.Add(output);
            output.Connections.Add(input);
            connections.Add(new ConnectionInfo(output, input));

            output.ParentFunction.Calculate();
        }

        private void RemoveConnection(IFunctionPort port)
        {
            var connection = connections.FirstOrDefault(c => 
                c.Input == port || c.Output == port);
            
            if (connection != null)
            {
                connection.Input.Connections.Remove(connection.Output);
                connection.Output.Connections.Remove(connection.Input);
                
                connections.Remove(connection);
                
                connection.Input.ParentFunction.Calculate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (currentPort != null || disconnectingPort != null)
            {
                currentMousePos = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            
            if (disconnectingPort != null)
            {
                var hitNode = nodes.FirstOrDefault(n => n.ClientRectangle.Contains(e.Location));
                if (hitNode == null)
                {
                    RemoveConnection(disconnectingPort);
                }
                disconnectingPort = null;
                Invalidate();
            }
            
            currentPort = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw existing connections
            foreach (var connection in connections)
            {
                DrawConnection(e.Graphics, connection);
            }

            // Draw current connection being made or disconnected
            if (currentPort != null)
            {
                DrawConnectionLine(e.Graphics, 
                    GetPortPosition(currentPort),
                    currentMousePos,
                    GetPortColor(currentPort.DataType));
            }
            else if (disconnectingPort != null)
            {
                DrawConnectionLine(e.Graphics, 
                    GetPortPosition(disconnectingPort),
                    currentMousePos,
                    GetPortColor(disconnectingPort.DataType));
            }
        }

        private void DrawConnection(Graphics g, ConnectionInfo connection)
        {
            var startPos = GetPortPosition(connection.Output);
            var endPos = GetPortPosition(connection.Input);
            DrawConnectionLine(g, startPos, endPos, GetPortColor(connection.Output.DataType));
        }

        private void DrawConnectionLine(Graphics g, Point start, Point end, Color color)
        {
            using (var pen = new Pen(color, 2f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                var cp1 = new Point(start.X + 50, start.Y);
                var cp2 = new Point(end.X - 50, end.Y);

                g.DrawBezier(pen, start, cp1, cp2, end);
            }
        }

        private Point GetPortPosition(IFunctionPort port)
        {
            var node = nodes.First(n => n.Function == port.ParentFunction);
            var portBounds = node.GetPortBounds(port);
            return new Point(
                port.IsInput ? portBounds.Left : portBounds.Right,
                portBounds.Top + portBounds.Height / 2);
        }

        private Color GetPortColor(Type dataType)
        {
            if (dataType == typeof(float)) return Color.LightGreen;
            if (dataType == typeof(Color)) return Color.Pink;
            if (dataType == typeof(bool)) return Color.Yellow;
            return Color.White;
        }

        private class ConnectionInfo
        {
            public IFunctionPort Output { get; }
            public IFunctionPort Input { get; }

            public ConnectionInfo(IFunctionPort output, IFunctionPort input)
            {
                Output = output;
                Input = input;
            }
        }
    }
} 