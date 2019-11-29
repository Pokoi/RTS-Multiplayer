/*
 * File: PlayerController.cs
 * File Created: Monday, 4th November 2019 5:38:24 pm
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


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int                     choosenUnitsCount;
    static PlayerController instance;

    ArmyAction playerFormation;
    
    public static PlayerController Get()        => instance;
    public void UpdateChoosenUnits(int value)   => choosenUnitsCount += value;
    public void ResetChoosenUnits()             => choosenUnitsCount = 0;
    public int  GetChoosenUnitsCount()          => choosenUnitsCount;
    public ArmyAction GetPlayerFormation()      => playerFormation;
    public ArmyAction SetPlayerFormation(ArmyAction formation) => playerFormation = formation;

    public PlayerController Create()
    {
        if(instance == null)
        {
            instance = this;
        }

        return instance;
    }
    public void AddUnitToFormation(Unit u)
    {
        if(playerFormation is null)
        {
            playerFormation = new ArmyAction(AIController.Get().GetMaxUnitsInTeam());
        }

        if(choosenUnitsCount < (playerFormation.GetUnits().Length))
        {
            playerFormation.GetUnits()[choosenUnitsCount] = u;
            ++choosenUnitsCount;
        }
    }

    public void RemoveLastUnitFromFormation()
    {
        if(playerFormation != null && choosenUnitsCount > 0)
        {
            --choosenUnitsCount;
            playerFormation.GetUnits()[choosenUnitsCount] = null;
        }
    }

    public void RemoveUnitFromFormation(Unit u)
    {
        if(playerFormation != null && choosenUnitsCount > 0)
        {
            Unit[] playerUnits = playerFormation.GetUnits();
            
            int index;
            for (index = 0; index < playerUnits.Length && playerUnits[index] != u; ++index);
            if(index < playerUnits.Length)
            {
                playerUnits[index] = null;
                --choosenUnitsCount;
            }
        }        
    }


    private void Start() => Create();

}
