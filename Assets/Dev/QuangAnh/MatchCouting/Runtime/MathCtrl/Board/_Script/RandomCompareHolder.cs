using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MathCounting {

    /// <summary>
    /// Script ramdon value (>, < , = ) for ComparePrefab
    /// </summary>

    public class RandomCompareHolder : NewMonobehavior {

        [SerializeField] protected List<ComparePrefab> comparePrefabs = new();

        protected override void Awake() {
            base.Awake();
            ShuffleAndSetNumbers();
        }

        protected override void LoadComponents() {
            base.LoadComponents();
            this.LoadComparePrefabs();
        
        }

        protected virtual void LoadComparePrefabs() { 
            if(comparePrefabs.Count > 0 && !this.comparePrefabs.Any(c => c == null)) return;
            comparePrefabs = new List<ComparePrefab>(transform.GetComponentsInChildren<ComparePrefab>());
            Debug.Log(transform.name + ": LoadComparePrefab" + gameObject);
        }

        public void ShuffleAndSetNumbers() {
            List<int> numbers = Enumerable.Range(1, 10).ToList(); 
            numbers = numbers.OrderBy(x => Random.value).ToList(); 

            for (int i = 0; i < comparePrefabs.Count; i++) {
                if (i < numbers.Count) {
                    int leftNumber = numbers[i];
                    int rightNumber = Random.Range(1, 11); 
                    string comparisonSymbol = GetComparisonSymbol(leftNumber, rightNumber);

                    comparePrefabs[i].SetNumberLeft(leftNumber.ToString());
                    comparePrefabs[i].SetNumberRight(rightNumber.ToString());

                    
                    if (comparePrefabs[i].CheckingNumber != null) {
                        comparePrefabs[i].CheckingNumber.SetObj(comparisonSymbol);
                    }
                }
            }
        }

        private string GetComparisonSymbol( int left, int right ) {
            if (left > right) return ">";
            if (left < right) return "<";
            return "=";
        }


    }
}
