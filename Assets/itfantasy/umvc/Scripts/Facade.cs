﻿using UnityEngine;
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

        public static void RegisterCommand(int index, Command command, string sceneName="")
        {
            if (!_commandDictionary.ContainsKey(index))
            {
                command.SignInfo(index, sceneName);
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

        private static Dictionary<string, bool> _monitorDict = new Dictionary<string, bool>();

        public static void SetMonitor(string name, bool val)
        {
            _monitorDict[name] = val;
        }

        public static bool CheckMonitor(string name)
        {
            if (_monitorDict.ContainsKey(name))
            {
                return _monitorDict[name];
            }
            return false;
        }

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

        public static SceneLoader _sceneLoader;
        public static void RegisterSceneLoader(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

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

        private static bool _sceneChangePausing = false;
        private static string _pausingSceneName = "";
        private static object _scenePausingCustom = null;

        public static IScenePauser PauseSceneChange()
        {
            _sceneChangePausing = true;
            return new ScenePauser(() => {
                if (_sceneLoader != null)
                {
                    _sceneLoader.Invoke(_pausingSceneName, _scenePausingCustom);
                }
                else
                {
                    SceneManager.LoadScene(_pausingSceneName);
                }
            });
        }

        public static void ChangeScene(string sceneName, Action<object> callback = null, object token = null, object custom = null)
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
                _sceneChangePausing = false;
                _pausingSceneName = sceneName;
                _scenePausingCustom = custom;

                SystemNotice(Command.Monitor_SceneLeaving, new object[] { _curSceneName, sceneName });
                foreach (Command cmd in _commandDictionary.Values)
                {
                    if (cmd.bindScene && cmd.sceneName == _curSceneName)
                    {
                        cmd.Execute(new Notice(Command.Scene_Leave, new object[] { _curSceneName, sceneName }));
                    }

                    if (cmd.bindScene && cmd.sceneName == sceneName)
                    {
                        cmd.Execute(new Notice(Command.Scene_Loading, new object[] { sceneName, _curSceneName }));
                    }
                }
                SystemNotice(Command.Monitor_SceneLeaved, new object[] { _curSceneName, sceneName });

                if (_sceneChangePausing)
                {
                    _sceneChangePausing = false;
                    return;
                }

                if (_sceneLoader != null)
                {
                    _sceneLoader.Invoke(sceneName, custom);
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        private static void OnSceneChange(Scene scene, LoadSceneMode mode)
        {
            if (mode == LoadSceneMode.Additive)
            {
                return;
            }

            _lstSceneName = _curSceneName;
            _curSceneName = scene.name;

            SystemNotice(Command.Monitor_SceneEntering, new object[] { _curSceneName, _lstSceneName });
            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.bindScene && cmd.sceneName == _curSceneName)
                {
                    cmd.Execute(new Notice(Command.Scene_Enter, new object[] { _curSceneName, _lstSceneName }));
                }

                if (cmd.sceneName == _curSceneName && cmd.isRegisted)
                {
                    cmd.Execute(new Notice(Command.Command_Reactive, null));
                }
            }
            SystemNotice(Command.Monitor_SceneEntered, new object[] { _curSceneName, _lstSceneName });

            SystemNotice(Command.Monitor_SceneChanging, new object[] { _curSceneName, _lstSceneName });
            foreach (Command cmd in _commandDictionary.Values)
            {
                if (cmd.bindScene && cmd.sceneName == _curSceneName)
                {
                    cmd.Execute(new Notice(Command.Scene_Change, new object[] { _curSceneName, _lstSceneName }));
                }
            }
            SystemNotice(Command.Monitor_SceneChanged, new object[] { _curSceneName, _lstSceneName });

            DealSceneChangeCallbacks(_curSceneName);
        }

        #endregion

        #region ------------------------> resloader

        public static ResourceLoader _resourceLoader;
        public static void RegisterResourceLoader(ResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }
        public static SyncResourceLoader _syncResourceLoader;
        public static void RegisterSyncResourceLoader(SyncResourceLoader syncResourceLoader)
        {
            _syncResourceLoader = syncResourceLoader;
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

            _monitorDict.Clear();
            _sceneCallbacks.Clear();

            _sceneChangePausing = false;
            _pausingSceneName = "";
            _scenePausingCustom = null;
        }

        public static void RegisterDefaultLoaders(SceneLoader sceneLoader, 
            ResourceLoader resourceLoader, SyncResourceLoader syncResourceLoader)
        {
            _sceneLoader = sceneLoader;
            _resourceLoader = resourceLoader;
            _syncResourceLoader = syncResourceLoader;
        }
    }

}
