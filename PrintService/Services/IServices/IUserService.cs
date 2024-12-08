namespace PrintService.Services;

/// <summary>
/// UserService.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Update User Balance Account
    /// </summary>
    /// <param name="amount">Amount</param>
    /// <returns>Success or Fail</returns>
    Task<bool> UpdateBalance(decimal amount);
}

