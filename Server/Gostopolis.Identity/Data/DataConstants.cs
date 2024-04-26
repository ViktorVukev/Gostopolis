namespace Gostopolis.Identity.Data;

public class DataConstants
{
    public class Identity
    { 
        public const string PhoneNumberRegularExpression = @"\+[0-9]*";

        public const int MinEmailLength = 3;
        public const int MaxEmailLength = 50;
    }
}