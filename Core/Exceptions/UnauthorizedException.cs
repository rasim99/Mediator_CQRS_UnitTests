
namespace Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public List<string> Errors { get; set; } = new List<string>();

        public UnauthorizedException(string error)
        {
            Errors.Add(error);
        }
    }
}
