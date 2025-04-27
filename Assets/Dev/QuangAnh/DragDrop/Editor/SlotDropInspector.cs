using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Dev.QuangAnh.WallDragDrop;
using System.Collections.Generic;
using UnityEditor.Search;


namespace Dev.QuangAnh.WallDragDrop {

    [CustomEditor(typeof(SlotDrop))]
    public class SlotDropInspector : Editor {
        private VisualTreeAsset m_InspectorXML;

        public override VisualElement CreateInspectorGUI() {
            m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Dev/QuangAnh/DragDrop/Editor/SlotDropInspector.uxml"
            );

            VisualElement myInspector = m_InspectorXML.CloneTree();

            SlotDrop ctrl = (SlotDrop)target;


            return myInspector;
        }

    }

}

