using System;
using System.Collections.Generic;

namespace MyTravelApp.Web.Models
{
    // =========================================================================
    // DashboardViewModel
    // -------------------------------------------------------------------------
    // This is the strongly-typed model the Dashboard view (@model directive)
    // binds to. Every property here corresponds to something the .cshtml file
    // reads with @Model.PropertyName — nothing extra, nothing missing.
    //
    // In a real app your controller would build this from your database
    // (Entity Framework, a repository, an API call, etc.) instead of the
    // static SampleData() method below. SampleData() exists purely so this
    // project compiles and shows a fully populated dashboard the first time
    // you run it, before you've wired up real data access.
    // =========================================================================
    public class DashboardViewModel
    {
        // ---- Identity / greeting -------------------------------------------
        public string UserName { get; set; } = string.Empty;

        // ---- Quick statistics cards -----------------------------------------
        public int UpcomingTripsCount { get; set; }
        public int CompletedTripsCount { get; set; }
        public int WishlistCount { get; set; }
        public decimal TotalSpentThisYear { get; set; }

        // ---- Notifications bell badge ---------------------------------------
        public int UnreadNotificationCount { get; set; }

        // ---- Hero "weather at a glance" chip --------------------------------
        public WeatherSummary? HomeWeather { get; set; }

        // ---- Upcoming bookings (boarding-pass cards) ------------------------
        public List<BookingViewModel> Bookings { get; set; } = new();

        // ---- Recommended destinations carousel ------------------------------
        public List<DestinationViewModel> RecommendedDestinations { get; set; } = new();

        // ---- Notifications / recent activity feed ---------------------------
        public List<ActivityViewModel> Activities { get; set; } = new();

        // ---- Chart data (Monthly Spend line chart) --------------------------
        // These back the Chart.js example mentioned in the view's script
        // comments: @Html.Raw(Json.Serialize(Model.SpendLabels)) etc.
        // Wire these up in the view if/when you replace the hard-coded
        // sample arrays in the <script> block with real server data.
        public List<string> SpendLabels { get; set; } = new();
        public List<decimal> SpendThisYear { get; set; } = new();
        public List<decimal> SpendLastYear { get; set; } = new();

        // ---------------------------------------------------------------
        // SampleData()
        // Quick way to see the dashboard fully populated. Call this from
        // your controller action until real data access is in place:
        //
        //   public IActionResult Index()
        //   {
        //       var model = DashboardViewModel.SampleData("Sarah");
        //       return View(model);
        //   }
        // ---------------------------------------------------------------
        public static DashboardViewModel SampleData(string userName = "Traveler")
        {
            return new DashboardViewModel
            {
                UserName = userName,
                UpcomingTripsCount = 3,
                CompletedTripsCount = 12,
                WishlistCount = 8,
                TotalSpentThisYear = 4820m,
                UnreadNotificationCount = 2,

                HomeWeather = new WeatherSummary
                {
                    City = "Delhi",
                    Condition = "Clear skies",
                    TemperatureC = 34,
                    Icon = "bi-brightness-high"
                },

                Bookings = new List<BookingViewModel>
                {
                    new BookingViewModel
                    {
                        Destination = "Santorini, Greece",
                        ImageUrl = "https://images.unsplash.com/photo-1512100356356-de1b84283e18?auto=format&fit=crop&w=200&q=60",
                        CheckIn = new DateTime(2026, 9, 12),
                        CheckOut = new DateTime(2026, 9, 18),
                        Status = "Confirmed",
                        ConfirmationCode = "TG-4821"
                    },
                    new BookingViewModel
                    {
                        Destination = "Kyoto, Japan",
                        ImageUrl = "https://images.unsplash.com/photo-1493246507139-91e8fad9978e?auto=format&fit=crop&w=200&q=60",
                        CheckIn = new DateTime(2026, 11, 3),
                        CheckOut = new DateTime(2026, 11, 10),
                        Status = "Pending",
                        ConfirmationCode = "TG-5190"
                    },
                    new BookingViewModel
                    {
                        Destination = "Reykjavik, Iceland",
                        ImageUrl = "https://images.unsplash.com/photo-1504829857797-ddff29c27927?auto=format&fit=crop&w=200&q=60",
                        CheckIn = new DateTime(2027, 1, 20),
                        CheckOut = new DateTime(2027, 1, 26),
                        Status = "Confirmed",
                        ConfirmationCode = "TG-6034"
                    }
                },

                RecommendedDestinations = new List<DestinationViewModel>
                {
                    new DestinationViewModel
                    {
                        Id = 101,
                        Name = "Bali",
                        Country = "Indonesia",
                        ImageUrl = "https://images.unsplash.com/photo-1537996194471-e657df975ab4?auto=format&fit=crop&w=500&q=60",
                        PriceFrom = 640m,
                        Rating = 4.8,
                        ReviewCount = 1284,
                        IsSaved = true
                    },
                    new DestinationViewModel
                    {
                        Id = 102,
                        Name = "Lisbon",
                        Country = "Portugal",
                        ImageUrl = "https://images.unsplash.com/photo-1585208798174-6cedd86e019a?auto=format&fit=crop&w=500&q=60",
                        PriceFrom = 410m,
                        Rating = 4.6,
                        ReviewCount = 902,
                        IsSaved = false
                    },
                    new DestinationViewModel
                    {
                        Id = 103,
                        Name = "Queenstown",
                        Country = "New Zealand",
                        ImageUrl = "https://images.unsplash.com/photo-1589871173980-5c353c9f0f8f?auto=format&fit=crop&w=500&q=60",
                        PriceFrom = 890m,
                        Rating = 4.9,
                        ReviewCount = 611,
                        IsSaved = false
                    },
                    new DestinationViewModel
                    {
                        Id = 104,
                        Name = "Marrakech",
                        Country = "Morocco",
                        ImageUrl = "https://images.unsplash.com/photo-1489749798305-4fea3ae63d43?auto=format&fit=crop&w=500&q=60",
                        PriceFrom = 380m,
                        Rating = 4.5,
                        ReviewCount = 745,
                        IsSaved = false
                    }
                },

                Activities = new List<ActivityViewModel>
                {
                    new ActivityViewModel
                    {
                        Message = "Price drop: your Santorini flight is now $85 cheaper.",
                        SubText = "Fare tracked since Jul 14",
                        Icon = "bi-graph-down-arrow",
                        IconClass = "price",
                        TimeAgo = "2h ago",
                        IsUnread = true
                    },
                    new ActivityViewModel
                    {
                        Message = "Reminder: Kyoto trip check-in opens in 48 hours.",
                        SubText = null,
                        Icon = "bi-alarm",
                        IconClass = "reminder",
                        TimeAgo = "5h ago",
                        IsUnread = true
                    },
                    new ActivityViewModel
                    {
                        Message = "Your payment for booking TG-4821 was processed.",
                        SubText = "Card ending 4432",
                        Icon = "bi-credit-card",
                        IconClass = "system",
                        TimeAgo = "1d ago",
                        IsUnread = false
                    },
                    new ActivityViewModel
                    {
                        Message = "3 friends liked your Bali wishlist item.",
                        SubText = null,
                        Icon = "bi-people",
                        IconClass = "social",
                        TimeAgo = "2d ago",
                        IsUnread = false
                    }
                },

                SpendLabels = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug" },
                SpendThisYear = new List<decimal> { 120, 190, 90, 340, 280, 500, 410, 460 },
                SpendLastYear = new List<decimal> { 80, 130, 120, 210, 190, 300, 260, 300 }
            };
        }
    }

