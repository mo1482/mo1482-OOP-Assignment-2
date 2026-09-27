using SrpLab;

Console.WriteLine("SrpLab — refactored SRP design");
Console.WriteLine("==============================");

// 1. WardBoard
var ward = new WardBoard();
var acuityScorer = new AcuityScorer();
var pagerPolicy = new PagerPolicy();
var pagerLog = new PagerLog();
var handoffFormatter = new HandoffFormatter();

var acuity = acuityScorer.Score(130, 89);
ward.AssignBed(1, "p-88", acuity);

if (pagerPolicy.RequiresYellowCode(acuity))
    pagerLog.Add(pagerPolicy.BuildMessage(1, DateTime.UtcNow));

ward.TryGetBed(1, out var patient, out var score);
Console.WriteLine(handoffFormatter.Format(1, patient, score, DateTime.UtcNow));
Console.WriteLine(string.Join(" | ", pagerLog.Drain()));

// 2. CheckoutBasket
var basket = new CheckoutBasket();
basket.AddLine("SKU-1", 40m, 2);
basket.ApplyCouponText("SAVE10");
basket.EnableGiftWrap();

var couponParser = new CouponParser();
var pricing = new BasketPricing();
var total = pricing.GrandTotal(basket, couponParser);
var payment = new PaymentAuthorizer();

Console.WriteLine($"basket total={total} auth={payment.Authorize(basket, total, "4242")}");

// 3. SupportTicket
var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
var priorityClassifier = new TicketPriorityClassifier();
var sla = new SlaCalculator();
var priority = priorityClassifier.Classify(ticket);
var deadline = sla.Deadline(ticket, priority);
var reply = new SupportReplyFormatter();

Console.WriteLine(reply.Draft(ticket, priority, deadline, "Nora"));

// 4. LoanDesk responsibilities split
var loan = new LoanApplication(60_000m, 640, 4, false);
var riskCalculator = new LoanRiskCalculator();
var documentPolicy = new LoanDocumentPolicy();
var risk = riskCalculator.Calculate(loan);
var eligible = riskCalculator.IsEligible(loan);
var documents = documentPolicy.Required(loan, eligible);
var loanFormatter = new LoanDecisionFormatter();

Console.WriteLine(loanFormatter.DecisionLetter("Omar", loan, risk, eligible, documents));

// 5. CourseEnrollmentDesk
var course = new CourseEnrollmentDesk("SEF-101", 1, 3000m);
Console.WriteLine(course.Register("a@mail.com"));
Console.WriteLine(course.Register("b@mail.com"));

var welcome = new WelcomePacketFormatter();
Console.WriteLine(welcome.Format(course, "b@mail.com", "Bea"));

// 6. KitchenTicket
var kitchen = new KitchenTicket();
kitchen.AddItem("Pasta", new[] { "wheat", "milk" }, 12);

var allergens = new AllergenDetector();
var eta = new KitchenEtaCalculator(allergens);
var thermal = new ThermalTicketFormatter(allergens, eta);

Console.WriteLine(thermal.Render(kitchen, 42));

// 7. SubscriptionBilling
var sub = new SubscriptionBilling("c-9", 99m,
    new DateOnly(2026, 9, 1),
    new DateOnly(2026, 10, 1));

sub.RegisterFailedPayment();

var proration = new ProrationCalculator();
var invoiceNumbers = new InvoiceNumberGenerator();
var invoice = invoiceNumbers.Next(sub.PeriodStart);
var amount = proration.Calculate(sub, sub.PeriodStart);
var dunning = new DunningEmailFormatter();

Console.WriteLine(dunning.Format(sub, "Sara", new DateOnly(2026, 9, 20), amount, invoice));

// 8. WarehousePickList
var pick = new WarehousePickList();
pick.AddNeed("BOLT", "A", 3, 10, 7);
pick.AddNeed("NUT", "B", 1, 5, 5);

var allocator = new StockAllocator();
var planner = new WalkingOrderPlanner();
var picker = new PickerScriptFormatter(allocator, planner);

Console.WriteLine(picker.Format(pick));

// 9. GradeBook
var grades = new GradeBook();
grades.Record("s1", 92);
grades.Record("s1", 88);

var gradeCalculator = new GradeCalculator();
var gradePolicy = new GradePolicy();
var transcript = new TranscriptFormatter(gradeCalculator, gradePolicy);

Console.WriteLine(transcript.Plain(grades, "s1", "Ali"));

// 10. AppointmentDesk
var appt = new AppointmentDesk(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
var scheduler = new AppointmentScheduler(appt);

var slot = scheduler.FindNextSlot(
    DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);

if (slot is null)
    throw new InvalidOperationException("no slot");

scheduler.TryBook(slot.Value);

var sms = new SmsReminderFormatter();
Console.WriteLine(sms.Format(slot.Value, "0100"));

Console.WriteLine("Done. Responsibilities are now separated.");
