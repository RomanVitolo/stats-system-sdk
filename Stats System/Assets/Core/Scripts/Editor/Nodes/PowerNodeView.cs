﻿using Core.Scripts.Runtime.Nodes;
using UnityEngine;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(PowerNode))]
    [Title("Math", "Power")]
    public class PowerNodeView : NodeView
    {
        public PowerNodeView()
        {
            title = "Power";
            Node = ScriptableObject.CreateInstance<PowerNode>();
            output = CreateOutputPort();
            inputs.Add(CreateInputPort("A"));
            inputs.Add(CreateInputPort("B"));
        }
    }
}