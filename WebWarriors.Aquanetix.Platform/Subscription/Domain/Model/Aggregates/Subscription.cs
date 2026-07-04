namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates;

public class Subscription
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public string Plan { get; private set; } = string.Empty;

    public string Status { get; private set; } = string.Empty;

    protected Subscription() { }

    public Subscription(
        int userId,
        string plan,
        string status)
    {
        UserId = userId;
        Plan = plan;
        Status = status;
    }
    public void Cancel()
    {
        Status = "Cancelled";
    }
    public void Renew()
    {
        Status = "Active";
    }

    /// <summary>Changes the subscription's plan. Domain operation, like Cancel/Renew.</summary>
    public void ChangePlan(string newPlan)
    {
        Plan = newPlan;
    }
}
