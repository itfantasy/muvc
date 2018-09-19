using UnityEngine;
using System.Collections;

public class iTweenPath : MonoBehaviour{
	public Transform[] path;
	public float m_time = 1f;
	
	void OnDrawGizmos(){
		iTween.DrawPath(path);	
	}
	
	void Start(){
		iTween.MoveTo(gameObject,iTween.Hash("path",path,"time",1,"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.loop,"movetopath",false));
	}
}

