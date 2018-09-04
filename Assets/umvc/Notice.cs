using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public class Notice
    {
        /// <summary>
        /// 命令码
        /// </summary>
        public int code;

        /// <summary>
        /// 信息体
        /// </summary>
        public Dictionary<Type, NoticeParam> body = new Dictionary<Type, NoticeParam>();

        /// <summary>
        /// 创建者
        /// </summary>
        private object creator;

        /// <summary>
        /// 传递者
        /// </summary>
        private List<object> passers = new List<object>();

        /// <summary>
        /// 接收者
        /// </summary>
        private object receiver;

        public Notice(int code, object value, object creator)
        {
            this.code = code;
            NoticeParam param = new NoticeParam(value, creator);
            this.body[param.type] = param;
            this.creator = creator;
        }

        public void AddPasser(object passers)
        {
            this.passers.Add(passers);
        }

        public void SignReceiver(object receiver)
        {
            if(this.receiver == null)
            {
                this.receiver = receiver;
            }
        }

        public bool PushParam(object value, object owner)
        {
            NoticeParam param = new NoticeParam(value, owner);
            if (!this.body.ContainsKey(param.type))
            {
                this.body[param.type] = param;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public T GetParam<T>()
        {
            Type type = typeof(T);
            if (this.body.ContainsKey(type))
            {
                return (T)this.body[type].value;
            }
            else
            {
                return default(T);
            }
        }

        public string PrintStack()
        {
            return "";
        }
    }

    public class NoticeParam
    {
        public Type type { get; set; }

        public object value { get; set; }

        public object owner { get; set; }

        public NoticeParam(object value, object owner)
        {
            this.type = value.GetType();
            this.value = value;
            this.owner = owner;
        }
    }


}
