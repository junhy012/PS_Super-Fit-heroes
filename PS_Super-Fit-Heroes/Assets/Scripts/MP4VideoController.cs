using UnityEngine;
using UnityEngine.Video;
using System.IO;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class MP4VideoController : MonoBehaviour
{
    [Header("File in StreamingAssets, e.g. Level1.mp4")]
    public string videoFileName = "Level1.mp4";
    public bool loop = false;
    public bool startMuted = false;

    private VideoPlayer vp;
    private AudioSource audioSrc;
    private bool prepared = false;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        audioSrc = GetComponent<AudioSource>();

        vp.playOnAwake = false;
        vp.isLooping = loop;
        vp.waitForFirstFrame = true;
        vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
        vp.SetTargetAudioSource(0, audioSrc);

        audioSrc.playOnAwake = false;
        audioSrc.mute = startMuted;

        vp.errorReceived += (player, msg) => Debug.LogError("[Video] " + msg);
        vp.prepareCompleted += (player) => { prepared = true; };
    }

    void Start()
    {
        string rawPath = Path.Combine(Application.streamingAssetsPath, videoFileName);
#if UNITY_ANDROID && !UNITY_EDITOR
        vp.source = VideoSource.Url;
        vp.url = rawPath; // Android jar path handled by Unity
#else
        vp.source = VideoSource.Url;
        vp.url = rawPath; // absolute OS path; works fine for Editor/Standalone
        if (!File.Exists(rawPath))
        {
            Debug.LogError("[Video] File not found at: " + rawPath);
        }
#endif
        vp.Prepare(); // prepare once so first play is instant
    }

    public bool IsPlaying => vp != null && vp.isPlaying;

    public void Play()
    {
        if (!prepared) { StartCoroutine(PlayWhenReady()); return; }
        vp.Play();
        if (!audioSrc.mute) audioSrc.Play();
    }

    public void Pause()
    {
        vp.Pause();
        audioSrc.Pause();
    }

    public void Stop()
    {
        vp.Stop();
        audioSrc.Stop();
    }

    // Called when the player leaves the zone: stop + scrub to start + re-prepare
    public void StopAndReset()
    {
        Stop();
        
        // Reset to first frame
        if (vp.canSetTime)
        {
            vp.time = 0;
        }
        else
        {
            vp.frame = 0;
        }
        prepared = false;
        vp.Prepare(); // so the next entry starts from the beginning instantly
    }

    private System.Collections.IEnumerator PlayWhenReady()
    {
        while (!prepared) yield return null;
        Play();
    }
}
