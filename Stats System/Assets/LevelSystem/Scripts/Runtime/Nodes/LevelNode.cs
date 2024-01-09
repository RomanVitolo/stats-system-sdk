using Core.Runtime;

namespace LevelSystem.Nodes
{
    public class LevelNode : CodeFunctionNode
    {
        public ILevelable Levelable;
        public override float Value => Levelable.Level;
    }
}