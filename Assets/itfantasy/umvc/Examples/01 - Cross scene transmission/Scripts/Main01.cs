using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Main01 : MonoBehaviour {

    static bool _inited = false;

	// Use this for initialization
	void Start () {
        if (!_inited) {
            Facade.InitMVC();
            Worker_Ken.RegisterCommands();
            _inited = true;
        }
	}
	
}
