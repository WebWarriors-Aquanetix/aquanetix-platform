using Microsoft.AspNetCore.Mvc;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Transform;

namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST;

[ApiController]
[Route("api/v1/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionQueryService queryService;
    private readonly ISubscriptionCommandService commandService;

    public SubscriptionsController(
        ISubscriptionQueryService queryService,
        ISubscriptionCommandService commandService)
    {
        this.queryService = queryService;
        this.commandService = commandService;
    }

    // GET /subscriptions/plans  — fixed catalog (no DB). Declared before "{id}".
    [HttpGet("plans")]
    public IActionResult GetPlans()
    {
        var plans = PlanCatalog.All
            .Select(PlanResourceFromDefinitionAssembler.ToResource);
        return Ok(plans);
    }

    // GET /subscriptions  — list all subscriptions.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscriptions = await queryService.Handle(new GetAllSubscriptionsQuery());
        var resources = subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subscription = await queryService.Handle(new GetSubscriptionByIdQuery(id));
        if (subscription is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResource(subscription));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionResource resource)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommand(resource);
        var subscription = await commandService.Handle(command);
        if (subscription is null) return BadRequest(new { message = "Invalid plan or data." });
        return Ok(SubscriptionResourceFromEntityAssembler.ToResource(subscription));
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelSubscription(int id)
    {
        var subscription = await commandService.Handle(new CancelSubscriptionCommand(id));
        if (subscription is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResource(subscription));
    }

    [HttpPut("{id}/renew")]
    public async Task<IActionResult> RenewSubscription(int id)
    {
        var subscription = await commandService.Handle(new RenewSubscriptionCommand(id));
        if (subscription is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResource(subscription));
    }

    // PUT /subscriptions/{id}/plan  — change the plan (validated against catalog).
    [HttpPut("{id}/plan")]
    public async Task<IActionResult> ChangePlan(int id, [FromBody] ChangePlanResource resource)
    {
        var subscription = await commandService.Handle(new ChangePlanCommand(id, resource.NewPlan));
        if (subscription is null)
            return BadRequest(new { message = "Subscription not found or invalid plan." });
        return Ok(SubscriptionResourceFromEntityAssembler.ToResource(subscription));
    }
}
