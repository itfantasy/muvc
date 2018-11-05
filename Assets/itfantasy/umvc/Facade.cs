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

        public static void RegisterCommand(int index, Command command, bool system=false)
        {
            if (!_commandDictionary.ContainsKey(index))
            {
                command.SignInfo(index, system);
                if (index == Command.SystemIndex)
                {
                    command.SignInfo(Command.SystemIndex, true);
                }
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

        public static void PushNotice(int cmdIndex, int noticeType, object[] body=null)
        {
            if (_commandDictionary.ContainsKey(cmdIndex))
            {
                Notice notice = new Notice(noticeType, body);
                _commandDictionary[cmdIndex].InsertNotice(notice);
            }
        }

        #endregion

        #region -------------------------> system notice

        public static void SystemNotice(int noticeType, object[] body)
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
        private static string _lstSceneName = "";

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

        private static Dictionary<string, List<AsyncArg>> _sceneCallbacks
            = new Dictionary<string, List<AsyncArg>>();

        public static LoadSceneBehaviour loadSceneBehaviour { get; set; }

        private static void DealSceneChangeCallbacks(string sceneName)
        {
            if (_sceneCallbacks.ContainsKey(_curSceneName))
            {
                List<AsyncArg> args = _sceneCallbacks[_curSceneName];
                foreach (AsyncArg arg in args)
                {
                    arg.callback.Invoke(arg.token);
                }
                args.Clear();
                _sceneCallbacks.Remove(_curSceneName);
            }
        }

        public static void AddSceneChangeCallback(string sceneName, Action<object> callback, object token = null)
        {
            if (!_sceneCallbacks.ContainsKey(sceneName))
            {
                _sceneCallbacks[sceneName] = new List<AsyncArg>();
            }
            _sceneCallbacks[sceneName].Add(new AsyncArg(callback, token));
        }

        public static void ChangeScene(string sceneName, Action<object> callback = null, object token = null)
        {
            if (callback != null)
            {
                AddSceneChangeCallback(sceneName, callback, token);
            }

            if (sceneName == _curSceneName)
            {
                DealSceneChangeCallbacks(sceneName);
            }
            else
            {
                foreach (Command cmd in _commandDictionary.Values)
                {
                    if (cmd.isSystem)
                    {
                        cmd.Execute(new Notice(Command.System_SceneLeave, new object[] { _curSceneName, sceneName }));
                    }
                }

                if (loadSceneBehaviour != null)
                {
                    loadSceneBehaviour.Invoke(sceneName);
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        private static void OnSceneChange(Scene scene, LoadSceneMode mode)
        {
            _lstSceneName = _curSceneName;
            _curSceneName = scene.name;

            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.isSystem)
                {
                    cmd.Execute(new Notice(Command.System_SceneEnter, new object[] { _curSceneName, _lstSceneName }));
                }

                if (cmd.sceneName == _curSceneName && cmd.isRegisted)
                {
                    cmd.Execute(new Notice(Command.Command_Reactive, null));
                }
            }

            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.isSystem)
                {
                    cmd.Execute(new Notice(Command.System_SceneChanged, new object[] { _curSceneName, _lstSceneName }));
                }
            }

            DealSceneChangeCallbacks(_curSceneName);
        }

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
            SceneManager.sceneLoaded -= OnSceneChange;
            SceneManager.sceneLoaded += OnSceneChange;
        }

    }

    public class AsyncArg
    {
        public Action<object> callback;
        public object token;

        public AsyncArg(Action<object> callback, object token)
        {
            this.callback = callback;
            this.token = token;
        }
    }

    public delegate void LoadSceneBehaviour(string sceneName);
}
