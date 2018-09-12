using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace itfantasy.umvc
{
    public class Notice
    {
        /// <summary>
        /// 命令码
        /// </summary>
        public int code { get { return this._code; } }
        private int _code;

        /// <summary>
        /// 传递者
        /// </summary>
        private List<object> passers = new List<object>();

        /// <summary>
        /// 是否广播中
        /// </summary>
        public bool isBroading { get; set; }

        public object tag;

        public Notice(int code)
        {
            this._code = code;
        }

        public void AddPasser(object passers)
        {
            this.passers.Add(passers);
            this.PrintStack();
        }

        private void PrintStack()
        {
            string stackInfo = "[ " + this.GetType().Name + "|" + this._code.ToString() + " ] :: ";
            if (!isBroading)
            {
                if (this.passers.Count == 1)
                {
                    stackInfo += this.passers[0].GetType().Name + " Created!!";
                }
                else if (this.passers.Count > 1)
                {
                    stackInfo += this.passers[this.passers.Count - 2].GetType().Name +
                        "---->" +
                        this.passers[this.passers.Count - 1].GetType().Name;
                }
            }
            else
            {
                if (this.passers.Count >= 1)
                {
                    stackInfo += this.passers[this.passers.Count - 1].GetType().Name + " Received!!";
                }
            }
            Debug.Log(stackInfo);
        }
    }
}
