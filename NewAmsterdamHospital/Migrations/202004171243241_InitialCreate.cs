namespace NewAmsterdamHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appoitments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailUser = c.String(),
                        IdDoctorSpecialty = c.Int(nullable: false),
                        DateId = c.Int(nullable: false),
                        TimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataForCarousels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Class = c.String(),
                        TextHeading = c.String(),
                        Text = c.String(),
                        UrlPicture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataForContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        WorkDaysWeek = c.String(),
                        StartWorkTime = c.DateTime(nullable: false),
                        EndWorkTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataForNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextHeading = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DateReceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateName = c.String(),
                        Date = c.DateTime(nullable: false),
                        DoctorSpecialtyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorSpecialties", t => t.DoctorSpecialtyId, cascadeDelete: true)
                .Index(t => t.DoctorSpecialtyId);
            
            CreateTable(
                "dbo.DoctorSpecialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NumberRooms", t => t.NumberRoomId, cascadeDelete: true)
                .Index(t => t.NumberRoomId);
            
            CreateTable(
                "dbo.NumberRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        IsItUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeName = c.String(),
                        DateReceptionId = c.Int(nullable: false),
                        IsUse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DateReceptions", t => t.DateReceptionId, cascadeDelete: true)
                .Index(t => t.DateReceptionId);
            
            CreateTable(
                "dbo.MedicalRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorSpecialty = c.String(),
                        EmailCustomer = c.String(),
                        DateOfChange = c.DateTime(nullable: false),
                        Record = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        DoctorSpecialtyId = c.Int(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorSpecialties", t => t.DoctorSpecialtyId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.DoctorSpecialtyId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "DoctorSpecialtyId", "dbo.DoctorSpecialties");
            DropForeignKey("dbo.Receptions", "DateReceptionId", "dbo.DateReceptions");
            DropForeignKey("dbo.DoctorSpecialties", "NumberRoomId", "dbo.NumberRooms");
            DropForeignKey("dbo.DateReceptions", "DoctorSpecialtyId", "dbo.DoctorSpecialties");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "DoctorSpecialtyId" });
            DropIndex("dbo.Receptions", new[] { "DateReceptionId" });
            DropIndex("dbo.DoctorSpecialties", new[] { "NumberRoomId" });
            DropIndex("dbo.DateReceptions", new[] { "DoctorSpecialtyId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.MedicalRecords");
            DropTable("dbo.Receptions");
            DropTable("dbo.NumberRooms");
            DropTable("dbo.DoctorSpecialties");
            DropTable("dbo.DateReceptions");
            DropTable("dbo.DataForNews");
            DropTable("dbo.DataForContacts");
            DropTable("dbo.DataForCarousels");
            DropTable("dbo.Appoitments");
        }
    }
}
