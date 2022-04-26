using System;

namespace RadioApp.AudioBackend
{
	public interface IAudioBackend
	{
		bool IsInitialized();

		bool Play(Uri uri);
		bool Play();
		void Pause();
		bool Resume();
		void Stop();
		PlaybackState GetPlaybackState();

		bool IsMediaLoaded();

		(bool Success, IPlaybackSource Result) LoadMedia(Uri uri);

		void Dispose();

		event EventHandler PlaybackStop;
		event EventHandler PlaybackEnd;
	}

	public enum PlaybackState
	{
		Undefined = 0,
		Playing = 1,
		Paused = 2,
		Stopped = 3,
	}
}
