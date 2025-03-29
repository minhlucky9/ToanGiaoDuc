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

    public class CreateNewTopic : EditorWindow
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
            CreateNewTopic wnd = GetWindow<CreateNewTopic>();
            wnd.titleContent = new GUIContent("CreateNewTopic");
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
            _label.text = "Topic Information";
            _id_input.labelElement.text = "Topic id";
            _name_input.labelElement.text = "Topic name";
            _sprite_input.labelElement.text = "Topic image";
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
                    SubjectSO currentSubject = LearningGameSOEditor.instance.selectedSubject;
                    string topicId = _id_input.value.ToLower();
                    if(topicId == "" || LearningGameSOEditor.instance.isTopicIdExist(topicId) )
                    {
                        Debug.Log("exist");
                    } else
                    {
                        TopicSO topic = ScriptableObject.CreateInstance<TopicSO>();
                        topic.subjectId = currentSubject.subjectId;
                        topic.topicId = topicId;
                        topic.topicName = _name_input.value;
                        topic.topicImage = _sprite_input.value as Sprite;

                        //save to editor
                        string path = "Assets/Resources/" 
                            + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(topic.subjectId)
                            + "/" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(topicId);

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        AssetDatabase.CreateAsset(topic, path + "/" + topicId + ".asset");
                        //
                        LearningGameSOEditor.instance.AddTopicToList(topic);
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