using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace itfantasy.umvc
{
    public class Command
    {
        public const int TryReactive = 101;
        public const int SceneChange = 102;

        Mediator _mediator;

        public bool isActive
        {
            get
            {
                return _mediator != null && 
                    _mediator.gameObject != null && 
                    _mediator.gameObject.activeSelf;
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

        public object token;

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
                if (dispose)
                {
                    _mediator.Dispose();
                }
                else
                {
                    _mediator.gameObject.SetActive(false);
                }
            }
            _sceneName = "";
            _isRegisted = false;
        }

        protected void SendNotice(int index, Notice notice)
        {
            notice.AddPasser(this);
            Facade.SendNotice(index, notice);
        }

        protected void SendNoticeToMediator(Notice notice)
        {
            if (_mediator != null)
            {
                notice.AddPasser(this);
                _mediator.HandleNotice(notice);
            }
        }

        public virtual void Execute(Notice notice) { }
        
    }
}
