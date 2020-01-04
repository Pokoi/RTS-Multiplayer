/*
 * File: InputController.cs
 * File Created: Saturday, 4th January 2020 4:27:39 pm
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

public class InputController : MonoBehaviour
{
    PlayerController cachedPlayerController;
    FormationManager cachedFormationManager;

    int formationIndexSelected;

    // Start is called before the first frame update
    void Start()
    {
        cachedPlayerController = GetComponent<PlayerController>();
        cachedFormationManager = GetComponent<FormationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if LT is being pressed
        //CenterCameraInFormation(0);

        //else if RT is being pressed
        //CenterCameraInFormation(1);

        //else if LB is being pressed
        //CenterCameraInFormation(2);

        //else if RB is being pressed
        //CenterCameraInFormation(-1);

        //else if LT is released 
        //     or RT is released
        //     or LB is released
        //     or RB is released
        //CenterCameraInCursor();

        //else if triangle is pressed
        //MoveFormationToCursor(0);

        //else if circle is pressed
        //MoveFormationToCursor(1);

        //else if cross is pressed
        //MoveFormationToCursor(2);

        //else if square is pressed
        //MoveAllFormationToCursor();

        //if LJoyStick is moving
        //MoveCursor();

        //if RJoyStick is moving
        //RotateCamera();

    }

    private void MoveFormationToCursor(int index)
    {
        cachedFormationManager.MoveFormationToPosition(index, cachedPlayerController.GetPlayerCursor().GetPosition());
    }

    private void MoveAllFormationToCursor()
    {
        cachedFormationManager.MoveAllFormationsToPosition(cachedPlayerController.GetPlayerCursor().GetPosition());
    }

    private void CenterCameraInFormation(int index)
    {
        Vector3 center;

        if(index == -1)
        {
            center = cachedFormationManager.GetCenterFormation();
        }
        else
        {
            center = cachedFormationManager.GetCenterOfFormation(index);
        }

        cachedPlayerController.GetPlayerCamera().CenterToPosition(center);
    }

    private void MoveCursor()
    {
        Vector3 position = cachedPlayerController.GetPlayerCursor().GetPosition();

        //position +=
    }

    private void MoveCamera()
    {

    }

    private void CenterCameraInCursor()
    {
        cachedPlayerController.GetPlayerCamera().CenterToPosition(cachedPlayerController.GetPlayerCursor().GetPosition());
    }

}
