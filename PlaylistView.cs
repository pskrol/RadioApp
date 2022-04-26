using Microsoft.Data.Sqlite;
using RadioApp.AudioBackend;
using RadioApp.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RadioApp.Forms
{
	public partial class PlaylistView : Form
	{
		private readonly IAudioBackend audioBackend;

		public PlaylistView(AudioBackendController audioBackendController)
		{
			this.InitializeComponent();

			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			this.playlistItemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			this.addLocalFilesButton.Click += this.OnAddFilesButton_Click;
			this.addPlaylistItemButton.Click += this.OnAddPlaylistItemButton_Click;
			this.removePlaylistItemButton.Click += this.OnRemovePlaylistItemButton_Click;
			this.playControlButton.Click += this.OnPlayButton_Click;
			this.stopControlButton.Click += this.OnStopButton_Click;

			this.LoadConfiguration();
			this.LoadPlaylist();

			this.audioBackend = audioBackendController.GetDefaultAudioBackend();
			this.audioBackend.PlaybackStop += this.AudioBackend_OnPlaybackStop;
			this.audioBackend.PlaybackEnd += this.AudioBackend_OnPlaybackStop;

			if (this.continuePlaybackOnStartupCheckBox.Checked)
			{
				this.InitializePlaybackOnStartup();
			}
		}

		private void LoadConfiguration()
		{
			using var connection = new SqliteConnection($"Data Source={ProgramPaths.DatabaseFilepath}");

			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

			connection.Open();

			CreateDatabase(connection);

			var settings = FetchSettingsFromDatabase(connection);

			var defaultUrl = "http://wdr-wdr2-rheinruhr.icecast.wdr.de/wdr/wdr2/rheinruhr/mp3/128/stream.mp3";

			this.continuePlaybackOnStartupCheckBox.Checked = settings?.ContinuePlaybackAfterStartup ?? false;
			this.radioUrlTextBox.Text = string.IsNullOrWhiteSpace(settings?.LastTypedUrl) ? defaultUrl : settings.LastTypedUrl;

			static void CreateDatabase(SqliteConnection connection)
            {
				using var command = connection.CreateCommand();

				command.CommandText = @"
					CREATE TABLE IF NOT EXISTS ProgramSettings (
						bContinuePlaybackAfterStartup BIT,
						cLastTypedUrl NVARCHAR(35)
					);

					INSERT INTO ProgramSettings (bContinuePlaybackAfterStartup, cLastTypedUrl)
					VALUES (false, '');

					CREATE TABLE IF NOT EXISTS PlaylistItem (
						dIndex INTEGER PRIMARY_KEY AUTO_INCREMENT,
						cUrl STRING,
						bIsSelected BIT
					);";

				command.ExecuteNonQuery();
			}

			static DbProgramSettings FetchSettingsFromDatabase(SqliteConnection connection)
            {
				using var command = connection.CreateCommand();

				command.CommandText = @"
					SELECT bContinuePlaybackAfterStartup, cLastTypedUrl
					FROM ProgramSettings";

				using var reader = command.ExecuteReader();

				if (reader.Read())
                {
					return new DbProgramSettings
					{
						ContinuePlaybackAfterStartup = reader.GetFieldValue<bool>(0),
						LastTypedUrl = reader.GetFieldValue<string>(1)
					};
                }

				return null;
            }
		}

		private void LoadPlaylist()
		{
			using var connection = new SqliteConnection($"Data Source={ProgramPaths.DatabaseFilepath}");

			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

			connection.Open();

			var playlistItems = FetchPlaylistItemsFromDatabase(connection);

			foreach (var playlistItem in playlistItems)
            {
				var index = this.AddPlaylistItemRow(playlistItem.Url);

				if (playlistItem.IsSelected)
                {
					var selectedRow = this.playlistItemsDataGridView.Rows[index];

					selectedRow.Selected = true;
					this.playlistItemsDataGridView.CurrentCell = selectedRow.Cells[0];
				}
            }

			static IEnumerable<DbPlaylistItem> FetchPlaylistItemsFromDatabase(SqliteConnection connection)
			{
				using var command = connection.CreateCommand();

				command.CommandText = @"
					SELECT dIndex, cUrl, bIsSelected
					FROM PlaylistItem";

				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
                    yield return new DbPlaylistItem
                    {
                        Url = reader.GetFieldValue<string>(1),
                        IsSelected = reader.GetFieldValue<bool>(2)
                    };
				}
			}
		}

		private void SaveConfiguration()
		{
			using var connection = new SqliteConnection($"Data Source={ProgramPaths.DatabaseFilepath}");

			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

			connection.Open();

			using var command = connection.CreateCommand();

			command.Parameters.AddWithValue("@ContinueAfterStartup", this.continuePlaybackOnStartupCheckBox.Checked);
			command.Parameters.AddWithValue("@LastTypedUrl", this.radioUrlTextBox.Text);

			command.CommandText = @$"
				UPDATE ProgramSettings
				SET
					bContinuePlaybackAfterStartup = @ContinueAfterStartup,
					cLastTypedUrl = @LastTypedUrl";

			command.ExecuteNonQuery();
		}

		private void SavePlaylist()
		{
			if (this.playlistItemsDataGridView.Rows.Count == 0)
            {
				return;
            }

			using var connection = new SqliteConnection($"Data Source={ProgramPaths.DatabaseFilepath}");

			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

			connection.Open();

			var commandTextBuilder = new StringBuilder(@"
				DELETE FROM PlaylistItem;

				INSERT INTO PlaylistItem
					(cUrl, bIsSelected)
				VALUES ");

			var isFirst = true;

			foreach (var item in this.playlistItemsDataGridView.Rows)
            {
				var row = item as DataGridViewRow;
				var trackCell = GetTrackCellByRow(row);

				commandTextBuilder.Append($"{(isFirst ? "" : ", ")} ('{trackCell.Value}', {row.Selected})");

				isFirst = false;
            }

			using var command = connection.CreateCommand();
			command.CommandText = commandTextBuilder.ToString();

			command.ExecuteNonQuery();
		}

		private void InitializePlaybackOnStartup()
		{
			if (this.playlistItemsDataGridView.SelectedRows.Count > 0)
			{
				var trackTitle = Convert.ToString(GetTrackCellByRow(this.playlistItemsDataGridView.SelectedRows[0])?.Value);

				if (string.IsNullOrWhiteSpace(trackTitle))
				{
					return;
				}

				var playlistItemUri = new Uri(trackTitle);

				if (this.audioBackend.Play(playlistItemUri))
				{
					this.playingTrackTitleLabel.Text = playlistItemUri.OriginalString;
				}
			}
		}

		private int AddPlaylistItemRow(string value)
		{
			var trackCell = new DataGridViewTextBoxCell
			{
				Value = value
			};

			var row = new DataGridViewRow();

			row.Cells.Add(trackCell);

			return this.playlistItemsDataGridView.Rows.Add(row);
		}

		static DataGridViewCell GetTrackCellByRow(DataGridViewRow row)
		{
			if (row == null || row.Cells.Count == 0)
			{
				return null;
			}

			return row.Cells[0];
		}

		private void AudioBackend_OnPlaybackStop(object sender, EventArgs e)
		{
			if (this.playingTrackTitleLabel.InvokeRequired)
            {
				this.playingTrackTitleLabel.Invoke(() =>
				{
					this.playingTrackTitleLabel.Text = string.Empty;
				});

				return;
			}

			this.playingTrackTitleLabel.Text = string.Empty;
		}

		public void OnPlayButton_Click(object sender, EventArgs e)
		{
			if (this.playlistItemsDataGridView.Rows.Count == 0)
			{
				MessageBox.Show("Please add radio stations or files to the playlist");
				return;
			}

            if (this.playlistItemsDataGridView.SelectedRows.Count != 1)
			{
				this.playlistItemsDataGridView.Rows[0].Selected = true;
			}

			var trackTitle = Convert.ToString(GetTrackCellByRow(this.playlistItemsDataGridView.SelectedRows[0])?.Value);

			if (string.IsNullOrWhiteSpace(trackTitle))
            {
				return;
            }

			var playlistItemUri = new Uri(trackTitle);

			if (this.audioBackend.Play(playlistItemUri))
            {
				this.playingTrackTitleLabel.Text = playlistItemUri.OriginalString;
            }
		}

		public void OnStopButton_Click(object sender, EventArgs e)
		{
			this.audioBackend.Stop();
		}
		
		public void OnAddPlaylistItemButton_Click(object sender, EventArgs e)
		{
			var urlText = this.radioUrlTextBox.Text;

			if (!string.IsNullOrWhiteSpace(urlText) && Uri.IsWellFormedUriString(urlText, UriKind.Absolute))
			{
				this.AddPlaylistItemRow(urlText);
			}
		}

		public void OnRemovePlaylistItemButton_Click(object sender, EventArgs e)
		{
			foreach (var row in this.playlistItemsDataGridView.SelectedRows)
            {
				this.playlistItemsDataGridView.Rows.Remove(row as DataGridViewRow);
			}
		}

		public void OnAddFilesButton_Click(object sender, EventArgs e)
		{
			using var dialog = new OpenFileDialog()
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
				Multiselect = true,
				Filter = "Audio files (*.mp3;*.ogg;*.flac;*.aac;*.opus;*.wav)|*.mp3;*.ogg;*.flac;*.aac;*.opus;*.wav"
			};

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var filename in dialog.FileNames)
                {
					this.AddPlaylistItemRow(filename);
                }
            }
        }

		public void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveConfiguration();
			this.SavePlaylist();
			this.Dispose();	
		}
    }
}
