using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace PrintService.Services;

using Dtos;
using Payments;

public class MomoService : IPaymentStrategy
{
    private readonly IConfiguration _configuration;

    public MomoService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreatePaymentAsync(PaymentRequest studentDto, HttpContext context)
    {
        //var orderId = DateTime.UtcNow.Ticks.ToString();
        //var orderInfo = "Khách hàng: " + UserHelper.GetCurrentUserName() + ". Nội dung: " + UserHelper.GetCurrentUserId();

        var orderId = DateTime.UtcNow.Ticks.ToString();
        var orderInfo = "Khách hàng: " + "Nghia" + " Nội dung: " + "Nạp Tiền Tài Khoản";

        var rawData =
        $"partnerCode={_configuration.GetValue<string>("MomoAPI:PartnerCode")}&accessKey={_configuration.GetValue<string>("MomoAPI:AccessKey")}&requestId={orderId}&amount={studentDto.Amount}&orderId={orderId}&orderInfo={orderInfo}&returnUrl={_configuration.GetValue<string>("MomoAPI:ReturnUrl")}&notifyUrl={_configuration.GetValue<string>("MomoAPI:NotifyUrl")}&extraData=";

        var signature = ComputeHmacSha256(rawData, _configuration.GetValue<string>("MomoAPI:SecretKey"));

        var client = new RestClient(_configuration.GetValue<string>("MomoAPI:MomoApiUrl"));
        var request = new RestRequest() { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json; charset=UTF-8");


        // Create an object representing the request data
        var requestData = new
        {
            accessKey = _configuration.GetValue<string>("MomoAPI:AccessKey"),
            partnerCode = _configuration.GetValue<string>("MomoAPI:PartnerCode"),
            requestType = _configuration.GetValue<string>("MomoAPI:RequestType"),
            notifyUrl = _configuration.GetValue<string>("MomoAPI:NotifyUrl"),
            returnUrl = _configuration.GetValue<string>("MomoAPI:ReturnUrl"),
            orderId,
            amount = studentDto.Amount.ToString(),
            orderInfo,
            requestId = orderId,
            extraData = "",
            signature
        };

        request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

        var response = await client.ExecuteAsync(request);

        MomoCreatePaymentResponseModel momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);

        return momoResponse.PayUrl;
    }

    public PaymentResponse PaymentExecute(IQueryCollection collection)
    {
        var amount = collection.First(s => s.Key == "amount").Value;
        var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
        var StudentId = collection.First(s => s.Key == "StudentId").Value;
        return new PaymentResponse()
        {
            Amount = amount,
            OrderId = StudentId,
            OrderInfo = orderInfo
        };
    }

    private string ComputeHmacSha256(string message, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        byte[] hashBytes;

        using (var hmac = new HMACSHA256(keyBytes))
        {
            hashBytes = hmac.ComputeHash(messageBytes);
        }

        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        return hashString;
    }
}