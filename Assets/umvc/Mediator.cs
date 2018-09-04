using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Mediator : MonoBehaviour, IDisposable
    {
        bool inited = false;
        Command _command = null;

        public void SignCommand(Command command)
        {
            this._command = command;
        }

        // Use this for initialization
        void Start()
        {
            OnInitialize();
            inited = true;
            OnShowing();
        }

        void OnEnable()
        {
            if (inited)
            {
                OnShowing();
            }
        }

        void OnDisable()
        {
            OnClosing();
        }

        void OnDestroy()
        {
            OnDispose();
        }

        protected virtual void OnInitialize() {
            SetClickListener();
        }

        protected virtual void OnShowing() {
            UpdateViewContent();
        }

        protected virtual void OnClosing() { }

        protected virtual void OnDispose() { }

        protected void SendToCommand(int index, int code, object value)
        {
            Notice notice = new Notice(code, value, this);
            this._command.Execute(notice);
        }

        protected T AttachView<T>() where T : View
        {
            return this.gameObject.AddComponent<T>();
        }

        public virtual void HandleNotice(Notice notice) { }

        protected virtual void OnClick(GameObject go) { }

        protected virtual void SetClickListener() { }

        protected virtual void UpdateViewContent() { }

        public virtual void Dispose() 
        {
            Destroy(this.gameObject);
        }
        
    }
}
