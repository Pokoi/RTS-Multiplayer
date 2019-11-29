/*
 * File: ScoreController.cs
 * File Created: Saturday, 23rd November 2019 4:00:24 pm
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
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI playerTeamScoreText;
    public TextMeshProUGUI AITeamScoreText;

    public int CalculateScoreRelativeToAI(TeamData playerTeam, TeamData AITeam)
    {
        int playerScore = 0;
        int AIScore     = 0;

        foreach (Soldier soldier in playerTeam.GetSoldiers())
        {
            playerScore -= soldier.GetHealth();
        }

        foreach (Soldier soldier in AITeam.GetSoldiers())
        {
            AIScore += soldier.GetHealth();
        }

        playerTeam.SetScore(-playerScore);
        AITeam.SetScore(AIScore);

        ShowScores(playerTeam, AITeam);

        if(playerScore == 0 && AIScore != 0) return AIScore;
        if(playerScore != 0 && AIScore == 0) return playerScore;

        return AIScore + playerScore;
    }

    void ShowScores(TeamData playerTeam, TeamData AITeam)
    {
        playerTeamScoreText.text = $"Player score: {playerTeam.GetScore()}";
        AITeamScoreText.text     = $"AI score: {AITeam.GetScore()}";
    }
}
