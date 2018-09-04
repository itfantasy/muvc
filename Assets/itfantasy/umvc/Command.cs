using UnityEngine;
using System.Collections;

namespace itfantasy.umvc
{
    public class Command
    {

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

        protected void RegisterMediator<T>(GameObject go) where T : Mediator
        {
            if (_mediator == null)
            {
                _mediator = go.AddComponent<T>();
            }
            _mediator.gameObject.SetActive(true);
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
        }

        protected void SendNotice(int index, int code, object value)
        {
            Notice notice = new Notice(code, value, this);
            Facade.SendNotice(index, notice);
        }

        protected void SendNotice(int index, Notice notice)
        {
            notice.AddPasser(this);
            Facade.SendNotice(index, notice);
        }

        protected void SendNoticeToMediator(int code, object value)
        {
            if (_mediator != null)
            {
                Notice notice = new Notice(code, value, this);
                SendNoticeToMediator(notice);
            }
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
