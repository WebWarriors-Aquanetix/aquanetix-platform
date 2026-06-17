using Microsoft.AspNetCore.Mvc;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Transform;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetSubscriptionByIdQuery(id);

        var subscription =
            await queryService.Handle(query);

        if (subscription is null)
            return NotFound();

        var resource =
            SubscriptionResourceFromEntityAssembler
                .ToResource(subscription);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(
        CreateSubscriptionResource resource)
    {
        var command =
            CreateSubscriptionCommandFromResourceAssembler
                .ToCommand(resource);

        var subscription =
            await commandService.Handle(command);

        if (subscription is null)
            return BadRequest();

        var subscriptionResource =
            SubscriptionResourceFromEntityAssembler
                .ToResource(subscription);

        return Ok(subscriptionResource);
    }
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelSubscription(int id)
    {
        var command = new CancelSubscriptionCommand(id);

        var subscription =
            await commandService.Handle(command);

        if (subscription is null)
            return NotFound();

        var resource =
            SubscriptionResourceFromEntityAssembler
                .ToResource(subscription);

        return Ok(resource);
    }
    [HttpPut("{id}/renew")]
    public async Task<IActionResult> RenewSubscription(int id)
    {
        var command =
            new RenewSubscriptionCommand(id);

        var subscription =
            await commandService.Handle(command);

        if (subscription is null)
            return NotFound();

        var resource =
            SubscriptionResourceFromEntityAssembler
                .ToResource(subscription);

        return Ok(resource);
    }
}