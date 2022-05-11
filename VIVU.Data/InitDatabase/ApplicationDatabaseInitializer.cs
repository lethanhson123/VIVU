
namespace VIVU.Data.InitDatabase;

public class ApplicationDatabaseInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
        if (!context.Blogs.Any())
        {
            var initBlogs = new List<Blog>()
            {
                new Blog
                {                    
                    ParentId = null,
                    Name = "SOHU 001",
                    DescriptionHTML = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    ContentHTML = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    ImageFileName = "mat-ong-nguyen-chat.jpg",                                        
                },                 
            };

            foreach (Blog item in initBlogs)
            {
                item.SetCreateAudit("admin");
                item.Initialization();
            }

            await context.Blogs.AddRangeAsync(initBlogs);

            await context.SaveChangesAsync();
        }
    }
}
