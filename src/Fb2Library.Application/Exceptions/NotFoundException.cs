namespace Fb2Library.Application.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity '{entityName}' ID '{key}' not found", "NOT_FOUND")
        {
        }
    }
}
