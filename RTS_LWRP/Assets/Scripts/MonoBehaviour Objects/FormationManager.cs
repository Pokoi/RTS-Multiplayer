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

public class FormationManager : Formation <NavAgentsFormation>
{
    
    public void AddFormation    (NavAgentsFormation formation) => this.formationUnits.Add(formation);
    public void RemoveFormation (NavAgentsFormation formation) => this.formationUnits.Remove(formation);
    
    public void MoveFormationToPosition(int index, Vector3 position)
    {
        if(!formationUnits[index].IsEmpty())
        {
            formationUnits[index].CalculateCenter();
            formationUnits[index].SetDestination(position);
        }
    }

    public void MoveAllFormationsToPosition(Vector3 position)
    {
        int iterator = 0;
        
        foreach(NavAgentsFormation formation in formationUnits)
        {
            formation.StopFormation();
            formation.CalculateCenter();
        }

        CalculateCenter();

        centerFormation = position;

        foreach(NavAgentsFormation formation in formationUnits)
        {
            formation.SetDestination(centerFormation + unitsOffset[iterator]);
            ++iterator;
        }
    }
    
    public override void CalculateCenter()
    {
        centerFormation = Vector3.zero;

        foreach (NavAgentsFormation formation in formationUnits)
        {
            centerFormation += formation.GetCenterFormation();
        }

        centerFormation = new Vector3 (centerFormation.x / formationUnits.Count, 0, centerFormation.z / formationUnits.Count);

        foreach (NavAgentsFormation formation in formationUnits)
        {
            unitsOffset.Add(formation.GetCenterFormation() - centerFormation);
        }
    }
    public override void RegroupFormation()
    {
        radius = 50f;
        int iterator = 0;
        foreach(NavAgentsFormation formation in formationUnits)
        {
            formation.SetDestination(centerFormation + GetVertex(formationUnits.Count, iterator));
            ++iterator;
        }
    }
    public Vector3 GetCenterOfFormation(int index) => formationUnits[index].GetCenterFormation();
    public void SetCenterOfFormation(Vector3 center) => centerFormation = center;
    public void RegroupEachFormation()
    {
        foreach(NavAgentsFormation formation in formationUnits)
        {
            formation.RegroupFormation();
        }
    }
}

public class NavAgentsFormation : Formation <NavAgentFollowCursor> 
{
    public void SetDestination(Vector3 destination)
    {
        centerFormation = destination;
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

    public override void RegroupFormation()
    {
        radius = 15f;
        int iterator = 0;
        foreach(NavAgentFollowCursor agent in formationUnits)
        {
            agent.SetDestination(centerFormation + GetVertex(formationUnits.Count, iterator));
            ++iterator;
        }
    }

    public override void CalculateCenter()
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
}

public abstract class Formation <T> : MonoBehaviour
{
    protected List <T>        formationUnits  = new List<T>();
    protected List <Vector3>  unitsOffset     = new List<Vector3>();
    protected Vector3         centerFormation;
    protected float           radius;

    public Vector3  GetCenterFormation() => centerFormation; 
    public bool     IsEmpty()            => formationUnits.Count == 0;
    public abstract void CalculateCenter();
    public abstract void RegroupFormation();

    protected Vector3 GetVertex (int totalCount, int index)
    {
        float pi2 = 6.283185f;

        return new Vector3  (
                                radius * Mathf.Cos(pi2 * index / totalCount),
                                radius * Mathf.Sin(pi2 * index / totalCount),
                                0
                            );
    }
}