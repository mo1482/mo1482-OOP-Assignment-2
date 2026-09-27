namespace SrpLab;

public sealed class SupportReplyFormatter
{
    public string Draft(SupportTicket ticket, string priority, DateTimeOffset deadline, string agentName)
    {
        var apology = priority == "P1"
            ? "We are treating this as a critical incident."
            : "Thanks for reaching out.";

        return $"Hi,\n{apology}\nTicket {ticket.Id} is with {agentName}. Next update before {deadline:u}.\n";
    }

    public string Escalation(SupportTicket ticket, string priority, DateTimeOffset deadline) =>
        $"ESCALATE {ticket.Id} priority={priority} breachAt={deadline:u} keywords-scanned=yes";
}
