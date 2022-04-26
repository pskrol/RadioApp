namespace RadioApp.Forms
{
	partial class PlaylistView
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button playControlButton;
		private System.Windows.Forms.Button stopControlButton;
		private System.Windows.Forms.TextBox radioUrlTextBox;
		private System.Windows.Forms.Button removePlaylistItemButton;
		private System.Windows.Forms.Button addPlaylistItemButton;
		private System.Windows.Forms.Label playingTrackTitleLabel;
		private System.Windows.Forms.CheckBox continuePlaybackOnStartupCheckBox;
		private System.Windows.Forms.Button addLocalFilesButton;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.playControlButton = new System.Windows.Forms.Button();
            this.stopControlButton = new System.Windows.Forms.Button();
            this.radioUrlTextBox = new System.Windows.Forms.TextBox();
            this.removePlaylistItemButton = new System.Windows.Forms.Button();
            this.addPlaylistItemButton = new System.Windows.Forms.Button();
            this.playingTrackTitleLabel = new System.Windows.Forms.Label();
            this.continuePlaybackOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.addLocalFilesButton = new System.Windows.Forms.Button();
            this.radioUrlLabel = new System.Windows.Forms.Label();
            this.playlistItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.playlistItemsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // playControlButton
            // 
            this.playControlButton.Location = new System.Drawing.Point(12, 15);
            this.playControlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playControlButton.Name = "playControlButton";
            this.playControlButton.Size = new System.Drawing.Size(72, 35);
            this.playControlButton.TabIndex = 0;
            this.playControlButton.Text = "Play";
            this.playControlButton.UseVisualStyleBackColor = true;
            // 
            // stopControlButton
            // 
            this.stopControlButton.Location = new System.Drawing.Point(92, 15);
            this.stopControlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stopControlButton.Name = "stopControlButton";
            this.stopControlButton.Size = new System.Drawing.Size(72, 35);
            this.stopControlButton.TabIndex = 1;
            this.stopControlButton.Text = "Stop";
            this.stopControlButton.UseVisualStyleBackColor = true;
            // 
            // radioUrlTextBox
            // 
            this.radioUrlTextBox.Location = new System.Drawing.Point(13, 92);
            this.radioUrlTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioUrlTextBox.Name = "radioUrlTextBox";
            this.radioUrlTextBox.Size = new System.Drawing.Size(773, 27);
            this.radioUrlTextBox.TabIndex = 3;
            // 
            // removePlaylistItemButton
            // 
            this.removePlaylistItemButton.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.removePlaylistItemButton.Location = new System.Drawing.Point(849, 89);
            this.removePlaylistItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removePlaylistItemButton.Name = "removePlaylistItemButton";
            this.removePlaylistItemButton.Size = new System.Drawing.Size(45, 35);
            this.removePlaylistItemButton.TabIndex = 6;
            this.removePlaylistItemButton.Text = "-";
            this.removePlaylistItemButton.UseVisualStyleBackColor = true;
            // 
            // addPlaylistItemButton
            // 
            this.addPlaylistItemButton.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addPlaylistItemButton.Location = new System.Drawing.Point(794, 89);
            this.addPlaylistItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addPlaylistItemButton.Name = "addPlaylistItemButton";
            this.addPlaylistItemButton.Size = new System.Drawing.Size(47, 35);
            this.addPlaylistItemButton.TabIndex = 7;
            this.addPlaylistItemButton.Text = "+";
            this.addPlaylistItemButton.UseVisualStyleBackColor = true;
            // 
            // playingTrackTitleLabel
            // 
            this.playingTrackTitleLabel.Location = new System.Drawing.Point(13, 505);
            this.playingTrackTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.playingTrackTitleLabel.Name = "playingTrackTitleLabel";
            this.playingTrackTitleLabel.Size = new System.Drawing.Size(865, 35);
            this.playingTrackTitleLabel.TabIndex = 12;
            this.playingTrackTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // continuePlaybackOnStartupCheckBox
            // 
            this.continuePlaybackOnStartupCheckBox.Location = new System.Drawing.Point(182, 14);
            this.continuePlaybackOnStartupCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.continuePlaybackOnStartupCheckBox.Name = "continuePlaybackOnStartupCheckBox";
            this.continuePlaybackOnStartupCheckBox.Size = new System.Drawing.Size(228, 38);
            this.continuePlaybackOnStartupCheckBox.TabIndex = 14;
            this.continuePlaybackOnStartupCheckBox.Text = "Continue playback on startup";
            this.continuePlaybackOnStartupCheckBox.UseVisualStyleBackColor = true;
            // 
            // addLocalFilesButton
            // 
            this.addLocalFilesButton.Location = new System.Drawing.Point(759, 15);
            this.addLocalFilesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addLocalFilesButton.Name = "addLocalFilesButton";
            this.addLocalFilesButton.Size = new System.Drawing.Size(135, 35);
            this.addLocalFilesButton.TabIndex = 15;
            this.addLocalFilesButton.Text = "Add local files ...";
            this.addLocalFilesButton.UseVisualStyleBackColor = true;
            // 
            // radioUrlLabel
            // 
            this.radioUrlLabel.AutoSize = true;
            this.radioUrlLabel.Location = new System.Drawing.Point(13, 67);
            this.radioUrlLabel.Name = "radioUrlLabel";
            this.radioUrlLabel.Size = new System.Drawing.Size(81, 20);
            this.radioUrlLabel.TabIndex = 16;
            this.radioUrlLabel.Text = "Radio URL:";
            // 
            // playlistItemsDataGridView
            // 
            this.playlistItemsDataGridView.AllowUserToAddRows = false;
            this.playlistItemsDataGridView.AllowUserToDeleteRows = false;
            this.playlistItemsDataGridView.AllowUserToResizeColumns = false;
            this.playlistItemsDataGridView.AllowUserToResizeRows = false;
            this.playlistItemsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.playlistItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playlistItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Track});
            this.playlistItemsDataGridView.Location = new System.Drawing.Point(13, 140);
            this.playlistItemsDataGridView.Name = "playlistItemsDataGridView";
            this.playlistItemsDataGridView.RowHeadersVisible = false;
            this.playlistItemsDataGridView.RowHeadersWidth = 51;
            this.playlistItemsDataGridView.RowTemplate.Height = 29;
            this.playlistItemsDataGridView.Size = new System.Drawing.Size(881, 344);
            this.playlistItemsDataGridView.TabIndex = 17;
            // 
            // Track
            // 
            this.Track.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Track.HeaderText = "Track";
            this.Track.MinimumWidth = 6;
            this.Track.Name = "Track";
            this.Track.ReadOnly = true;
            this.Track.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PlaylistView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(907, 549);
            this.Controls.Add(this.playlistItemsDataGridView);
            this.Controls.Add(this.radioUrlLabel);
            this.Controls.Add(this.addLocalFilesButton);
            this.Controls.Add(this.continuePlaybackOnStartupCheckBox);
            this.Controls.Add(this.playingTrackTitleLabel);
            this.Controls.Add(this.addPlaylistItemButton);
            this.Controls.Add(this.removePlaylistItemButton);
            this.Controls.Add(this.radioUrlTextBox);
            this.Controls.Add(this.stopControlButton);
            this.Controls.Add(this.playControlButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PlaylistView";
            this.Text = "RadioApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.playlistItemsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Label radioUrlLabel;
        private System.Windows.Forms.DataGridView playlistItemsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Track;
    }
}
