using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace itfantasy.umvc
{
    public class Command : IDisposable
    {
        public const int Command_Reactive = 9999;

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

        protected void RegisterMediator<T>(GameObject go) where T : Mediator
        {
            if (_mediator == null)
            {
                _mediator = go.AddComponent<T>();
                _mediator.SignCommand(this);
            }
            _mediator.gameObject.SetActive(true);
            _sceneName = Facade.curSceneName;
            _isRegisted = true;
        }

        protected void RemoveMediator(bool dispose=false)
        {
            if (_mediator != null)
            {
                _mediator.OnClosing(() => {
                    if (dispose)
                    {
                        _mediator.Dispose();
                    }
                    else
                    {
                        _mediator.gameObject.SetActive(false);
                    }
                });
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
