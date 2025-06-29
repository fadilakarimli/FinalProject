using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

[Route("payment")]
public class PaymentController : Controller
{
    [HttpPost("create-checkout-session")]
    public IActionResult CreateCheckoutSession()
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 5000, 
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Test Ödəniş"
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:7014/payment/success",
            CancelUrl = "https://localhost:7014/payment/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Redirect(session.Url);
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
        return View();
    }

    [HttpGet("cancel")]
    public IActionResult Cancel()
    {
        return View();
    }
}
