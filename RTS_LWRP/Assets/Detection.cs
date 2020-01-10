/*
 * File: Detection.cs
 * File Created: Friday, 10th January 2020 4:58:44 pm
 * ––––––––––––––––––––––––
 * Author: Jesus Fermin, 'Pokoi', Villar  (hello@pokoidev.com)
 * ––––––––––––––––––––––––
 * MIT License
 * 
 * Copyright (c) 2020 Pokoidev
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

public class Detection : MonoBehaviour
{
    Soldier cachedSoldier;   
    List<Soldier> detectedSoldiers = new List<Soldier>();

    public List<Soldier> GetDetectedSoldiers() => detectedSoldiers;

    private void Start() => cachedSoldier = GetComponentInParent<Soldier>();
    
    private void OnTriggerEnter(Collider other) 
    {
        Soldier otherSoldier = other.GetComponentInParent<Soldier>();
        
        if(otherSoldier != null && !detectedSoldiers.Contains(otherSoldier))
        {
            detectedSoldiers.Add(otherSoldier);
        }

        cachedSoldier.Decide();
    }

    private void OnTriggerExit(Collider other) 
    {
        Soldier otherSoldier = other.GetComponentInParent<Soldier>();
        
        if(otherSoldier != null && detectedSoldiers.Contains(otherSoldier))
        {
            detectedSoldiers.Remove(otherSoldier);
        }

        cachedSoldier.Decide();
    }
}
