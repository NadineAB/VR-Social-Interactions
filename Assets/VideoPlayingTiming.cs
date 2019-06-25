/* Video Timing Class
// Nadine Abu Rumman, UCL 2019 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayingTiming : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer video;
    public float startTime=0;
    public float endTime = 0;
    public static double currentTimePlaying =0;
    public bool videoIsPlaying = false;
    
    void Start()
    {
        startTime = Time.time;
        gameObject.GetComponent<VideoPlayer>().EnableAudioTrack(0, false);
        videoIsPlaying = true;
        video.loopPointReached += CheckOver;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<VideoPlayer>().EnableAudioTrack(0, false);
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
  
        videoIsPlaying = false;
        currentTimePlaying = Time.time ;
        endTime = (float)currentTimePlaying;
    }
}
