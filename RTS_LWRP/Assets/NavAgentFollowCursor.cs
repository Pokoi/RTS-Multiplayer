using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentFollowCursor : MonoBehaviour
{
    NavMeshAgent    agent;
    Transform       cachedTransform;
    Vector3         destination;
   
    
    // Start is called before the first frame update
    void Start()
    {
        agent           = GetComponent<NavMeshAgent>();
        cachedTransform = transform;
        StartCoroutine(FollowingCursor(1));
    }
    
    public void SetDestination(Vector3 target) => destination = target;
   

    IEnumerator FollowingCursor(float delta)
    {
        while(true)
        {
            agent.destination = new Vector3(destination.x, cachedTransform.position.y, destination.z);
            yield return new WaitForSeconds(delta);
        }
    }


}
