using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CursorElement : MonoBehaviour
{

    static  CursorElement   instance;
    private NavMeshAgent    selfAgent;
    private Transform       selfTransform;
    public  Camera          cameraToRayCast;
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        selfAgent       = GetComponent<NavMeshAgent>();
        selfTransform   = transform;

        StartCoroutine(SetPosition(1.0f));
    }

   

    IEnumerator SetPosition(float delta)
    {
        while (true)
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraToRayCast.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                selfAgent.destination = hit.point;
            }

                yield return new WaitForSeconds(delta);
        }

        
    }
}
