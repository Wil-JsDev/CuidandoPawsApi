﻿using Asp.Versioning;
using CuidandoPawsApi.Application.DTOs.Account;
using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.Password.Forgot;
using CuidandoPawsApi.Application.DTOs.Account.Password.Reset;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Utils;
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

        public AccountController(ICreateAccount<RegisterResponse, RegisterRequest> createAccount, IConfirmAccount confirmAccount, IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest> authenticateAccount, 
            IDeleteAccount deleteAccount, IForgotPassword<ForgotResponse, ForgotRequest> forgotPassword, 
            IGetAccountDetails<AccountDto> getAccountDetails, ILogout logout, IResetPassword<ResetPasswordResponse, ResetPasswordRequest> resetPassword, 
            IUpdateAccountDetails<AccountDto,UpdateAccountDTo> updateAccountDetails)
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
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisteCaregiverAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];
           
            var result = await _createAccount.RegisterAccountAsync(resquest,origin,Roles.Caregiver);
            if (result.Success)
                return Ok(result.Data); 

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("register-pet-owner")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistePetOwnerAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];

            var result = await _createAccount.RegisterAccountAsync(resquest, origin, Roles.PetOwner);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("register-admin")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisteAdminAsync(RegisterRequest resquest)
        {
            var origin = Request.Headers["origin"];

            var result = await _createAccount.RegisterAdminAsync(resquest, origin);
            if (result.Success)
                return Ok(result.Data);

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
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotRequest request)
        {
            var origin = Request.Headers["origin"];
            var result = await _forgotPassword.GetForgotPasswordAsync(request,origin);
            if (result.Success)
                return Ok(result.Data);
             
            return NotFound(result.ErrorMessage);
        }

        [HttpPost("reset-password")]
        [DisableRateLimiting]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var passwordReset = await _resetPassword.ResetPasswordAsync(request);
            if (passwordReset.Success)
                return Ok(passwordReset.Data);
           
            return NotFound(passwordReset.ErrorMessage);
        }

        [HttpGet("{userId}")]
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
        [EnableRateLimiting("fixed")]
        public async Task LogoutAsync()
        {
            await _logout.LogOutAsync();
        }

        [HttpPut("{userId}")]
        [EnableRateLimiting("fixed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAccountDetailsAsync(UpdateAccountDTo accountDTo,[FromRoute] string userId)
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
