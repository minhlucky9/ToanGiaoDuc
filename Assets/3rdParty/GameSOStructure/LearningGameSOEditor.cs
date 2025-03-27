using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR

namespace LearningGame.Editor
{
    public class LearningGameSOEditor : EditorWindow
    {
        public static LearningGameSOEditor instance;
        [SerializeField]
        private VisualTreeAsset _tree;

        SubjectSO[] allSubjects;
        Dictionary<string, SubjectSO> subjectDict;
        public SubjectSO selectedSubject;

        TopicSO[] allTopics;
        Dictionary<string, TopicSO> topicDict;
        private TopicSO selectedTopic;

        LearningGameSO[] allGames;
        Dictionary<string, LearningGameSO> gameDict;
        private LearningGameSO seletedGame;

        private ObjectField _spriteInput;
        private DropdownField _subjectDropdown;
        private VisualElement _subjectSpriteViewer;
        private TextField _subjectName;
        private Button _createSubjectBtn;

        private VisualElement _topicGroup;
        private ObjectField _spriteTopic;
        private VisualElement _topicSpriteViewer;
        private DropdownField _topicDropdown;
        private TextField _topicName;
        private Button _createTopicBtn;

        private VisualElement _gameGroup;
        private DropdownField _gameDropdown;
        private DropdownField _modeDropdown;
        private TextField _gameIdInput;
        private Button _createGameBtn;
        private Button _viewGameBtn;

        [MenuItem("Tools/Learning Game SO Editor")]
        public static void ShowWindow()
        {
            GetWindow<LearningGameSOEditor>("Learning Game SO Editor");
        }

        private void OnEnable()
        {
            instance = this;
            // loads all QuestInfo Scriptable Objects under the Assets/Resources/Quests folder
            allSubjects = Resources.LoadAll<SubjectSO>("");
            subjectDict = new Dictionary<string, SubjectSO>();
            for(int i = 0; i < allSubjects.Length; i ++)
            {
                subjectDict.Add(allSubjects[i].subjectId, allSubjects[i]);
            }

            //
            allTopics = Resources.LoadAll<TopicSO>("");
            topicDict = new Dictionary<string, TopicSO>();
            for (int i = 0; i < allTopics.Length; i++)
            {
                topicDict.Add(allTopics[i].subjectId + "_" + allTopics[i].topicId, allTopics[i]);
            }

            //
            allGames = Resources.LoadAll<LearningGameSO>("");
            gameDict = new Dictionary<string, LearningGameSO>();
            for (int i = 0; i < allGames.Length; i++)
            {
                gameDict.Add(allGames[i].topicId + "_" + allGames[i].gameId, allGames[i]);
            }
        }

        private void OnDisable()
        {
            instance = null;
        }

