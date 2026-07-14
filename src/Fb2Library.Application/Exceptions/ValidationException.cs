using Microsoft.IdentityModel.Tokens.Experimental;

namespace Fb2Library.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationError> Errors { get; }

        public ValidationException(List<ValidationError> errors)
            : base("Validation error")
        {
            Errors = errors;
        }
    }
}
