using UnityEngine;
using UnityEngine.Playables;

namespace MathCounting {
    [System.Serializable]
    public class ExposedBinding {
        public ExposedReference<GameObject> exposedObject;
    }

    public class ExposedReferenceExample : MonoBehaviour {
        public PlayableDirector director;
        public ExposedBinding bindingData;

        void Start() {
            if (director == null) return;


            GameObject targetObject = bindingData.exposedObject.Resolve(director);

            if (targetObject != null) {

                foreach (var output in director.playableAsset.outputs) {
                    if (output.streamName == "Animation Track") {
                        director.SetGenericBinding(output.sourceObject, targetObject);
                    }
                }
                director.RebuildGraph();
            } else {
                Debug.LogError("Cannot resolve ExposedReference!");
            }
        }
    }

}