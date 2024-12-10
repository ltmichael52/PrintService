using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PrintService.Controllers;

using Enums;
using Extentions;
using Microsoft.AspNetCore.Http;
using Payments;
using PrintService.Models.Authentication;
using Services;
using Validators.Payment;
[AuthenticationM_S]
public class RechargeController : Controller
{
    private readonly PaymentContext _paymentContext;
    private readonly PayPalService _payPalService;
    private readonly VnPayService _vnPayService;
    private readonly MomoService _momoService;
    private readonly IUserService _userService;
    private readonly IRechargeService _rechargeService;

    public RechargeController(PaymentContext paymentContext,
                              PayPalService payPalService,
                              VnPayService vnPayService,
                              MomoService momoService,
                              IUserService userService,
                              IRechargeService rechargeService)
    {
        _paymentContext = paymentContext;
        _payPalService = payPalService;
        _vnPayService = vnPayService;
        _momoService = momoService;
        _userService = userService;
        _rechargeService = rechargeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult History()
    {
        return View(_rechargeService.GetHistory());
    }

    public IActionResult Success()
    {
        try
        {
            decimal amount = decimal.Parse(Request.Query["amount"]);
            string? paymentMethod = HttpContext.Session.GetString("PaymentMethod");

            _userService.UpdateBalance(amount);
            _rechargeService.UpdateHistory(paymentMethod, amount);

            TempData["SuccessMessage"] = "Recharge Success!";

            return RedirectToAction("Index", "UserInfo");
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
        HttpContext.Session.SetString("PaymentMethod", method.ToString());
        return Redirect(url);
    }

    public IActionResult PaymentCallback()
    {
        var paymentResponse = _paymentContext.ExecutePaymentCallback(Request.Query);
        return Json(paymentResponse);
    }
}
