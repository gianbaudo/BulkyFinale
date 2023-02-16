using BulkyBook.Models1;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data

{

    public class ApplicationDbContext : DbContext

    {

        //ApplicationDbContext riceverà nel costruttore le opzioni per potersi connettere al database

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //la property Categories permetterà di creare la tabella Categories in fase di Migration e di poter accedere ai dati in

        //essa contenuti

        public DbSet<Category> Categories { get; set; } = null!;

    }

}
