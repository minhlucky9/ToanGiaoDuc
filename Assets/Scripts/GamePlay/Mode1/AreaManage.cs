using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManage : MonoBehaviour
{
    [SerializeField]
    private List<AreaPlay> listArea;
    public LayerMask LayerGround;
    public List<AreaPlay> GetList()
    {
        return listArea;
    }
    public void Check(GameObject itemCheck)
    {
        ItemContainBoxcollider item = itemCheck.GetComponent<ItemContainBoxcollider>();
        Ray ray = new Ray(itemCheck.transform.position, itemCheck.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 15f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerGround))
        {
            AreaPlay area = hit.transform.GetComponent<AreaPlay>();
            if (area != null)
            {
                Debug.Log("Vao r nay: Item kham pha cham vao area roi!");
                area.CheckPositionOfItem(item);
            }
            else
            {
                item.SetAreaIdSelected(-1);
            }
        }
        else
        {
            Debug.Log("Khong vao r: Item kham pha khong cham vao area roi!");
            item.SetAreaIdSelected(-1);
        }
    }
}
