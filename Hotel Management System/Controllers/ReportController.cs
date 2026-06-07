using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Services;
using HotelManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly PdfService _pdfService;
        private readonly IBookingService _bookingService;
        private readonly IGuestService _guestService;
        private readonly IRoomService _roomService;
        private readonly IPaymentService _paymentService;
        private readonly IInvoiceService _invoiceService;
        private readonly IEmployeeService _employeeService;
        private readonly IHousekeepingService _housekeepingService;
        private readonly ICancellationService _cancellationService;
        private readonly IRoomServiceService _roomServiceService;

        public ReportController(PdfService pdfService, IBookingService bookingService,
            IGuestService guestService, IRoomService roomService,
            IPaymentService paymentService, IInvoiceService invoiceService,
            IEmployeeService employeeService, IHousekeepingService housekeepingService,
            ICancellationService cancellationService, IRoomServiceService roomServiceService)
        {
            _pdfService = pdfService;
            _bookingService = bookingService;
            _guestService = guestService;
            _roomService = roomService;
            _paymentService = paymentService;
            _invoiceService = invoiceService;
            _employeeService = employeeService;
            _housekeepingService = housekeepingService;
            _cancellationService = cancellationService;
            _roomServiceService = roomServiceService;
        }

        public IActionResult Index() => View();

        // Report 1 - All Bookings
        public async Task<IActionResult> AllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            string html = "<h1>All Bookings</h1><table border='1' width='100%'><tr><th>ID</th><th>Guest ID</th><th>Room ID</th><th>Check In</th><th>Check Out</th><th>Status</th><th>Total</th></tr>";
            foreach (var b in bookings)
                html += $"<tr><td>{b.BookingID}</td><td>{b.GuestID}</td><td>{b.RoomID}</td><td>{b.CheckIn:d}</td><td>{b.CheckOut:d}</td><td>{b.Status}</td><td>{b.TotalAmount}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllBookings.pdf");
        }

        // Report 2 - All Guests
        public async Task<IActionResult> AllGuests()
        {
            var guests = await _guestService.GetAllGuestsAsync();
            string html = "<h1>All Guests</h1><table border='1' width='100%'><tr><th>ID</th><th>Full Name</th><th>Email</th><th>Phone</th><th>CNIC</th></tr>";
            foreach (var g in guests)
                html += $"<tr><td>{g.GuestID}</td><td>{g.FullName}</td><td>{g.Email}</td><td>{g.Phone}</td><td>{g.CNIC}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllGuests.pdf");
        }

        // Report 3 - All Rooms
        public async Task<IActionResult> AllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            string html = "<h1>All Rooms</h1><table border='1' width='100%'><tr><th>ID</th><th>Room Number</th><th>Floor</th><th>Price/Night</th><th>Status</th></tr>";
            foreach (var r in rooms)
                html += $"<tr><td>{r.RoomID}</td><td>{r.RoomNumber}</td><td>{r.Floor}</td><td>{r.PricePerNight}</td><td>{r.Status}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllRooms.pdf");
        }

        // Report 4 - All Payments
        public async Task<IActionResult> AllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            string html = "<h1>All Payments</h1><table border='1' width='100%'><tr><th>ID</th><th>Booking ID</th><th>Amount</th><th>Method</th><th>Status</th><th>Date</th></tr>";
            foreach (var p in payments)
                html += $"<tr><td>{p.PaymentID}</td><td>{p.BookingID}</td><td>{p.Amount}</td><td>{p.Method}</td><td>{p.Status}</td><td>{p.PaymentDate:d}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllPayments.pdf");
        }

        // Report 5 - All Invoices
        public async Task<IActionResult> AllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            string html = "<h1>All Invoices</h1><table border='1' width='100%'><tr><th>ID</th><th>Booking ID</th><th>Total</th><th>Generated</th><th>Paid</th></tr>";
            foreach (var i in invoices)
                html += $"<tr><td>{i.InvoiceID}</td><td>{i.BookingID}</td><td>{i.TotalAmount}</td><td>{i.GeneratedDate:d}</td><td>{(i.PaidStatus ? "Yes" : "No")}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllInvoices.pdf");
        }

        // Report 6 - All Employees
        public async Task<IActionResult> AllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            string html = "<h1>All Employees</h1><table border='1' width='100%'><tr><th>ID</th><th>Full Name</th><th>Role</th><th>Phone</th><th>Salary</th><th>Hire Date</th></tr>";
            foreach (var e in employees)
                html += $"<tr><td>{e.EmployeeID}</td><td>{e.FullName}</td><td>{e.Role}</td><td>{e.Phone}</td><td>{e.Salary}</td><td>{e.HireDate:d}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllEmployees.pdf");
        }

        // Report 7 - Housekeeping Tasks
        public async Task<IActionResult> HousekeepingTasks()
        {
            var tasks = await _housekeepingService.GetAllTasksAsync();
            string html = "<h1>Housekeeping Tasks</h1><table border='1' width='100%'><tr><th>ID</th><th>Room ID</th><th>Employee ID</th><th>Scheduled</th><th>Status</th><th>Notes</th></tr>";
            foreach (var h in tasks)
                html += $"<tr><td>{h.TaskID}</td><td>{h.RoomID}</td><td>{h.EmployeeID}</td><td>{h.ScheduledDate:d}</td><td>{h.Status}</td><td>{h.Notes}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "HousekeepingTasks.pdf");
        }

        // Report 8 - All Cancellations
        public async Task<IActionResult> AllCancellations()
        {
            var cancellations = await _cancellationService.GetAllCancellationsAsync();
            string html = "<h1>All Cancellations</h1><table border='1' width='100%'><tr><th>ID</th><th>Booking ID</th><th>Reason</th><th>Refund</th><th>Date</th></tr>";
            foreach (var c in cancellations)
                html += $"<tr><td>{c.CancellationID}</td><td>{c.BookingID}</td><td>{c.Reason}</td><td>{c.RefundAmount}</td><td>{c.CancelDate:d}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AllCancellations.pdf");
        }

        // Report 9 - Room Services
        public async Task<IActionResult> RoomServices()
        {
            var services = await _roomServiceService.GetAllRoomServicesAsync();
            string html = "<h1>Room Services</h1><table border='1' width='100%'><tr><th>ID</th><th>Booking ID</th><th>Service Type</th><th>Quantity</th><th>Status</th><th>Requested</th></tr>";
            foreach (var s in services)
                html += $"<tr><td>{s.ServiceID}</td><td>{s.BookingID}</td><td>{s.ServiceTypeID}</td><td>{s.Quantity}</td><td>{s.Status}</td><td>{s.RequestTime:d}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "RoomServices.pdf");
        }

        // Report 10 - Available Rooms
        public async Task<IActionResult> AvailableRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            var available = rooms.Where(r => r.Status == "Available");
            string html = "<h1>Available Rooms</h1><table border='1' width='100%'><tr><th>ID</th><th>Room Number</th><th>Floor</th><th>Price/Night</th></tr>";
            foreach (var r in available)
                html += $"<tr><td>{r.RoomID}</td><td>{r.RoomNumber}</td><td>{r.Floor}</td><td>{r.PricePerNight}</td></tr>";
            html += "</table>";
            var pdf = _pdfService.GeneratePdf(html);
            return File(pdf, "application/pdf", "AvailableRooms.pdf");
        }
    }
}