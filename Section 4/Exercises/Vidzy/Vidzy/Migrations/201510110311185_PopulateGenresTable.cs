namespace Vidzy.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO Genres (Id, Name)
VALUES 
	(1, 'Comedy'), 
	(2, 'Action'), 
	(3, 'Horror'), 
	(4, 'Thriller'), 
	(5, 'Family'), 
	(6, 'Romance')
");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id BETWEEN 1 AND 6");
        }
    }
}
