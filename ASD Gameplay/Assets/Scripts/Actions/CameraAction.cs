using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Camera")]
public class CameraAction : Action
{
    private CameraController camera;

    public override void Act(StateController controller)
    {
        camera.RotateCamera();
    }

    public override void OnExit(StateController controller)
    {
    }

    public override void OnStart(StateController controller)
    {
        camera = controller.transform.GetComponent<CameraController>();
    }
}