using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class Main08 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Facade.InitMVC();
        Worker_CZ.RegisterCommands();
	}
	
	
}
