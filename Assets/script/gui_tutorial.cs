using UnityEngine;
using System.Collections;

public class gui_tutorial: MonoBehaviour {

	private Transform label_time;
	private Transform label_difference;
	private Transform label_num;
	private Transform label_error;
	private Transform label_message;
	//~ private Transform gui_box;
	
	private Transform button_restart;
	private Transform button_reset;
	private Transform button_prev;
	private Transform button_next;
	
	public bool using_mouse=false;
	
	public float time_given=0;
	public int total_difference=0;
	private float time_completed=0.0f;
	private int score=0;
	private int reset_count=0;
	private int click_count=0;
	private int error_click=0;
	private bool level_end=false;
	private bool level_started=false;
	private bool[] error_array= new bool[5];
	//~ private Transform[] button = new Transform[3];
	
	private string[]  instruction= new string [6];
	private int inst_num=0;
	private Transform label_instruct;
	
	public string next_level="";
	
	private string language = "english";
	private string platform_type = "PC";
	

// Use this for initialization
	void Start () {
		//~ print(Application.loadedLevel.ToString());
		
		GameObject language_indicator=GameObject.FindWithTag("language_indicator");
		if(language_indicator!=null){
			language=language_indicator.GetComponent<language_indicator>().Get_Language();
			platform_type=language_indicator.GetComponent<language_indicator>().Get_Platform();
		}
		
		label_time=transform.Find("time_left");
		label_difference=transform.Find("difference_left");
		label_error=transform.Find("error_click");
		label_num=transform.Find("num_click");
		label_message=transform.Find("message");
		//~ gui_box=transform.Find("box");
		
		button_restart=transform.Find("button_restart");
		button_reset=transform.Find("button_reset");
		button_prev=transform.Find("button_prev");
		button_next=transform.Find("button_next");
		
		button_restart.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
		button_reset.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
		button_prev.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
		button_next.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
		
		button_restart.gameObject.AddComponent<button>();
		button_reset.gameObject.AddComponent<button>();
		button_prev.gameObject.AddComponent<button>();
		button_next.gameObject.AddComponent<button>();
		
		
		if(language=="english"){
			//~ button_restart.guiText.text="";
			//~ button_prev.guiText.text="Main Menu";
			button_next.guiText.text="Next";
			button_reset.guiText.text="Reset";
		}
		else if(language=="japanese"){
			button_prev.guiText.text="��˥�`";
			button_next.guiText.text="�Τ�";
			button_reset.guiText.text="�ꥻ�å�";
			
			label_time.guiText.text="�Ф�r�g: "+(time_given).ToString("f1")+"��";
			label_difference.guiText.text="�Ф���`��: "+total_difference+"    �`����Ҋ�Ĥ���: 0";
			label_num.guiText.text="����å�����: 0";
			label_error.guiText.text="����`�򥯥�å�: 0";
		}
			
		//~ label_time.guiText.text="Time Left: "+time_given.ToString("f1")+"sec";
		//~ label_difference.guiText.text="Error Left: "+total_difference+"    Error Found: "+score;
		//~ label_num.guiText.text="Number of Click: "+click_count;
		//~ label_error.guiText.text="Number of Error Click: "+error_click;
		
		if(language=="english"){
			if(platform_type=="PC"){
				instruction[0]="Hold right button mouse and drag to rotate\nPress 'next' button when you are ready to move on";
				instruction[1]="Use Mouse Scroll to zoom in and zoom out\nPress 'next' button when you are ready to move on";
				instruction[2]="Hold 'shift' + mouse movement to pan the objects around\nPress 'next' button when you are ready to move on";
				instruction[3]="Press 'Reset' to reset the object to initial position\nPress 'next' button when you are ready to move on";
				instruction[4]="Find the difference between the two object and double click on the difference\nTry to find all the differences";
				instruction[5]="You have completed the tutorial, Lets practice now!\nPress 'next' button when you are ready to move on";
			}
			else{
				instruction[0]="Swipe around the screen to rotate\nTap 'next' button when you are ready to move on";
				instruction[1]="Pinch the screen to zoom in and unpinch to zoom out\nTap 'next' button when you are ready to move on";
				instruction[2]="Swipe the screen with 2 fingers to pan the objects around\nTap 'next' button when you are ready to move on";
				instruction[3]="Tap 'Reset' to reset the object to initial position\nTap 'next' button when you are ready to move on";
				instruction[4]="Find the difference between the two object and double click on the difference\nTry to find all the differences";
				instruction[5]="You have completed the tutorial, Lets practice now!\nTap 'next' button when you are ready to move on";
			}
		}
		else if(language=="japanese"){
			if(platform_type=="PC"){
				instruction[0]="������ܞ������ˤϡ��ޥ�������ܥ����Ѻ���ʤ��顢�ޥ�����Ӥ������¤�����\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[1]="�ޥ���������Υۥ��`������򤭤˻ؤ����¤������sС����ˤϡ� ��β����򤷤��¤�����\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[2]="���`�ܩ`�ɤγ�ܥ����Ѻ���ʤ��顢�ޥ����ǻ����ʤ��ä��¤�����\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[3]="�ꥻ�åȥܥ����Ѻ���ȡ����夬�����λ�äˑ���ޤ���\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[4]="��Ȥ�������҂Ȥ�������`����̽�����¤������`��������Ҋ�Ĥ����顣���Έ�������֥륯��å����Ƥ���������";
				instruction[5]="�������K��ä��顢�Τإܥ����Ѻ�����¤������g�`��̽���ξ������M�ߤޤ���";
			}
			else{
				instruction[0]="������ܞ������ˤϡ������ʤ��ä��¤�����\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[1]="���󤹤�ˤϡ� ����������Ϥˣ�����ָ���ä��Ǝڤ����¤���\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[2]="�����λ�ä�䤨��ˤϡ�������ָ�ǻ����ʤ��ä��¤���\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[3]="�ꥻ�åȥܥ����Ѻ���ȡ����夬�����λ�äˑ���ޤ���\n�ΤΥ�٥���M��ˤϡ����Τأ��ܥ����Ѻ���Ƥ�������";
				instruction[4]="��Ȥ�������҂Ȥ�������`����̽�����¤������`��������Ҋ�Ĥ����顣���Έ�������֥륯��å����Ƥ���������";
				instruction[5]="�������K��ä��顢�Τإܥ����Ѻ�����¤������g�`��̽���ξ������M�ߤޤ���";
			}
		}
		
		label_instruct=transform.Find("instruction");
		label_instruct.guiText.text=instruction[0];
		
		
		for(int i=0; i<5; i++) error_array[i]=false;
		
		StartCoroutine(GUI_Update());
		StartCoroutine(Indicator_Update());
		
		camera_1=GameObject.Find("Cameras/Camera_1");
		camera_2=GameObject.Find("Cameras/Camera_2");
	}
	
