using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(MeshRenderer))]
public class TargetVideoPlayer : MonoBehaviour
{
    public ImageTargetExtended imageTargetBehaviour;
    public float startFromTime = 0f;
    public bool debug;

    public VideoPlayer VideoPlayer { get; private set; }
    private MeshRenderer meshRenderer;

    void Awake()
    {
        Log("Awake");
        VideoPlayer = GetComponent<VideoPlayer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    
    private void Start()
    {
        Log("Start");
        VideoPlayer.time = startFromTime;
        VideoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
        imageTargetBehaviour.TargetFound += ImageTargetBehaviour_TargetFound;
        imageTargetBehaviour.TargetLost += ImageTargetBehaviour_TargetLost;
    }

    private void Update()
    {
        startFromTime = (float)VideoPlayer.time;
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
        Log("VideoPlayer_prepareCompleted");

        meshRenderer.enabled = true;
    }
    
    private void ImageTargetBehaviour_TargetLost(ImageTargetController obj)
    {
       Log(string.Format("Target lost, time {0}, is active: {1}", startFromTime, gameObject.activeInHierarchy));
        VideoPlayer?.Pause();
        meshRenderer.enabled = false;
    }

    private void ImageTargetBehaviour_TargetFound(ImageTargetController obj)
    {
        Log("Target found, start time = " + startFromTime.ToString());
        VideoPlayer.time = startFromTime;
        VideoPlayer.Play();

        if (VideoPlayer.isPrepared)
        {
            meshRenderer.enabled = true;
        }
    }

    private void Log(string message)
    {
        if (debug)
            Debug.Log(message);
    }
}
