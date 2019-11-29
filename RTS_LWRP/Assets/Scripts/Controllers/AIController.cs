/*
 * File: AIController.cs
 * File Created: Wednesday, 30th October 2019 4:03:51 pm
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

public class AIController : MonoBehaviour
{
    public enum FormationAlgorithims {UCB1, RegretMatching};
    public FormationAlgorithims formationAlgorithim;
    public int              maxUnitsInTeam; 
    public BoardBehaviour   boardBehaviour;
    
    static AIController     instance;

    Actions<ArmyAction>     possibleActions;
    ArmyAction              selfFormation; 
    AIAlgorithim<ArmyAction> teamFormer = null;
  
    public static AIController      Get()                       => instance;
    public int                      GetMaxUnitsInTeam()         => maxUnitsInTeam;
    public AIAlgorithim<ArmyAction> GetTeamFormer()             => teamFormer;
    public uint                     GetPossibleActionsCount()   => possibleActions.GetCount();
    public Actions<ArmyAction>      GetPossibleActions()        => possibleActions;
    public FormationAlgorithims     GetFormationAlgorithim()    => formationAlgorithim;
    public ArmyAction               GetFormation()              => selfFormation;
    
    public AIController Create()
    {
        if(instance == null)
        {
            instance = this;
        }

        return instance;
    }
    
    public void OnBattleStart()
    {
        ChooseFormation();
        InterpretateFormation();

        if(GameController.Get().currentGameMode == GameController.GameMode.AivsAi)
        {
            InterpretatePlayerFormation();
        }
    }

    public void OnBattleEnd(ArmyAction oponentFormation, int score)
    {
        teamFormer.UpdateUtility(selfFormation, oponentFormation, score);
        int utility = teamFormer.GetUtilityOf(selfFormation, oponentFormation);
        
        teamFormer.UpdateValues(oponentFormation);
    }

    void Awake() 
    {
        Create();
        
        possibleActions = new Actions<ArmyAction>(maxUnitsInTeam);
        
        switch(formationAlgorithim)
        {
            case FormationAlgorithims.UCB1:
            teamFormer = new UCB1<ArmyAction>(possibleActions);
            break;

            case FormationAlgorithims.RegretMatching:
            teamFormer = new RegretMatching<ArmyAction>(possibleActions);
            break;
        }
         
    }


    void ChooseFormation()  => selfFormation = teamFormer.Play(); 

    void InterpretateFormation()
    {
        ArmyAction formation  = selfFormation;
        foreach (Unit unit in formation.GetUnits())
        {
            int cellDataRelativeX = BoardData.Get().GetColumns() - unit.GetPosition().GetX() - 1;
            int cellDataRelativeY = BoardData.Get().GetRows()    - unit.GetPosition().GetY() - 1;

            CellData relativeCellData       = BoardData.Get().GetCellDataAt(cellDataRelativeX, cellDataRelativeY);
            Vector3  cellDataWorldPosition  = boardBehaviour.GetWorldPositionOfCell(relativeCellData);
        
            UnitType    AI_unitUnitType = unit.GetUnitData().GetType();
            GameObject  AI_unit         = GameController.Get().unitsPool.GetUnitInstance(AI_unitUnitType);
            
            relativeCellData.SetEmpty(false);

            AI_unit.SetActive(true);
            
            Soldier  AI_unitSoldier = AI_unit.GetComponent<Soldier>();
            TeamData team           = GameController.Get().GetAITeamData();

            AI_unitSoldier.SetUnitType(AI_unitUnitType);
            team.AddSoldier(AI_unitSoldier);
            AI_unitSoldier.SetTeam(team);
            AI_unitSoldier.SetUnit(new Unit(unit));
            AI_unitSoldier.Start();
            AI_unit.GetComponent<DraggeableUnit>().enabled = false;
            
            AI_unit.transform.localPosition = new Vector3 (cellDataWorldPosition.x, cellDataWorldPosition.y + 0.5f, cellDataWorldPosition.z);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------
    // FOR AUTOMATIC KNOWLEDGE


    uint playerIndex = 0;
    uint selfIndex = 0;
    uint iterator = 0;
    public void DecideFormations()
    {
        selfFormation = possibleActions.GetAt(selfIndex);
        GameController.Get().SetPlayerFormation(possibleActions.GetAt(playerIndex));
        playerIndex++;
        if(playerIndex == possibleActions.GetCount())
        {
            playerIndex = 0;
            selfIndex++;
            GameController.Get().UpdateXmL();
            if(selfIndex == possibleActions.GetCount())
            {
                // GAME OVER
                GameController.Get().OnGameEnds();
            }
        }


    }

    void InterpretatePlayerFormation()
    {
        ArmyAction formation  = GameController.Get().playerController.GetPlayerFormation();
        foreach (Unit unit in formation.GetUnits())
        {
            int cellDataRelativeX = unit.GetPosition().GetX();
            int cellDataRelativeY = unit.GetPosition().GetY();

            CellData relativeCellData       = BoardData.Get().GetCellDataAt(cellDataRelativeX, cellDataRelativeY);
            Vector3  cellDataWorldPosition  = boardBehaviour.GetWorldPositionOfCell(relativeCellData);
        
            UnitType    AI_unitUnitType = unit.GetUnitData().GetType();
            GameObject  AI_unit         = GameController.Get().unitsPool.GetUnitInstance(AI_unitUnitType);
            
            relativeCellData.SetEmpty(false);

            AI_unit.SetActive(true);
            
            Soldier  AI_unitSoldier = AI_unit.GetComponent<Soldier>();
            TeamData team           = GameController.Get().GetPlayerTeamData();

            AI_unitSoldier.SetUnitType(AI_unitUnitType);
            team.AddSoldier(AI_unitSoldier);
            AI_unitSoldier.SetTeam(team);
            AI_unitSoldier.SetUnit(new Unit(unit));
            AI_unitSoldier.Start();
            AI_unit.GetComponent<DraggeableUnit>().enabled = false;
            
            AI_unit.transform.position = new Vector3 (cellDataWorldPosition.x, cellDataWorldPosition.y + 0.5f, cellDataWorldPosition.z);
        }
    }

}


