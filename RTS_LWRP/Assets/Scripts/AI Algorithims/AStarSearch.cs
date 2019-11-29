/*
 * File: AStarSearch.cs
 * File Created: Friday, 22nd November 2019 11:34:22 am
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

using System.Collections.Generic;
using UnityEngine;

public class AStarSearch
{
    enum Phases 
    { 
        Searching, PathFound, NotPathFound
    }
    Phases                          currentPhase;
    Node                            goalNode;
    List<Node>                      openList;
    List<Locomotion.MoveDirection>  movements;
    float                           g;


    public AStarSearch() => currentPhase = Phases.Searching;

    public Locomotion.MoveDirection GetNextMove(CellData currentPos, CellData goal)
    {
        switch (currentPhase)
        {
            case Phases.Searching: 
                SetMovements(Search(currentPos, goal)); 
            break;
            case Phases.PathFound: 
                return ReadActions();
            case Phases.NotPathFound: 
                return Locomotion.MoveDirection.None;
        }

        return Locomotion.MoveDirection.None;
    }

    public void SetSearching() => currentPhase = Phases.Searching;

    private Node Search(CellData initialPosition, CellData goal)
    {
        openList = new List<Node>();
        openList.Add(new Node(null, initialPosition, Locomotion.MoveDirection.None));

        while (openList.Count > 0)
        {
            Node node = null;

            foreach (Node n in openList)
            {
                if (node == null || n.GetF() < node.GetF())
                node = n;
            }

            openList.Remove(node);

            if (IsGoal(node, goal)) return node;
            
            node.UpdateValues(goal, g);
            g = node.GetG();
            
            foreach (Node child in node.Expand())
            {
                if (!InList(child))
                {
                    child.UpdateValues(goal, g);
                    openList.Add(child);
                }
            }

        }

        currentPhase = Phases.NotPathFound;
        return null; 
    }

    private Locomotion.MoveDirection ReadActions()
    {
        Locomotion.MoveDirection movement_to_return;
        movement_to_return = movements[0];
        movements.RemoveAt(0);
        return movement_to_return;
    }

    private bool IsGoal(Node node, CellData goal) => node.GetCell() == goal;
  
    private void SetMovements(Node finalNode)
    {
        movements = new List<Locomotion.MoveDirection>();
        while (finalNode.GetParent() != null)   
        {
            movements.Add(finalNode.GetActionNeeded());
            finalNode = finalNode.GetParent();
        }
        movements.Reverse();
        currentPhase = Phases.PathFound;
    }

    private bool InList(Node node)
    {
        foreach (Node n in openList)
        {
            if (node.GetCell() == n.GetCell())
            { 
                return true;
            }
        }

        return false;
    }

}



public class Node
{
    private Node                     parent;
    private CellData                 cell;
    private float                   g;
    private float                   h;
    private float                   f;
    private Locomotion.MoveDirection actionNeeded;

    public Node     GetParent() => this.parent;
    public CellData GetCell()   => this.cell;
    public float    GetG()      => this.g;
    public float    GetH()      => this.h;
    public float    GetF()      => this.f;
    public Locomotion.MoveDirection GetActionNeeded() => this.actionNeeded;

    public Node(Node parent, CellData cell, Locomotion.MoveDirection action)
    {
        this.parent         = parent;
        this.cell           = cell;
        this.actionNeeded   = action;
    }

    public List<Node> Expand()
    {
        CellData[] neighbours = GetCell().Expand();
        List<Node> neighboursNodes = new List<Node>();

        for (int iterator = 0; iterator < neighbours.Length; iterator++)
        {
            if (!(neighbours[iterator] is null) && neighbours[iterator].IsEmpty())
            {
                switch (iterator)
                {
                    case 0: neighboursNodes.Add(new Node(this, neighbours[iterator], Locomotion.MoveDirection.Up)); break;
                    case 1: neighboursNodes.Add(new Node(this, neighbours[iterator], Locomotion.MoveDirection.Right)); break;
                    case 2: neighboursNodes.Add(new Node(this, neighbours[iterator], Locomotion.MoveDirection.Down)); break;
                    case 3: neighboursNodes.Add(new Node(this, neighbours[iterator], Locomotion.MoveDirection.Left)); break;
                }
            }
        }

        return neighboursNodes;
    }

    public void UpdateValues(CellData end, float g)
    {
        this.g = 1 + g;
        this.h = CalculateEuclideanGeometry(end.GetX(), end.GetY());
        this.f = this.g + this.h;
    }


    private float CalculateEuclideanGeometry(float final_column, float final_row)
    {
        return Mathf.Abs(this.GetCell().GetX() - final_column) + Mathf.Abs(this.GetCell().GetY() - final_row);
    }

}
