using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CursorElement : MonoBehaviour
{

    public static   CursorElement   instance;    
    private         Transform       selfTransform;
    public          Camera          cameraToRayCast;
    private         NavMeshAgent    selfAgent;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        selfTransform   = transform;
        selfAgent       = GetComponent<NavMeshAgent>();
        StartCoroutine(SetPosition(0.5f));
    }

    public Vector3 GetPosition() => selfTransform.position;

    IEnumerator SetPosition(float delta)
    {
        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraToRayCast.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                selfAgent.destination = new Vector3(hit.point.x, selfTransform.position.y, hit.point.z);
            }

            yield return new WaitForSeconds(delta);
        }

        
    }
}
