using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ComplexShape
{
    public enum ShapeType
    {
        TopRightTriangle,
        TopLeftTriangle,
        BottomRightTriangle,
        BottomLeftTriangle,
        Square,
        Circle
    }

    public class DraggableShape : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] ShapeType shapeType;
        RectTransform rectTransform;
        RectTransform parentRectTransform;

        Vector2 startPosition;
        Vector2 targetPosition;

        const string BASE_TAG = "BaseShape";
        SnapShape currentSnapShape;
        bool isChosen;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            parentRectTransform = transform.parent.GetComponent<RectTransform>();
            startPosition = rectTransform.localPosition;
            targetPosition = startPosition;
        }

        private void Update()
        {
            if (!isChosen)
            {
                if (Vector2.Distance(rectTransform.localPosition, targetPosition) < 0.05f)
                {
                    rectTransform.localPosition = targetPosition;
                    return;
                }

                rectTransform.localPosition = Vector2.Lerp(
                rectTransform.localPosition,
                targetPosition,
                Time.deltaTime * 10);
            }
        }

        public void BackToStartPosition()
        {
            currentSnapShape = null;
            targetPosition = startPosition;
            SwitchRole();
        }

        public void SetStartPosition(Vector2 startPosition)
        {
            this.startPosition = startPosition;
            targetPosition = startPosition;
            rectTransform.localPosition = startPosition;
        }

        public ShapeType GetShapeType() { return shapeType; }

        #region Base & Clone Functions
        private void RespawnBaseShape()
        {
            GameObject baseShape = Instantiate(gameObject, 
                Vector2.zero, Quaternion.identity, transform.parent);

            baseShape.name = gameObject.name;
            baseShape.tag = BASE_TAG;
            tag = "Untagged";

            DraggableShape draggableShape = baseShape.GetComponent<DraggableShape>();
            draggableShape.SetStartPosition(startPosition);
        }

        void SwitchRole()
        {
            GameObject[] baseShapes = GameObject.FindGameObjectsWithTag(BASE_TAG);
            foreach (GameObject baseShape in baseShapes)
            {
                if (baseShape.name == gameObject.name && baseShape != gameObject)
                {
                    Destroy(baseShape);
                    tag = BASE_TAG;
                    break;
                }
            }
        }

        #endregion

        #region Snap Functions
        public void SnapTo(SnapShape snapShape)
        {
            if (currentSnapShape != null) return;

            RespawnBaseShape();

            currentSnapShape = snapShape;
            targetPosition = parentRectTransform.InverseTransformPoint(snapShape.GetRectPosition());
            rectTransform.localPosition = targetPosition;
        }

        public bool CanBeSnapped()
        {
            return currentSnapShape == null && !isChosen;
        }
        #endregion

        #region Interaction Functions
        public void OnPointerDown(PointerEventData eventData)
        {
            isChosen = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isChosen)
            {
                Vector2 localPoint;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentRectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out localPoint
                );

                rectTransform.localPosition = localPoint;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isChosen = false;
        }
        #endregion
    }
}
