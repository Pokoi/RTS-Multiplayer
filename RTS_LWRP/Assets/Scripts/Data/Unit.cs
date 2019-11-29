/*
 * File: Unit.cs
 * File Created: Saturday, 23rd November 2019 3:19:10 am
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


public class Unit
{
    UnitData unitData;
    CellData position;


    public CellData GetPosition() => this.position;
    public void SetPosition(CellData position) => this.position = position;

    public UnitData GetUnitData() => this.unitData;

    public Unit(CellData cd, UnitData ud)
    {
        this.unitData = ud;
        this.position = cd;
    }

    public Unit(Unit unit)
    {
        this.position = unit.GetPosition();
        this.unitData = new UnitData(unit.GetUnitData());
    }

    static public bool operator == (Unit thisCell, Unit otherCell)
    {
        return thisCell.GetPosition() == otherCell.GetPosition() && thisCell.GetUnitData() == otherCell.GetUnitData();
    }

    static public bool operator != (Unit thisCell, Unit otherCell) => !(thisCell == otherCell);

}
