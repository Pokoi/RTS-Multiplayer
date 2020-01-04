using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CursorElement : MonoBehaviour
{
    private         Transform       selfTransform;
    private         NavMeshAgent    selfAgent;
    private         Vector3         destination;
    

    // Start is called before the first frame update
    void Awake()
    {
        selfTransform   = transform;
        selfAgent       = GetComponent<NavMeshAgent>();
        StartCoroutine(SetPosition(0.5f));
    }

    public Vector3 GetPosition() => selfTransform.position;

    public void SetDestination(Vector3 target)
    {
        StopCoroutine("SetPosition");
        destination = target;
        StartCoroutine(SetPosition(0.5f));
    }
    IEnumerator SetPosition(float delta)
    {
        while (true)
        {
            selfAgent.destination = new Vector3 (destination.x, selfTransform.position.y, destination.y);
            yield return new WaitForSeconds(delta);
        }
    }
}
