using System;
using System.Collections.Generic;
using Core.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Core.Editor.Nodes
{
    public class NodeView : Node
    {
        public CodeFunctionNode Node;
        public List<Port> inputs = new List<Port>();
        public Port output;
        public Action<NodeView> NodeSelected;

        protected Port CreateOutputPort(string portName = "", Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(float));
            outputPort.portName = portName;
            outputContainer.Add(outputPort);
            RefreshPorts();
            return outputPort;
        }
        protected Port CreateInputPort(string portName = "", Port.Capacity capacity = Port.Capacity.Single)
        {
            Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(float));
            input.portName = portName;
            inputContainer.Add(input);
            RefreshPorts();
            return input;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            NodeSelected?.Invoke(this);
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.Position.x = newPos.xMin;
            Node.Position.y = newPos.yMin;
            EditorUtility.SetDirty(Node);
        }
    }
    
    
}