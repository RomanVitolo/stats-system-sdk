using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Editor.Nodes;
using Core.Runtime;
using UnityEngine;

namespace Core.Scripts.Runtime.Nodes
{
    public class PowerNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode Exponent;
        [HideInInspector] public CodeFunctionNode @Base;
        public override float Value => (float)Math.Pow(@Base.Value, Exponent.Value);

        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                @Base = null;
            }
            else
            {
                Exponent = null;
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                @Base = child;
            }
            else
            {
                Exponent = child;
            }
        }

        public override ReadOnlyCollection<CodeFunctionNode> Children
        {
            get
            {
                List<CodeFunctionNode> nodes = new List<CodeFunctionNode>();
                if (@Base != null)
                {
                    nodes.Add(@Base);
                }

                if (@Exponent != null)
                {
                    nodes.Add(@Exponent);
                }

                return nodes.AsReadOnly();
            }
        }
    }
}