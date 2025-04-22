using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LinkPair
{
    [CustomEditor(typeof(LinkPairManager))]
    public class LinkPairManagerInspector : Editor
    {
        public VisualTreeAsset inspectorXML;

        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement myInspector = new VisualElement();

            // Add a simple label.
            myInspector.Add(new Label("This is a custom Inspector"));

            // Load the UXML file.
            inspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Dev/Hai/LinkPairGame/InspectorLayout/Link_Pair_Manager_Inspector_UXML.uxml");

            // Instantiate the UXML.
            myInspector = inspectorXML.Instantiate();

            // Return the finished Inspector UI.
            return myInspector;
        }

    }

}