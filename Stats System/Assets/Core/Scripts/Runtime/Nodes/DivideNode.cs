using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Editor.Nodes;
using Core.Runtime;
using UnityEngine;

namespace Core.Scripts.Runtime.Nodes
{
    public class DivideNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode Dividend;
        [HideInInspector] public CodeFunctionNode Divisor;
        public override float Value => Dividend.Value / Divisor.Value;

        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                Dividend = null;
            }
            else
            {
                Divisor = null;
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                Dividend = child;
            }
            else
            {
                Divisor = child;
            }
        }

        public override ReadOnlyCollection<CodeFunctionNode> Children
        {
            get
            {
                List<CodeFunctionNode> nodes = new List<CodeFunctionNode>();
                if (Dividend != null)
                {
                    nodes.Add(Dividend);
                }

                if (Divisor != null)
                {
                    nodes.Add(Divisor);
                }

                return nodes.AsReadOnly();
            }
        }
    }
}