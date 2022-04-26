using System.Collections.Generic;
using System.Linq;

namespace RadioApp.AudioBackend
{
    public class AudioBackendController
	{
		private readonly IList<IAudioBackend> backends;
		private readonly IAudioBackend defaultBackend;

		public AudioBackendController()
		{
			this.backends = new List<IAudioBackend>
			{
				new VlcAudioPlaybackBackend()
			};

			this.defaultBackend = this.backends.FirstOrDefault();
		}

		public IAudioBackend GetAudioBackend(int index)
		{
			return this.backends.Count > index ? this.backends[index] : null;

		}

		public IAudioBackend GetDefaultAudioBackend()
        {
			return this.defaultBackend;
        }
	}
}