	IEnumerator GUI_Update(){
		//~ while(!level_started){
			//~ if(language=="english"){
				//~ label_message.guiText.text="There are "+total_difference+" differences in this level. Find them All!\nGame start in "+(2.5f-Time.timeSinceLevelLoad).ToString("f1")+"sec";
			//~ }
			//~ else if(language=="japanese"){
				//~ label_message.guiText.text="���Υ�٥�"+total_difference+"���`��������ޤ�. �����򤹤٤�Ҋ�Ĥ��Ƥ�������!\n"+(2.5f-Time.timeSinceLevelLoad).ToString("f1")+"�ǥ��`�ॹ���`��";
			//~ }
			//~ //Rect rect_box=label_message.guiText.GetScreenRect ();
			//~ //gui_box.guiTexture.pixelInset=new Rect(-rect_box.width/2-10, -rect_box.height/2-10, rect_box.width+20, rect_box.height+20);
			//~ if(2.5f-Time.timeSinceLevelLoad<=0.15) level_started=true;
			//~ yield return new WaitForSeconds (0.1f);
		//~ }
		level_started=true;
		StartCoroutine(Start_Message());
		while(!level_end){
			if(language=="english"){
				label_time.guiText.text="Time Left: "+(time_given-Time.timeSinceLevelLoad+1.5f).ToString("f1")+"sec";
				label_difference.guiText.text="Error Left: "+(total_difference-score)+"    Error Found: "+score;
				label_num.guiText.text="Number of Click: "+click_count;
				label_error.guiText.text="Number of Error Click: "+error_click;
			}
			else if(language=="japanese"){
				label_time.guiText.text="�Ф�r�g: "+(time_given-Time.timeSinceLevelLoad+1.5f).ToString("f1")+"��";
				label_difference.guiText.text="�Ф���`��: "+(total_difference-score)+"    �`����Ҋ�Ĥ���: "+score;
				label_num.guiText.text="����å�����: "+click_count;
				label_error.guiText.text="����`�򥯥�å�: "+error_click;
			}
			yield return new WaitForSeconds (0.1f);
		}
		
		//~ gui_box.guiTexture.enabled=true;
		if(score==total_difference){
			if(language=="english"){
				label_message.guiText.text="Level Cleared!\nyou did it in "+(time_completed).ToString("f1")+" secconds\nPress next button to proceed";
				label_difference.guiText.text="Error Left: 0    Error Found: "+total_difference;
			}
			else if(language=="japanese"){
				label_message.guiText.text="��٥륯�ꥢ!\n���ʤ���"+(time_completed).ToString("f1")+"�������\n�ΤΥܥ���Ѻ���A�����";
				label_difference.guiText.text="�Ф���`��: 0    �`����Ҋ�Ĥ���: "+total_difference;
			}
			//~ Rect rect_box=label_message.guiText.GetScreenRect ();
			//~ gui_box.guiTexture.pixelInset=new Rect(-rect_box.width/2-10, -rect_box.height/2-10, rect_box.width+20, rect_box.height+20);
		}
		else {
			if(language=="english"){
				button_next.guiText.text="Next";
				label_time.guiText.text="Time Left: 0.0sec";
				label_message.guiText.text="Game Over\nPress next button to proceed";
			}
			else if(language=="japanese"){
				button_next.guiText.text="�Τ�";
				label_time.guiText.text="�Ф�r�g: 0.0��";
				label_message.guiText.text="Game Over\n�ΤΥܥ���Ѻ���A�����";
			}
			//~ Rect rect_box=label_message.guiText.GetScreenRect ();
			//~ gui_box.guiTexture.pixelInset=new Rect(-rect_box.width/2-10, -rect_box.height/2-10, rect_box.width+20, rect_box.height+20);
		}
	}
	
	
	
