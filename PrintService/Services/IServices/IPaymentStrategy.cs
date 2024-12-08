namespace PrintService.Services;

using Payments;

/// <summary>
/// Defines payment strategy methods.
/// </summary>
public interface IPaymentStrategy
{
    /// <summary>
    /// Creates a payment URL or payment details.
    /// </summary>
    /// <param name="paymentInformationModel">Payment details.</param>
    /// <returns>Payment URL or information.</returns>
    Task<string> CreatePaymentAsync(PaymentRequest paymentInformationModel, HttpContext context);

    /// <summary>
    /// Executes the payment based on callback data.
    /// </summary>
    /// <param name="collections">Callback data from payment gateway.</param>
    /// <returns>Payment result.</returns>
    PaymentResponse PaymentExecute(IQueryCollection collections);
}

