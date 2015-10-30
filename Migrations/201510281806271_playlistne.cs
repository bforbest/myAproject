namespace myimportantproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playlistne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "Video_VideoID", "dbo.Videos");
            DropForeignKey("dbo.Videos", "Playlist_PlaylistID", "dbo.Playlists");
            DropIndex("dbo.Videos", new[] { "Video_VideoID" });
            DropIndex("dbo.Videos", new[] { "Playlist_PlaylistID" });
            CreateTable(
                "dbo.PlaylistVideos",
                c => new
                    {
                        Playlist_PlaylistID = c.Int(nullable: false),
                        Video_VideoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Playlist_PlaylistID, t.Video_VideoID })
                .ForeignKey("dbo.Playlists", t => t.Playlist_PlaylistID, cascadeDelete: true)
                .ForeignKey("dbo.Videos", t => t.Video_VideoID, cascadeDelete: true)
                .Index(t => t.Playlist_PlaylistID)
                .Index(t => t.Video_VideoID);
            
            DropColumn("dbo.Videos", "Video_VideoID");
            DropColumn("dbo.Videos", "Playlist_PlaylistID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "Playlist_PlaylistID", c => c.Int());
            AddColumn("dbo.Videos", "Video_VideoID", c => c.Int());
            DropForeignKey("dbo.PlaylistVideos", "Video_VideoID", "dbo.Videos");
            DropForeignKey("dbo.PlaylistVideos", "Playlist_PlaylistID", "dbo.Playlists");
            DropIndex("dbo.PlaylistVideos", new[] { "Video_VideoID" });
            DropIndex("dbo.PlaylistVideos", new[] { "Playlist_PlaylistID" });
            DropTable("dbo.PlaylistVideos");
            CreateIndex("dbo.Videos", "Playlist_PlaylistID");
            CreateIndex("dbo.Videos", "Video_VideoID");
            AddForeignKey("dbo.Videos", "Playlist_PlaylistID", "dbo.Playlists", "PlaylistID");
            AddForeignKey("dbo.Videos", "Video_VideoID", "dbo.Videos", "VideoID");
        }
    }
}
