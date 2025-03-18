using LearningGame.Editor;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR

namespace LearningGame.Editor
{

    public class CreateNewSubject : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        private Label _label;
        private TextField _id_input;
        private TextField _name_input;
        private ObjectField _sprite_input;
        private VisualElement _sprite_viewer;
        private Button _submit_btn;

        public static void ShowWindow()
        {
            CreateNewSubject wnd = GetWindow<CreateNewSubject>();
            wnd.titleContent = new GUIContent("CreateNewSubject");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
            root.Add(labelFromUXML);

            //
            BindVisualElements();
            BindVisualElementEvent();
        }

        public void BindVisualElements()
        {
            _label = rootVisualElement.Q<Label>("label");
            _id_input = rootVisualElement.Q<TextField>("id_input");
            _name_input = rootVisualElement.Q<TextField>("name_input");
            _sprite_input = rootVisualElement.Q<ObjectField>("image_input");
            _sprite_viewer = rootVisualElement.Q<VisualElement>("image_viewer");
            _submit_btn = rootVisualElement.Q<Button>("submit_btn");
            //
            _label.text = "Subject Information";
            _id_input.labelElement.text = "Subject id";
            _name_input.labelElement.text = "Subject name";
            _sprite_input.labelElement.text = "Subject image";
        }

        public void BindVisualElementEvent()
        {
            _sprite_input.RegisterValueChangedCallback(evt =>
            {
                _sprite_viewer.style.backgroundImage = new StyleBackground(evt.newValue as Sprite);
            });

            _submit_btn.clicked += () =>
            {
                if (LearningGameSOEditor.instance)
                {
                    string subjectId = _id_input.value.ToLower();
                    if( subjectId == "" || LearningGameSOEditor.instance.isSubjectIdExist(subjectId) )
                    {
                        
                        Debug.Log("exist");
                    } else
                    {
                        SubjectSO subject = ScriptableObject.CreateInstance<SubjectSO>();
                        subject.subjectId = subjectId;
                        subject.subjectName = _name_input.value;
                        subject.subjectImage = _sprite_input.value as Sprite;
                        //save to editor
                        
                        string path = "Assets/Resources/" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(subjectId);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        AssetDatabase.CreateAsset(subject, path + "/" + subjectId + ".asset");
                        //
                        LearningGameSOEditor.instance.AddSubjectToList(subject);
                        Close();
                    }
                } else
                {
                    Close();
                }
            };
        }
    }

}

#endif