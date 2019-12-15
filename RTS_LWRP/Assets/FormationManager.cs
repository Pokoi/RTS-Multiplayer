using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public  List<Formation>  playerFormations   = new List<Formation>();
    private List<Vector3>    FormationsOffset   = new List<Vector3>();
    private Formation        selectedFormation;
    Vector3                  compositionCenter;

    public void AddFormation    (Formation formation) => this.playerFormations.Add(formation);
    public void RemoveFormation (Formation formation) => this.playerFormations.Remove(formation);
    /*
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
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { 
        
        }
        //else if (Input.GetKeyDown())
    }

    /*
    IEnumerator FollowingCursor(float delta)
    {
        while (true)
        {
            compositionCenter = CursorElement.instance.GetPosition();
            int iterator    = 0;

            foreach (NavAgentFollowCursor agent in formationUnits)
            {
                agent.SetDestination(centerFormation + unitsOffset[iterator]);
                ++iterator;
            }

            yield return new WaitForSeconds(delta);
        }
    }*/
}

public class Formation
{
    private  List<NavAgentFollowCursor> formationUnits  = new List<NavAgentFollowCursor>();
    private  List<Vector3>              unitsOffset     = new List<Vector3>();
    private  Vector3                    centerFormation;


    public void CalculateCenter()
    {
        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            centerFormation += agent.transform.position;
        }

        centerFormation = new Vector3(centerFormation.x / formationUnits.Count, 0, centerFormation.z / formationUnits.Count);
    }

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
        /*
        StartCoroutine(FollowingCursor(0.5f));*/
    }

    IEnumerator FollowingCursor(float delta)
    {
        while (true)
        {
            centerFormation = CursorElement.instance.GetPosition();
            int iterator = 0;

            foreach (NavAgentFollowCursor agent in formationUnits)
            {
                agent.SetDestination(centerFormation + unitsOffset[iterator]);
                ++iterator;
            }

            yield return new WaitForSeconds(delta);
        }
    }
}