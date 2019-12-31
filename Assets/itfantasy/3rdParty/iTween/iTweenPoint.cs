using UnityEngine;
public class iTweenPoint : MonoBehaviour {
	public float size = .1f;
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position,size);	
	}
}
