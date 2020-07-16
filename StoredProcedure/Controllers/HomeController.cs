using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoredProcedure.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
            db.Books.ToList();
            //Book kitap = new Book()
            //{
            //    name = "LOTR",
            //    Description = "Fantastik",
            //    PublishedDate = DateTime.Now
            //};
            //db.Books.Add(kitap);
            //db.SaveChanges();
            //db.ExecuteFakeData();
            //var result = db.ExecuteGetBooksGroupByPublishedDateSP(1999, 2010);
            //var result2 = db.GetBookInfos();
            //var result3 = db.GetBookInfos(4);
            return View();
        }
    }
    public class DatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author>Authors { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext,StoredProcedure.Migrations.Configuration>());
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>()
        //        .MapToStoredProcedures(config =>
        //        {
        //            config.Insert(i => i.HasName("BookInsertSP"));
        //            config.Update(u =>
        //            {
        //                u.HasName("BookUpdateSP");
        //                u.Parameter(p => p.ID, "BookdID");
        //            });
        //            config.Delete(d => d.HasName("BookDeleteSP"));

        //        });
        //}

        //public void ExecuteFakeData()
        //{
        //    Database.ExecuteSqlCommand("EXEC InsertFakeDataSP");
        //}
        //public List<BookGroupByPublishedDate> ExecuteGetBooksGroupByPublishedDateSP(int startyear, int endyear)
        //{
        //    return
        //    Database.SqlQuery<BookGroupByPublishedDate>(
        //        "EXEC GetBooksGroupByPublishedDateSP @p0,@p1",
        //        startyear, endyear)
        //    .ToList();

        //}

        //public List<BookInfo> GetBookInfos()
        //{
        //    return Database.SqlQuery<BookInfo>("SELECT * FROM GetBooksInfoVW").ToList();
        //}
        //public List<BookInfo> GetBookInfos(int minID)
        //{
        //    return Database.SqlQuery<BookInfo>("SELECT * FROM GetBooksInfoVW WHERE Id>@p0", minID).ToList();
        //}
    }


    //public class BookGroupByPublishedDate
    //{
    //    public int PublishedDate { get; set; }
    //    public int Count { get; set; }
    //}
    public class DnInitializer: CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Database.ExecuteSqlCommand(
                @"CREATE PROCEDURE InsertFakeDataSP
                  AS
                  BEGIN
                    INSERT INTO [dbo].[Books] ([Name],[Description],[PublishedDate]) VALUES ('Da Vinci Code','Da Vinci Şifresi','2003-02-01' )
                    INSERT INTO [dbo].[Books] ([Name],[Description],[PublishedDate]) VALUES ('Angels & Demons','Melekler Ve Şeytanlar','2003-02-01' )
                    INSERT INTO [dbo].[Books] ([Name],[Description],[PublishedDate]) VALUES ('Lost Symbol','Kayıp Sembol','2005-02-01' )
                 END");
            context.Database.ExecuteSqlCommand(
                @"CREATE PROCEDURE GetBooksGroupByPublishedDateSP
                    @p0 int,
                    @p1 int
                AS
                BEGIN
                    SELECT
                        TBL.PublishedDate,
                        COUNT(TBL.PublishedDate) AS [Count]
                    FROM(
                             SELECT
                                YEAR(PublishedDate) AS PublishedDate
                             FROM Books
                             WHERE YEAR (PublishedDate) BETWEEN @p0 AND @p1
                             )AS TBL
                    GROUP BY TBL.PublishedDate
                    END");
            context.Database.ExecuteSqlCommand(
                @"CREATE VIEW [dbo].[GetBooksInfoVW]
                    AS
                    SELECT
                        Id, Name+' : '+Description+' ( '+ CONVERT (nvarchar(20), PublishedDate) +' ) ' AS Info"
);
        }
    }
    [Table("Books")]
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(50)]
        public string name { get; set; }
        public string Description { get; set; }
        [Required,StringLength(50)]
        public string Description2 { get; set; }
        public int Description3 { get; set; }
        public string Description4 { get; set; }
        public DateTime PublishedDate { get; set; }
    }
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(50)]
        public string name { get; set; }
        [Required, StringLength(50)]
        public string surname { get; set; }
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
    }
    //public class BookInfo
    //{
    //    public int Id { get; set; }
    //    public string Info { get; set; }
    //}
}