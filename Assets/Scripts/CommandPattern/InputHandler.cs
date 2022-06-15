using UnityEngine;

public class InputHandler : GameSceneController, ICameraComponent
{
    BattleSystemController battleSystem;
    CommandHandler commandHandler = new CommandHandler();
    IGameManager gameManager;
    bool enableInput =true;
    ShootRaycast shootRaycast;

    public override void LoadDependencies(IGameManager manager)
    {
        gameManager = manager;
        shootRaycast = new ShootRaycast(this);
        battleSystem = (BattleSystemController)gameManager.GetGameControllerComponent(typeof(BattleSystemController));
        if (battleSystem == null)
        {
            enableInput = false;
            Debug.LogError($"{typeof(BattleSystemController)} is not found!!!");
            return;
        }
    }
    public Camera GetSceneCamera()
    {
        return  gameManager.GetSceneCamera();
    }

    void Update()
    {
        if(!enableInput)
            return;

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                commandHandler.ScreenInteraction(shootRaycast,battleSystem,Input.GetTouch(i).position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            commandHandler.ScreenInteraction(shootRaycast,battleSystem, Input.mousePosition);
        }

        
    }

}
