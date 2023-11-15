using UnityEngine;

namespace Core.Runtime
{         
    public abstract class AbstractNode : ScriptableObject
    {
        [HideInInspector] public Vector2 Position;
        [HideInInspector] public string Guid;
    }
}