using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public static float Horizontal = 0f;

    public static bool JumpPressed = false;
    public static bool InteractPressed = false;

    public void LeftDown()
    {
        Horizontal = -1f;
    }

    public void RightDown()
    {
        Horizontal = 1f;
    }

    public void StopMoving()
    {
        Horizontal = 0f;
    }

    public void Jump()
    {
        JumpPressed = true;
    }

    public void Interact()
    {
        InteractPressed = true;
    }

    private void LateUpdate()
    {
        JumpPressed = false;
        InteractPressed = false;
    }
}