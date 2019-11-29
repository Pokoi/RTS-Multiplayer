/*
 * File: UCB1.cs
 * File Created: Tuesday, 29th October 2019 5:24:36 pm
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCB1 <T> : AIAlgorithim <T> where T : Action
{
    int         playedRounds;    
    int     []  timesPlayedByAction;        
    float   []  scoreByAction;  
    float   []  UCB1scoresByAction; 
    uint        totalAvailableActions;   
    T           selfLastAction;

    public UCB1(Actions<T> _possibleActions) : base (_possibleActions)
    {
        this.totalAvailableActions  = this.possibleActions.GetCount();
        playedRounds                = (int) totalAvailableActions;
        timesPlayedByAction         = new int   [totalAvailableActions];
        scoreByAction               = new float [totalAvailableActions];
        UCB1scoresByAction          = new float [totalAvailableActions];

        for(int i = 0; i < timesPlayedByAction.Length; ++i)
        {
            timesPlayedByAction[i] = 1;
        }

    }

    public override T Play() => GetNextAction();

    public override T GetNextAction()
    {
        int i, best;
        float bestScore;
        float tempScore;

        if(scoreByAction[0] == 0f)
        {
            for(int index = 0; index < timesPlayedByAction.Length; ++index)
            {
                scoreByAction[index] = this.GetUtility()[0][index];
            }
        }
        
        best        = -1;
        bestScore   = int.MinValue;
        for(i = 0; i <totalAvailableActions; i++)
        {
            tempScore  = GetUCB1(scoreByAction[i] / timesPlayedByAction[i], timesPlayedByAction[i], playedRounds);
            UCB1scoresByAction[i] = tempScore;
            if (tempScore > bestScore)
            {
                best = i;
                bestScore = tempScore;
            }
        }
        selfLastAction = possibleActions.GetAt((uint)best);
        return selfLastAction;    
    }
    private float GetUCB1(float averageUtility, float count, float totalActions)
    {
        return averageUtility + Mathf.Sqrt(2 + Mathf.Log10(totalActions) / count);
    }

    public override void UpdateValues (T oponentAction)
    {
        playedRounds++;
        float utility;
        //T selfAction = GameController.Get().aiController.GetFormation() as T;
        //utility = GetUtilityOf(selfAction,oponentAction);
        utility = GetUtilityOf(selfLastAction, oponentAction);
        //scoreByAction[selfAction.Index] += utility;
        scoreByAction[selfLastAction.Index] += utility;
        //timesPlayedByAction[selfAction.Index]++;
        timesPlayedByAction[selfLastAction.Index]++;
    }
}