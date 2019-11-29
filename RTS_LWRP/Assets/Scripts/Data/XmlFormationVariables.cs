/*
 * File: XmlFormationVariables.cs
 * File Created: Sunday, 17th November 2019 3:55:15 pm
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


public class XmlFormationVariables : XmlObjectData
{
    public byte boardRows;
    public byte boardColumns;
    public byte maxUnitsPerTeam;
    public byte unitTypesCount;
    public uint possibleFormationsCount;
    public string UCB1ObjectDataPath;
    public string RegretMatchingObjectDataPath;

    public XmlFormationVariables
    (
        byte boardRows, 
        byte boardColumns, 
        byte maxUnitsPerTeam, 
        byte unitTypesCount,
        uint possibleFormations,
        string UCB1ObjectDataPath, 
        string RegretMatchingObjectDataPath
    )
    {
        this.boardRows                    = boardRows;
        this.boardColumns                 = boardColumns;
        this.maxUnitsPerTeam              = maxUnitsPerTeam;
        this.unitTypesCount               = unitTypesCount;
        this.possibleFormationsCount      = possibleFormations;
        this.UCB1ObjectDataPath           = UCB1ObjectDataPath;
        this.RegretMatchingObjectDataPath = RegretMatchingObjectDataPath;
    }

    public XmlFormationVariables(){}

}
