using UnityEngine;
using UnityEngine.Video;

public class videoController : extendedFunctions
{
    public static videoController instance;
    [SerializeField] private GameObject videoPlayerGameObject;
    private VideoPlayer videoPlayer;

    [SerializeField] private VideoClip clip;

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
                            gameController.instance.SecondPart();
                            return;
                        }
                        else
                        {
                            //there is a known bug that causes the videoplayer to not loop
                            //So you need to stop the clip and play it again to avoid the bug
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