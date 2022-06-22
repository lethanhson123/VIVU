using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VIVU.Data.Contexts;
public class AppDatabase : IdentityDbContext<User>
{
    public AppDatabase() { }
    public AppDatabase(DbContextOptions<AppDatabase> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer();
    
    /// <summary>
    /// User
    /// </summary>
    public new virtual DbSet<UserToken> UserTokens => Set<UserToken>();

    /// <summary>
    /// Post
    /// </summary>
    public virtual DbSet<Banner> Banners => Set<Banner>();
    public virtual DbSet<Blog> Blogs => Set<Blog>();
    public virtual DbSet<BlogRelated> PostRelateds => Set<BlogRelated>();
    public virtual DbSet<BlogTag> PostTags => Set<BlogTag>();
    public virtual DbSet<BlogCategory> PostCategories => Set<BlogCategory>();

    /// <summary>
    /// Category
    /// </summary>
    public virtual DbSet<Category> Categories => Set<Category>();
    public virtual DbSet<Tag> Tags => Set<Tag>();

    /// <summary>
    /// Ecommerece
    /// </summary>
    public virtual DbSet<Customer> Customers => Set<Customer>();
    public virtual DbSet<Product> Products => Set<Product>();
    public virtual DbSet<MarketLead> MarketLeads => Set<MarketLead>();
    public virtual DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public virtual DbSet<SalesOrderDetail> SalesOrderDetails => Set<SalesOrderDetail>();
    public virtual DbSet<ProductImage> ProductImages => Set<ProductImage>();


    protected override void OnModelCreating(ModelBuilder builder)
    {         
        base.OnModelCreating(builder);
        builder.Entity<Banner>().HasKey(p => p.Id);
        builder.Entity<Banner>().Property(x => x.Id).UseIdentityColumn();

        builder.Entity<Blog>().HasKey(p => p.Id);
        builder.Entity<Blog>().Property(x => x.Id).UseIdentityColumn();

        builder.Entity<BlogTag>().HasKey(p => p.Id);
        builder.Entity<BlogTag>().Property(x => x.Id).UseIdentityColumn();
        builder.Entity<BlogCategory>().HasKey(p => p.Id);
        builder.Entity<BlogCategory>().Property(x => x.Id).UseIdentityColumn();
        builder.Entity<BlogRelated>().HasKey(p => p.Id);
        builder.Entity<BlogRelated>().Property(x => x.Id).UseIdentityColumn();

        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(x => x.Id).UseIdentityColumn();
        builder.Entity<Tag>().HasKey(p => p.Id);
        builder.Entity<Tag>().Property(x => x.Id).UseIdentityColumn();

        builder.Entity<Customer>().HasKey(x => x.Id);

        builder.Entity<Product>().HasKey(x => x.Id);

        builder.Entity<MarketLead>().HasKey(x => x.Id);
        builder.Entity<SalesOrder>().HasKey(x => x.Id);
        builder.Entity<SalesOrderDetail>().HasKey(x => x.Id);
        builder.Entity<SalesOrderDetail>().Property(x => x.Id).UseIdentityColumn();  
        builder.Entity<ProductImage>().HasKey(x => x.Id);
        builder.Entity<ProductImage>().Property(x => x.Id).UseIdentityColumn();


    }
}

