using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrintService.Controllers;

using Enums;
using Extentions;
using Payments;
using Services;
using Validators.Payment;

public class RechargeController : Controller
{
    private readonly PaymentContext _paymentContext;
    private readonly PayPalService _payPalService;
    private readonly VnPayService _vnPayService;
    private readonly MomoService _momoService;
    private readonly IUserService _userService;

    public RechargeController(PaymentContext paymentContext,
                              PayPalService payPalService,
                              VnPayService vnPayService,
                              MomoService momoService,
                              IUserService userService)
    {
        _paymentContext = paymentContext;
        _payPalService = payPalService;
        _vnPayService = vnPayService;
        _momoService = momoService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Success()
    {
        try
        {
            decimal amount = decimal.Parse(Request.Query["amount"]);
            _userService.UpdateBalance(amount);
        }
        catch (Exception ex)
        {
        }
        return View();
    }

    public async Task<IActionResult> CreatePaymentUrl(PaymentRequest request)
    {

        var vr = new PaymentValidator().Validate(request);
        if (!vr.IsValid)
        {
            var errors = string.Join("; ", vr.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }

        var method = request.PaymentMethod.ToEnum(PaymentMethod.Momo);

        switch (method)
        {
            case PaymentMethod.Paypal:
                _paymentContext.SetPaymentStrategy(_payPalService);
                break;

            case PaymentMethod.VnPay:
                _paymentContext.SetPaymentStrategy(_vnPayService);
                break;

            case PaymentMethod.Momo:
                _paymentContext.SetPaymentStrategy(_momoService);
                break;

            default:
                _paymentContext.SetPaymentStrategy(_momoService);
                break;
        }

        var url = await _paymentContext.ExecutePaymentUrl(request, HttpContext);
        return Redirect(url);
    }

    public IActionResult PaymentCallback()
    {
        var paymentResponse = _paymentContext.ExecutePaymentCallback(Request.Query);
        return Json(paymentResponse);
    }
}
