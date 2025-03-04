using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCounting {
    public class ScriptableObjCtrl : NewMonobehavior {
        [SerializeField] private List<ImagePrefabObj> imagePrefabs = new();
    }
}
