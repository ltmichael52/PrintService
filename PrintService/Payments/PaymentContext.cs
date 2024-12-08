namespace PrintService.Payments;

using Services;

public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;
    private IConfiguration _configuration;

    public PaymentContext(IConfiguration configuration)
    {
        _paymentStrategy = new PayPalService(configuration);
        _configuration = configuration;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public async Task<string> ExecutePaymentUrl(PaymentRequest model, HttpContext httpContext)
    {
        return await _paymentStrategy.CreatePaymentAsync(model, httpContext);
    }

    public PaymentResponse ExecutePaymentCallback(IQueryCollection query)
    {
        return _paymentStrategy.PaymentExecute(query);
    }
}

