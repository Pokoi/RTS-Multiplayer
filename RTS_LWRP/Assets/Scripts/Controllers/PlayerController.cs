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
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
    public CursorElement    playerCursor;
    public CameraBehaviour  playerCamera;

    int             choosenFormationsCount;
    ArmyAction      playerFormation;
    List<Soldier>   targetSoldiers;

    FormationManager formationManager;
    
    int[] formationCount = new int[Enum.GetNames(typeof(UnitType)).Length];
    
    public void UpdateChoosenFormations(int value)  => choosenFormationsCount += value;
    public void ResetChoosenUnits()                 => choosenFormationsCount = 0;
    public int  GetChoosenFormationsCount()         => choosenFormationsCount;
    public int[] GetFormationCount()                => formationCount;
    public CursorElement GetPlayerCursor()          => playerCursor;
    public CameraBehaviour GetPlayerCamera()        => playerCamera;
    public ArmyAction GetPlayerFormation()          => playerFormation;
    public ArmyAction SetPlayerFormation(ArmyAction formation)  => playerFormation = formation;
    public void UpdateFormationCount(int value, int index)      => formationCount[index] += value;
    public void SetTargetSoldiers(List<Soldier> soldiers) => targetSoldiers = soldiers;
    public List<Soldier> GetTargetSoldiers()        => targetSoldiers;
    public FormationManager GetFormationManager()   => formationManager;

    public void OnUnitIsKilled(Soldier soldier)
    {

    }


    public void InitializeArmyAction()
    {
        playerFormation = new ArmyAction(GameController.Get().GetFormationsPerPlayer());

        for(int i = 0; i < formationCount.Length; ++i)
        {
            if(formationCount[i] > 0)
            {
                for(int j = 0; j < formationCount[i]; ++j)
                {
                    ArmyFormation formation = new ArmyFormation (GameController.Get().GetUnitsPerFormation());
                    for(int k = 0; k < GameController.Get().GetUnitsPerFormation(); ++k)
                    {
                        formation.AddSoldier(GenerateUnit((UnitType) i));
                    }
                    playerFormation.AddFormation(formation);
                }
            }
        }
    }

    private Soldier GenerateUnit(UnitType unitType)
    {
        GameObject unit = GameController.Get().unitsPool.GetUnitInstance(unitType);
        unit.SetActive(true);
        unit.GetComponent<Soldier>().SetUnitType(unitType);
        unit.GetComponent<Soldier>().SetPlayerController(this);

        return unit.GetComponent<Soldier>();
    }

    private void Awake() 
    {
        formationManager = GetComponent<FormationManager>();    
    }
}
