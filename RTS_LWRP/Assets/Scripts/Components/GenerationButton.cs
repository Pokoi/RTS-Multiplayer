/*
 * File: GenerationButton.cs
 * File Created: Friday, 8th November 2019 3:47:41 pm
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

public class GenerationButton : MonoBehaviour
{
    public void GenerateMeleeUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.melee);
    }

    public void GenerateRangedUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.ranged);
    }

    public void GenerateTankUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.tank);
    }

    public void GenerateHealerUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.healer);
    }

    private void GenerateUnit(UnitType unitType)
    {
        GameObject unit = GameController.Get().unitsPool.GetUnitInstance(unitType);
        unit.SetActive(true);

        DraggeableUnit draggeableUnitComponent = unit.GetComponent<DraggeableUnit>();
        draggeableUnitComponent.enabled = true;

        if (draggeableUnitComponent == null)
        {
           draggeableUnitComponent = unit.AddComponent<DraggeableUnit>();
        }
        unit.GetComponent<Soldier>().SetUnitType(unitType);
        draggeableUnitComponent.AddFollowCursorComponent();
    }
   
   private bool AbleToGenerateMoreUnits()
   {
       return ( PlayerController.Get().GetChoosenUnitsCount() < AIController.Get().GetMaxUnitsInTeam()) && !(DraggeableUnit.draggingUnit);
   }
}
