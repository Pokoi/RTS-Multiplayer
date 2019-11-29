/*
 * File: XmlRegretMatchingObjectData.cs
 * File Created: Tuesday, 19th November 2019 9:09:58 am
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


using System.Xml.Serialization;

public class XmlRegretMatchingObjectData : XmlObjectData
{
     [XmlArray ("utility")] [XmlArrayItem ("ArmyAction")]
    public int      [] utility;
    
    [XmlArray ("regret")] [XmlArrayItem ("percentage")]
    public float    [] regret;

    [XmlArray ("chance")] [XmlArrayItem ("percentage")]
    public float    [] chance;

    public void ConvertArray(int [][] utility)
    {
        this.utility = new int [utility.Length * utility.Length];
        int iterator = 0;
        
        for(int i = 0; i < utility.Length; ++i)
        {
            for (int j = 0; j < utility.Length; ++j)
            {
                this.utility[iterator] = utility[i][j];
                ++iterator;
            }
        }
    }

    public int[][] CastToArrayOfArray()
    {
        int[][] toReturn = new int[utility.Length][];
        int iterator     = 0;
        
        for(int i = 0; i < utility.Length; ++i)
        {
            for (int j = 0; j < utility.Length; ++j)
            {
                toReturn[i][j] = this.utility[iterator];
                ++iterator;
            }
        }

        return toReturn;
    }

    public XmlRegretMatchingObjectData() {}
}
