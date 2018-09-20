public class SendRentalAggreement : Controller
    {

        private readonly JKTRentalPropertiesContext _context;
        private readonly AppSettings _appSettings;

        public SendRentalAggreement(JKTRentalPropertiesContext context, IOptions<AppSettings> appSettings) {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: /< controller>/
        public IActionResult Index(Rental rental)
        {
            SendRentalContract viewModel = new SendRentalAggreement(rental);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send([Bind("FirstName,LastName,Email,FullName")] SendRentalAggreement rental) {
            //return Content("" + id);

            ScheduleViewModel viewModel = new ScheduleViewModel(showing.RentalID);

            if (ModelState.IsValid) {
                _context.Add(rental);
                await _context.SaveChangesAsync();

                return Content("Your Showing has been Saved");
            }

            return new RedirectResult(viewModel.Auth.authUrl);
        }

        public IActionResult Confirm() {
            QueryString queryString = this.Request.QueryString;
            string qs = queryString.Value.Remove(0);
            string[] query = qs.Split("=");
            string authCode = query.GetValue(query.Length - 1).ToString();
            return View();
        }
    }