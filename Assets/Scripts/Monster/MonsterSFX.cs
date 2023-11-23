using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX : MonoBehaviour
{
    Monster monsterScript;

    StateIdle idle;
    StateChase chase;
    StatePatrol patrol;

    [SerializeField]
    Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        monsterScript = GetComponent<Monster>();
        idle = GetComponent<StateIdle>();
        chase = GetComponent<StateChase>();
        patrol = GetComponent<StatePatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterScript.CurrentState == idle)
        {
            anime.SetBool("isMoving", false);
        }else if(monsterScript.CurrentState == chase || monsterScript.CurrentState == patrol)
        {
            anime.SetBool("isMoving", true);
            Debug.Log("iosMoving");
        }
    }
}
