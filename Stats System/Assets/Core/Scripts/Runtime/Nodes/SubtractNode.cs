using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Editor.Nodes;
using Core.Runtime;
using UnityEngine;

namespace Core.Scripts.Runtime.Nodes
{
    public class SubtractNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode Minuend;
        [HideInInspector] public CodeFunctionNode Subtrahend;
        public override float Value => Minuend.Value - Subtrahend.Value;

        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                Minuend = null;
            }
            else
            {
                Subtrahend = null;
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                Minuend = child;
            }
            else
            {
                Subtrahend = child;
            }
        }

        public override ReadOnlyCollection<CodeFunctionNode> Children
        {
            get
            {
                List<CodeFunctionNode> nodes = new List<CodeFunctionNode>();
                if (Minuend != null)
                {
                    nodes.Add(Minuend);
                }

                if (Subtrahend != null)
                {
                    nodes.Add(Subtrahend);
                }

                return nodes.AsReadOnly();
            }
        }
    }
}