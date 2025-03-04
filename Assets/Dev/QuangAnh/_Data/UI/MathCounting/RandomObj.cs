using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MathCounting {
    public class RandomObj : NewMonobehavior {
        [SerializeField] protected List<ImagePrefabObj> imagePrefabObjs;

        [SerializeField] protected List<ImagePrefab> imagePrefabs;

        private List<ImagePrefabObj> shuffledList;

        protected override void Awake() {
            base.Awake();
            ShufferItem();
            SpawnUniquePrefabs();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadImagePrefabs();
            
        }

        protected virtual void LoadImagePrefabs() { 
            if(imagePrefabs.Count > 0) return;
            imagePrefabs = new List<ImagePrefab>(transform.GetComponentsInChildren<ImagePrefab>());
            Debug.Log(transform.name + ": LoadImagePrefabs" + gameObject);

        }
        public void SpawnUniquePrefabs() {
            if (imagePrefabObjs == null || imagePrefabObjs.Count < 12) {
                Debug.LogWarning("Not have 12 item ");
                return;
            }

            for (int i = 0; i < 12; i++) {
                imagePrefabs[i].CheckingNumber.SetObj(shuffledList[i]);
            }
        }

        public void ShufferItem() {

            shuffledList = new List<ImagePrefabObj>(imagePrefabObjs);
            int count = shuffledList.Count;

            for (int i = 0; i < count - 1; i++) {
                int randomIndex = Random.Range(i, count);
                (shuffledList[i], shuffledList[randomIndex]) = (shuffledList[randomIndex], shuffledList[i]);
            }
        }

    }
}
