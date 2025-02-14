using System;
using FFmpeg.AutoGen;

public static unsafe class FFmpegHelper
{
    public static void ConfigureFFmpeg()
    {
        // Set FFmpeg log level to error only
        ffmpeg.av_log_set_level(ffmpeg.AV_LOG_ERROR);

        // Set up FFmpeg format names
        ffmpeg.avformat_network_init();
    }
} 