        private void CreateGUI()
        {
            _tree.CloneTree(rootVisualElement);
            BindVisualElement();

            if(selectedSubject == null)
            {
                _topicGroup.visible = false;
            }

            if(selectedTopic == null)
            {
                _gameGroup.visible = false;
            }

            _modeDropdown.choices =  new List<string>( Enum.GetNames(typeof( LearningGameType )) );

            //setup event for subject information
            _subjectDropdown.choices = GetListSubjectId(allSubjects);
            _subjectDropdown.RegisterValueChangedCallback(evt =>
            {
                InitSelectedSubject(evt.newValue);
            });

            _subjectName.RegisterValueChangedCallback(evt =>
            {
                if(selectedSubject)
                {
                    selectedSubject.subjectName = evt.newValue;
                }
            });

            _spriteInput.RegisterValueChangedCallback(evt =>
            {
                if(selectedSubject)
                {
                    selectedSubject.subjectImage = evt.newValue as Sprite;
                    _subjectSpriteViewer.style.backgroundImage = new StyleBackground(selectedSubject.subjectImage);
                }
            });

            _createSubjectBtn.clicked += () => {
                CreateNewSubject.ShowWindow();
            };

            //setup event for topic information

            _topicDropdown.RegisterValueChangedCallback(evt =>
            {
                InitSelectedTopic(evt.newValue);
            });

            _topicName.RegisterValueChangedCallback(evt =>
            {
                if (selectedTopic)
                {
                    selectedTopic.topicName = evt.newValue;
                }
            });

            _spriteTopic.RegisterValueChangedCallback(evt =>
            {
                if (selectedTopic)
                {
                    selectedTopic.topicImage = evt.newValue as Sprite;
                    _topicSpriteViewer.style.backgroundImage = new StyleBackground(selectedTopic.topicImage);
                }
            });

            _createTopicBtn.clicked += () =>
            {
                CreateNewTopic.ShowWindow();
            };

            // setup event for game information

            _gameDropdown.RegisterValueChangedCallback(evt =>
            {
                InitSelectedGame(evt.newValue);
            });

            _viewGameBtn.clicked += () =>
            {
                Selection.activeObject = seletedGame;
            };

            _createGameBtn.clicked += () =>
            {
                string game_id = _gameIdInput.value;

                if( !isGameIdExist(selectedTopic.topicId + "_" + game_id) )
                {
                    LearningGameSO gameSO = CreateInstance<LearningGameSO>();
                    gameSO.topicId = selectedTopic.topicId;
                    gameSO.gameId = game_id;
                    gameSO.gameType = (LearningGameType)Enum.Parse(typeof(LearningGameType), _modeDropdown.value);
                    //save to editor
                    string path = "Assets/Resources/" 
                        + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(selectedSubject.subjectId) + "/"
                        + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(selectedTopic.topicId) + "/"
                        + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_modeDropdown.value);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    AssetDatabase.CreateAsset(gameSO, path + "/" + game_id + ".asset");
                    //
                    AddGameToList(gameSO);
                    seletedGame = gameSO;
                    _gameDropdown.value = game_id;
                    Selection.activeObject = seletedGame;
                    //

                } else
                {
                    Debug.Log("Game Id is existed");
                }
            };
        }

        void BindVisualElement()
        {
            _spriteInput = rootVisualElement.Q<ObjectField>("subject_image");
            _subjectDropdown = rootVisualElement.Q<DropdownField>("subject_dropdown");
            _subjectName = rootVisualElement.Q<TextField>("subject_name");
            _subjectSpriteViewer = rootVisualElement.Q<VisualElement>("subject_image_viewer");
            _createSubjectBtn = rootVisualElement.Q<Button>("create_subject_btn");
            //
            _topicGroup = rootVisualElement.Q<VisualElement>("topic_group");
            _spriteTopic = rootVisualElement.Q<ObjectField>("topic_image");
            _topicDropdown = rootVisualElement.Q<DropdownField>("topic_dropdown");
            _topicName = rootVisualElement.Q<TextField>("topic_name");
            _topicSpriteViewer = rootVisualElement.Q<VisualElement>("topic_image_viewer");
            _createTopicBtn = rootVisualElement.Q<Button>("create_topic_btn");
            //
            _gameGroup = rootVisualElement.Q<VisualElement>("game_group");
            _gameDropdown = rootVisualElement.Q<DropdownField>("game_dropdown");
            _gameIdInput = rootVisualElement.Q<TextField>("game_id_input");
            _createGameBtn = rootVisualElement.Q<Button>("create_game_btn");
            _viewGameBtn = rootVisualElement.Q<Button>("view_inspector_btn");
            _modeDropdown = rootVisualElement.Q<DropdownField>("mode_dropdown");
        }

        void InitSelectedSubject(string selectedId)
        {
            if(selectedSubject != null)
            {
                EditorUtility.ClearDirty(selectedSubject);
            }
            //allow modify SO
            selectedSubject = subjectDict[selectedId];
            EditorUtility.SetDirty(selectedSubject);
            _topicGroup.visible = true;

            //update field
            _subjectName.value = selectedSubject.subjectName;
            _spriteInput.value = selectedSubject.subjectImage;
            _subjectSpriteViewer.style.backgroundImage = new StyleBackground(selectedSubject.subjectImage);

            //init dropdown topic
            _topicDropdown.choices = GetListTopicId(allTopics);
        }

