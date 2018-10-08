using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace itfantasy.umvc
{
    public class Facade
    {
        #region --------------------> command

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

        #region --------------------> notice

        public static void SendNotice(int cmdIndex, int noticeType, object[] body=null)
        {
            if (_commandDictionary.ContainsKey(cmdIndex))
            {
                Command command = _commandDictionary[cmdIndex];
                Notice notice = new Notice(noticeType, body);
                command.Execute(notice);
            }
        }

        public static void SendAsyncNotice(int cmdIndex, int noticeType, Action<INotice> callback, object token, object[] body=null)
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

        #region -------------------------> system notice

        public static void SystemNotice(int noticeType, params object[] body)
        {
            SendNotice(Command.SystemIndex, noticeType, body);
        }

        #endregion

        #region --------------------> proxy

        private static List<IBaseProxy> _proxyList = new List<IBaseProxy>();

        public static void RegisterProxy(IBaseProxy proxy)
        {
            if (!_proxyList.Contains(proxy))
            {
                _proxyList.Add(proxy);
            }
        }

        #endregion

        #region ------------------------> scene

        private static string _curSceneName = "";

        public static string curSceneName
        {
            get
            {
                if (_curSceneName == "")
                {
                    _curSceneName = SceneManager.GetActiveScene().name;
                }
                return _curSceneName;
            }
        }

        private static void OnSceneChange(Scene scene, LoadSceneMode mode)
        {
            _curSceneName = scene.name;
            SystemNotice(Command.System_SceneChange, _curSceneName);

            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.sceneName == _curSceneName && cmd.isRegisted)
                {
                    cmd.Execute(new Notice(Command.Command_Reactive, null));
                }
            }

            if (waitSceneName != "" &&
                waitSceneName == _curSceneName &&
                waitSceneChangeCallback != null)
            {
                waitSceneChangeCallback.Invoke();
                waitSceneName = "";
                waitSceneChangeCallback = null;
            }
        }

        public static void WaitForSceneChangeOnce(string sceneName, Action callback)
        {
            waitSceneName = sceneName;
            waitSceneChangeCallback = callback;
        }

        static string waitSceneName;
        static Action waitSceneChangeCallback;

        #endregion

        public static void InitMVC()
        {
            foreach (Command command in _commandDictionary.Values)
            {
                command.Dispose();
            }
            _commandDictionary.Clear();
            foreach (IBaseProxy proxy in _proxyList)
            {
                proxy.Dispose();
            }
            _proxyList.Clear();
            SceneManager.sceneLoaded += OnSceneChange;
        }

    }
}
