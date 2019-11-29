/*
 * File: Board.cs
 * File Created: Tuesday, 29th October 2019 4:32:55 pm
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

using UnityEngine;

public class BoardData
{
    static byte         rows    = 0;
    static byte         columns = 0;
    static CellData [,] cells;
    static BoardData    instance;

    static CellData     playerCellsCenterCell;
    static CellData     boardCenterCell;

    public static BoardData Create(byte rows, byte columns)
    {   
        if(instance == null)
        {
            instance = new BoardData(columns, rows);
        }
        return instance;
    }

    public static BoardData Get() => instance;

    public int GetRows()        => rows;
    public int GetColumns()     => columns;
    public int GetTotalCells()  => rows * columns;

    public CellData GetCellDataAt(int x, int y)     => cells[x, y];
    public CellData GetPlayerCellsCenterCell()  => playerCellsCenterCell;
    public CellData GetBoardCenterCell()        => boardCenterCell;
    public CellData [,] GetCells()              => cells;

    private BoardData(byte _rows, byte _columns)
    {
        rows    = _rows;
        columns = _columns;
        cells   = new CellData[columns, rows];

        InitializeCells();
        AssignCenterCells();
    }
    private void InitializeCells()
    {
        byte currentRow, currentColumn;
        currentRow = currentColumn = 0;

        for (int i = 0; i < cells.Length; ++i)
        {
            cells[currentColumn, currentRow] = new CellData(currentColumn, currentRow);
            
            ++currentColumn;
            if(currentColumn == columns)
            {
                currentColumn = 0;
                ++currentRow;
            }
        }
    }

    private void AssignCenterCells()
    {
        boardCenterCell         = cells[columns >> 1, rows >> 1];
        playerCellsCenterCell   = cells[columns >> 2, rows >> 2];
    }
}

public class CellData
{
    Vector2 index; 
    VisibleItem visibleItem;
    bool empty = true;

    public CellData (byte x, byte y)
    {
        this.index          = new Vector2 (x, y);
        this.visibleItem    = new VisibleItem();
    }

    public byte         GetX()              => (byte) index.x;
    public byte         GetY()              => (byte) index.y;
    public Vector2      GetPosition()       => this.index;
    public VisibleItem  GetVisibleItem()    => this.visibleItem;
    public bool         IsEmpty()           => this.empty;
    public bool         SetEmpty(bool empty) => this.empty = empty;

    public static bool operator == (CellData thisCell, CellData otherCell)
    {
        return thisCell.GetPosition() == otherCell.GetPosition();
    }
    public static bool operator != (CellData thisCell, CellData otherCell) => !(thisCell == otherCell);  
    public static CellData operator ++(CellData thisCell)
    {
        BoardData boardData = BoardData.Get();
        int columns = boardData.GetColumns();
        int rows    = boardData.GetRows();
        int newX    = thisCell.GetX();
        int newY    = thisCell.GetY();

        ++newY;
        if(newY == rows)
        {
            newY = 0;
            ++newX;
        }

        return boardData.GetCellDataAt(newX, newY);
    } 

    public static CellData operator --(CellData thisCell)
    {
        BoardData boardData = BoardData.Get();
        int columns = boardData.GetColumns();
        int rows    = boardData.GetRows();
        int newX    = thisCell.GetX();
        int newY    = thisCell.GetY();

        --newY;
        if(newY < 0)
        {
            newY = rows - 1;
            --newX;
        }

        return boardData.GetCellDataAt(newX, newY);
    }

    public CellData [] Expand()
    {
        CellData[]  toReturn    = new CellData[8];
        BoardData   boardData   = BoardData.Get();
        int         columns     = boardData.GetColumns();
        int         rows        = boardData.GetRows();

        //Up
        toReturn[0] =  this.GetX() < (columns-1) ? boardData.GetCellDataAt(this.GetX()+1, this.GetY()) : null;
        //Right
        toReturn[1] =  this.GetY() < (rows-1)    ? boardData.GetCellDataAt(this.GetX(), this.GetY()+1) : null;
        //Down
        toReturn[2] =  this.GetX() > 0           ? boardData.GetCellDataAt(this.GetX()-1, this.GetY()) : null;
        //Left
        toReturn[3] =  this.GetY() > 0           ? boardData.GetCellDataAt(this.GetX(), this.GetY()-1) : null;
        //UpRight
        toReturn[4] =  !(toReturn[0] is null) && !(toReturn[1] is null) ? boardData.GetCellDataAt(this.GetX()+1, this.GetY()+1) : null;
        //RightDown
        toReturn[5] =  !(toReturn[1] is null) && !(toReturn[2] is null) ? boardData.GetCellDataAt(this.GetX()-1, this.GetY()+1) : null;
        //LeftDown
        toReturn[6] =  !(toReturn[2] is null) && !(toReturn[3] is null) ? boardData.GetCellDataAt(this.GetX()-1, this.GetY()-1) : null;
        //UpLeft
        toReturn[7] =  !(toReturn[0] is null) && !(toReturn[3] is null) ? boardData.GetCellDataAt(this.GetX()+1, this.GetY()-1) : null;

        return toReturn;
    }
    
}