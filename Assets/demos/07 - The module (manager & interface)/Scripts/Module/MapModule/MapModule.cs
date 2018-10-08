using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MapModule : MonoBehaviour, IMapModule
{

    static IMapModule _ins;

    public static IMapModule ins
    {
        get
        {
            return _ins;
        }
    }

    void Awake() {
        _ins = this;
        heroSprite = this.transform.Find("HeroSprite");
    }

    Transform heroSprite;

	// Use this for initialization
	void Start () {
        
	}

    public Vector3 GetHeroPosition()
    {
        return heroSprite.position;
    }
}
