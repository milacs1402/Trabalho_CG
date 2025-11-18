using UnityEngine;

public class mudarCameras : MonoBehaviour
{
    public Camera cameraOne;
    public Camera cameraTwo;
    private bool isOnMap = false;

    void Start()
    {
        cameraOne.enabled = true;
        cameraTwo.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();

            isOnMap = !(isOnMap);

            if (isOnMap)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    void SwitchCameras()
    {
        cameraOne.enabled = !cameraOne.enabled;
        cameraTwo.enabled = !cameraTwo.enabled;
    }
}