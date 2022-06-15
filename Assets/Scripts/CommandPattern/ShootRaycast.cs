using UnityEngine;
public class ShootRaycast
{
    private Camera rayCastCamera;
 

    public ShootRaycast(ICameraComponent camera)
    {
        rayCastCamera = camera.GetSceneCamera();
    }


    public GameObject RaycastHit(Vector3 originPoint)
    {
        RaycastHit hit;
        Ray ray = rayCastCamera.ScreenPointToRay(originPoint);

        if (Physics.Raycast(ray, out hit))
            return hit.transform.gameObject;
        else
            return null;
    }
}


