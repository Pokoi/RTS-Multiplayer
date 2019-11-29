/*
 * File: Unit.cs
 * File Created: Tuesday, 29th October 2019 4:12:30 pm
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

public class UnitData 
{
    private UnitType        type;
    private UnitBehaviour   behaviour;

    public UnitData(UnitType type)
    {
        this.type = type;
            
        switch(this.type)
        {
            case UnitType.healer:
                this.behaviour = new HealerSoldier();
            break;
            case UnitType.melee:
                this.behaviour = new MeleeSoldier();
            break;
            case UnitType.ranged:
                this.behaviour = new RangedSoldier();
            break;
            case UnitType.tank:
                this.behaviour = new TankSoldier();
            break;
        }
    }

    public UnitData(UnitData unitData)
    {
        this.type = unitData.GetType();
        switch(this.type)
        {
            case UnitType.healer:
                this.behaviour = new HealerSoldier();
            break;
            case UnitType.melee:
                this.behaviour = new MeleeSoldier();
            break;
            case UnitType.ranged:
                this.behaviour = new RangedSoldier();
            break;
            case UnitType.tank:
                this.behaviour = new TankSoldier();
            break;
        }
    }

    public UnitBehaviour GetBehaviour() => this.behaviour;
    public UnitType      GetType()      => this.type;

    public void SetUnitBehaviour (UnitBehaviour behaviour) => this.behaviour = behaviour;

    static public bool operator == (UnitData thisUnitData, UnitData otherUnitData)
    {
        return thisUnitData.GetType() == otherUnitData.GetType();
    }

    static public bool operator != (UnitData thisUnitData, UnitData otherUnitData) => !(thisUnitData == otherUnitData);
    
}

public enum UnitType
{
    ranged, melee, tank, healer
}


