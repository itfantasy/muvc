using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace itfantasy.umvc
{
    public class Facade
    {
        #region --------------------> command管理

        private static Dictionary<int, Command> _commandDictionary = new Dictionary<int, Command>();

        public static void RegisterCommand(int index, Command command)
        {
            if (!_commandDictionary.ContainsKey(index))
            {
                _commandDictionary.Add(index, command);
            }
            else
            {

            }
        }

        public static void RemoveCommand(int index)
        {
            if (_commandDictionary.ContainsKey(index))
            {
                _commandDictionary.Remove(index);
            }
            else
            {

            }
        }

        #endregion

        #region --------------------> notice管理

        public static void SendNotice(int index, Notice notice)
        {
            if (_commandDictionary.ContainsKey(index))
            {
                notice.index = index;
                _commandDictionary[index].Execute(notice);
            }
        }

        public static void BroadNotice(Notice notice)
        {
            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.isActive)
                {
                    cmd.Execute(notice);
                }
            }
        }

        private static List<Notice> _noticeStack = new List<Notice>();

        public static void PushNotice(int index, Notice notice)
        {
            notice.index = index;
            _noticeStack.Add(notice);
        }

        public static void PushBroadNotice(Notice notice)
        {
            _noticeStack.Add(notice);
        }

        public static void PopNotice(int code)
        {
            Notice target = null;
            foreach (Notice notice in _noticeStack)
            {
                if (notice.code == code)
                {
                    if (notice.index == 0)
                    {
                        BroadNotice(notice);
                    }
                    else
                    {
                        SendNotice(notice.index, notice);
                    }
                    target = notice;
                }
            }
            _noticeStack.Remove(target);
        }

        public static void PopNotices(int code)
        {
            List<Notice> dirtyNotice = new List<Notice>();
            foreach(Notice notice in _noticeStack)
            {
                if(notice.code == code)
                {
                    dirtyNotice.Add(notice);
                }
            }
            foreach(Notice notice in dirtyNotice)
            {
                if(notice.index == 0)
                {
                    BroadNotice(notice);
                }
                else
                {
                    SendNotice(notice.index, notice);
                }
                _noticeStack.Remove(notice);
            }
            dirtyNotice.Clear();
        }

        public static void PopAllNotices()
        {
            foreach (Notice notice in _noticeStack)
            {
                if (notice.index == 0)
                {
                    BroadNotice(notice);
                }
                else
                {
                    SendNotice(notice.index, notice);
                }
            }
            _noticeStack.Clear();
        }

        public static void ClearStackNotices()
        {
            _noticeStack.Clear();
        }

        #endregion

        #region --------------------> proxy管理

        private static List<IBaseProxy> _proxyList = new List<IBaseProxy>();

        public static void RegisterProxy(IBaseProxy proxy)
        {
            if (!_proxyList.Contains(proxy))
            {
                _proxyList.Add(proxy);
            }
        }

        #endregion

        public static void InitMVC()
        {
            ClearStackNotices();
            _commandDictionary.Clear();
            foreach (IBaseProxy proxy in _proxyList)
            {
                proxy.Dispose();
            }
            _proxyList.Clear();
        }

    }
}
