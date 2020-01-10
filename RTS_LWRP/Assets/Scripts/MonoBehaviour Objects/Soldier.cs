/*
 * File: Soldier.cs
 * File Created: Thursday, 14th November 2019 3:05:40 pm
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
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    Unit                unit;
    UnitType            unitType;
    BattleDecisionMaker battleDecisionMaker;
    PlayerController    owner;
    Detection           detector;
    bool                readyToFight;

    public void     SetUnit(Unit unit)             => this.unit = unit;
    public void     SetUnitType(UnitType unitType) => this.unitType = unitType;
    
    public Unit     GetUnit()        => this.unit;
    public UnitType GetUnitType()    => this.unitType;

    public PlayerController GetOwner() => this.owner;

    public int      GetHealth()      => unit.GetUnitData().GetBehaviour().GetHealth();
    public int      GetTotalHealth() => unit.GetUnitData().GetBehaviour().GetTotalHealth();
    public int      GetDamageDone()  => unit.GetUnitData().GetBehaviour().GetDamageDone();
    public void     SetPlayerController(PlayerController playerController) => owner = playerController;
    public void     Reset()          
    {
        if(!(unit is null)) 
        {
            unit.GetUnitData().GetBehaviour().Reset();
        }
    } 
        
    public void Start() 
    {
        battleDecisionMaker = new BattleDecisionMaker(this);
        detector            = GetComponentInChildren<Detection>();
    }

    public void Decide()
    {
        StopCoroutine("Fight");
        List<Soldier> targets;

        if(unitType == UnitType.healer)
        {
            targets = owner.GetPlayerFormation().GetOwnSoldiers();
        }
        else
        {
            targets = owner.GetEnemySoldiers();
        } 

        List<Soldier> sawSoldiers = new List<Soldier>();
        sawSoldiers.AddRange(detector.GetDetectedSoldiers());

        foreach(Soldier soldier in sawSoldiers)
        {
            if(!targets.Contains(soldier))
            {
                sawSoldiers.Remove(soldier);
            }
        }

        Battle(sawSoldiers);
    }

    void Battle(List<Soldier> targets)
    {
        Soldier target = battleDecisionMaker.ChooseSoldierToAttack(targets);
        if(target)
        {
            if(Mathf.Abs(Vector3.Distance(this.GetComponent<Transform>().position, target.GetComponent<Transform>().position)) < unit.GetUnitData().GetBehaviour().GetAttackRange())
            {
                StartCoroutine(Fight(target));
            }
            else 
            {
                NavAgentFollowCursor agent = GetComponent<NavAgentFollowCursor>();
                agent.SetDestination(target.GetComponent<Transform>().position);
            }
        }
    }

    IEnumerator Fight(Soldier target)
    {
        UnitBehaviour   behaviour   = unit.GetUnitData().GetBehaviour();
        int             range       = behaviour.GetAttackRange();

        while(true && target != null)
        {
            
            behaviour.Attack(target);
            yield return new WaitForSeconds(1 - behaviour.GetActionSpeed());
            
            if(unitType == UnitType.healer)
            {
                if(target.GetHealth() == target.GetTotalHealth() || target.GetHealth() == 0)
                {
                    break;
                }
            }
            else
            {
                if(target.GetHealth() <= 0)
                {
                    target.gameObject.SetActive(false);
                    var formationUnits = target.GetOwner().GetFormationManager().GetFormationUnits();
                    
                    NavAgentFollowCursor agent = target.GetComponent<NavAgentFollowCursor>();

                    foreach(NavAgentsFormation formation in formationUnits)
                    {
                        if(formation.GetFormationUnits().Contains(agent))
                        {
                            formation.RemoveAgent(agent);
                            break;
                        }
                    }
                    
                    break;
                }
            }
        }

        Decide();
    }

}
