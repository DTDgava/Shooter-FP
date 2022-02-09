using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TBA_ENEMY : MonoBehaviour
{
    public GameObject Player;
    public float distance;
    NavMeshAgent nav;
    public float radius = 15;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position,transform.position);
        if (distance > radius)
        {
            nav.enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        else if(distance<radius)
        {
            nav.enabled = true;
            nav.SetDestination(Player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
    }
}
