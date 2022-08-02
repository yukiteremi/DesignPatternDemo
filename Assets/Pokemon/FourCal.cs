using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourCal : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(time());
    }
    IEnumerator time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Ray ray = new Ray(transform.position,Vector3.down);
            if (Physics.Raycast(ray,out RaycastHit info))
            {
                Debug.Log(info.collider.tag);
            }
        }
    }
    
    void Update()
    {
       
    }
}

