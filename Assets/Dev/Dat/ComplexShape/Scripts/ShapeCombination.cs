using System.Collections.Generic;
using UnityEngine;

namespace ComplexShape
{
    [System.Serializable]
    public class ShapeCombination
    {
        public List<ShapeType> shapeList;
        List<DraggableShape> draggableShapes = new List<DraggableShape>();

        public void Initialize()
        {
            foreach (ShapeType shapeType in shapeList)
            {
                draggableShapes.Add(null);
            }
        }

        #region Bool Functions

        public bool IsFullyCombined()
        {
            foreach (DraggableShape draggableShape in draggableShapes)
            {
                if (draggableShape == null) return false;
            }
            return true;
        }

        public bool NoShapeIsFound()
        {
            foreach (DraggableShape draggableShape in draggableShapes)
            {
                if (draggableShape != null) return false;
            }
            return true;
        }

        public bool IsShapeExist(DraggableShape draggableShape)
        {
            return draggableShapes.Contains(draggableShape);
        }

        #endregion

        #region Shape Functions
        public bool CanBeCombined(DraggableShape draggableShape)
        {
            if(draggableShape == null || draggableShape.IsSnapped()) return false;

            ShapeType shapeType = draggableShape.GetShapeType();

            if (!shapeList.Contains(shapeType)) return false;

            int shapeIndex = GetShapeIndex(shapeType);
            if (draggableShapes[shapeIndex] != null) return false;

            return true;
        }
        public void PlaceShape(DraggableShape draggableShape)
        {
            int shapeIndex = GetShapeIndex(draggableShape.GetShapeType());
            if (shapeIndex != -1)
            {
                draggableShapes[shapeIndex] = draggableShape;
            }
        }

        public void RemoveShape(DraggableShape draggableShape)
        {
            int shapeIndex = GetShapeIndex(draggableShape.GetShapeType());
            if (shapeIndex != -1)
            {
                draggableShapes[shapeIndex] = null;
                draggableShape.SnapTo(null);
            }
        }
        #endregion

        int GetShapeIndex(ShapeType shapeType)
        {
            if (shapeList.Contains(shapeType))
            {
                return shapeList.IndexOf(shapeType);
            }
            return -1;
        }

    }
}

