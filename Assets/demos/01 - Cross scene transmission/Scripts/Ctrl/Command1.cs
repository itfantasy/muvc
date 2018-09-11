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

    public const int Command1_Index = Worker_Ken.CommandIndex + 100;
    public const int Command1_Show = Command1_Index + 1;
    public const int Command1_OK = Command1_Index + 2;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case Command1.Command1_Show:
                GameObject go = GameObject.Find("Canvas1");
                RegisterMediator<Mediator1>(go);
                break;
            case Command1.Command1_OK:
                Facade.WaitForSceneChangeOnce("Scene2", () =>
                {
                    this.SendNotice(Command2.Command2_Index, notice);
                });
                SceneManager.LoadScene("Scene2");
                break;
            case Command2.Command2_OK:
                this.SendNotice(Command1_Index, new Notice(Command1_Show));
                this.SendNoticeToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}
