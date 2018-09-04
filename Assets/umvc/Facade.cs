using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace itfantasy.umvc
{
    public class Facade
    {

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

        public static void SendNotice(int index, Notice notice)
        {
            if (_commandDictionary.ContainsKey(index))
            {
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

        private static List<IBaseProxy> _proxyList = new List<IBaseProxy>();

        public static void RegisterProxy(IBaseProxy proxy)
        {
            if (!_proxyList.Contains(proxy))
            {
                _proxyList.Add(proxy);
            }
        }

        public static void InitialMVC()
        {
            _commandDictionary.Clear();
            foreach (IBaseProxy proxy in _proxyList)
            {
                proxy.Dispose();
            }
            _proxyList.Clear();
        }
    }
}
