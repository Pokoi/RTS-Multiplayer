/*
 * File: Action.cs
 * File Created: Friday, 1st November 2019 10:31:59 am
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


using System.Collections.Generic;

public class ArmyAction
{

    List <ArmyFormation> formations;

    public ArmyAction(int unitsCount)           => this.formations = new List<ArmyFormation>(unitsCount);
    public ArmyAction (List <ArmyFormation> formations) =>  this.formations = formations;
    
    public List<ArmyFormation> GetFormations()       => formations;
    public void AddFormation (ArmyFormation formation) => formations.Add(formation);
    public List<Soldier> GetOwnSoldiers()
    {
        List<Soldier> soldiers = new List<Soldier>();

        foreach (ArmyFormation formation in formations)
        {
            soldiers.AddRange(formation.GetSoldiers());
        }

        return soldiers;
    }

}

public class ArmyFormation
{
    List <Soldier> soldiers;

    public ArmyFormation(int unitsCount)           => this.soldiers = new List<Soldier>(unitsCount);
    public ArmyFormation (List <Soldier> soldiers) =>  this.soldiers = soldiers;
    
    public List<Soldier> GetSoldiers()       => soldiers;
    public void AddSoldier (Soldier soldier) => soldiers.Add(soldier);
}
