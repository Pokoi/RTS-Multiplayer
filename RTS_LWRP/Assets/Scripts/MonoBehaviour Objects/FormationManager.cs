/*
 * File: FormationManager.cs
 * File Created: Monday, 16th December 2019 6:13:47 pm
 * ––––––––––––––––––––––––
 * Author: Jesus Fermin, 'Pokoi', Villar  (hello@pokoidev.com)
 * ––––––––––––––––––––––––
 * MIT License
 * 
 * Copyright (c) 2020 Pokoidev
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    public  List<NavAgentsFormation>  playerFormations   = new List<NavAgentsFormation>();
    private List<Vector3>             formationsOffset   = new List<Vector3>();
    private NavAgentsFormation        selectedFormation;
    Vector3                           compositionCenter;

    public void AddFormation    (NavAgentsFormation formation) => this.playerFormations.Add(formation);
    public void RemoveFormation (NavAgentsFormation formation) => this.playerFormations.Remove(formation);
    
    public void MoveFormationToPosition(int index, Vector3 position)
    {
        playerFormations[index].CalculateCenter();
        playerFormations[index].SetDestination(position);
    }

    public void MoveAllFormationsToPosition(Vector3 position)
    {
        int iterator = 0;
        
        foreach(NavAgentsFormation formation in playerFormations)
        {
            formation.StopFormation();
            formation.CalculateCenter();
        }

        CalculateCenter();

        compositionCenter = position;

        foreach(NavAgentsFormation formation in playerFormations)
        {
            formation.SetDestination(compositionCenter + formationsOffset[iterator]);
            ++iterator;
        }
    }
    
    private void CalculateCenter()
    {
        compositionCenter = Vector3.zero;

        foreach (NavAgentsFormation formation in playerFormations)
        {
            compositionCenter += formation.GetCenterFormation();
        }

        compositionCenter = new Vector3 (compositionCenter.x / playerFormations.Count, 0, compositionCenter.z / playerFormations.Count);

        foreach (NavAgentsFormation formation in playerFormations)
        {
            formationsOffset.Add(formation.GetCenterFormation() - compositionCenter);
        }

    }

}

public class NavAgentsFormation
{
    private  List<NavAgentFollowCursor> formationUnits  = new List<NavAgentFollowCursor>();
    private  List<Vector3>              unitsOffset     = new List<Vector3>();
    private  Vector3                    centerFormation;
    private  float                      radius          = 1.5f;

    public Vector3 GetCenterFormation() => centerFormation;

    public void CalculateCenter()
    {

        centerFormation = Vector3.zero;

        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            centerFormation += agent.transform.position;
        }

        centerFormation = new Vector3(centerFormation.x / formationUnits.Count, 0, centerFormation.z / formationUnits.Count);

        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            unitsOffset.Add(agent.transform.position - centerFormation);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        centerFormation = CursorElement.instance.GetPosition();
        int iterator    = 0;

        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            agent.SetDestination(centerFormation + unitsOffset[iterator]);
            ++iterator;
        }
    }

    public void StopFormation()
    {
        foreach (NavAgentFollowCursor agent in formationUnits)
        {
            agent.StopMovement();
        }
    }

    public void RegroupFormation()
    {
        int iterator = 0;
        foreach(NavAgentFollowCursor agent in formationUnits)
        {
            agent.SetDestination(centerFormation + GetVertex(formationUnits.Count, iterator));
            ++iterator;
        }
    }

    private Vector3 GetVertex(int totalCount, int index)
    {
        float pi2 = 6.283185f;

        return new Vector3  (
                                radius * Mathf.Cos(pi2 * index / totalCount),
                                radius * Mathf.Sin(pi2 * index / totalCount),
                                0
                            );
    }
}

public class Formation
{
    
}