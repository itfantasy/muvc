using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace itfantasy.umvc
{
    public class Command : IDisposable
    {
        public const int Monitor_Inited = 101;
        public const int Monitor_Showed = 102;
        public const int Monitor_Closed = 103;
        public const int Monitor_Disposed = 104;
        
        public const int Command_Reactive = 1001;
        public const int Command_Show = 1002;
        public const int Command_Close = 1003;
        public const int Command_OK = 1004;
        public const int Command_Trigger = 1005;
        public const int Command_Cancel = 1006;

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

        private bool _isRegisted;
        public bool isRegisted
        {
            get
            {
                return _isRegisted;
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

        protected void RegisterMediator<T>(GameObject go, bool monitor=true) where T : Mediator
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
            _sceneName = Facade.curSceneName;
            _isRegisted = true;
        }

        protected void RemoveMediator(bool dispose=false)
        {
            if (_mediator != null)
            {
                _mediator.Close(dispose);
            }
            _sceneName = "";
            _isRegisted = false;
        }

        protected void SendNotice(int cmdIndex, int noticeType, params object[] body)
        {
            Facade.SendNotice(cmdIndex, noticeType, body);
        }

        protected void SendAsyncNotice(int cmdIndex, int noticeType, Action<INotice> callback, object token, params object[] body)
        {
            Facade.SendAsyncNotice(cmdIndex, noticeType, callback, token, body);
        }

        protected void SendNotice(int noticeType, params object[] body)
        {
            Notice notice = new Notice(noticeType, body);
            SendNotice(notice);
        }

        protected void SendNotice(INotice notice)
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

        public virtual void Execute(INotice notice) { }

        public virtual void Dispose()
        {
            
        }
    }
}
