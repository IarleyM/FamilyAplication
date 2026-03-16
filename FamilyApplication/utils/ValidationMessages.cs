namespace FamilyApplication.utils
{
    public class ValidationMessages
    {
        public bool IsValidMember { get; set; }
        public string Message { get; set; }

        public ValidationMessages(bool IsValidMember, string Message)
        {
            this.IsValidMember = IsValidMember;
            this.Message = Message; 
        }
    }
}
