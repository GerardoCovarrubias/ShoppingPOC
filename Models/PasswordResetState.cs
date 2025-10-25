namespace Models
{
    public class PasswordResetState
    {
        public string Email { get; set; }
        public string ValidationCode { get; set; }

    }
}
