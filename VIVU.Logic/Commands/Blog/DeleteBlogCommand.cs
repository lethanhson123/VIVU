namespace VIVU.Logic.Commands;

public class DeleteBlogCommand : CommonAuditCommand, IRequest<CommonCommandResult>
{
    public int Id { get; set; } = 0;
}

