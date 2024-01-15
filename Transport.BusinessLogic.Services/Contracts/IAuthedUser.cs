namespace Transport.BusinessLogic.Services.Contracts;

public interface IAuthedUser
{
    /// <summary>
    /// The ID of the currently authenticated user
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// The email of the currently authenticated user
    /// </summary>
    string Email { get; }
}
