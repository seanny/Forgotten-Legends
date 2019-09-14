using System.IO;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Video;

namespace Core.Utility
{
    public static class VideoUtility
    {
        public static bool isPlaying { get; private set; }
        
        /// <summary>
        /// Play's the specified video inside of videoPath which is relevant to StreamingAssets/Videos.
        /// Example #1: PlayVideo("Intro.mov") -> StreamingAssets/Videos/Intro.mov
        /// Example #2: PlayVideo("TestFolder/Intro.mov") -> StreamingAssets/Videos/TestFolder/Intro.mov
        /// </summary>
        /// <param name="videoPath"></param>
        public static void PlayVideo(string videoPath)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "Videos", videoPath);
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Video {videoPath} does not exist in {Path.Combine(Application.streamingAssetsPath, "Videos")}");
                return;
            }
            
            GameObject camera = UnityEngine.Camera.main.gameObject;
            var videoPlayer = camera.AddComponent<VideoPlayer>();

            videoPlayer.playOnAwake = false;
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCameraAlpha = 1.0f;
            videoPlayer.url = filePath;
            
            videoPlayer.Play();
        }
    }
}