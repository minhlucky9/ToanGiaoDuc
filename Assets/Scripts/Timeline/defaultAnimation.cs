using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultAnimation : MonoBehaviour
{

    public Animator giaovienAnim;
    public Animator lanAnim;
    public Animator haiAnim;

    // Start is called before the first frame update
    void Start()
    {
        giaovienAnim.Play("hello");
        lanAnim.Play("hello");
        haiAnim.Play("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
