/*
 * File: UnitBehaviour.cs
 * File Created: Friday, 8th November 2019 8:52:04 pm
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

public abstract class UnitBehaviour
{
    protected int   health;
    protected int   totalHealth;
    protected int   damage;
    protected float actionSpeed;
    protected int   damageDone;
    protected int   attackRange;


    public void Attack(Soldier target)
    {
        target.GetUnit().GetUnitData().GetBehaviour().ReceiveDamage(damage);
        damageDone += damage;
    }

    public void ReceiveDamage(int damage) => health -= damage;

    public abstract void ApplyBuffEfect(Soldier target);

    public void     SetHealth(int value)            => this.health = value;
    public void     SetTotalHealth (int value)      => this.totalHealth = value;
    public void     SetActionSpeed (float value)    => this.actionSpeed = value;
    public void     SetDamage (int value)           => this.damage = value;

    public int      GetHealth()      => health;
    public int      GetTotalHealth() => totalHealth;
    public int      GetDamageDone()  => damageDone;
    public int      GetDamage()      => damage;
    public int      GetAttackRange() => attackRange;
    public float    GetActionSpeed() => actionSpeed;

    public abstract void Reset();
    
}

public class HealerSoldier : UnitBehaviour
{
    public HealerSoldier() => Reset();
    public override void ApplyBuffEfect(Soldier target)
    {
        UnitBehaviour targetBehaviour = target.GetUnit().GetUnitData().GetBehaviour();
        targetBehaviour.SetTotalHealth(targetBehaviour.GetTotalHealth() + 20);
        targetBehaviour.SetHealth(targetBehaviour.GetTotalHealth() + 20);
    }

    public override void Reset()
    {
        this.health         = this.totalHealth = 400;
        this.damage         = -5;
        this.actionSpeed    = 0.3f;
        this.attackRange    = 3;
        this.damageDone     = 0;
    }
}

public class MeleeSoldier : UnitBehaviour
{
    public MeleeSoldier() => Reset();

    public override void ApplyBuffEfect(Soldier target)
    {
        UnitBehaviour targetBehaviour = target.GetUnit().GetUnitData().GetBehaviour();
        targetBehaviour.SetDamage(targetBehaviour.GetDamage() + 5);
    }

    public override void Reset()
    {
        this.health         = this.totalHealth = 500;
        this.damage         = 10;
        this.actionSpeed    = 0.45f;
        this.attackRange    = 1;
        this.damageDone     = 0;
    }
}

public class TankSoldier : UnitBehaviour
{
    public TankSoldier() => Reset();

    public override void ApplyBuffEfect(Soldier target)
    {
        UnitBehaviour targetBehaviour = target.GetUnit().GetUnitData().GetBehaviour();
        targetBehaviour.SetTotalHealth(targetBehaviour.GetTotalHealth() + 50);
        targetBehaviour.SetHealth(targetBehaviour.GetTotalHealth() + 50);
    }

    public override void Reset()
    {
        this.health         = this.totalHealth = 1000;
        this.damage         = 7;
        this.actionSpeed    = 0.4f;
        this.attackRange    = 1;
        this.damageDone     = 0;
    }
}

public class RangedSoldier : UnitBehaviour
{
    public RangedSoldier() => Reset();

    public override void ApplyBuffEfect(Soldier target)
    {
        UnitBehaviour targetBehaviour = target.GetUnit().GetUnitData().GetBehaviour();
        targetBehaviour.SetActionSpeed(targetBehaviour.GetActionSpeed() + 0.2f);
    }

    public override void Reset()
    {
        this.health         = this.totalHealth = 200;
        this.damage         = 15;
        this.actionSpeed    = 0.6f;
        this.attackRange    = 2;
        this.damageDone     = 0;
    }
}
