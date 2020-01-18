/*
 * File: GameController.cs
 * File Created: Wednesday, 6th November 2019 10:39:46 am
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

public class GameController : MonoBehaviour
{
    public UnitsPool                unitsPool;
    public List <PlayerController>  playerControllers;
    public ScoreController          scoreController;
    public GameObject               decideFormationCanvas;
    public List<Transform>          originPoints;

    static GameController           instance;

    bool        callToEnd;
    int         unitsPerFormation   = 4;
    int         formationsPerPlayer = 3;
    int         callsToStart        = 0;
    int         playersReady        = 0;

    public static GameController Get()      => instance;
    public int  GetUnitsPerFormation()      => unitsPerFormation;
    public int  GetFormationsPerPlayer()    => formationsPerPlayer;
    
    public void OnBattleEnds()
    {
        if(!callToEnd)
        {
            callToEnd = true;

            // Calculate score
            //int score = scoreController.CalculateScoreRelativeToAI(playerTeamData, AITeamData);

            
            unitsPool.Reset();
        }
    }

    private void Awake() 
    {
        if(instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }

        unitsPool.SetMaxUnitsCount(unitsPerFormation * formationsPerPlayer * playerControllers.Count);
    }

   public void OnPlayerReady()
   {
       ++playersReady;

       if(playersReady == playerControllers.Count)
       {
           foreach(PlayerController controller in playerControllers)
           {
               controller.InitializeArmyAction();
           }

           OnStartBattle();
       }
   }

    public void OnPlayerDecideFormation()
    {
        //Reset choosen units player controllers
        foreach(PlayerController playerController in playerControllers)
        {
            playerController.ResetChoosenUnits();
        }

        //Show canvas of decide formation
        decideFormationCanvas.SetActive(true);
        
    }

    public void OnStartBattle()
    {
        callToEnd = false;

        InitializePlayerControllersTargetSoldiers();
        
        decideFormationCanvas.SetActive(false);

        InitializeUnitsPositionsInWorld();
        

        foreach (PlayerController playerController in playerControllers)
        {
            playerController.GetComponent<InputController>().SetInGame(true);
        }
        
    }

    public void OnGameEnds()
    {
        
       

       #if UNITY_EDITOR
       UnityEditor.EditorApplication.isPlaying = false;
       #endif
    }
 

    private void InitializePlayerControllersTargetSoldiers()
    {
        for(int i = 0; i < playerControllers.Count; ++i)
        {
            List<Soldier> soldiers = new List<Soldier>();
            
            for(int j = i+1; j<playerControllers.Count; ++j)
            {
                if(playerControllers[i] != playerControllers[j])
                {
                    soldiers.AddRange(playerControllers[j].GetPlayerFormation().GetOwnSoldiers());
                }
            }
            playerControllers[i].SetEnemySoldiers(soldiers);
        }
    }

    private void InitializeUnitsPositionsInWorld()
    {
        int iterator = 0;
        foreach(PlayerController playerController in playerControllers)
        {
            Vector3 point = originPoints[iterator].position;

            foreach (Soldier soldier in playerController.GetPlayerFormation().GetOwnSoldiers())
            {
                soldier.GetComponent<Transform>().position = point;
            }
            
            
            /*FormationManager formationManager = playerController.GetFormationManager();
            formationManager.CalculateCenter();
            formationManager.SetCenterOfFormation(point);
            formationManager.RegroupFormation();*/

            ++iterator;
        }
    }


}
