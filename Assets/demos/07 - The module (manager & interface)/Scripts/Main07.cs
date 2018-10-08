using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Main07 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Facade.InitMVC();
        Worker_Nan.RegisterCommands();
	}
	
}
