using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Mediator : MonoBehaviour, IDisposable
    {
        protected Command _command = null;
        protected Mediator _parent = null;

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
            OnClose();
            OnDispose();
        }

        protected virtual void OnInitialize() {
            
        }

        protected virtual void OnShowing() {
            UpdateViewContent();
        }

        protected virtual void OnClosing(Action callback) { callback.Invoke(); }

        protected virtual void OnClose() { }

        protected virtual void OnDispose() { }

        protected T AttachView<T>() where T : View
        {
            return this.gameObject.AddComponent<T>();
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

        public virtual void HandleNotice(INotice notice) { }

        protected virtual void OnClick(GameObject go) { }

        protected virtual void SetEventListener() { }

        protected virtual void UpdateViewContent() { }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Close(bool dispose=false)
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
