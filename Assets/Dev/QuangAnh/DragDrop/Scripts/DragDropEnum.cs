using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DragDropEnumDatabase", menuName = "WallDragDrop/Enum Database")]
public class DragDropEnumDatabase : ScriptableObject {
    public List<string> enumValues = new List<string>() { "none", "slot1", "slot2", "slot3", "slot4" };
}
