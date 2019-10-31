using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Main06 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Facade.InitMVC();
        Worker_Yue.RegisterCommands();

        WindowStackManager.ins.CreateWindowStack()
            .PushWindow("PreAWindow", PreACommand.Index)
            .PushWindow("PreBWindow", PreBCommand.Index)
            .PushWindow("PreCWindow", PreCCommand.Index)
            .BeginShow();
	}
	
}
