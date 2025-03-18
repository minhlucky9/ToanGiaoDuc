using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightObjectMove : MonoBehaviour
{
    public GameObject baseObject;
    public void StartMove()
    {
        StartCoroutine(Move(1f));
        //StartCoroutine(Rot(1f));
    }

    private IEnumerator Move(float delayTime)
    {
        //baseObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //Quaternion originalRot = baseObject.transform.rotation;
        //transform.SetParent(baseObject.transform);
        //transform.rotation = Quaternion.Euler(baseObject.transform.rotation.eulerAngles.x, baseObject.transform.rotation.eulerAngles.y, baseObject.transform.rotation.eulerAngles.z);
        transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(baseObject.transform.rotation.eulerAngles.x, baseObject.transform.rotation.eulerAngles.y, baseObject.transform.rotation.eulerAngles.z);
        yield return new WaitForSeconds(delayTime);
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 2)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, baseObject.transform.position, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }
    private IEnumerator Rot(float delayTime)
    {
        //transform.localEulerAngles = new Vector3(x, y, z);
        Quaternion target1 = Quaternion.Euler(0, 1, 0);
        Quaternion target2 = Quaternion.Euler(baseObject.transform.rotation.eulerAngles.x, baseObject.transform.rotation.eulerAngles.y, baseObject.transform.rotation.eulerAngles.z);
        yield return new WaitForSeconds(delayTime);
        float startTime = Time.time;
        while (Time.time - startTime <= 1)
        { // until one second passed
            transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, target1, Time.deltaTime * 0.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime * 0.5f);
            yield return 1; // wait for next frame
        }
        yield return new WaitForSeconds(delayTime);
        startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 1)
        { // until one second passed
            //transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime * 0.5f);
            yield return 1; // wait for next frame
        }
    }
}
