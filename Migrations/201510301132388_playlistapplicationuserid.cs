namespace myimportantproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playlistapplicationuserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Playlists", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Playlists", "ApplicationUserID");
            AddForeignKey("dbo.Playlists", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Playlists", new[] { "ApplicationUserID" });
            DropColumn("dbo.Playlists", "ApplicationUserID");
        }
    }
}
