/*
 * File: AlternativeMovement.cs
 * File Created: Saturday, 23rd November 2019 1:49:53 pm
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

public class AlternativeMovement
{
    public AlternativeMovement(){}

    public CellData GetBestCell(CellData currentPosition, CellData goal, int range)
    {
        int             initialRange    = range;
        CellData        bestCell        = null;
        float           bestDistance    = float.MaxValue;
        CellData        cell            = goal;
        List<CellData>  possibleCells   = new List<CellData>();
        BoardData       boardData       = BoardData.Get();

        while(range > 0)
        {
            foreach(CellData c in cell.Expand())
            {
                if(!(c is null))
                {
                    if(c.IsEmpty() && bestDistance > CalculateEuclideanGeometry(currentPosition, goal))
                    {
                        bestCell     = c;
                        bestDistance = CalculateEuclideanGeometry(currentPosition, goal);
                    }
                    
                    if(!possibleCells.Contains(c) && c.IsEmpty())
                    {
                        possibleCells.Add(c);
                    }
                }
            }
            
            if(!(bestCell is null) && cell != bestCell)
            {
                cell = bestCell;
                --range;
            }
            else
            {
                possibleCells.Remove(cell);
                cell         = goal;
                range        = initialRange;
                bestDistance = float.MaxValue;

                if(possibleCells.Count == 0)
                {
                    return null;
                }
            }
        }

        return bestCell;
    }

    private float CalculateEuclideanGeometry(CellData currentCell, CellData goalCell)
    {
        return Mathf.Abs(currentCell.GetX() - goalCell.GetX()) + Mathf.Abs(currentCell.GetY() - goalCell.GetY());
    }
}
