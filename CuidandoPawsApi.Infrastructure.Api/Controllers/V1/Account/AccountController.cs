using Asp.Versioning;
using CuidandoPawsApi.Application.Common;
using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public AccountController(ICreateAccount<RegisterResponse, RegisterRequest> createAccount, IConfirmAccount confirmAccount, IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest> authenticateAccount, 
            IDeleteAccount deleteAccount, IForgotPassword<ForgotResponse, ForgotRequest> forgotPassword, 
            IGetAccountDetails<AccountDto> getAccountDetails, ILogout logout, IResetPassword<ResetPasswordResponse, ResetPasswordRequest> resetPassword, 
            IUpdateAccountDetails<AccountDto, UpdateAccountDTo> updateAccountDetails)
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
        }


        [HttpPost("register-caregiver")]
        public async Task<IActionResult> RegisteCaregiverAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];
           
            var registerRequest = await _createAccount.RegisterAccountAsync(resquest,origin,Roles.Caregiver);
            if (registerRequest.StatusCode == 400)
            {
                return BadRequest(registerRequest);
            }
            else if (registerRequest.StatusCode == 500)
            {
                return StatusCode(500, registerRequest);
            }

            return Ok(registerRequest);
        }

        [HttpPost("register-pet-owner")]
        public async Task<IActionResult> RegistePetOwnerAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];

            var registerRequest = await _createAccount.RegisterAccountAsync(resquest, origin, Roles.PetOwner);

            if (registerRequest.StatusCode == 400)
            {
                return BadRequest(registerRequest);
            }
            else if (registerRequest.StatusCode == 500)
            {
                return StatusCode(500, registerRequest);
            }

            return Ok(registerRequest);
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisteAdminAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];

            var registerRequest = await _createAccount.RegisterAdminAsync(resquest, origin);

            if (registerRequest.StatusCode == 400)
            {
                return BadRequest(registerRequest);
            }
            else if (registerRequest.StatusCode == 500)
            {
                return StatusCode(500, registerRequest);
            }

            return Ok(registerRequest);
        }

        [HttpGet("confirm-account")]
        public async Task<IActionResult> ConfirmAccountAsync([FromQuery] string userId, [FromQuery] string token)
        {
            var userAccount = await _getAccountDetails.GetAccountDetailsAsync(userId);
            if (userAccount == null)
                return NotFound(ApiResponse<string>.ErrorResponse($"No account registered with this {userId} user"));
             
           var user = await _confirmAccount.ConfirmAccountAsync(userId,token);
           return Ok(ApiResponse<string>.SuccessResponse(user));

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest request)
        {
            var authenticateRequest = await _authenticateAccount.AuthenticateAsync(request);

            if (authenticateRequest.StatusCode == 404)
            {
              return NotFound(ApiResponse<string>.ErrorResponse($"this {request.Email} email not found "));

            }else if (authenticateRequest.StatusCode == 400)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse($"Account no confirmed for {request.Email}"));
            }
            else if (authenticateRequest.StatusCode == 401)
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse($"Invalid credentials for {request.Email}"));
            }

            return Ok(ApiResponse<AuthenticateResponse>.SuccessResponse(authenticateRequest));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotRequest request)
        {
            var origin = Request.Headers["origin"];
            var forgotRequest = await _forgotPassword.GetForgotPasswordAsync(request,origin);
            if (forgotRequest == null)
            {
                return NotFound(forgotRequest);
            }
            return Ok(forgotRequest);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var passwordReset = await _resetPassword.ResetPasswordAsync(request);
            if (passwordReset == null)
            {
                return BadRequest(passwordReset);
            }

            return Ok(passwordReset);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> AccountDetailsAsync([FromRoute] string userId)
        {
            var user = await _getAccountDetails.GetAccountDetailsAsync(userId);
            if (user != null)
             return Ok(ApiResponse<AccountDto>.SuccessResponse(user));
             
             return NotFound(ApiResponse<string>.ErrorResponse($"this {userId} account not found"));
        }

        [HttpPost("logout")]
        public async Task LogoutAsync()
        {
            await _logout.LogOutAsync();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAccountDetailsAsync(UpdateAccountDTo accountDTo,[FromRoute] string userId)
        {
            var user = await _getAccountDetails.GetAccountDetailsAsync(userId);
            if (user == null)
            {
                return NotFound(ApiResponse<string>.ErrorResponse($"this {userId} account not found"));
            }

            var userUpdate = await _updateAccountDetails.UpdateAccountDetailsAsync(accountDTo,userId);
            return Ok(ApiResponse<AccountDto>.SuccessResponse(userUpdate));
        }
    }
}
