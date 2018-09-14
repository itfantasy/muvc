using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

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

        #region --------------------> notice机制

        public static void SendNotice(int cmdIndex, int noticeType, object[] body=null)
        {
            if (_commandDictionary.ContainsKey(cmdIndex))
            {
                Notice notice = new Notice(noticeType, body);
                _commandDictionary[cmdIndex].Execute(notice);
            }
        }

        public static void SendAsyncNotice(int cmdIndex, int noticeType, Action<object> callback, object token, object[] body=null)
        {
            if (_commandDictionary.ContainsKey(cmdIndex))
            {
                Notice notice = new Notice(noticeType, body);
                notice.SetCallback(callback, token);
                _commandDictionary[cmdIndex].Execute(notice);
            }
        }

        public static void BroadNotice(int noticeType, object[] body=null)
        {
            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.isActive)
                {
                    Notice notice = new Notice(noticeType, body);
                    cmd.Execute(notice);
                }
            }
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
            _commandDictionary.Clear();
            foreach (IBaseProxy proxy in _proxyList)
            {
                proxy.Dispose();
            }
            _proxyList.Clear();
            SceneManager.sceneLoaded += OnSceneChange;
        }

        private static string _curSceneName;

        public static string curSceneName
        {
            get
            {
                return _curSceneName;
            }
        }

        public static void OnSceneChange(Scene scene, LoadSceneMode mode)
        {
            _curSceneName = scene.name;
            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.sceneName == _curSceneName)
                {
                    cmd.Execute(new Notice(Command.TryReactive, null));
                }
            }

            if (waitSceneName != "" &&
                waitSceneName == _curSceneName &&
                waitSceneChangeCallback != null)
            {
                waitSceneChangeCallback.Invoke();
            }
        }

        public static void WaitForSceneChangeOnce(string sceneName, Action callback)
        {
            waitSceneName = sceneName;
            waitSceneChangeCallback = callback;
        }

        static string waitSceneName;
        static Action waitSceneChangeCallback;
    }
}
