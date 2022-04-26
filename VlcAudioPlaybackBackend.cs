using LibVLCSharp.Shared;
using RadioApp.Network;
using System;
using System.IO;

namespace RadioApp.AudioBackend
{
    public class VlcAudioPlaybackBackend : IAudioBackend
	{
		private readonly LibVLC libVlc;
		private readonly MediaPlayer mediaPlayer;

		public event EventHandler PlaybackStop;
		public event EventHandler PlaybackEnd;

		public VlcAudioPlaybackBackend()
		{
			LibVLCSharp.Shared.Core.Initialize();

			this.libVlc = new LibVLC();
			this.mediaPlayer = new MediaPlayer(this.libVlc);

            this.mediaPlayer.EndReached += MediaPlayer_OnEndReached;
            this.mediaPlayer.Stopped += MediaPlayer_OnStopped;
		}

        public void Dispose()
		{
			this.mediaPlayer?.Dispose();
			this.libVlc?.Dispose();
		}

		public bool IsInitialized()
		{
			return this.libVlc != null;
		}

		public bool Play(Uri uri)
		{
			var successfullyOpened = false;

			if (this.GetPlaybackState() is not PlaybackState.Playing)
			{
				var openFileResult = this.LoadMedia(uri);

				if (openFileResult.Success)
				{
					this.mediaPlayer.Media = openFileResult.Result.Source as Media;

					if (this.IsMediaLoaded())
					{
						successfullyOpened = this.mediaPlayer.Play();
					}
				}
			}

			return successfullyOpened;
		}

		public bool Play()
		{
			if (this.GetPlaybackState() is not PlaybackState.Playing && this.IsMediaLoaded())
			{
				return this.mediaPlayer.Play();
			}
			else
            {
				return false;
            }
		}

		public void Pause()
		{
			if (this.GetPlaybackState() is PlaybackState.Playing && this.IsMediaLoaded())
			{
				this.mediaPlayer.Pause();
			}
		}

		public bool Resume()
		{
			if (this.GetPlaybackState() is PlaybackState.Paused && this.IsMediaLoaded())
			{
				return this.mediaPlayer.Play();
			}
			else
            {
				return false;
            }
		}

		public void Stop()
		{
			var playbackMode = this.GetPlaybackState();

			if (playbackMode is PlaybackState.Playing || playbackMode is PlaybackState.Paused)
			{
				this.mediaPlayer.Stop();
				this.mediaPlayer.Media?.Dispose();
			}
		}

		public bool IsMediaLoaded()
		{
			return this.mediaPlayer?.Media != null;
		}

		public (bool Success, IPlaybackSource Result) LoadMedia(Uri uri)
		{
			if (File.Exists(uri.LocalPath) || NetworkUtils.IsRemoteAudioSource(uri.AbsoluteUri))
			{
				var playbackSource = new PlaybackSource
				{
					Source = new Media(this.libVlc, uri)
				};

				return (true, playbackSource);
			}

			return (false, null);
		}

		public PlaybackState GetPlaybackState()
		{
			if (!this.IsInitialized())
			{
				return PlaybackState.Undefined;
			}

			return this.mediaPlayer.State switch
			{
				VLCState.Playing => PlaybackState.Playing,
				VLCState.Paused => PlaybackState.Paused,
				VLCState.Stopped => PlaybackState.Stopped,
				_ => PlaybackState.Undefined
			};
		}

		private void MediaPlayer_OnStopped(object sender, EventArgs e)
		{
			this.PlaybackStop?.Invoke(this, e);
		}

		private void MediaPlayer_OnEndReached(object sender, EventArgs e)
		{
			this.PlaybackEnd?.Invoke(this, e);
		}
	}
}
