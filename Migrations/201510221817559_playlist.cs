namespace myimportantproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Video_VideoID", c => c.Int());
            CreateIndex("dbo.Videos", "Video_VideoID");
            AddForeignKey("dbo.Videos", "Video_VideoID", "dbo.Videos", "VideoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Video_VideoID", "dbo.Videos");
            DropIndex("dbo.Videos", new[] { "Video_VideoID" });
            DropColumn("dbo.Videos", "Video_VideoID");
        }
    }
}
