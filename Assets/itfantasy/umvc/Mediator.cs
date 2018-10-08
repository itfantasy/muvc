using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Mediator : MonoBehaviour, IDisposable
    {
        protected Command _command = null;
        protected Mediator _parent = null;

        private bool _monitoring = false;

        public object token { get; set; }

        public void SignCommand(Command command, bool monitor=false)
        {
            this._command = command;
            this._monitoring = monitor;
        }

        public void SignParent(Mediator parent)
        {
            this._parent = parent;
        }

        void Awake()
        {
            OnInitialize();
        }

        // Use this for initialization
        void Start()
        {
            SetEventListener();
        }

        void OnEnable()
        {
            OnShowing();
        }

        void OnDisable()
        {
            OnClose();
        }

        void OnDestroy()
        {
            OnDispose();
        }

        protected virtual void OnInitialize() {
            SendMonitoringNotice(Command.Monitor_Inited);
        }

        protected virtual void OnShowing() {
            UpdateViewContent();
            SendMonitoringNotice(Command.Monitor_Showed);
        }

        protected virtual void OnClosing(Action callback) { callback.Invoke(); }

        protected virtual void OnClose() {
            SendMonitoringNotice(Command.Monitor_Closed);
        }

        protected virtual void OnDispose() {
            SendMonitoringNotice(Command.Monitor_Disposed);
        }

        protected T AttachComponent<T>() where T : Component
        {
            T comp = this.gameObject.GetComponent<T>();
            if (comp == null)
            {
                comp = this.gameObject.AddComponent<T>();
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
            if (_command != null)
            {
                _command.Execute(notice);
            }
        }

        private void SendMonitoringNotice(int noticeType)
        {
            if(this._monitoring)
            {
                Facade.SystemNotice(noticeType, this.name);
            }
        }

        public virtual void HandleNotice(INotice notice) { }

        protected virtual void OnClick(GameObject go) { }

        protected virtual void SetEventListener() { }

        protected virtual void UpdateViewContent() { }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Close(bool dispose = false)
        {
            this.OnClosing(() =>
            {
                if (dispose)
                {
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

    }
}
