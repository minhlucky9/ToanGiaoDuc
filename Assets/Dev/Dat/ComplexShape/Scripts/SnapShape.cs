using System.Collections.Generic;
using UnityEngine;

namespace ComplexShape
{
    public class SnapShape : MonoBehaviour
    {
        [SerializeField] List<ShapeCombination> shapeCombinationList;
        [SerializeField] LayerMask draggabbleShapeLayer;

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
            Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center,
                boxCollider.bounds.extents, Quaternion.identity, draggabbleShapeLayer);

            foreach (Collider col in colliders)
            {
                DraggableShape shapeFound = col.GetComponent<DraggableShape>();
                if (shapeFound == null || !shapeFound.CanBeSnapped()) continue;

                HandleSnapShape(shapeFound);
            }
        }

        private void HandleSnapShape(DraggableShape shapeFound)
        {
            if (IsFullyCombined())
            {
                lesson.PlusMistake();
                return;
            }

            bool canBeCombined = false;
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
                canBeCombined = true;
            }
            if (!canBeCombined) lesson.PlusMistake();
        }

        private void OnTriggerExit(Collider other)
        {
            DraggableShape shapeFound = other.GetComponent<DraggableShape>();
            if (shapeFound != null
                && chosenShapeCombination != null
                && chosenShapeCombination.IsShapeExist(shapeFound))
            {
                chosenShapeCombination.RemoveShape(shapeFound);
                if (chosenShapeCombination.NoShapeIsFound()) chosenShapeCombination = null;
            }
        }


        public Vector2 GetRectPosition()
        {
            return rectTransform.position;
        }

        public void SelfCorrect()
        {
            if (chosenShapeCombination == null)
            {
                chosenShapeCombination = shapeCombinationList[Random.Range(0, shapeCombinationList.Count)];
            }
            if (chosenShapeCombination.IsFullyCombined()) return;

            int attempt = 10;

            while (!chosenShapeCombination.IsFullyCombined() && attempt > 0)
            {
                attempt--;
                Collider[] cols = Physics.OverlapSphere(transform.position, 10, draggabbleShapeLayer);
                foreach (Collider col in cols)
                {
                    DraggableShape shapeFound = col.GetComponent<DraggableShape>();
                    if (shapeFound != null && chosenShapeCombination.CanBeCombined(shapeFound))
                    {
                        chosenShapeCombination.PlaceShape(shapeFound);
                        shapeFound.SnapTo(this);
                    }
                }

            }
        }

        public bool IsFullyCombined()
        {
            return chosenShapeCombination != null && chosenShapeCombination.IsFullyCombined();
        }
    }
}

