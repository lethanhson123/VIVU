namespace VIVU.Data.Contexts;
public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer();
    public virtual DbSet<Blog> Blogs => Set<Blog>();
    protected override void OnModelCreating(ModelBuilder builder)
    {                
    }
}

