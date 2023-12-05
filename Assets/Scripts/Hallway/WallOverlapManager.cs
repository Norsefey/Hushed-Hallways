using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOverlapManager : MonoBehaviour
{//Top will check for overlap, bottom will have taged collider and Top deleted if overlapping, same for right wall check for left, and right be deleted

    // Start is called before the first frame update
    void Awake()
    {
        Collider[] overlapedColliders = Physics.OverlapSphere(transform.position, .01f);//get colliders overlapping wall
        foreach (Collider col in overlapedColliders)
        {
            if (col.tag == "Overlapping")
            {
                Destroy(gameObject);
                return;
            }
        }
        GetComponent<Collider>().enabled = true;
    }

    
}
