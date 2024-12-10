using PrintService.Models;

namespace PrintService.Services;

/// <summary>
/// Recharge Service.
/// </summary>
public interface IRechargeService
{
    /// <summary>
    /// Update Recahrge History
    /// </summary>
    /// <param name="userId">Amount</param>
    /// <param name="method">Amount</param>
    /// <param name="amount">Amount</param>
    /// <returns>Success or Fail</returns>
    bool UpdateHistory(string method, decimal amount);

    /// <summary>
    /// Get Recharge History Of Student
    /// </summary>
    /// <param name="userId">Amount</param>
    /// <param name="method">Amount</param>
    /// <param name="amount">Amount</param>
    /// <returns>Success or Fail</returns>
    List<RechargeHistory> GetHistory();
}