	IEnumerator Start_Message(){
		if(language=="english"){
			label_message.guiText.text="Game started!";
		}
		else if(language=="japanese"){
			label_message.guiText.text="���`����_ʼ!";
		}
		//~ Rect rect_box=label_message.guiText.GetScreenRect ();
		//~ gui_box.guiTexture.pixelInset=new Rect(-rect_box.width/2-10, -rect_box.height/2-10, rect_box.width+20, rect_box.height+20);
		yield return new WaitForSeconds (1.0f);
		label_message.guiText.text="";
		//~ gui_box.guiTexture.enabled=false;
	}
	
	private GameObject[] object_indicators;
	private float dis;
	public float scale_modifier=10.0f;
	
	IEnumerator Indicator_Update(){
		while(true){
			object_indicators = GameObject.FindGameObjectsWithTag("indicator");
			foreach (GameObject ind in object_indicators) {
				temp_camera=camera_1.GetComponent("Camera") as Camera;
				dis=Vector3.Distance(ind.transform.parent.transform.position, camera_1.transform.position);
				
				Vector3 temp=ind.transform.parent.transform.localScale;
				float size=(temp[0]+temp[1]+temp[2])/3;
				
				Vector3 screenPos = temp_camera.WorldToViewportPoint(ind.transform.parent.transform.position);
				ind.transform.position=new Vector3(screenPos.x, screenPos.y, 0);
				float scale=128*(scale_modifier/dis)*size;
				ind.guiTexture.pixelInset=new Rect(screenPos.x-scale/2, screenPos.y-scale/2, scale, scale);
			}
			//~ if(level_end) break;
			yield return null;
		}
		//~ object_indicators = GameObject.FindGameObjectsWithTag("indicator");
		//~ foreach (GameObject ind in object_indicators) Destroy(ind);
	}
	
	
	//~ bool touch_state=false;
	
