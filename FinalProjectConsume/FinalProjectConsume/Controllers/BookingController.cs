using FinalProjectConsume.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using FinalProjectConsume.Models.Booking;
using FinalProjectConsume.Models.Tour;

namespace FinalProjectConsume.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7145")  
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateVM vm)
        {
            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7145/api/Booking/CreateBooking", content);

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Error");

            var resultJson = await response.Content.ReadAsStringAsync();
            var booking = JsonSerializer.Deserialize<Booking>(resultJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (booking.Tour == null)
            {
                var tourResponse = await _httpClient.GetAsync($"https://localhost:7145/api/Tour/{booking.TourId}");
                if (!tourResponse.IsSuccessStatusCode)
                    return RedirectToAction("Error");

                var tourJson = await tourResponse.Content.ReadAsStringAsync();
                booking.Tour = JsonSerializer.Deserialize<Tour>(tourJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            decimal totalPrice = (booking.Tour.Price * booking.AdultsCount) +
                                 (booking.Tour.Price * 0.5m * booking.ChildrenCount);

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
        {
            new Stripe.Checkout.SessionLineItemOptions
            {
                PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = totalPrice * 100,
                    Currency = "usd",
                    ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                    {
                        Name = booking.Tour.Name,
                    },
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/booking/success?bookingId={booking.Id}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/booking/error"
            };

            var service = new Stripe.Checkout.SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }



        public async Task<IActionResult> Success(string bookingId)
        {
            if (string.IsNullOrEmpty(bookingId))
                return RedirectToAction("Error");

            var response = await _httpClient.GetAsync($"https://localhost:7145/api/Booking/{bookingId}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Error");

            var bookingJson = await response.Content.ReadAsStringAsync();
            var booking = JsonSerializer.Deserialize<Booking>(bookingJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (booking == null)
                return RedirectToAction("Error");

            if (booking.Tour == null)
            {
                var tourResponse = await _httpClient.GetAsync($"https://localhost:7145/api/Tour/{booking.TourId}");
                if (!tourResponse.IsSuccessStatusCode)
                    return RedirectToAction("Error");

                var tourJson = await tourResponse.Content.ReadAsStringAsync();
                booking.Tour = JsonSerializer.Deserialize<Tour>(tourJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // Mail göndərmə
            try
            {
                var smtpHost = "smtp.gmail.com";
                var smtpPort = 587;
                var smtpUser = "fadilafk@code.edu.az";
                var smtpPass = "fqen ovmf kvou rvos"; // App Password

                var mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress(smtpUser);

                if (!string.IsNullOrEmpty(booking.UserEmail))
                {
                    mail.To.Add(booking.UserEmail);
                    mail.Subject = "Rezervasiyanız təsdiqləndi - Travel.az";
                    mail.Body = " "; // bir boşluq qoy ki, mail spam düşməsin
                    mail.IsBodyHtml = true;

                    using var smtp = new System.Net.Mail.SmtpClient(smtpHost, smtpPort)
                    {
                        Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
                        EnableSsl = true
                    };

                    await smtp.SendMailAsync(mail);
                    Console.WriteLine("✅ Email göndərildi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Mail xətası: " + ex.Message);
            }

            return View(booking);
        }

        public IActionResult Error()
        {
            return View();
        }
    }

}
