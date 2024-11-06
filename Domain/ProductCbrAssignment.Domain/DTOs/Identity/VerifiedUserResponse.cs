using ProductCbrAssignment.Domain.Entities.Identity;
using System.Text.Json.Serialization;

namespace ProductCbrAssignment.Domain.DTOs.Identity
{
    public class VerifiedUserResponse
    {
        public string? Username { get; set; }
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }

        [JsonIgnore]
        public DateTime AccessTokenExpiration { get; set; }

        public VerifiedUserResponse() { }

        public VerifiedUserResponse( string jwtToken, string Username, bool isSuccess)
        {
            Username = Username;
            Token = jwtToken;
            IsSuccess = isSuccess;
        }
    }
}