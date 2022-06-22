namespace VIVU.Data.InitDatabase;

public class ApplicationDatabaseInitializer
{
    public static Task InitializeAsync(AppDatabase context)
    {
        context.Database.EnsureCreated();
        return Task.CompletedTask;
    }
}
