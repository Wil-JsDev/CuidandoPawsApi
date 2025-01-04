using Asp.Versioning;
using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CuidandoPawsApi.Infrastructure.Api.Controllers.V1.Account
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : ControllerBase
    {
        private readonly ICreateAccount<RegisterResponse, RegisterRequest> _createAccount;
        private readonly IConfirmAccount _confirmAccount;
        private readonly IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest> _authenticateAccount;
        private readonly IDeleteAccount _deleteAccount;
        private readonly IForgotPassword<ForgotResponse, ForgotRequest> _forgotPassword;
        private readonly IGetAccountDetails<AccountDto> _getAccountDetails;
        private readonly ILogout _logout;
        private readonly IResetPassword<ResetPasswordResponse, ResetPasswordRequest> _resetPassword; 
        private readonly IUpdateAccountDetails<AccountDto, UpdateAccountDTo> _updateAccountDetails;
        private readonly IValidator<AuthenticateRequest> _authenticateValidator;
        private readonly IValidator<ForgotRequest> _forgotPasswordValidation;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IValidator<ResetPasswordRequest> _resetPasswordRequestValidator;

        public AccountController(ICreateAccount<RegisterResponse, RegisterRequest> createAccount, IConfirmAccount confirmAccount, IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest> authenticateAccount, 
            IDeleteAccount deleteAccount, IForgotPassword<ForgotResponse, ForgotRequest> forgotPassword, 
            IGetAccountDetails<AccountDto> getAccountDetails, ILogout logout, IResetPassword<ResetPasswordResponse, ResetPasswordRequest> resetPassword, 
            IUpdateAccountDetails<AccountDto,UpdateAccountDTo> updateAccountDetails, IValidator<AuthenticateRequest> authenticateValidator, IValidator<ForgotRequest> forgotPasswordValidation,
           IValidator<RegisterRequest> registerRequestValidator, IValidator<ResetPasswordRequest> resetPasswordRequestValidator)
        {
            _createAccount = createAccount;
            _confirmAccount = confirmAccount;
            _authenticateAccount = authenticateAccount;
            _deleteAccount = deleteAccount;
            _forgotPassword = forgotPassword;
            _getAccountDetails = getAccountDetails;
            _logout = logout;
            _resetPassword = resetPassword;
            _updateAccountDetails = updateAccountDetails;
            _authenticateValidator = authenticateValidator;
            _registerRequestValidator = registerRequestValidator;
            _forgotPasswordValidation = forgotPasswordValidation;
            _resetPasswordRequestValidator = resetPasswordRequestValidator;
        }

        [HttpPost("register-caregiver")]
        [Authorize(Roles = "Admin" )]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisteCaregiverAsync([FromBody] RegisterRequest resquest)
        {
            var resultValidation = await _registerRequestValidator.ValidateAsync(resquest);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var origin = Request.Headers["origin"];
           
            var result = await _createAccount.RegisterAccountAsync(resquest,origin,Roles.Caregiver);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("register-pet-owner")]
        [Authorize(Roles = "Admin")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistePetOwnerAsync([FromBody] RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];

            var result = await _createAccount.RegisterAccountAsync(resquest, origin, Roles.PetOwner);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisteAdminAsync([FromBody] RegisterRequest resquest)
        {
            var resultValidation = await _registerRequestValidator.ValidateAsync(resquest);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }
            var origin = Request.Headers["origin"];

            var result = await _createAccount.RegisterAdminAsync(resquest, origin);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("confirm-account")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _confirmAccount.ConfirmAccountAsync(userId, token);
            if (result.Success)
                return Ok(result.Data);

           return NotFound(result.ErrorMessage);
        }

        [HttpPost("authenticate")]
        [EnableRateLimiting("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request)
        {

            var result = await _authenticateValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var authenticateRequest = await _authenticateAccount.AuthenticateAsync(request);

            return authenticateRequest.StatusCode switch
            {
                404 => NotFound(ApiResponse<string>.ErrorResponse($"Email {request.Email} not found")),
                400 => BadRequest(ApiResponse<string>.ErrorResponse($"Account not confirmed for {request.Email}")),
                401 => Unauthorized(ApiResponse<string>.ErrorResponse($"Invalid credentials for {request.Email}")),
                _ => Ok(ApiResponse<AuthenticateResponse>.SuccessResponse(authenticateRequest))
            };
        }

        [HttpPost("forgot-password")]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotRequest request)
        {
            var resultValidation = await _forgotPasswordValidation.ValidateAsync(request);
            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var origin = Request.Headers["origin"];
            var result = await _forgotPassword.GetForgotPasswordAsync(request,origin);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("reset-password")]
        [Authorize]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            var result = await _resetPasswordRequestValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var passwordReset = await _resetPassword.ResetPasswordAsync(request);
            if (passwordReset.Success)
            {
                return Ok(passwordReset.Data);
            }
            return NotFound(passwordReset.ErrorMessage);
        }

        [HttpGet("{userId}")]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AccountDetailsAsync([FromRoute] string userId)
        {
             var result = await _getAccountDetails.GetAccountDetailsAsync(userId);
             if (result.Success)
                 return Ok(result.Data);

             return NotFound(result.ErrorMessage);
        }

        [HttpPost("logout")]
        [Authorize]
        [EnableRateLimiting("fixed")]
        public async Task LogoutAsync()
        {
            await _logout.LogOutAsync();
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAccountDetailsAsync([FromBody] UpdateAccountDTo accountDTo,[FromRoute] string userId)
        {
            var result = await _updateAccountDetails.UpdateAccountDetailsAsync(accountDTo,userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }
    }
}