	private bool touched_1=false;
	private bool touched_2=false;
	
	// Update is called once per frame
	void Update () {
		if(!level_end && level_started){
			if(!using_mouse){
				//~ if(iPhoneInput.touchCount==1 && iPhoneInput.GetTouch(0).phase==iPhoneTouchPhase.Began){
				if(iPhoneInput.touchCount==1 && !touched_1){
					//~ print(Time.time);
					touched_1=true;
					if ((Time.time - doubleTapStart) < 0.8f){
						OnSelectDifference(iPhoneInput.GetTouch(0).position);
						doubleTapStart=-1;
					}
					else{
						doubleTapStart = Time.time;
					}
				}
				else if(iPhoneInput.touchCount!=1) touched_1=false;
			}
			else if(using_mouse){
				if(Input.GetMouseButtonDown(0)){
					print("mousedown");
					if ((Time.time - doubleClickStart) < 0.8f){
						OnSelectDifference(Input.mousePosition);
						doubleClickStart=-1;
					}
					else{
						doubleClickStart = Time.time;
					}
				}
			}

			
			if(time_given-Time.timeSinceLevelLoad<=0) {
				level_end=true;
				Show_All_Errors();
			}
		}
		
		if(using_mouse){
			if(Input.GetMouseButtonDown(0)){
				Vector2 pos=Input.mousePosition;
				Test_Button_Pressed(pos);
			}			
		}
		else{
			//~ if(iPhoneInput.touchCount==1 && iPhoneInput.GetTouch(0).phase==iPhoneTouchPhase.Began){
			if(iPhoneInput.touchCount==1 && !touched_2){
				//~ print(Time.time);
				touched_2=true;
				iPhoneTouch touch = iPhoneInput.GetTouch(0);
				Test_Button_Pressed(touch.position);
			}
			else if(iPhoneInput.touchCount!=1) touched_2=false;
		}
		
		//~ if(iPhoneInput.touchCount==1 && !touch_state){
		if(iPhoneInput.touchCount==1){
			//~ touch_state=true;
			iPhoneTouch touch = iPhoneInput.GetTouch(0);
			if (button_restart.guiText.HitTest( touch.position )){
				button_restart.guiText.material.color=Color.green;
			}
			else if (button_reset.guiText.HitTest( touch.position )){
				button_reset.guiText.material.color=Color.green;
			}
			else if (button_prev.guiText.HitTest( touch.position )){
				button_prev.guiText.material.color=Color.green;
			}
			else if (button_next.guiText.HitTest( touch.position )){
				button_next.guiText.material.color=Color.green;
			}
			else{
				button_restart.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
				button_reset.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
				button_next.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
				button_prev.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
			}
		}
		//~ else{
			//~ button_reset.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
			//~ button_restart.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
			//~ button_next.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
			//~ button_prev.guiText.material.color=new Color(0.5f, 0.5f, 0.5f, 0.5f);
		//~ }
		
		
		if(score==total_difference && !level_end) {
			time_completed=(Time.timeSinceLevelLoad-1.5f);
			level_end=true;
			if(language=="english") button_next.guiText.text="Next";
			else if(language=="japanese") button_next.guiText.text="�Τ�";

			if(inst_num==4){
				On_Instruction_Changed();
			}
		}
	}
	
