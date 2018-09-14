using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Command1 : Command {

    public const int Index = Worker_Ken.CommandIndex + 100;
    public const int Command1_Show = Index + 1;
    public const int Command1_OK = Index + 2;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command1_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<Mediator1>(root.transform.Find("Canvas1").gameObject);
                break;
            case Command1_OK:
                this.SendNotice(Index, Command1_Show);
                this.SendNotice(notice);
                break;
        }
        base.Execute(notice);
    }

    public void TransValueToScene2(string msg)
    {
        Facade.WaitForSceneChangeOnce("Scene2", () =>
        {
            this.SendNotice(Command2.Index, Command2.Command2_OK, msg);
        });
        SceneManager.LoadScene("Scene2");
    }
}
