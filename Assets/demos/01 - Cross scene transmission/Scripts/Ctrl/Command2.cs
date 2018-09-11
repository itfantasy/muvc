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

    public const int Command2_Index = Worker_Ken.CommandIndex + 200;
    public const int Command2_Show = Command2_Index + 1;
    public const int Command2_OK = Command2_Index + 2;

    public override void Execute(Notice notice)
    {
        switch (notice.code)
        {
            case Command2.Command2_Show:
                GameObject go = GameObject.Find("Canvas2");
                RegisterMediator<Mediator2>(go);
                break;
            case Command2.Command2_OK:
                Facade.WaitForSceneChangeOnce("scene1", () =>
                {
                    this.SendNotice(Command1.Command1_Index, notice);
                });
                SceneManager.LoadScene("scene1");
                break;
            case Command1.Command1_OK:
                this.SendNotice(Command2_Index, new Notice(Command2_Show));
                this.SendNoticeToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}
