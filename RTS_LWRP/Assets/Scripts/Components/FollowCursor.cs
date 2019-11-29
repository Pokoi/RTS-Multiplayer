/*
 * File: FollowCursor.cs
 * File Created: Wednesday, 13th November 2019 8:42:51 am
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
using UnityEngine;

public class FollowCursor
{
    bool        following;
    Transform   transformToMove;
    float       movementSpeed = 2.6f;
    LayerMask   layerMask;

    public FollowCursor(Transform transformToMove, LayerMask layerMask) 
    {
        this.transformToMove    = transformToMove;
        this.layerMask          = layerMask;
    }

    public void StartMoving()
    {
        this.following = true;
        GameController.Get().StartCoroutine(this.UpdatePosition());
    }

    public void StopMoving()
    {
        this.following = false;
        GameController.Get().StopCoroutine(this.UpdatePosition());
    }

    public bool GetFollowing() => following;

    IEnumerator UpdatePosition()
    {
        while(following)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                transformToMove.position = Vector3.Slerp(transformToMove.position, hit.point, Time.deltaTime * movementSpeed);
            }

            yield return new WaitForEndOfFrame();
        }
    }


}
