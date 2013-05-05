using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	
	public ButtonType buttonEvent;
	
	
	//private int levelValue=1;
	
	void OnClick()
	{
		switch(buttonEvent)
		{
		case ButtonType.Start:
				Application.LoadLevel("TetrisRegister");
			break;
		case ButtonType.Tutorial:
			PlayerPrefs.SetString("GameKind", "Tutorial");
			Application.LoadLevel("TetrisClone");
			break;
		case ButtonType.Score:
			Application.LoadLevel("TetrisScore");
			break;
		case ButtonType.Exit:
			Application.Quit();
			break;
		case ButtonType.Register_ok:
			string pname = GameObject.Find("pname").GetComponent<UIInput>().text;
			if(pname.Length > 0)
				{
					
								
					PlayerPrefs.SetString("CurrentPlayerName", pname);
					//Starts the screen to choose game
					Application.LoadLevel("TetrisStart");
				}
			break;
		case ButtonType.Register_back:
			Application.LoadLevel("TetrisInit");
			break;
			
		case ButtonType.Dynamic:
			PlayerPrefs.SetString("GameKind", "TimePlus");
			PlayerPrefs.SetInt("GameSpeed", GameManager.levelValue);
			Application.LoadLevel("TetrisClone");
			break;
		case ButtonType.Constant:
			PlayerPrefs.SetString("GameKind", "Constant");
			PlayerPrefs.SetInt("GameSpeed", GameManager.levelValue);
			Application.LoadLevel("TetrisClone");
			break;
		case ButtonType.Manual:
			PlayerPrefs.SetString("GameKind", "Manually");
			PlayerPrefs.SetInt("GameSpeed", GameManager.levelValue);
			Application.LoadLevel("TetrisClone");
			break;
		case ButtonType.Level_inc:
			
			if(GameManager.levelValue < 10){
				GameManager.levelValue++;
			GameObject.Find("levelValue").GetComponent<UILabel>().text=GameManager.levelValue.ToString();
			}
			
			break;
		case ButtonType.Level_dec:
			if(GameManager.levelValue > 1) {
				GameManager.levelValue--;
			GameObject.Find("levelValue").GetComponent<UILabel>().text=GameManager.levelValue.ToString();
			}
			break;
		case ButtonType.Return:
			Application.LoadLevel("TetrisInit");
			break;
		case ButtonType.inGameReset:
			Application.LoadLevel("TetrisClone");
			break;
		case ButtonType.inGameExit:
			Application.LoadLevel("TetrisInit");
			break;
		case ButtonType.inGameBack:
			Application.LoadLevel("TetrisStart");
			break;
		case ButtonType.exportCSV:
			
			StartCoroutine(WaitForResult(ScoreboardScreen.instance.ExportToCSV()));
			
			break;
		case ButtonType.deleteDB:
			PlayerPrefs.SetInt("Players", 1);
			Application.LoadLevel("TetrisScore");
			break;
			
		}
		
	}
	
	public IEnumerator WaitForResult(bool isResult)
	{
		if(isResult)
		{
		//	this.GetComponentsInChildren<UILabel>()[0].
		}
		else
		{
			
			
		}
		
		yield return new WaitForSeconds(10f);
	}
}
