/*
 * File: RegretMatching.cs
 * File Created: Wednesday, 30th October 2019 10:52:51 am
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

public class RegretMatching <T> : AIAlgorithim <T> where T : Action
{
    float           initial_regret = 10f;
    public float[]  regret;
    public float[]  chance;
    public T        last_action;
    public uint     num_actions;

    public float [] GetRegret()                 => regret;
    public float [] GetChance()                 => chance;
    public void     SetRegret(float [] regret)  => this.regret = regret;
    public void     SetChance(float [] chance)  => this.chance = chance;
    public override T Play()                    => GetNextAction();
    
    public RegretMatching(Actions<T> _possibleActions) : base (_possibleActions)
    {
        num_actions = _possibleActions.GetCount();
        regret      = new float[num_actions];
        chance      = new float[num_actions];
    }

    public override T GetNextAction()
    {
        float sum = 0;
        float prob = 0f;

        for (int i = 0; i < num_actions; ++i)
        {
            if (regret[i] > 0f) sum += regret[i];
        }

        if (sum <= 0)
        {
            last_action = possibleActions.GetAt((uint)UnityEngine.Random.Range(0, num_actions));
        }
        else
        {
            for (int i = 0; i < num_actions; ++i)
            {
                chance[i] = 0;
                if (regret[i] > 0f) chance[i] = regret[i];
                if (i > 0) chance[i] += chance[i - 1];
            }
            if (chance[num_actions - 1] > 0)
            {
                prob = UnityEngine.Random.Range(0, chance[num_actions - 1]);
                for (int i = 0; i < num_actions; ++i)
                {
                    if (prob < chance[i])
                    {
                        last_action = possibleActions.GetAt((uint)i);
                    }
                }
            }
        }
        return last_action;
    }

    public override void UpdateValues(T oponentAction)
    {
        for (int i = 0; i < num_actions; ++i)
        {
            regret[i] += GetUtilityOf(possibleActions.GetAt((uint)i), oponentAction);
            regret[i] -= GetUtilityOf(last_action, oponentAction);
        }
    }

}
