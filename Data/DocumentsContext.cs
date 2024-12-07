using documents_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace documents_web_api.Data
{
    public class DocumentsContext : DbContext
    {
        public DocumentsContext(DbContextOptions<DocumentsContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}
