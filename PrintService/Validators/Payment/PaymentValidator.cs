using FluentValidation;

namespace PrintService.Validators.Payment;

using Payments;

public class PaymentValidator : AbstractValidator<PaymentRequest>
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    public PaymentValidator()
    {
        var t = "Amount";
        RuleFor(p => p.Amount).NotEmpty().WithMessage($"{t} Not Engouh");

        t = "Payment Method";
        RuleFor(p => p.PaymentMethod)
           .NotEmpty().WithMessage($"{t} method is required")
           .Must(BeAValidPaymentMethod).WithMessage("Invalid payment method");
    }

    /// <summary>
    /// Check if the PaymentMethod value is valid
    /// </summary>
    /// <param name="method">Payment method</param>
    /// <returns>true if valid, otherwise false</returns>
    private bool BeAValidPaymentMethod(string? method)
    {
        if (string.IsNullOrEmpty(method))
        {
            return false;
        }
        return Enum.TryParse(typeof(Enums.PaymentMethod), method, true, out _);
    }

    #endregion
}
