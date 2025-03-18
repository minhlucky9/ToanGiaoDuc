using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MathCounting {
    public class ControlObject : NewMonobehavior {
        
        [SerializeField] protected ScriptableObjCtrl ScriptableObjCtrl;

        [SerializeField] protected List<ImagePrefab> imagePrefabs;

        [SerializeField] protected List<ImageScriptableObj> imagePrefabObjs;

        private List<ImageScriptableObj> shuffledList;

        protected override void Awake() {
            base.Awake();
            ShufferItem();
            SpawnUniquePrefabs();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImagePrefabs();
            this.LoadScriptableObjCtrl();
        }

        protected virtual void LoadImagePrefabs() { 
            if(imagePrefabs.Count > 0 && !this.imagePrefabs.Any(c => c == null)) return;
            imagePrefabs = new List<ImagePrefab>(transform.GetComponentsInChildren<ImagePrefab>());
            Debug.Log(transform.name + ": LoadImagePrefabs" + gameObject);
        }


        protected virtual void LoadScriptableObjCtrl() { 
            if(ScriptableObjCtrl != null) return;
            this.ScriptableObjCtrl = FindAnyObjectByType<ScriptableObjCtrl>();
            this.imagePrefabObjs = new List<ImageScriptableObj>(ScriptableObjCtrl.ImagePrefabs);
            Debug.Log(transform.name + ": LoadScriptableObjCtrl" + gameObject);
        }

        public void SpawnUniquePrefabs() {
            for (int i = 0; i < imagePrefabObjs.Count; i++) {
                if (i >= imagePrefabs.Count || i >= shuffledList.Count) break;
                imagePrefabs[i].CheckingNumber.SetObj(shuffledList[i]);
            }
        }

        public void ShufferItem() {

            shuffledList = new List<ImageScriptableObj>(imagePrefabObjs);
            int count = shuffledList.Count;

            for (int i = 0; i < count - 1; i++) {
                int randomIndex = Random.Range(i, count);
                (shuffledList[i], shuffledList[randomIndex]) = (shuffledList[randomIndex], shuffledList[i]);
            }
        }

    }
}
