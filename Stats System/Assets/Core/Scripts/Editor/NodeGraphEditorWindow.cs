using System;
using System.Collections.Generic;
using System.Linq;
using Core.Editor.Nodes;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Core.Editor
{
    public class NodeGraphEditorWindow : EditorWindow, ISearchWindowProvider
    {
        private NodeGraph m_NodeGraph;
        private NodeGraphView m_NodeGraphView;
        private VisualElement m_LeftPanel;
        private Texture2D m_Icon;
        private UnityEditor.Editor m_Editor;

        public static void ShowWindow(NodeGraph nodeGraph)
        {
            NodeGraphEditorWindow window = GetWindow<NodeGraphEditorWindow>();
            window.SelectNodeGraph(nodeGraph);
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("NodeGraph");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceId) is NodeGraph nodeGraph)
            {
              ShowWindow(nodeGraph);
              return true;
            }

            return false;
        }
        
        private void OnEnable()
        {       
            m_Icon = new Texture2D(1, 1);
            m_Icon.SetPixel(0, 0, new Color(0, 0, 0, 0));
            m_Icon.Apply();
        }
        public void CreateGUI()
        {                                               
            VisualElement root = rootVisualElement;         
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Core/Scripts/Editor/NodeGraphEditorWindow.uxml");
            visualTree.CloneTree(root);
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Core/Scripts/Editor/NodeGraphEditorWindow.uss");             
            root.styleSheets.Add(styleSheet);
            
            m_LeftPanel = root.Q("left-panel");
            m_NodeGraphView = root.Q<NodeGraphView>();
            m_NodeGraphView.nodeCreationRequest += OnRequestNodeCreation;
            m_NodeGraphView.NodeSelected = OnNodeSelected;
        }

        private void OnNodeSelected(NodeView nodeView)
        {
            m_LeftPanel.Clear();
            DestroyImmediate(m_Editor);
            m_Editor = UnityEditor.Editor.CreateEditor(nodeView.Node);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (m_Editor && m_Editor.target)
                {
                   m_Editor.OnInspectorGUI(); 
                }
            });
            m_LeftPanel.Add(container);
        }

        private void OnRequestNodeCreation(NodeCreationContext context)
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), this);
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is NodeGraph nodeGraph)
            {
                SelectNodeGraph(nodeGraph);
            }
        }

        private void SelectNodeGraph(NodeGraph nodeGraph)
        {
            m_NodeGraph = nodeGraph;
            m_NodeGraphView.PopulateView(m_NodeGraph);
        }
        
        internal struct NodeEntry
        {
            public string[] Title;
            public NodeView NodeView;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var nodeEntries = new List<NodeEntry>();

            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(
                assembly => assembly.GetTypes()).Where(type =>
                typeof(NodeView).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract &&
                type != typeof(NodeView) && type != typeof(ResultNodeView)).ToArray();
            foreach (Type type in types)
            {
                if (type.GetCustomAttributes(typeof(TitleAttribute), false) is TitleAttribute[] attrs &&
                    attrs.Length > 0)
                {
                    var node = (NodeView) Activator.CreateInstance(type);
                    nodeEntries.Add(new NodeEntry
                    {
                        NodeView = node,
                        Title = attrs[0].Title
                    });
                }          
            }
            var groups = new List<string>();

            // First item in the tree is the title of the window.
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
            };

            foreach (var nodeEntry in nodeEntries)
            {
                // `createIndex` represents from where we should add new group entries from the current entry's group path.
                var createIndex = int.MaxValue;

                // Compare the group path of the current entry to the current group path.
                for (var i = 0; i < nodeEntry.Title.Length - 1; i++)
                {
                    var group = nodeEntry.Title[i];
                    if (i >= groups.Count)
                    {
                        // The current group path matches a prefix of the current entry's group path, so we add the
                        // rest of the group path from the currrent entry.
                        createIndex = i;
                        break;
                    }
                    if (groups[i] != group)
                    {
                        // A prefix of the current group path matches a prefix of the current entry's group path,
                        // so we remove everyfrom from the point where it doesn't match anymore, and then add the rest
                        // of the group path from the current entry.
                        groups.RemoveRange(i, groups.Count - i);
                        createIndex = i;
                        break;
                    }
                }

                // Create new group entries as needed.
                // If we don't need to modify the group path, `createIndex` will be `int.MaxValue` and thus the loop won't run.
                for (var i = createIndex; i < nodeEntry.Title.Length - 1; i++)
                {
                    var group = nodeEntry.Title[i];
                    groups.Add(group);
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(group)) { level = i + 1 });
                }

                // Finally, add the actual entry.
                tree.Add(new SearchTreeEntry(new GUIContent(nodeEntry.Title.Last(), m_Icon)) { level = nodeEntry.Title.Length, userData = nodeEntry });
            }

            return tree;        
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            var nodeEntry = (NodeEntry)entry.userData;
            var nodeView = nodeEntry.NodeView;
            nodeView.Node.name = nodeEntry.Title[nodeEntry.Title.Length - 1];
            Vector2 worldMousePosition = context.screenMousePosition - position.position;
            Vector2 mousePosition = m_NodeGraphView.contentViewContainer.WorldToLocal(worldMousePosition);
            nodeView.Node.Guid = GUID.Generate().ToString();
            nodeView.Node.Position = mousePosition;
            nodeView.viewDataKey = nodeView.Node.Guid;
            nodeView.style.left = mousePosition.x;
            nodeView.style.top = mousePosition.y;
            m_NodeGraph.AddNode(nodeView.Node);
            m_NodeGraphView.AddNodeView(nodeView);
            return true;
        }
    }
}
