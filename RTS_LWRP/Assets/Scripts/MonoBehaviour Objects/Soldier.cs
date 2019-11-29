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
    Locomotion          locomotion;
    TeamData            team;
    List<Soldier>       targetSoldiers;
    Slider              slider;
    bool                readyToFight;

    public void     SetUnit(Unit unit)             => this.unit = unit;
    public void     SetUnitType(UnitType unitType) => this.unitType = unitType;
    public void     SetTeam(TeamData team)         => this.team = team;
    public TeamData GetTeam()                      => this.team;

    public Unit     GetUnit()        => this.unit;
    public UnitType GetUnitType()    => this.unitType;
    public CellData GetPosition()    => unit.GetPosition();
    public int      GetHealth()      => unit.GetUnitData().GetBehaviour().GetHealth();
    public int      GetTotalHealth() => unit.GetUnitData().GetBehaviour().GetTotalHealth();
    public int      GetDamageDone()  => unit.GetUnitData().GetBehaviour().GetDamageDone();
    public void     Reset()          
    {
        if(!(unit is null)) 
        {
            unit.GetUnitData().GetBehaviour().Reset();
        }
    } 
        
    public void SetReadyToFight(bool readyToFight)
    {
        this.readyToFight = readyToFight;
        if(this.readyToFight)
        {
            SetTargetSoldiers();
            Battle();
        }
        else
        {
            slider.value = 1;
        }
    }

    public void ApplyBuff()
    {
        List<Soldier> buffableSoldiers = team.GetSoldiers();

        foreach(Soldier soldier in buffableSoldiers)
        {
            if (IsInBuffRange(this.GetPosition(), soldier.GetPosition()))
            {
                this.GetUnit().GetUnitData().GetBehaviour().ApplyBuffEfect(soldier);
            }
        }

    }
    public void Start() 
    {
        battleDecisionMaker = new BattleDecisionMaker(this);
        locomotion          = new Locomotion(transform);
        slider              = GetComponentInChildren<Slider>();
        slider.GetComponentInParent<Canvas>().worldCamera = Camera.main; 
    }

    private void Update() 
    {
        if(this.readyToFight)
        {
            slider.value = (float) GetHealth() / (float) GetTotalHealth();
        }
    }

    void Battle()
    {
        if(this.readyToFight)
        {
            Soldier target = battleDecisionMaker.ChooseSoldierToAttack(targetSoldiers);
            //locomotion.Move(target, unit.GetUnitData().GetBehaviour().GetAttackRange());

            StartCoroutine(Fight(target));
        }
    }

    void SetTargetSoldiers()
    {
        if(unitType == UnitType.healer)
        {
            targetSoldiers = team.GetSoldiers();
        }
        else
        {
            switch(team.GetOwner())
            {
                case TeamData.Owners.Player:
                    targetSoldiers = GameController.Get().GetAITeamData().GetSoldiers();
                break;

                case TeamData.Owners.AI:
                    targetSoldiers = GameController.Get().GetPlayerTeamData().GetSoldiers();
                break;                
            }
        }
    }
    bool IsInBuffRange(CellData soldier, CellData otherSoldier)
    {
        int soldierX        = soldier.GetX();
        int soldierY        = soldier.GetY();
        int otherSoldierX   = otherSoldier.GetX();
        int otherSoldierY   = otherSoldier.GetY();

        int deltaX = Mathf.Abs(soldierX - otherSoldierX);
        int deltaY = Mathf.Abs(soldierY - otherSoldierY);

        return ((deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1));
    }

    IEnumerator Fight(Soldier target)
    {
        UnitBehaviour   behaviour   = unit.GetUnitData().GetBehaviour();
        int             range       = behaviour.GetAttackRange();

        //while(locomotion.IsInRange(this.GetPosition(), target.GetPosition(), range))
        while(true && target != null)
        {
            Debug.DrawLine(transform.position, target.transform.position, team.GetDebugColor(), behaviour.GetActionSpeed());
            
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
                    target.GetPosition().SetEmpty(true);
                    targetSoldiers.Remove(target);
                    target.GetTeam().OnUnitKilled();
                    break;
                }
            }
        }

        Battle();
    }

}
