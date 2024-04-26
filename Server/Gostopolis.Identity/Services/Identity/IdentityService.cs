namespace Gostopolis.Identity.Services.Identity;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Gostopolis.Services;
using Gostopolis.Services.Emails;
using Gostopolis.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Models.Identity;
using static Constants.Email;

public class IdentityService(
        UserManager<User> userManager,
        ITokenGeneratorService jwtTokenGenerator,
        ICurrentUserService currentUser,
        IEmailService emailService)
    : IIdentityService
{
    private const string InvalidErrorMessage = "Грешен имейл или парола.";

    public async Task<Result<User>> Register(UserInputModel userInput)
    {
        var user = new User
        {
            FirstName = userInput.FirstName,
            LastName = userInput.LastName,
            UserName = userInput.Email,
            Email = userInput.Email,
            PhoneNumber = userInput.PhoneNumber
        };

        var identityResult = await userManager.CreateAsync(user, userInput.Password);

        if (identityResult.Succeeded)
        {
            await this.EmailVerification(user.Id);
        }

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result<User>.SuccessWith(user)
            : Result<User>.Failure(errors);
    }

    public async Task<Result<UserOutputModel>> Login(UserInputModel userInput)
    {
        var user = await userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
        {
            return "Не съществува потребител с този имейл.";
        }

        var emailConfirmed = await userManager.IsEmailConfirmedAsync(user);
        if (!emailConfirmed)
        {
            return "Имейлът Ви не е потвърден.";
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var roles = await userManager.GetRolesAsync(user);

        var token = jwtTokenGenerator.GenerateToken(user, roles);

        if (user.LoginNotification)
        {
            await emailService.Send(NewLogInTitle, NewLogInContent, user.Email);
        }

        return new UserOutputModel(token, user.Id);
    }

    public async Task<Result> EmailVerification(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await emailService.Send(EmailVerificationTitle, $"To confirm your email, click on the link below.\n\n\n https://gostopolis-noit.web.app/auth/confirm-email?id={user.Id}&token={token}", user.Email);
    }

    public async Task<Result<bool>> ConfirmEmail(UserOutputModel input)
    {
        var user = await userManager.FindByIdAsync(input.UserId);

        var result = await userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input.Token)));

        return result.Succeeded;
    }

    public async Task<Result<bool>> ResetEmail(ResetEmailInputModel input)
    {
        var user = await userManager.FindByEmailAsync(input.NewEmail);
        if (user != null)
        {
            return "Потребител с този имейл вече съществува.";
        }

        user = await userManager.FindByIdAsync(currentUser.UserId);

        var token = await userManager.GenerateChangeEmailTokenAsync(user, input.NewEmail);

        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await emailService.Send(EmailVerificationTitle, $"To confirm your new email, click on the link below.\n\n\n https://gostopolis-noit.web.app/auth/change-email?id={user.Id}&email={input.NewEmail}&token={token}", user.Email);
    }

    public async Task<Result> ChangeEmail(ChangeEmailInputModel input)
    {
        var user = await userManager.FindByIdAsync(input.UserId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await userManager.ChangeEmailAsync(
            user,
            input.NewEmail,
            Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input.Token)));

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }

    public async Task<Result> ResetPassword(ResetPasswordInputModel changePasswordInput)
    {
        var user = await userManager.FindByEmailAsync(changePasswordInput.Email);
        if (user == null)
        {
            return "There is no such email in our application.";
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return await emailService.Send(ResetPasswordTitle, $"To change your password, click on the link below.\n\n\n https://gostopolis-noit.web.app/auth/change-password?id={user.Id}&token={token}", user.Email);
    }

    public async Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput)
    {
        var user = await userManager.FindByIdAsync(changePasswordInput.UserId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await userManager.ResetPasswordAsync(
            user,
            Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(changePasswordInput.Token)),
            changePasswordInput.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }
}