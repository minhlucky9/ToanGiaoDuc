using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MathCounting {
    public class Find : MonoBehaviour {

        void Start() {

            Test1 obj = GameObject.FindAnyObjectByType<Test1>();

            Debug.Log(obj.gameObject.name);

        }

        
    }

}
