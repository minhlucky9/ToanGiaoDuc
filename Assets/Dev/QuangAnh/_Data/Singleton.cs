using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {

    public abstract class Singleton<T> : NewMonobehavior where T : MonoBehaviour {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    Debug.LogError(typeof(T).Name + "instance hasn't not been created yet");
                }
                return _instance;
            }
        }

        protected override void Awake() {
            base.Awake();
            this.LoadInstance();
        }

        private void LoadInstance() {
            if (_instance == null) {
                _instance = this as T;
                DontDestroyOnLoad(this);
                return;
            }
            if (_instance == this) {
                Debug.LogError("Another instance of " + typeof(T).Name + "already exists!");
            }
        }
    }

}