	//if an instruction has been done
	void On_Instruction_Changed(){
		inst_num+=1;
		label_instruct.guiText.text=instruction[inst_num];
		if(inst_num==5){
			if(language=="english") button_next.guiText.text="Next";
			else if(language=="japanese") button_next.guiText.text="�Τ�";
			button_next.guiText.enabled=true;
		}
		else if(inst_num==4){
			button_next.guiText.enabled=false;
		}
	}
	
	//test if a button has been pressed
	void Test_Button_Pressed(Vector2 pos){
		if (button_restart.guiText.HitTest( pos )){
			button_restart.guiText.material.color=Color.red;
			StartCoroutine(Load_Next_Level(Application.loadedLevelName));
			if(score==total_difference) Record("completed");
			else Record("skipped");
			//~ Application.LoadLevel("level_"+restart);
		}
		else if (button_reset.guiText.HitTest( pos )){
			button_reset.guiText.material.color=Color.red;
			reset_count+=1;
			gameObject.SendMessage("Reset_Scene");
		}
		else if (button_prev.guiText.HitTest( pos )){
			button_prev.guiText.material.color=Color.red;
			//~ if(prev!="0") StartCoroutine(Load_Next_Level("level_"+prev));
			StartCoroutine(Load_Next_Level("main_menu"));
			if(score==total_difference) Record("completed");
			else Record("skipped");
		}
		else if (button_next.guiText.HitTest( pos )){
			button_next.guiText.material.color=Color.red;
			//~ if(next!="0") StartCoroutine(Load_Next_Level("level_"+next));
			if(inst_num==5){
				if(next_level!="") {
					StartCoroutine(Load_Next_Level("level_"+next_level));
					if(score==total_difference) Record("completed");
					else Record("skipped");
				}
				else StartCoroutine(Load_Next_Level("game_finished_screen"));
			}
			else if(inst_num<=4){
				On_Instruction_Changed();
			}
		}
	}
	
	void Record(string status){

	}
	
	private GameObject camera_1;
	private GameObject camera_2;
	//~ GameObject temp_camera;
	Camera temp_camera;
	
	private Ray ray; 
	private RaycastHit hit;
	
	private float doubleTapStart=0f;
	private float doubleClickStart=0f;
	
	public Transform indicator;
	private Transform instance_indicator;
	
	

	void OnSelectDifference(Vector3 position){
		if(inst_num==4){
			click_count+=1;
			if(position.x<Screen.width/2)
				temp_camera=camera_1.GetComponent("Camera") as Camera;
			else
				temp_camera=camera_2.GetComponent("Camera") as Camera;

			
			ray = temp_camera.ScreenPointToRay(position);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if(hit.collider.tag=="difference"){
					score+=1;
					hit.collider.gameObject.tag="Untagged";
					instance_indicator=Instantiate(indicator, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
					instance_indicator.parent=hit.collider.gameObject.transform;
					if(int.Parse(hit.collider.gameObject.name)-1<5)
						error_array[int.Parse(hit.collider.gameObject.name)-1]=true;
				}
				else {
					error_click+=1;
					//~ print("error_click wrong object");
				}
			}
			else {
				error_click+=1;
				//~ print("error_click_not hitting anything");
			}
		}
	}
	
	void Show_All_Errors(){
		GameObject[] difference_objects = GameObject.FindGameObjectsWithTag("difference");
		foreach (GameObject dif_object in difference_objects) {
			dif_object.tag="Untagged";
			instance_indicator=Instantiate(indicator, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
			instance_indicator.parent=dif_object.transform;
			instance_indicator.guiTexture.color= new Color(1.0f, 0.0f, 0.0f, 0.8f);
		}
	}
	
	IEnumerator Load_Next_Level(string next_level){
		yield return new WaitForSeconds (0.1f);
		Application.LoadLevel(next_level);
	}
	
	
}
