using UnityEngine;

public class TestKeyInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Phím B hoạt động!");
        }
    }
}
