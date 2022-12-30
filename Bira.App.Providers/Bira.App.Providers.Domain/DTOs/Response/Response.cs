using System.Collections.ObjectModel;

namespace Bira.App.Providers.Domain.DTOs.Response
{
    public class Response
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; set; }

        public bool Success { get; set; }

        public object Result { get; set; }

        public Response() => Errors = new ReadOnlyCollection<string>(_messages);

        public Response(object result, bool success) : this()
        {
            Result = result;
            Success = success;
        }

        public Response(bool success) : this()
        {
            Success = success;
        }

        public Response AddError(string message)
        {
            _messages.Add(message);
            return this;
        }
    }
}
