namespace SmartHome.DomainCore.Data.Validations
{
    public class SmartHomeValidation
    {
        public SmartHomeValidation(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; }
        public string Message { get; }
    }
}