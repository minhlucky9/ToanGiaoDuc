using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Dev.QuangAnh.WallDragDrop;
using UnityEditor.UIElements;
using System;

namespace Dev.QuangAnh.WallDragDrop {
    [CustomEditor(typeof(DragObject))]
    public class DragObjectEditor : Editor {
        private VisualTreeAsset m_InspectorXML;

        public override VisualElement CreateInspectorGUI() {
            m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Dev/QuangAnh/DragDrop/Editor/DragObject.uxml"
            );

            VisualElement myInspector = m_InspectorXML.CloneTree();
            DragObject ctrl = (DragObject)target;

            Button btn = myInspector.Q<Button>("Ctrl");
            if (btn != null) {
                btn.clicked += () => ctrl.RestartSlotDrop();
            }
            PropertyField slotDrop = myInspector.Q<PropertyField>("slotDrop");

            Label label = myInspector.Q<Label>("customSlotDrop");
            ObjectField imageField = myInspector.Q<ObjectField>("image");
            FloatField maxSnapDistance = myInspector.Q<FloatField>("maxSnapDistance");

            ObjectField dragImageField = myInspector.Q<ObjectField>("imageDragObject");

            if (dragImageField != null) {
                dragImageField.objectType = typeof(Sprite);
                dragImageField.value = ctrl.imageDragObject;

                dragImageField.RegisterValueChangedCallback(evt => {
                    ctrl.imageDragObject = (Sprite)evt.newValue;

                    if (ctrl.imageComponent != null) {
                        ctrl.imageComponent.sprite = ctrl.imageDragObject;
                        ctrl.imageComponent.SetNativeSize();
                    }

                    EditorUtility.SetDirty(ctrl); 
                });
            }
            // Keep last known slotDrop reference
            var lastSlotDrop = ctrl.m_slotDrop;

            Action updateVisibility = () =>
            {
                if (ctrl.m_slotDrop != null) {
                    if (label != null) label.style.display = DisplayStyle.Flex;

                    if (imageField != null) {
                        imageField.style.display = DisplayStyle.Flex;
                        imageField.objectType = typeof(Sprite);
                        imageField.value = ctrl.m_slotDrop.image.sprite;

                        imageField.RegisterValueChangedCallback(evt =>
                        {
                            ctrl.m_slotDrop.image.sprite = (Sprite)evt.newValue;
                            ctrl.m_slotDrop.spriteImage = (Sprite)evt.newValue;
                            ctrl.m_slotDrop.image.SetNativeSize();
                            EditorUtility.SetDirty(ctrl);
                        });
                    }

                    if (maxSnapDistance != null) {
                        maxSnapDistance.style.display = DisplayStyle.Flex;
                        maxSnapDistance.value = ctrl.m_slotDrop.maxSnapDistance;

                        maxSnapDistance.RegisterValueChangedCallback(evt =>
                        {
                            ctrl.m_slotDrop.maxSnapDistance = evt.newValue;
                            EditorUtility.SetDirty(ctrl);
                        });
                    }
                } else {
                    if (label != null) label.style.display = DisplayStyle.None;
                    if (imageField != null) imageField.style.display = DisplayStyle.None;
                    if (maxSnapDistance != null) maxSnapDistance.style.display = DisplayStyle.None;
                }
            };

            // Initial run
            updateVisibility();

            // Monitor changes every 200ms
            myInspector.schedule.Execute(() =>
            {
                if (lastSlotDrop != ctrl.m_slotDrop) {
                    lastSlotDrop = ctrl.m_slotDrop;
                    updateVisibility();
                }
            }).Every(200);

            return myInspector;
        }

    }
}
