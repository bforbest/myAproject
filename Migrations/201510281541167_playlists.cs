namespace myimportantproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playlists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PlaylistID);
            
            AddColumn("dbo.Videos", "Playlist_PlaylistID", c => c.Int());
            CreateIndex("dbo.Videos", "Playlist_PlaylistID");
            AddForeignKey("dbo.Videos", "Playlist_PlaylistID", "dbo.Playlists", "PlaylistID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Playlist_PlaylistID", "dbo.Playlists");
            DropIndex("dbo.Videos", new[] { "Playlist_PlaylistID" });
            DropColumn("dbo.Videos", "Playlist_PlaylistID");
            DropTable("dbo.Playlists");
        }
    }
}
