/*
 * File: BattleDecisionMaker.cs
 * File Created: Wednesday, 20th November 2019 10:02:48 am
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

public class BattleDecisionMaker 
{
    Soldier selfSoldier;
    byte    remainingHealthWeight  = 1;
    byte    damageDoneWeight       = 2;
    byte    distanceWeight         = 1;

    public BattleDecisionMaker(Soldier selfSoldier) => this.selfSoldier = selfSoldier;

    public Soldier ChooseSoldierToAttack(List<Soldier> soldiers)
    {
        int bestHeuristic   = int.MinValue;
        Soldier bestSoldier = null;

        foreach(Soldier soldier in soldiers)
        {
            int soldierHeuristic = CalculateHeuristics(soldier);
            if (soldierHeuristic > bestHeuristic)
            {
                bestHeuristic = soldierHeuristic;
                bestSoldier   = soldier;
            }
        }

        return bestSoldier;
    }
    private int CalculateHeuristics( Soldier soldier)
    {
       return (int) (remainingHealthWeight  *  (soldier.GetHealth() / soldier.GetTotalHealth()) +
                     damageDoneWeight       * soldier.GetDamageDone() +
                     distanceWeight         * (Vector3.Distance(selfSoldier.transform.position, soldier.transform.position))
                    );
    }
}
