using RadioApp.AudioBackend;
using RadioApp.Forms;
using System;
using System.Windows.Forms;

namespace RadioApp
{
    internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);

			new Audioplayer().Run();
		}
	}

	class Audioplayer
    {
		private readonly PlaylistView playlistView;
		private readonly AudioBackendController audioBackendController;

		public Audioplayer()
        {
			this.audioBackendController = new AudioBackendController();
			this.playlistView = new PlaylistView(audioBackendController);
		}

		public void Run()
        {
			Application.Run(this.playlistView);
		}
    }
}
