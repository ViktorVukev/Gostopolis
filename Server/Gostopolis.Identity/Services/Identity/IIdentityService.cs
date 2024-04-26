namespace Gostopolis.Identity.Services.Identity;

using Data.Models;
using Gostopolis.Services;
using Models.Identity;

public interface IIdentityService
{
    Task<Result<User>> Register(UserInputModel userInput);

    Task<Result> EmailVerification(string userId);

    Task<Result<bool>> ConfirmEmail(UserOutputModel input);

    Task<Result<bool>> ResetEmail(ResetEmailInputModel input);

    Task<Result> ChangeEmail(ChangeEmailInputModel input);

    Task<Result<UserOutputModel>> Login(UserInputModel userInput);

    Task<Result> ResetPassword(ResetPasswordInputModel resetPasswordInput);

    Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);
}