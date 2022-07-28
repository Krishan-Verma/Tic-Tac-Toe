using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}


[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}


public class GameControler : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;
    private int moveCount;
    private int count;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject RestartButton;
    public GameObject PvsCButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    private string computerSide;
    public bool playerMove;
    public float delay;
    private int value;
    public bool PvsC=false;
    public GameObject startInfo;
     
    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControlerRefrenceOnButton();
        moveCount=0;
        count=0;
        RestartButton.SetActive(false);
        playerMove=true;
        
    }
    public void PlayAgainstComputer()
    {
        PvsC=true;
        RestartGame();
    }
    void Update()
    {
        if(PvsC==true)
        { 
             if(playerMove==false)
             {
                delay+=delay*Time.deltaTime;
             }
        if (delay>=50)
        {
            int alpha=-1000;
            int beta=1000;
            int Move=0;
            int bestScore=-100;
            for (int i=0;i<9;i++)
            {
                if(buttonList[i].text=="")
                {
         
                    buttonList[i].text=playerSide;
                    int score=MinMax(buttonList,false,0,alpha,beta);
                    buttonList[i].text="";
                    if(score>bestScore)
                    {
                        bestScore=score;
                        Move=i;
                    }
                    
                }
            }
                buttonList[Move].text=computerSide;
                buttonList[Move].GetComponentInParent<Button>().interactable=false;
                EndTurn(); 

        }
        }
    }

    int MinMax(Text [] blist,bool isMaximizing,int depth,int alpha,int beta)
    {
        int score;
        string result=WinnerResult();

        if(result!=null)
        {
            if(result==computerSide)
                score=-10+depth;
            else if(result==playerSide)
                score=10-depth;
            else
                score=0;

            return score;
        }

        if(isMaximizing)
        {   
        int bestScore=-1000;
          for(int i=0;i<9;i++)
            {
             if(blist[i].text=="")
                {
                    blist[i].text=playerSide;
                    score=MinMax(blist,false,depth+1,alpha,beta);
                    blist[i].text="";
                    if(score<bestScore)
                    {
                        bestScore=score;
                    }
                    
                    if(bestScore>alpha)
                    {
                        alpha=bestScore;
                    }
                    if(beta<=alpha)
                    {
                        break;
                    }

                }
            }
            return bestScore;
        }
        else
        {   
            int bestScore=1000;
          for(int i=0;i<9;i++)
            {
             if(blist[i].text=="")
                {
                    blist[i].text=computerSide;
                    score=MinMax(blist,true,depth+1,alpha,beta);
                    Debug.Log("Score:"+score);
                    blist[i].text="";
                    if(score<bestScore)
                    {
                        bestScore=score;
                    }
                    
                    if(bestScore<beta)
                    {
                        beta=bestScore;
                    }
                    if(beta<=alpha)
                    {
                        break;
                    }

                }
            }
            return bestScore;
        }
    }
    
    public string WinnerResult()
    {
        count++;
        if(buttonList[0].text==playerSide&&buttonList[1].text==playerSide&&buttonList[2].text==playerSide)
        {
            return playerSide;
        }
        else if(buttonList[3].text==playerSide&&buttonList[4].text==playerSide&&buttonList[5].text==playerSide)
        {
            return playerSide;
        } 
        else if(buttonList[6].text==playerSide&&buttonList[7].text==playerSide&&buttonList[8].text==playerSide)
        {
            return playerSide;
        } 
        else if(buttonList[0].text==playerSide&&buttonList[4].text==playerSide&&buttonList[8].text==playerSide)
        {
            return playerSide;
        } 
        else if(buttonList[2].text==playerSide&&buttonList[4].text==playerSide&&buttonList[6].text==playerSide)
        {
            return playerSide;
        } 
        else if(buttonList[0].text==playerSide&&buttonList[3].text==playerSide&&buttonList[6].text==playerSide)
        {
           return playerSide;
        } 
        else if(buttonList[1].text==playerSide&&buttonList[4].text==playerSide&&buttonList[7].text==playerSide)
        {
           return playerSide;
        } 
        else if(buttonList[2].text==playerSide&&buttonList[5].text==playerSide&&buttonList[8].text==playerSide)
        {
            return playerSide;
        }
        else if(buttonList[0].text==computerSide&&buttonList[1].text==computerSide&&buttonList[2].text==computerSide)
        {
            return computerSide;
        }
        else if(buttonList[3].text==computerSide&&buttonList[4].text==computerSide&&buttonList[5].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[6].text==computerSide&&buttonList[7].text==computerSide&&buttonList[8].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[0].text==computerSide&&buttonList[4].text==computerSide&&buttonList[8].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[2].text==computerSide&&buttonList[4].text==computerSide&&buttonList[6].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[0].text==computerSide&&buttonList[3].text==computerSide&&buttonList[6].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[1].text==computerSide&&buttonList[4].text==computerSide&&buttonList[7].text==computerSide)
        {
            return computerSide;
        } 
        else if(buttonList[2].text==computerSide&&buttonList[5].text==computerSide&&buttonList[8].text==computerSide)
        {
            return computerSide;
        }
        else if(count>=9)
        {
           return "draw";
        }
        else
        {
            return null;
        }
    }
    void SetGameControlerRefrenceOnButton()
    {
        for (int i=0;i <buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControlerRefrence(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide=startingSide;
        if(playerSide=="X")
        {
            computerSide="O";
            SetPlayerColor(playerX,playerO);
        }
        else
        {   
            computerSide="X";
            SetPlayerColor(playerO,playerX);
        }
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButton(false);
        startInfo.SetActive(false);
        PvsCButton.SetActive(false);

    } 
    public string GetPlayerSide()
    {
        return playerSide;
    }
    public string GetComputerSide()
    {
        return computerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        if(buttonList[0].text==playerSide&&buttonList[1].text==playerSide&&buttonList[2].text==playerSide)
        {
            GameOver(playerSide);
        }
        else if(buttonList[3].text==playerSide&&buttonList[4].text==playerSide&&buttonList[5].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[6].text==playerSide&&buttonList[7].text==playerSide&&buttonList[8].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[0].text==playerSide&&buttonList[4].text==playerSide&&buttonList[8].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[2].text==playerSide&&buttonList[4].text==playerSide&&buttonList[6].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[0].text==playerSide&&buttonList[3].text==playerSide&&buttonList[6].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[1].text==playerSide&&buttonList[4].text==playerSide&&buttonList[7].text==playerSide)
        {
            GameOver(playerSide);
        } 
        else if(buttonList[2].text==playerSide&&buttonList[5].text==playerSide&&buttonList[8].text==playerSide)
        {
            GameOver(playerSide);
        }
        else if(buttonList[0].text==computerSide&&buttonList[1].text==computerSide&&buttonList[2].text==computerSide)
        {
            GameOver(computerSide);
        }
        else if(buttonList[3].text==computerSide&&buttonList[4].text==computerSide&&buttonList[5].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[6].text==computerSide&&buttonList[7].text==computerSide&&buttonList[8].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[0].text==computerSide&&buttonList[4].text==computerSide&&buttonList[8].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[2].text==computerSide&&buttonList[4].text==computerSide&&buttonList[6].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[0].text==computerSide&&buttonList[3].text==computerSide&&buttonList[6].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[1].text==computerSide&&buttonList[4].text==computerSide&&buttonList[7].text==computerSide)
        {
            GameOver(computerSide);
        } 
        else if(buttonList[2].text==computerSide&&buttonList[5].text==computerSide&&buttonList[8].text==computerSide)
        {
            GameOver(computerSide);
        }
        else if(moveCount>=9)
        {
            GameOver("draw");
        }
        else
        {
        ChangeSides();            
        delay=10;
        }
    }

    void ChangeSides()
    {
        if(PvsC==true)
        {
            
        playerMove=(playerMove==true)?false:true;

        if(playerMove==true)
        {
        SetPlayerColor(playerX,playerO);
        }
        else
        {
        SetPlayerColor(playerO,playerX);
            
        }
        }
        else
        {
        playerSide=(playerSide=="X")?"O":"X";
        if(playerSide=="X")
        {
        SetPlayerColor(playerX,playerO);
        }
        else
        {
        SetPlayerColor(playerO,playerX);
            
        }
        }
    }

    void SetPlayerColor(Player newPlayer,Player oldPlayer)
    {
        newPlayer.panel.color=activePlayerColor.panelColor;
        newPlayer.text.color=activePlayerColor.textColor;
        oldPlayer.panel.color=inactivePlayerColor.panelColor;
        oldPlayer.text.color=inactivePlayerColor.textColor;

    }

    
    void GameOver(string winningPlayer)
    {   
        
        SetBoardInteractable(false);
        if(winningPlayer=="draw")
        {
            SetGameOverText("!Match Draw!");
            SetPlayerColorInactive();
            
        }
        else
        {
            SetGameOverText(winningPlayer+" Wins!");
        
        }
               
        RestartButton.SetActive(true);
        PvsCButton.SetActive(true);
        

    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text=value;
        PvsC=false;
    }

    public void RestartGame()
    {
        moveCount=0;
        gameOverPanel.SetActive(false);
        RestartButton.SetActive(false);
        SetPlayerButton(true);
        SetPlayerColorInactive();
        startInfo.SetActive(true);
        PvsCButton.SetActive(true);
        playerMove=true;
        delay=10;
        if(PvsC==true)
        {
            PvsC=true;
        }
        for (int i=0;i<buttonList.Length;i++)
        {      
        buttonList[i].text="";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i=0;i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable=toggle;
        }
    }
    
    void SetPlayerButton(bool toggle)
    {
        playerX.button.interactable=toggle;
        playerO.button.interactable=toggle;
    }

    void SetPlayerColorInactive()
    {
        playerX.panel.color=inactivePlayerColor.panelColor;
        playerX.text.color=inactivePlayerColor.textColor;
        playerO.panel.color=inactivePlayerColor.panelColor;
        playerO.text.color=inactivePlayerColor.textColor;
    }

}
