using UnityEngine;

// This script will spawn a prefab when you tap the screen
public class SimpleSelect : MonoBehaviour
{

    protected virtual void OnEnable()
    {
        // Hook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap += OnFingerDown;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap -= OnFingerDown;
    }

    public void OnFingerDown(Lean.LeanFinger finger)
    {
        // Raycast information
        var ray = finger.GetRay();
        var hit = default(RaycastHit);

        // Was this finger pressed down on a collider?
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity) == true)
        {
            GameObject recepient = hit.transform.gameObject;
                     

        }
    }
}