        void InitSelectedTopic(string selectedId)
        {
            if (selectedSubject == null) return;

            if (selectedTopic != null)
            {
                EditorUtility.ClearDirty(selectedTopic);
            }
            //allow modify SO
            selectedTopic = topicDict[selectedSubject.subjectId + "_" + selectedId];
            EditorUtility.SetDirty(selectedTopic);
            _gameGroup.visible = true;

            //update field
            _topicName.value = selectedTopic.topicName;
            _spriteTopic.value = selectedTopic.topicImage;
            _topicSpriteViewer.style.backgroundImage = new StyleBackground(selectedTopic.topicImage);

            //init dropdown game
            _gameDropdown.choices = GetListGameId(allGames);
        }

        public void InitSelectedGame(string selectedId)
        {
            if (selectedTopic == null) return;

            if (seletedGame != null)
            {
                EditorUtility.ClearDirty(seletedGame);
            }

            //allow modify SO
            seletedGame = gameDict[selectedTopic.topicId + "_" + selectedId];
            EditorUtility.SetDirty(seletedGame);

            _gameIdInput.value = selectedId;
            _modeDropdown.value = seletedGame.gameType.ToString();
            //
            Selection.activeObject = seletedGame;
        }

        private void OnGUI()
        {
            
        }

        #region Subject SO Handle
        public void AddSubjectToList(SubjectSO newSubject)
        {
            Array.Resize(ref allSubjects, allSubjects.Length + 1);
            allSubjects[allSubjects.Length - 1] = newSubject;
            //
            subjectDict.Add(newSubject.subjectId, newSubject);
            //update dropdown
            _subjectDropdown.choices = GetListSubjectId(allSubjects);
        }

        public bool isSubjectIdExist(string id)
        {
            return subjectDict.ContainsKey(id);
        }

        private List<string> GetListSubjectId(SubjectSO[] subjects)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < subjects.Length; i++)
            {
                res.Add(subjects[i].subjectId);
            }
            return res;
        }

        #endregion

        #region Topic SO Handle

        public void UpdateTopicDictionary()
        {
            topicDict = new Dictionary<string, TopicSO>();
            for (int i = 0; i < allTopics.Length; i++)
            {
                if (allTopics[i].subjectId == selectedSubject.subjectId)
                {
                    topicDict.Add(allTopics[i].topicId, allTopics[i]);
                }
            }
        }

        public void AddTopicToList(TopicSO newTopic)
        {
            Array.Resize(ref allTopics, allTopics.Length + 1);
            allTopics[allTopics.Length - 1] = newTopic;
            //
            topicDict.Add(newTopic.subjectId + "_" + newTopic.topicId, newTopic);
            //update dropdown
            _topicDropdown.choices = GetListTopicId(allTopics);
        }

        public bool isTopicIdExist(string id)
        {
            return topicDict.ContainsKey(id);
        }

        private List<string> GetListTopicId(TopicSO[] topics)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < topics.Length; i++)
            {
                if(topics[i].subjectId == selectedSubject.subjectId)
                {
                    res.Add(topics[i].topicId);
                }
            }
            return res;
        }

        #endregion

        #region Handle Game SO

        public bool isGameIdExist(string id)
        {
            return gameDict.ContainsKey(id);
        }

        public void AddGameToList(LearningGameSO newGame)
        {
            Array.Resize(ref allGames, allGames.Length + 1);
            allGames[allGames.Length - 1] = newGame;
            //
            gameDict.Add(newGame.topicId + "_" + newGame.gameId, newGame);
            //update dropdown
            _gameDropdown.choices = GetListGameId(allGames);
        }

        private List<string> GetListGameId(LearningGameSO[] games)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].topicId == selectedTopic.topicId)
                {
                    res.Add(games[i].gameId);
                }
            }
            return res;
        }
        #endregion
    }
}
#endif