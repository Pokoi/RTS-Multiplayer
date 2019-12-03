using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public  List<NavAgentFollowCursor>  formationUnits  = new List<NavAgentFollowCursor>();
    private List<Vector3>               unitsOffset     = new List<Vector3>();
    Vector3                             centerFormation;

    // Start is called before the first frame update
    void Start()
    {
        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            centerFormation += agent.transform.position;
        }

        centerFormation = new Vector3(centerFormation.x / formationUnits.Count, 0, centerFormation.z / formationUnits.Count);

        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            unitsOffset.Add(agent.transform.position - centerFormation);
        }

        StartCoroutine(FollowingCursor(0.5f));
    }

    IEnumerator FollowingCursor(float delta)
    {
        while (true)
        {
            centerFormation = CursorElement.instance.GetPosition();
            int iterator    = 0;

            foreach (NavAgentFollowCursor agent in formationUnits)
            {
                agent.SetDestination(centerFormation + unitsOffset[iterator]);
                ++iterator;
            }

            yield return new WaitForSeconds(delta);
        }
    }
}
