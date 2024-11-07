namespace Customer.BusinessLogic.Models
{
    public class Result<T>
    {
        public Result()
        {
        }

        public Result(T data)
        {
            Data = data;
        }

        public Result(string errorMessage)
        {
            AddError(errorMessage);
        }

        public Result(Exception exception)
        {
            AddError(exception.Message);
        }

        public bool IsSuccess { get { return string.IsNullOrEmpty(ErrorMessage); } }

        public T Data { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        public void AddError(Exception exc)
        {
            string errorMessage = exc.Message;
            if (exc.InnerException != null)
            {
                errorMessage += exc.InnerException.Message;
            }
            ErrorMessage = errorMessage;
        }

        public void AddError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
