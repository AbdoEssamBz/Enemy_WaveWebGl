using UnityEngine;

public class TestLigt : MonoBehaviour
{
    public Light testLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            testLight.enabled = !testLight.enabled;
        }
    }
}
