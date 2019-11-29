/*
 * File: TeamData.cs
 * File Created: Saturday, 23rd November 2019 12:58:27 am
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
using UnityEngine;

public class TeamData 
{
    public enum Owners 
    {
        Player, AI, AIDebug
    }
    protected Owners owner;
    protected Color  debugLineColor;
    
    protected int teamScore;
    protected List<Soldier> teamSoldiers;
    protected int soldiersAliveInTeam = 0;

    public Color    GetDebugColor()     => this.debugLineColor;
    public int      GetScore()          => this.teamScore;
    public void     SetScore(int score) => this.teamScore += score;
    public Owners   GetOwner()          => this.owner;
    public void     ClearSoldiers()     => this.teamSoldiers.Clear();
    public List<Soldier> GetSoldiers()  => this.teamSoldiers;
    public void AddSoldier(Soldier soldier) 
    {
        this.teamSoldiers.Add(soldier);
        ++soldiersAliveInTeam;
    }
    public void OnUnitKilled()
    {
        --soldiersAliveInTeam;
        if(teamSoldiers.Count == 0)
        {
            GameController.Get().OnBattleEnds();
        }
        if(CheckIfOnlyLastAHealer())
        {
            switch(owner)
            {
                case Owners.AI:
                    if (GameController.Get().GetPlayerTeamData().CheckIfOnlyLastAHealer())
                    {
                        GameController.Get().OnBattleEnds();
                    }
                break;
                case Owners.Player:
                    if (GameController.Get().GetAITeamData().CheckIfOnlyLastAHealer())
                    {
                        GameController.Get().OnBattleEnds();
                    }
                break;
            }
        }
    }

    public bool CheckIfOnlyLastAHealer()
    {
        if(teamSoldiers.Count == 1)
        {
            foreach (Soldier soldier in teamSoldiers)
            {
                if (!(soldier is null) && soldier.GetUnitType() == UnitType.healer)
                {
                    return true;
                }
            }
        }

        return false;
    }

}

public class PlayerTeam : TeamData
{
    public PlayerTeam()
    {
        owner           = Owners.Player;
        debugLineColor  = Color.blue;
        teamScore       = 0;
        teamSoldiers    = new List<Soldier>();
    }
}

public class AITeam: TeamData
{
    public AITeam()
    {
        owner           = Owners.AI;
        debugLineColor  = Color.red;
        teamScore       = 0;
        teamSoldiers    = new List<Soldier>();
    }
}
