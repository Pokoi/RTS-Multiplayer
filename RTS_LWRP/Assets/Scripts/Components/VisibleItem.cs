/*
 * File: VisibleItem.cs
 * File Created: Friday, 8th November 2019 9:26:50 am
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

public class VisibleItem
{
    List<Transform> visualTransforms = new List<Transform>();
    Collider        collider = null;
    float           maxAparitionSpeed = 10f;
    float           minAparitionSpeed = 3f;


    public      VisibleItem()                                     => visualTransforms = new List<Transform>();
    public void SetVisibleItems(List<Transform> visualTransforms) => this.visualTransforms = visualTransforms;
    public void SetCollider(Collider collider)                    => this.collider = collider;
    public void Hide()
    {
        foreach (Transform t in this.visualTransforms)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
            if(collider != null) collider.enabled = false;
        }
    }

    public void Show()
    {
        float lastSpeed = Random.Range(maxAparitionSpeed, minAparitionSpeed);

        foreach (Transform t in this.visualTransforms)
        {
            Vector3 desiredPosition = t.localPosition;
            t.localPosition = new Vector3 (t.localPosition.x, t.localPosition.y - 100, t.localPosition.z);
            t.GetComponent<SpriteRenderer>().enabled = true;
            GameController.Get().StartCoroutine(this.InterpolatedMovement(desiredPosition, t, lastSpeed));
            if(collider != null) collider.enabled = true;
        }
    }

    IEnumerator InterpolatedMovement(Vector3 desiredPosition, Transform transformToMove, float speed)
    {
        float alpha = 1f;
        while ((desiredPosition - transformToMove.position).sqrMagnitude > alpha)
        {
            transformToMove.localPosition = Vector3.Lerp(transformToMove.localPosition, desiredPosition, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
