/*
 * File: Locomotion.cs
 * File Created: Thursday, 21st November 2019 9:53:48 am
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
using UnityEngine;

public class Locomotion
{
    Transform   cachedTransform;
    float       speed;
    AStarSearch AStar;
    Soldier     soldierGoal;
    int         movementRange;
    
    public enum MoveDirection
    {
        Up, Left, Down, Right, None
    }

    public Locomotion(Transform cachedTransform)
    {
        this.cachedTransform = cachedTransform;
        AStar                = new AStarSearch();
    }
 
    public void Move(Soldier soldierGoal, int range)
    {   
        this.soldierGoal             = soldierGoal;
        this.movementRange           = range;
        CellData soldierGoalPosition = soldierGoal.GetPosition();
        CellData currentPosition     = cachedTransform.GetComponent<Soldier>().GetPosition();

        AStar.SetSearching();

        while(!IsInRange(currentPosition, soldierGoalPosition, range))
        {
            switch(AStar.GetNextMove(currentPosition, soldierGoalPosition))
            {
                case Locomotion.MoveDirection.Up:    MoveUp(currentPosition);    break;
                case Locomotion.MoveDirection.Right: MoveRight(currentPosition); break;
                case Locomotion.MoveDirection.Left:  MoveLeft(currentPosition);  break;
                case Locomotion.MoveDirection.Down:  MoveDown(currentPosition);  break;
            }
        }
    }

    public bool IsInRange(CellData currentCellData, CellData goal, int range)
    {
        int currentY = currentCellData.GetY();
        int currentX = currentCellData.GetX();
        int desiredY = goal.GetY();
        int desiredX = goal.GetX();
        int deltaY   = Mathf.Abs(currentY - desiredY);
        int deltaX   = Mathf.Abs(currentX - desiredX);

        return (deltaY <= range) && (deltaX <= range);
    }

    private void MoveLeft(CellData currentPosition)
    {
        BoardData boardData   = BoardData.Get();
        CellData  desiredCell = boardData.GetCellDataAt(currentPosition.GetY(), currentPosition.GetX() - 1);
        
        MoveTo(desiredCell, currentPosition);
    }

    private void MoveRight(CellData currentPosition)
    {
        BoardData boardData   = BoardData.Get();
        CellData  desiredCell = boardData.GetCellDataAt(currentPosition.GetY(), currentPosition.GetX() + 1);
        
        MoveTo(desiredCell, currentPosition);
    }

    private void MoveUp(CellData currentPosition)
    {
        BoardData boardData   = BoardData.Get();
        CellData  desiredCell = boardData.GetCellDataAt(currentPosition.GetY() + 1, currentPosition.GetX());
        
        MoveTo(desiredCell, currentPosition);
    }

    private void MoveDown(CellData currentPosition)
    {
        BoardData boardData   = BoardData.Get();
        CellData  desiredCell = boardData.GetCellDataAt(currentPosition.GetY() - 1, currentPosition.GetX());
        
        MoveTo(desiredCell, currentPosition);
    }

    private void MoveTo(CellData desiredCell, CellData currentPosition)
    {
        if(desiredCell.IsEmpty())
        {
            Vector3 desiredCellPosition = BoardBehaviour.Get().GetWorldPositionOfCell(desiredCell);
            Vector3 destination         = new Vector3 (desiredCellPosition.x, cachedTransform.position.y, desiredCellPosition.z);
            
            currentPosition.SetEmpty(true);
            desiredCell.SetEmpty(false);

            GameController.Get().StartCoroutine(this.MovingToCell(destination));
        }
        else
        {
            Move(soldierGoal, movementRange);
        }
    }


    IEnumerator MovingToCell(Vector3 movingTo)
    {
        while(Vector3.Distance(cachedTransform.position, movingTo) > float.Epsilon)
        {
            cachedTransform.position += (movingTo - cachedTransform.position).normalized * speed;
            yield return new WaitForEndOfFrame();
        }
    }

}
