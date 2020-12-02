using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace itfantasy.umvc
{
    public class AsyncArg
    {
        public Action<object> callback;
        public object token;

        public AsyncArg(Action<object> callback, object token)
        {
            this.callback = callback;
            this.token = token;
        }
    }

    public delegate void SceneLoader(string sceneName, object custom);
    public delegate void ResourceLoader(string resourceName, Action<GameObject> callback, object custom);
    public delegate GameObject SyncResourceLoader(string resourceName, object custom);

    public interface IScenePauser { void Continue(); }

    public class ScenePauser : IScenePauser
    {
        Action _continuer = null;

        public ScenePauser(Action continuer)
        {
            this._continuer = continuer;
        }

        public void Continue()
        {
            if (this._continuer != null)
            {
                this._continuer.Invoke();
            }
        }
    }
}
