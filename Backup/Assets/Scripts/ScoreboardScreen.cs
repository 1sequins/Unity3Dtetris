using UnityEngine;
using System.Collections;
using System.IO;

public class ScoreboardScreen : MonoBehaviour {
	
	public GUISkin skin; // Skin
	
	private int totalPlayers;
	private string[] names;
	private float[] times;
	private int[] rows;
	private int[] levels;
	private int[] scores;
	private string[] games;
	
	private Vector2 scrollPosition = new Vector2(20, 20);
	
	// Use this for initialization
	void Start () {
		totalPlayers = PlayerPrefs.GetInt("Players", 0);
		names = new string[totalPlayers];
		times = new float[totalPlayers];
		rows = new int[totalPlayers];
		levels = new int[totalPlayers];
		scores = new int[totalPlayers];
		games = new string[totalPlayers];
		
		for(int i = 1; i < totalPlayers; i++)
		{
			var prefix = "Player"+i.ToString();
			names[i] = PlayerPrefs.GetString(prefix+"_Name");
			times[i] = PlayerPrefs.GetFloat(prefix+"_timetaken");
			rows[i] = PlayerPrefs.GetInt(prefix+"_rows");
			levels[i] = PlayerPrefs.GetInt(prefix+"_level");
			scores[i] = PlayerPrefs.GetInt(prefix+"_score");
			games[i] = PlayerPrefs.GetString(prefix+"_game");
		}
	
	//Sorting by Higher Score
	for(int i = 1; i < totalPlayers; i++)
	{
		for(int j = i + 1; j < totalPlayers; j++)
		{
			if(scores[j] > scores[i])
			{
				string nameaux = names[j];
				names[j] = names[i];
				names[i] = nameaux;
				
				float timeaux = times[j];
				times[j] = times[i];
				times[i] = timeaux;	
				
				int rowaux = rows[j];
				rows[j] = rows[i];
				rows[i] = rowaux;
				
				int levelaux = levels[j];
				levels[j] = levels[i];
				levels[i] = levelaux;	
				
				int scoreaux = scores[j];
				scores[j] = scores[i];
				scores[i] = scoreaux;			
				
				string gameaux = games[j];
				games[j] = games[i];
				games[i] = gameaux;
			}
		}
	}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUILayout.BeginArea( new Rect(Screen.width/2-300, 10, 600, Screen.height-20));
			Label("ScoreBoard", true, 600);
			//Build scoreboard header
			GUILayout.BeginHorizontal();
				Label("Name", true, 100);			
				Label("Game", true, 100); 			
				Label("Level", true, 100); 			
				Label("Destroyed Rows", true, 100);		
				Label("Time", true, 100);				
				Label("Score", true, 100);				
			GUILayout.EndHorizontal();
			
			//Fill the scoreboard with database data
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width-200), GUILayout.Height ( Screen.height-100));
				for(int i = 1; i < totalPlayers; i++) 
				{
					GUILayout.BeginHorizontal();
						Label( names[i], false, 100);
						Label( games[i], false, 100);
						Label( levels[i].ToString(), false, 100);
						Label( rows[i].ToString(), false, 100);
						Label( string.Format("{0:F1}", times[i]), false, 100);
						Label( scores[i].ToString(), false, 100);
					GUILayout.EndHorizontal();
				}
			GUILayout.EndScrollView ();
			
			GUILayout.BeginHorizontal();
				//Export button
				if(Button("Export CSV", 100))
				{
					ExportToCSV();
				}
				//Delete db button
				if(Button("Delete DB", 100))
				{
					PlayerPrefs.SetInt("Players", 1);
					Application.LoadLevel("TetrisScore");
				}
				//Back button
				if(Button("Back", 100))
				{
					Application.LoadLevel("TetrisInit");
				}
			GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
	}
	
	//Export to a csv file the database
	void ExportToCSV()
	{
		StreamWriter sw = new StreamWriter(Application.dataPath + "/export.csv");
		string message = string.Format("{0};{1};{2};{3};{4};{5};", "Name", "Game", "Level","Destroyed Rows","Time","Score");
		sw.WriteLine(message);
		for(int i = 1; i < totalPlayers; i++)
		{
			message = string.Format("{0};{1};{2};{3};{4};{5};", names[i], games[i], levels[i].ToString(),rows[i].ToString(),string.Format("{0:F1}", times[i]),scores[i].ToString());
			sw.WriteLine(message);
		}
		sw.Close();
	
	}
	
	//Aux functions//
	void Label(string text, bool headerStyle, int width)
	{
		GUILayout.Label(text, skin.GetStyle(headerStyle ? "sbh" : "sb"), GUILayout.Width(width), GUILayout.Height(25)); 	
	}
	
	bool Button(string text, int width)
	{
		if(GUILayout.Button( text, skin.GetStyle("sbbutton"), GUILayout.Width(width), GUILayout.Height(25)))
			return true;
		else
			return false;
	}
}
