namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tranactionstableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NewBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        CustomerId = c.Int(nullable: false),
                        AccountId = c.String(maxLength: 128),
                        TransactionType = c.Int(nullable: false),
                        TimeOfTransaction = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "AccountId" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropTable("dbo.Transactions");
        }
    }
}
