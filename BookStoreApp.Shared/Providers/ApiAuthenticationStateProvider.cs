using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using BookStoreApp.Shared.Static;
using BookStoreApp.Shared.DTO.Auth;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;

namespace BookStoreApp.Shared.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        private readonly ILocalStorageService _localStorage;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var notLoggedIn = await Task.FromResult(new AuthenticationState(_anonymous));
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>("accessToken");
                if (string.IsNullOrEmpty(savedToken)) return notLoggedIn;

                var getUserClaims = DecryptToken(savedToken);

                if (getUserClaims == null) return notLoggedIn;

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception ex)
            {
                return notLoggedIn;
            }
        }

        public async Task UpdateAuthenticationState(string? jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                await _localStorage.SetItemAsync("accessToken", jwtToken);
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await _localStorage.RemoveItemAsync("accessToken");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims customUserClaims)
        {
            if (customUserClaims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, customUserClaims.Name),
                    new(ClaimTypes.Email, customUserClaims.Email)
                }, "JwtAuth"));
        }

        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
            return new CustomUserClaims(name!.Value, email!.Value);
        }
    }
}
