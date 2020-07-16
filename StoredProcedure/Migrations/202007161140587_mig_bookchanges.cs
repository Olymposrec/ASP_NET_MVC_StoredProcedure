namespace StoredProcedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_bookchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Description4", c => c.String(nullable:false,defaultValue:"test",defaultValueSql:"test_"));
            AlterColumn("dbo.Books", "Description3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description3", c => c.Int());
            DropColumn("dbo.Books", "Description4");
        }
    }
}
