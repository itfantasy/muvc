using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class Worker_K {

    public const int Index = 100000;

    public static void RegisterCommands()
    {
        Facade.RegisterResourceLoader(ResourceLoader);
        Facade.RegisterSyncResourceLoader(SyncResourceLoader);

        Facade.RegisterCommand(CodeGenCommand.Index, new CodeGenCommand());
        Facade.SendNotice(CodeGenCommand.Index, Command.Command_Show, new object[] { new CodeGenVo() { name = "K" } });
    }

    public static void ResourceLoader(string resName, Action<GameObject> callback, object custom)
    {
        GameObject root = GameObject.Find("UIRoot");
        GameObject go = root.transform.Find(resName).gameObject;
        callback.Invoke(go);
    }

    public static GameObject SyncResourceLoader(string resName, object custom)
    {
        GameObject root = GameObject.Find("UIRoot");
        return root.transform.Find(resName).gameObject;
    }
}
