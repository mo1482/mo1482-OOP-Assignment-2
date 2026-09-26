# Task 1.1 — Responsibilities

## 1. WardBoard
Responsibilities found:
- Store the patient assigned to each bed and its acuity score.
- Calculate clinical acuity from heart rate and SpO2.
- Decide when a pager alert should be created.
- Format a nurse handoff note.
- Export the ward census as CSV.
- Drain/clear pager messages.

Why this is a problem:
Clinical calculation, ward state, alerting, presentation, and export have different reasons to change. A change to CSV format should not require changing clinical scoring logic.

## 2. CheckoutBasket
Responsibilities found:
- Store basket lines.
- Calculate subtotal and grand total.
- Parse coupon/marketing text.
- Apply gift-wrap pricing.
- Generate a customer-facing gift message.
- Simulate payment authorization.

Why this is a problem:
Pricing, coupon syntax, packaging policy, customer copy, and payment integration can all change independently.

## 3. SupportTicket
Responsibilities found:
- Store and update ticket data.
- Determine priority from ticket text.
- Calculate SLA deadlines and breaches.
- Generate public customer replies.
- Generate internal escalation text.

Why this is a problem:
Ticket state, keyword rules, SLA policy, and communication templates belong to different concerns.

## 4. LoanDesk
Responsibilities found:
- Store loan application data.
- Calculate risk.
- Decide eligibility.
- Build compliance document requirements.
- Generate applicant decision letters.
- Generate an underwriting CSV row.

Why this is a problem:
Risk policy, compliance rules, legal/customer wording, and export format can change separately.

## 5. CourseEnrollmentDesk
Responsibilities found:
- Store enrolled and waitlisted students.
- Register students.
- Find waitlist positions.
- Promote students from the waitlist.
- Generate welcome-packet Markdown.
- Generate tuition invoice lines.

Why this is a problem:
Enrollment rules, waitlist operations, marketing content, and finance formatting have separate reasons to change.

## 6. KitchenTicket
Responsibilities found:
- Store order items and ingredients.
- Detect allergens.
- Estimate preparation time.
- Decide the expo-lane hint.
- Render a thermal printer ticket.

Why this is a problem:
Regulatory allergen rules, kitchen scheduling, operational routing, and printer formatting are independent concerns.

## 7. SubscriptionBilling
Responsibilities found:
- Store subscription/billing state.
- Calculate prorated subscription charges.
- Generate invoice numbers.
- Track failed payments.
- Generate dunning email content.
- Generate accounting ledger output.

Why this is a problem:
Billing formulas, invoice numbering, collections communication, and accounting export have different reasons to change.

## 8. WarehousePickList
Responsibilities found:
- Store picking requirements.
- Allocate available stock.
- Calculate walking order.
- Generate picker instructions.
- Generate WMS XML.

Why this is a problem:
Inventory allocation, warehouse routing, human instructions, and integration format are separate concerns.

## 9. GradeBook
Responsibilities found:
- Store student scores.
- Calculate averages.
- Determine letter grades.
- Apply honor-roll rules.
- Generate transcript text.
- Export grades to CSV.

Why this is a problem:
Grade calculation, academic policy, registrar formatting, and CSV export can evolve independently.

## 10. AppointmentDesk
Responsibilities found:
- Store opening hours and bookings.
- Validate business hours.
- Search for available slots.
- Book appointments.
- Generate ICS calendar data.
- Generate SMS reminder text.

Why this is a problem:
Scheduling rules, booking state, calendar interoperability, and messaging templates are different responsibilities.
