using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSpace : MonoBehaviour
{
   public Button button;
   public Text buttonText;

   private GameControler gameControler;
   
    
   public void SetGameControlerRefrence(GameControler controler)
   {
       gameControler=controler;
   }
   public void SetSpace()
   {
       if(gameControler.playerMove==true)
       {
        buttonText.text=gameControler.GetPlayerSide();
        button.interactable=false;
        gameControler.EndTurn();
       }
    }
   
  
}
