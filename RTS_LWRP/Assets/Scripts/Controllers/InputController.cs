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

    string [] axisNames = 
    {
        "Left Stick Horizontal 1",
        "Left Stick Horizontal 2",
        "Left Stick Vertical 1",
        "Left Stick Vertical 2",
        "Right Stick Horizontal 1",
        "Right Stick Horizontal 2",
        "Right Stick Vertical 1",
        "Right Stick Vertical 2",
        "Horizontal",
        "Vertical",
        "Mouse X",
        "Mouse Y"
    };

    string [] ButtonNames = 
    {
        "Cross 1",
        "Cross 2",
        "Circle 1",
        "Circle 2",
        "Square 1",
        "Square 2",
        "Triangle 1",
        "Triangle 2",
        "L1 1",
        "L1 2",
        "L2 1",
        "L2 2",
        "R1 1",
        "R1 2",
        "R2 1",
        "R2 2",
        "Start 1",
        "Start 2",
        "Numeric 1",
        "Numeric 2",
        "Numeric 3",
        "Numeric 4",
        "Numeric 5",
        "Numeric 6",
        "Numeric 7",
        "Numeric 8",
        "Numeric 9",
        "Numeric 0"

    };

    public enum Axis 
    {
        LeftStick_Horizontal_Joystick1,
        LeftStick_Horizontal_Joystick2,
        LeftStick_Vertical_Joystick1,
        LeftStick_Vertical_Joystick2,
        RightStick_Horizontal_Joystick1,
        RightStick_Horizontal_Joystick2,
        RightStick_Vertical_Joystick1,
        RightStick_Vertical_Joystick2,
        Horizontal,
        Vertical,
        Mouse_X,
        Mouse_Y

    }

    public enum Buttons
    {
        Cross_Joystick1,
        Cross_Joystick2, 
        Circle_Joystick1,
        Circle_Joystick2,
        Square_Joystick1,
        Square_Joystick2,
        Triangle_Joystick1,
        Triangle_Joystick2,
        L1_Joystick1,
        L1_Joystick2,
        L2_Joystick1,
        L2_Joystick2,
        R1_Joystick1,
        R1_Joystick2,
        R2_Joystick1,
        R2_Joystick2,
        Start_Joystick1,
        Start_Joystick2,
        Numeric_1,
        Numeric_2,
        Numeric_3,
        Numeric_4,
        Numeric_5,
        Numeric_6,
        Numeric_7,
        Numeric_8,
        Numeric_9,
        Numeric_0
    }

    [SerializeField] private Buttons centerCameraInFirstFormationInput;
    [SerializeField] private Buttons centerCameraInSecondFormationInput;
    [SerializeField] private Buttons centerCameraInThirdFormationInput;
    [SerializeField] private Buttons centerCameraInFormationsCenterInput;
    [SerializeField] private Buttons moveFirstFormationToCursorInput;
    [SerializeField] private Buttons moveSecondFormationToCursorInput;
    [SerializeField] private Buttons moveThirdFormationToCursorInput;
    [SerializeField] private Buttons moveAllFormationsToCursorInput;
    [SerializeField] private Axis    moveCursorHorizontalInput;
    [SerializeField] private Axis    moveCursorVerticalInput;
    [SerializeField] private Axis    moveCameraHorizontalInput;
    [SerializeField] private Axis    moveCameraVerticalInput;
    

    PlayerController cachedPlayerController;
    FormationManager cachedFormationManager;
    int              formationIndexSelected;
    bool             inGame;

    public void SetInGame(bool status) => inGame = status;

    // Start is called before the first frame update
    void Start()
    {
        cachedPlayerController = GetComponent<PlayerController>();
        cachedFormationManager = GetComponent<FormationManager>();
        inGame                 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inGame)
        {
        
            /*
                 if (Input.GetAxis(ButtonNames[(int) centerCameraInFirstFormationInput])  != 0.0f   )
            {
                CenterCameraInFormation(0);
            }

            else if (Input.GetAxis(ButtonNames[(int) centerCameraInSecondFormationInput] )  != 0.0f )
            {
                CenterCameraInFormation(1);
            }

            else if (Input.GetAxis(ButtonNames[(int) centerCameraInThirdFormationInput]  )  != 0.0f )
            {
                CenterCameraInFormation(2);
            }

            else if (Input.GetAxis(ButtonNames[(int) centerCameraInFormationsCenterInput])  != 0.0f )
            {
                CenterCameraInFormation(-1);
            }
            */

                 if (Input.GetKey(KeyCode.Alpha1) )
            {
                CenterCameraInFormation(0);
            }

            else if (Input.GetKey(KeyCode.Alpha2) )
            {
                CenterCameraInFormation(1);
            }

            else if (Input.GetKey(KeyCode.Alpha3) )
            {
                CenterCameraInFormation(2);
            }

            else if (Input.GetKey(KeyCode.Alpha4) )
            {
                CenterCameraInFormation(-1);
            }

            else if (
                        Input.GetAxis(ButtonNames[(int) centerCameraInFirstFormationInput]  ) == 0.0f  &&
                        Input.GetAxis(ButtonNames[(int) centerCameraInSecondFormationInput] ) == 0.0f  &&
                        Input.GetAxis(ButtonNames[(int) centerCameraInThirdFormationInput]  ) == 0.0f &&
                        Input.GetAxis(ButtonNames[(int) centerCameraInFormationsCenterInput]) == 0.0f 
                    )
            {
                CenterCameraInCursor();
            }

            /*
            else if(Input.GetKey(ButtonNames[(int) moveFirstFormationToCursorInput]))
            {
                MoveFormationToCursor(0);
            }

            else if(Input.GetKey(ButtonNames[(int) moveSecondFormationToCursorInput]))
            {
                MoveFormationToCursor(1);
            }

            else if(Input.GetKey(ButtonNames[(int) moveThirdFormationToCursorInput]))
            {
                MoveFormationToCursor(2);
            }

            else if(Input.GetKey(ButtonNames[(int) moveAllFormationsToCursorInput]))
            {
                MoveAllFormationToCursor();
            }
            */
            if(Input.GetKey(KeyCode.Alpha5))
            {
                MoveFormationToCursor(0);
            }

            if(Input.GetKey(KeyCode.Alpha6))
            {
                MoveFormationToCursor(1);
            }

            if(Input.GetKey(KeyCode.Alpha7))
            {
                MoveFormationToCursor(2);
            }

            if(Input.GetKey(KeyCode.Alpha8))
            {
                MoveAllFormationToCursor();
            }



            Vector3 cursorVector = new Vector3  (
                                                    Input.GetAxis(axisNames[(int) moveCursorHorizontalInput]),
                                                    0,
                                                    Input.GetAxis(axisNames[(int) moveCursorVerticalInput])
                                                );

            Vector3 cameraVector = new Vector3  (
                                                    Input.GetAxis(axisNames[(int) moveCameraHorizontalInput]),
                                                    0,
                                                    Input.GetAxis(axisNames[(int) moveCameraVerticalInput])
                                                );
                                        

            MoveCursor(cursorVector);
            MoveCamera(cameraVector);
        }
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

    private void MoveCursor(Vector3 delta)
    {
        float cursorSpeed = 10f;
        Vector3 position = cachedPlayerController.GetPlayerCursor().GetPosition();

        position += delta * cursorSpeed * Time.deltaTime;
        
        cachedPlayerController.GetPlayerCursor().SetDestination(position);
    }

    private void MoveCamera(Vector3 delta)
    {
        CenterCameraInCursor();
        /*
        CameraBehaviour camera =  cachedPlayerController.GetPlayerCamera();
       
        float cameraSpeed = 5f;
        Vector3 position = cachedPlayerController.GetPlayerCursor().GetPosition();
        position += delta * cameraSpeed * Time.deltaTime;
        
        camera.CenterToPosition(position);*/
    }

    private void CenterCameraInCursor()
    {
        cachedPlayerController.GetPlayerCamera().CenterToPosition(cachedPlayerController.GetPlayerCursor().GetPosition());
    }

}
