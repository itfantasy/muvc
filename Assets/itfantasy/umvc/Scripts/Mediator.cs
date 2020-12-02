using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.igui;

namespace itfantasy.umvc
{
    public class Mediator : MonoBehaviour, IDisposable
    {
        protected Command _command = null;
        protected Mediator _parent = null;

        private bool _monitorchecked = false;
        private bool _monitoring = false;
        private bool monitoring
        {
            get
            {
                if (!_monitorchecked)
                {
                    _monitoring = Facade.CheckMonitor(this.name);
                }
                return _monitoring;
            }
        }

        private Dictionary<GameObject, Action<GameObject>> _clickListeners
            = new Dictionary<GameObject, Action<GameObject>>();

        public string NAME
        {
            get
            {
                string n = this.name;
                if (n.EndsWith("(Clone)"))
                {
                    n = n.Substring(0, n.Length - 7);
                }
                return n;
            }
        }

        protected object _viewObj { get; set; }

        public object token { get; set; }

        public void SignCommand(Command command)
        {
            this._command = command;
        }

        public void SignParent(Mediator parent)
        {
            this._parent = parent;
        }

        void Awake()
        {
            SendMonitoringNotice(Command.Monitor_Initing, this.NAME, this.gameObject);
            OnInitialize();
            SendMonitoringNotice(Command.Monitor_Inited, this.NAME, this.gameObject);
        }

        // Use this for initialization
        void Start()
        {
            _clickListeners.Clear();
            SetEventListener();
        }

        void OnEnable()
        {
            SendMonitoringNotice(Command.Monitor_Showing, this.NAME, this.gameObject);
            OnShowing();
            SendMonitoringNotice(Command.Monitor_Showed, this.NAME, this.gameObject);
        }

        void OnDisable()
        {
            OnClose();
            SendMonitoringNotice(Command.Monitor_Closed, this.NAME);
        }

        void OnDestroy()
        {
            OnDispose();
            SendMonitoringNotice(Command.Monitor_Disposed, this.NAME);
        }

        protected virtual void OnInitialize()
        {
            
        }

        protected virtual void OnShowing()
        {
            
        }

        protected virtual void OnClosing(Action callback) { callback.Invoke(); }

        protected virtual void OnClose()
        {
            
        }

        protected virtual void OnDispose()
        {
            
        }

        protected T AttachComponent<T>() where T : Component
        {
            return this.AttachComponent<T>(this.gameObject);
        }

        protected T AttachComponent<T>(GameObject go) where T : Component
        {
            T comp = go.GetComponent<T>();
            if (comp == null)
            {
                comp = go.AddComponent<T>();
            }
            return comp;
        }

        protected T AttachView<T>() where T : View
        {
            return this.AttachComponent<T>();
        }

        protected T AttachSubMediator<T>(GameObject go) where T : Mediator
        {
            T mediator = go.AddComponent<T>();
            mediator.SignCommand(_command);
            mediator.SignParent(this);
            return mediator;
        }

        protected T AttachSubMediator<T>(Transform child) where T : Mediator
        {
            GameObject go = child.gameObject;
            return AttachSubMediator<T>(go);
        }

        protected T AttachSubMediator<T>(string name) where T : Mediator
        {
            GameObject go = transform.Find(name).gameObject;
            return AttachSubMediator<T>(go);
        }

        protected void SendNotice(int noticeType, params object[] body)
        {
            Notice notice = new Notice(noticeType, body);
            SendNotice(notice);
        }

        protected void SendNotice(Notice notice)
        {
            if (_command != null)
            {
                _command.Execute(notice);
            }
        }

        protected virtual void OK(params object[] body)
        {
            Notice notice = new Notice(Command.Command_OK, body);
            SendNotice(notice);
        }

        protected virtual void Cancel()
        {
            SendNotice(Command.Command_Cancel);
        }

        private void SendMonitoringNotice(int noticeType, params object[] body)
        {
            if (this.monitoring)
            {
                Facade.SystemNotice(noticeType, body);
            }
        }

        public virtual void HandleNotice(INotice notice) { }

        protected void SetClick(Button btn, Action<GameObject> onClick = null)
        {
            SetClick(btn.gameObject, onClick);
        }

        protected void SetClick(GameObject go, Action<GameObject> onClick = null)
        {
            if(onClick == null)
            {
                onClick = this.OnClick;
            }
            _clickListeners[go] = onClick;
            UIClickListener.Get(go).onClick = this.Clicking;
        }

        private void Clicking(GameObject go)
        {
            if (_clickListeners.ContainsKey(go))
            {
                SendMonitoringNotice(Command.Monitor_Clicking, this.NAME, go.name);
                _clickListeners[go].Invoke(go);
                SendMonitoringNotice(Command.Monitor_Clicked, this.NAME, go.name);
            }
        }

        protected virtual void OnClick(GameObject go)
        {

        }

        protected virtual void SetEventListener() { }

        public virtual void UpdateViewContent() { }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Close(bool dispose = false)
        {
            SendMonitoringNotice(Command.Monitor_Closing, this.NAME);
            this.OnClosing(() =>
            {
                if (dispose)
                {
                    SendMonitoringNotice(Command.Monitor_Disposing, this.NAME);
                    this.Dispose();
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            });
        }

        public virtual void Dispose()
        {
            Destroy(this.gameObject);
        }

        #region state keeping...

        protected void SetViewObj(object vo)
        {
            if (vo != null)
            {
                this._viewObj = vo;
                this.UpdateViewContent();
            }
        }

        protected void SaveViewObj()
        {
            if (this._viewObj != null)
            {
                this._command.stateObj = this._viewObj;
            }
        }

        protected void LoadViewObj()
        {
            if (this._viewObj == null)
            {
                this.SetViewObj(this._command.stateObj);
            }
        }

        #endregion
    }
}