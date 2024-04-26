namespace Gostopolis;

public class Constants
{
    public const int Zero = 0;
    public const string AdministratorRoleName = "Administrator";
    public const string PartnerRoleName = "Partner";
    public const string AdministratorAndPartner = "Administrator,Partner";
    public const string PartnersDescription = "API that provides logic that is used by our partners like applying to become a partner or creating and managing locations and properties.";
    public const string IdentityDescription = "API that allows user to create and manage their accounts.";

    public class SendGrid
    {
        public const string ApiKey = "SENDGRID_API_KEY";
        public const string SenderName = "Гостополис";
        public const string SenderEmail = "gostopolis.team@gmail.com";
    }

    public class Email
    {
        public const string NewLogInTitle = "Ново влизане";
        public const string NewLogInContent = "Искаме да ви уведомим, че наскоро беше забелязано ново влизане във вашия потребителски профил в нашата система.\r\n\r\nАко това влизане е извършено от вас, моля, пренебрегнете този имейл. В противен случай, моля, предприемете незабавни действия, за да защитите сигурността на вашия профил. Промяната на паролата на профила ви може да помогне да се уверите, че профилът ви е защитен.\r\n\r\nАко имате въпроси или нужда от допълнителна помощ, не се колебайте да се свържете с нас. Ние сме тук, за да ви помогнем.\r\n\r\nБлагодарим ви за вниманието и за доверието, което проявявате към нашата услуга.\r\n\r\nС уважение,\r\nЕкипът на Гостополис";
        public const string ResetPasswordTitle = "Смяна на парола";
        public const string EmailVerificationTitle = "Потвърждение на имейл";
        public const string EmailVerificationContent = "In order to log into your newly created account, you need to verify your email by clicking on the link you see below. \n\n\n";

        // Restaurants
        public const string PropertyCreatedTitle = "Успешно създаден обект";
        public const string PropertyCreatedContent = "Уважаеми г-н/г-жо,\r\n\r\nБлагодарим ви за създаването на нов обект в нашата платформа. Ние получихме вашата заявка и с удоволствие ви информираме, че обектът е успешно създаден в нашата система.\r\n\r\nВашият обект в момента се преглежда от нашия администраторски екип. Предстои да бъде извършен подробен анализ на предоставената информация, за да се уверим, че отговаря на нашите стандарти за качество и съответства нашите изисквания.\r\n\r\nСлед одобрение от администратор, ще получите потвърждение и вашият обект ще бъде маркиран като отворен в нашата платформа. Това ще ви позволи да започнете да приемате резервации и да бъдете видими за нашите потребители.\r\n\r\nБлагодарим ви за вашето търпение и разбиране.\r\n\r\nС уважение,\r\nЕкипът на Гостополис";
    }

    public class Error
    {
        public const string CommonError = "Възникна грешка. Моля опитайте по-късно.";
        public const string PropertyForbidden = "Нямате право да редактирате този обект.";
        public const string ImagesForbidden = "Нямате право да редактирате галерията на този обект.";
        public const string MenusForbidden = "Нямате право да редактирате менютата на този обект.";
        public const string ProductsForbidden = "Нямате право да редактирате продуктите на този обект.";
        public const string TablesForbidden = "Нямате право да редактирате масите на този обект.";
        public const string WorkingHoursForbidden = "Нямате право да задавате работното време на този обект.";
    }
}