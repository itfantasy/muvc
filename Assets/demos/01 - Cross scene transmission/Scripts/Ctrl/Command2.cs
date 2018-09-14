using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Command2 : Command {

    public const int Index = Worker_Ken.Index + 200;
    public const int Command2_Show = Index + 1;
    public const int Command2_OK = Index + 2;

    public override void Execute(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command2_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<Mediator2>(root.transform.Find("Canvas2").gameObject);
                break;
            case Command2_OK:
                this.SendNotice(Index, Command2_Show);
                this.SendNotice(notice);
                break;
        }
        base.Execute(notice);
    }

    public void TransValueToScene1(string msg)
    {
        Facade.WaitForSceneChangeOnce("Scene1", () =>
        {
            this.SendNotice(Command1.Index, Command1.Command1_OK, msg);
        });
        SceneManager.LoadScene("Scene1");
    }
}