    // =========================================================================
    // WeatherSummary
    // Backs the small weather chip next to the hero greeting
    // (Model.HomeWeather.City / .Condition / .TemperatureC / .Icon).
    // "Icon" is expected to be a Bootstrap Icons class name, e.g. "bi-cloud".
    // =========================================================================
    public class WeatherSummary
    {
        public string City { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public int TemperatureC { get; set; }
        public string Icon { get; set; } = "bi-cloud-sun";
    }

    // =========================================================================
    // BookingViewModel
    // One "boarding pass" card in the Upcoming Bookings section.
    // =========================================================================
    public class BookingViewModel
    {
        public string Destination { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        // Expected values used by the view's conditional CSS class:
        // "Confirmed" -> teal "upcoming" badge, anything else -> sand "pending" badge.
        public string Status { get; set; } = "Pending";

        public string ConfirmationCode { get; set; } = string.Empty;
    }

    // =========================================================================
    // DestinationViewModel
    // One card in the Recommended Destinations carousel.
    // =========================================================================
    public class DestinationViewModel
    {
        // Used to build the "View Details" link via asp-route-id.
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal PriceFrom { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }

        // Controls whether the heart/save button renders filled on load.
        public bool IsSaved { get; set; }
    }

    // =========================================================================
    // ActivityViewModel
    // One row in the notifications dropdown AND the Recent Activity feed
    // section — both parts of the view render from the same Activities list.
    // =========================================================================
    public class ActivityViewModel
    {
        public string Message { get; set; } = string.Empty;

        // Optional secondary line shown under the message; the view only
        // renders this <div> when SubText is not null/empty.
        public string? SubText { get; set; }

        // Bootstrap Icons class, e.g. "bi-graph-down-arrow".
        public string Icon { get; set; } = "bi-bell";

        // One of: "price", "reminder", "system", "social" — matches the
        // .activity-icon.<price|reminder|system|social> CSS variants
        // defined in the view for tinted icon backgrounds.
        public string IconClass { get; set; } = "system";

        // Pre-formatted relative time string, e.g. "2h ago", "1d ago".
        // Kept as a plain string here for simplicity; a fuller implementation
        // could store a real DateTime and format it with a helper/extension.
        public string TimeAgo { get; set; } = string.Empty;

        public bool IsUnread { get; set; }
    }
}
