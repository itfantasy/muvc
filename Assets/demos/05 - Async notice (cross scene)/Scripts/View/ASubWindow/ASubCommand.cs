using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class ASubCommand : Command {

    public const int Index = Worker_Noctis.Index + 200;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<ASubMediator>(root.transform.Find("SubWindow").gameObject);
                break;
            case Command_Close:
                RemoveMediator();
                break;
            case Command_OK:
                Facade.ChangeScene("ASubScene", (token) =>
                {
                    SendNotice(Index, Command_Show);
                    SendNotice(notice);
                });
                break;
        }
        base.Execute(notice);
    }

    public void FinishNotice(INotice notice)
    {
        this.SendNotice(Index, Command_Close);
        Facade.ChangeScene("AMainScene", (token) =>
        {
            if (notice != null)
            {
                notice.Finish();
            }
        });
    }
}
