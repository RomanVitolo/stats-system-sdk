using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Editor.Nodes;
using Core.Runtime;
using UnityEngine;

namespace Core.Scripts.Runtime.Nodes
{
    public class MultiplyNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode FactorA;
        [HideInInspector] public CodeFunctionNode FactorB;
        public override float Value => FactorA.Value * FactorB.Value;

        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                FactorA = null;
            }
            else
            {
                FactorB = null;
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                FactorA = child;
            }
            else
            {
                FactorB = child;
            }
        }

        public override ReadOnlyCollection<CodeFunctionNode> Children
        {
            get
            {
                List<CodeFunctionNode> nodes = new List<CodeFunctionNode>();
                if (FactorA != null)
                {
                    nodes.Add(FactorA);
                }

                if (FactorB != null)
                {
                    nodes.Add(FactorB);
                }

                return nodes.AsReadOnly();
            }
        }
    }
}