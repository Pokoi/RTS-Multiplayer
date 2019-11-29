/*
 * File: PlaceableItem.cs
 * File Created: Tuesday, 5th November 2019 3:39:23 pm
 * ––––––––––––––––––––––––
 * Author: Jesus Fermin, 'Pokoi', Villar  (hello@pokoidev.com)
 * ––––––––––––––––––––––––
 * MIT License
 * 
 * Copyright (c) 2019 Pokoidev
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

public class DraggeableUnit : MonoBehaviour
{
    public LayerMask    layerMask;
    public static bool  draggingUnit;
    FollowCursor        followCursorComponent;
    Cell                lastCellCollidedWith;


    public FollowCursor GetFollowCursorComponent() => followCursorComponent;

    public void AddFollowCursorComponent()
    {
        if(this.followCursorComponent == null)
        {
            this.followCursorComponent = new FollowCursor(transform, layerMask);
        }

        DragPlaceable();
    }
    
    private void Start() =>  AddFollowCursorComponent(); 

    private void Update() 
    {
       if(Input.GetMouseButtonDown(0)&& followCursorComponent.GetFollowing())
        {
            DropPlaceable();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Cell otherCell = other.GetComponent<Cell>();
        if(otherCell)
        {
            this.lastCellCollidedWith = otherCell;
        }
    }

    private void DropPlaceable()
    {
        if(lastCellCollidedWith)
        {
            Soldier     thisSoldier     = transform.GetComponent<Soldier>();
            CellData    targetCellData  = lastCellCollidedWith.GetComponent<Cell>().GetBehaviour().GetCellData();
            Unit        placedUnit      = new Unit(targetCellData, new UnitData(thisSoldier.GetUnitType())); 

            if(targetCellData.IsEmpty())
            {
                float yModifier     = 0.5f;
                transform.localPosition  = new Vector3 (
                                                    lastCellCollidedWith.transform.position.x, 
                                                    lastCellCollidedWith.transform.position.y + yModifier,
                                                    lastCellCollidedWith.transform.position.z
                                                );

                this.followCursorComponent.StopMoving();
                
                TeamData team = GameController.Get().GetPlayerTeamData();
                thisSoldier.SetUnit(placedUnit);
                team.AddSoldier(thisSoldier);
                thisSoldier.SetTeam(team);
                PlayerController.Get().AddUnitToFormation(placedUnit);
                targetCellData.SetEmpty(false);
                draggingUnit = false;
            }
        }

    }

    private void DragPlaceable()
    {
        this.followCursorComponent.StartMoving();
        draggingUnit = true;
    }
    
    
}
