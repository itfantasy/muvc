using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace itfantasy.umvc
{
    public class Command : IDisposable
    {
        #region consts...

        public const int SystemIndex = 0;

        public const int Monitor_Inited = 101;
        public const int Monitor_Showed = 102;
        public const int Monitor_Closed = 103;
        public const int Monitor_Disposed = 104;
        public const int Monitor_Clicked = 105;

        public const int Monitor_Initing = 201;
        public const int Monitor_Showing = 202;
        public const int Monitor_Closing = 203;
        public const int Monitor_Disposing = 204;

        public const int Monitor_SceneChanged = 301;
        public const int Monitor_SceneEntered = 302;
        public const int Monitor_SceneLeaved = 303;

        public const int Monitor_SceneChanging = 401;
        public const int Monitor_SceneEntering = 402;
        public const int Monitor_SceneLeaving = 403;
        
        public const int Command_Reactive = 1001;
        public const int Command_Show = 1002;
        public const int Command_Close = 1003;
        public const int Command_OK = 1004;
        public const int Command_Trigger = 1005;
        public const int Command_Cancel = 1006;

        public const int Command_SceneChange = 1101;
        public const int Command_SceneEnter = 1102;
        public const int Command_SceneLeave = 1103;
        public const int Command_SceneLoading = 1104;

        public const int Command_UserCustom = 10000;

        #endregion

        #region properties...

        protected Mediator _mediator;

        public bool isActive
        {
            get
            {
                return _mediator != null && 
                    _mediator.gameObject != null && 
                    _mediator.gameObject.activeSelf;
            }
        }

        public bool isDispose
        {
            get
            {
                return _mediator == null || _mediator.gameObject == null;
            }
        }

        private bool _registed;
        public bool isRegisted
        {
            get
            {
                return _registed;
            }
        }

        private int _index;
        public int index 
        { 
            get
            {
                return _index;
            }
        }

        private bool _bindScene;
        public bool bindScene
        {
            get
            {
                return _bindScene;
            }
        }

        private string _sceneName;
        public string sceneName
        {
            get
            {
                return _sceneName;
            }
        }

        public object token { get; set; }

        #endregion

        public void SignInfo(int index, string bindSceneName = "")
        {
            this._index = index;
            if (bindSceneName != "")
            {
                this._sceneName = bindSceneName;
                this._bindScene = true;
            }
        }

        #region mediator...

        protected T RegisterMediator<T>(GameObject go, bool monitor=true) where T : Mediator
        {
            if (_mediator == null)
            {
                _mediator = go.GetComponent<T>();
                if (_mediator == null)
                {
                    _mediator = go.AddComponent<T>();
                }
                _mediator.SignCommand(this, monitor);
            }
            _mediator.Show();
            if (!_bindScene)
            {
                _sceneName = Facade.curSceneName;
            }
            _registed = true;
            return _mediator as T;
        }

        protected void RemoveMediator(bool dispose=false)
        {
            if (_mediator != null)
            {
                _mediator.Close(dispose);
            }
            if (!_bindScene)
            {
                _sceneName = "";
            }
            _registed = false;
        }

        protected void UpdateMediator()
        {
            if (_mediator != null)
            {
                _mediator.UpdateViewContent();
            }
        }

        #endregion

        #region notice...

        protected void SendNotice(int cmdIndex, int noticeType, params object[] body)
        {
            Facade.SendNotice(cmdIndex, noticeType, body);
        }

        protected void SendAsyncNotice(int cmdIndex, int noticeType, Action<INotice> callback, object token, params object[] body)
        {
            Facade.SendAsyncNotice(cmdIndex, noticeType, callback, token, body);
        }

        protected void SendToMediator(int noticeType, params object[] body)
        {
            Notice notice = new Notice(noticeType, body);
            SendToMediator(notice);
        }

        protected void SendToMediator(INotice notice)
        {
            if (_mediator != null)
            {
                _mediator.HandleNotice(notice);
            }
        }

        protected void BroadNotice(int noticeType, params object[] body)
        {
            Facade.BroadNotice(noticeType, body);
        }

        protected void SelfNotice(int noticeType, params object[] body)
        {
            Notice notice = new Notice(noticeType, body);
            this.Execute(notice);
        }

        private List<INotice> _noticeList = new List<INotice>();

        public void InsertNotice(INotice notice)
        {
            _noticeList.Add(notice);
        }

        protected void PushNotice(int cmdIndex, int noticeType, params object[] body)
        {
            Facade.PushNotice(cmdIndex, noticeType, body);
        }

        protected bool PopNotice(int noticeType=0)
        {
            if (_noticeList.Count > 0)
            {
                INotice target = null;
                foreach (INotice notice in _noticeList)
                {
                    if (notice.GetType() == noticeType || noticeType == 0)
                    {
                        target = notice;
                        break;
                    }
                }
                if (target != null)
                {
                    _noticeList.Remove(target);
                    Execute(target);
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region scene...

        protected void ChangeScene(string sceneName, Action<object> callback = null, object token = null)
        {
            Facade.ChangeScene(sceneName, callback, token);
        }

        protected Action CancelSceneChange()
        {
            return Facade.CancelSceneChange();
        }

        #endregion

        public virtual void Execute(INotice notice) { }

        public virtual void Dispose()
        {
            _noticeList.Clear();
            RemoveMediator(true);
        }

        #region resource loader...

        public void LoadResource(string resourceName, Action<GameObject> callback, object custom = null)
        {
            if (Facade._resourceLoader != null)
            {
                Facade._resourceLoader.Invoke(resourceName, callback, custom);
            }
            else
            {
                Debug.LogError("you must call the Facade.RegisterResourceLoader function to register a resloader behaviour at first!!");
            }
        }

        public GameObject SyncLoadResource(string resourceName, object custom = null)
        {
            if (Facade._syncResourceLoader != null)
            {
                return Facade._syncResourceLoader.Invoke(resourceName, custom);
            }
            else
            {
                Debug.LogError("you must call the Facade.RegisterSyncResourceLoader function to register a sync resloader behaviour at first!!");
                return null;
            }
        }

        #endregion
    }
}
