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
        public List<NoticeParam> body = new List<NoticeParam>();

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

        /// <summary>
        /// Command索引
        /// </summary>
        public int index = 0;

        /// <summary>
        /// 标签
        /// </summary>
        public object tag;

        public Notice(int code, object value, object creator)
        {
            this.code = code;
            NoticeParam param = new NoticeParam(value, creator);
            this.body.Add(param);
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

        public void PushParam(object value, object owner)
        {
            NoticeParam param = new NoticeParam(value, owner);
            this.body.Add(param);
        }
        
        public T GetParam<T>()
        {
            Type type = typeof(T);
            foreach(NoticeParam param in this.body)
            {
                if(param.type == type)
                {
                    return (T)param.value;
                }
            }
            return default(T);
        }

        public List<T> GetParams<T>()
        {
            List<T> ret = new List<T>();
            Type type = typeof(T);
            foreach (NoticeParam param in this.body)
            {
                if (param.type == type)
                {
                    ret.Add((T)param.value);
                }
            }
            return ret;
        }

        public ArrayList GetAllParams()
        {
            ArrayList ret = new ArrayList();
            foreach (NoticeParam param in this.body)
            {
                ret.Add(param.value);   
            }
            return ret;
        }

        public string PrintStack()
        {
            if(_printCallback != null)
            {
                _printCallback.Invoke("");
            }
            return "";
        }

        private static Action<string> _printCallback;
        public static void RegisterPrintCallback(Action<string> callback)
        {
            _printCallback = callback;
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

        public override string ToString()
        {
            return "";
        }
    }


}
