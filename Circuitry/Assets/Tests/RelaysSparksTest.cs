using UnityEngine;
using System.Collections.Generic;
using Shouldly;
using Require;

public class RelaysSparksTest : TestBehaviour
{
    MockCircuitComponent emitter;
    RelaysSparks it;
    MockCircuitComponent receiver;

    public override void Spec()
    {
        Given("it relays sparks").And("an emitter is chained to it").And("it is chained to a receiver")
            .When("the emitter sparks")
            .Then("the receiver should be enabled");
    }

    public void ItRelaysSparks()
    {
        it = transform.Require<RelaysSparks>();
    }

    public void AnEmitterIsChainedToIt()
    {
        emitter = new GameObject().transform.Require<MockCircuitComponent>();
        emitter.next = new List<CircuitComponent>();
        emitter.next.Add(it);
    }

    public void ItIsChainedToAReceiver()
    {
        receiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.next = new List<CircuitComponent>();
        it.next.Add(receiver);
    }

    public void TheEmitterSparks()
    {
        emitter.Spark(emitter.next);
    }

    public void TheReceiverShouldBeEnabled()
    {
        receiver.enabled.ShouldBe(true);
    }
}
