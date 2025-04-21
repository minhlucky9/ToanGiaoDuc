using System.Collections.Generic;
using UnityEngine;

namespace ComplexShape
{
    public class SnapShape : MonoBehaviour
    {
        [SerializeField] List<ShapeCombination> shapeCombinationList;

        Lesson lesson;

        RectTransform rectTransform;
        ShapeCombination chosenShapeCombination;
        BoxCollider boxCollider;

        private void Awake()
        {
            lesson = FindAnyObjectByType<Lesson>();

            boxCollider = GetComponent<BoxCollider>();
            rectTransform = GetComponent<RectTransform>();
            foreach (ShapeCombination shapeCombination in shapeCombinationList)
            {
                shapeCombination.Initialize();
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                FindShape();
            }
        }

        void FindShape()
        {
            if (chosenShapeCombination != null && chosenShapeCombination.IsShapeFullyCombined()) return;

            Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center,
                boxCollider.bounds.extents, Quaternion.identity);

            foreach (Collider col in colliders)
            {
                DraggableShape shapeFound = col.GetComponent<DraggableShape>();
                if (shapeFound == null || !shapeFound.CanBeSnapped()) continue;

                HandleSnapShape(shapeFound);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            DraggableShape shapeFound = other.GetComponent<DraggableShape>();
            if (shapeFound != null 
                && chosenShapeCombination != null 
                && chosenShapeCombination.IsShapeExist(shapeFound))
            {
                chosenShapeCombination.RemoveShape(shapeFound);
                if(chosenShapeCombination.NoShapeIsFound()) chosenShapeCombination = null;
            }
        }

        private void HandleSnapShape(DraggableShape shapeFound)
        {
            if (chosenShapeCombination == null)
            {
                foreach (ShapeCombination shapeCombination in shapeCombinationList)
                {
                    if (shapeCombination.CanBeCombined(shapeFound))
                    {
                        chosenShapeCombination = shapeCombination;
                        break;
                    }
                }
            }

            if (chosenShapeCombination != null && chosenShapeCombination.CanBeCombined(shapeFound))
            {
                chosenShapeCombination.PlaceShape(shapeFound);
                shapeFound.SnapTo(this);
            }
        }

        public Vector2 GetRectPosition()
        {
            return rectTransform.position;
        }
    }
}

