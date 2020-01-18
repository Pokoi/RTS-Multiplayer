/*
 * File: NavAgentFollowCursor.cs
 * File Created: Monday, 16th December 2019 6:13:47 pm
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
using UnityEngine.AI;

public class NavAgentFollowCursor : MonoBehaviour
{
    NavMeshAgent    agent;
    Transform       cachedTransform;
    Vector3         destination;
    AnimationController animationController;
   
    
    // Start is called before the first frame update
    void Awake()
    {
        agent               = GetComponent<NavMeshAgent>();
        cachedTransform     = transform;
        animationController = GetComponent<AnimationController>();
    }
    
    public void StopMovement()
    {
        StopCoroutine("FollowingCursor");
        agent.isStopped = true;
    }

    public void SetDestination(Vector3 target) 
    {
        StopCoroutine("FollowingCursor");
        destination = target;
        animationController.Walk();
        StartCoroutine(FollowingCursor(1));
    }
   

    IEnumerator FollowingCursor(float delta)
    {
        while(true)
        {
            agent.destination = new Vector3(destination.x, cachedTransform.position.y, destination.z);

            float distance = agent.remainingDistance;
            if  (
                    distance != Mathf.Infinity &&
                    agent.pathStatus == NavMeshPathStatus.PathComplete &&
                    agent.remainingDistance <= 0.1f
                )
                {
                    animationController.Idle();
                }
            yield return new WaitForSeconds(delta);
        }
    }


}
