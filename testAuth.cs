public class ScheduleShowingController : Controller
    {

        private readonly JKTRentalPropertiesContext _context;
        private readonly AppSettings _appSettings;

        public ScheduleShowingController(JKTRentalPropertiesContext context, IOptions<AppSettings> appSettings) {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: /< controller>/
        public IActionResult Index(Rental rental)
        {
            ScheduleShowingViewModel viewModel = new ScheduleShowingViewModel(rental);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule([Bind("FirstName,LastName,Email,FullName")] Showing showing) {
            //return Content("" + id);

            ScheduleViewModel viewModel = new ScheduleViewModel(showing.RentalID);

            Console.WriteLine($"MODEL STATE VALID: {ModelState.IsValid}");

            if (ModelState.IsValid) {
                _context.Add(showing);
                await _context.SaveChangesAsync();

                return Content("Your Showing has been Saved");
            }

            return new RedirectResult(viewModel.Auth.authUrl);
        }