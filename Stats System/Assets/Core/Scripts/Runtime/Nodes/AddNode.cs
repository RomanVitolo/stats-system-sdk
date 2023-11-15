using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Editor.Nodes;
using Core.Runtime;
using UnityEngine;

namespace Core.Scripts.Runtime.Nodes
{
    public class AddNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode AddendA;
        [HideInInspector] public CodeFunctionNode AddendB;

        public override float Value => AddendA.Value + AddendB.Value;         
        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                AddendA = null;
            }
            else
            {
                AddendB = null;
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                AddendA = child;
            }
            else
            {
                AddendB = child;
            }
        }

        public override ReadOnlyCollection<CodeFunctionNode> Children
        {
            get
            {
                List<CodeFunctionNode> nodes = new List<CodeFunctionNode>();
                if (AddendA != null)
                {
                   nodes.Add(AddendA);  
                }

                if (AddendB != null)
                {
                    nodes.Add(AddendB);
                }

                return nodes.AsReadOnly();
            }
        }
    }
}