using UnityEngine;
using UnityEngine.Video;

public class videoController : extendedFunctions
{
    public static videoController instance;
    [SerializeField] private GameObject videoPlayerGameObject;
    private VideoPlayer videoPlayer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            videoPlayer = videoPlayerGameObject.GetComponent<VideoPlayer>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartVideoHandler()
    {
        Wait(3.5f, () =>
        {
            videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
            Wait(10.5f, () =>
            {
                videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
                Wait(3.7f, () =>
                {
                    videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
                    Wait(19.5f, () =>
                    {
                        if (gameController.instance.isPhoneClicked)
                        {
                            //escape
                            Debug.Log("escape");

                            return;
                        }
                        else
                        {
                            Debug.Log("here");
                            videoPlayer.Stop();
                            StartVideoHandler();
                            videoPlayer.Play();
                            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
                        }
                    });
                });
            });
        });

    }
}