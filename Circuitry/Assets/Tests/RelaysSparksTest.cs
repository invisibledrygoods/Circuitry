using UnityEngine;
using System.Collections;
using Shouldly;
using Require;

public class RelaysSparksTest : TestBehaviour
{
    SimpleCircuitComponent emitter;
    RelaysSparks it;
    SimpleCircuitComponent receiver;

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
        emitter = new GameObject().transform.Require<SimpleCircuitComponent>();
        emitter.ChainTo(it);
    }

    public void ItIsChainedToAReceiver()
    {
        receiver = new GameObject().transform.Require<SimpleCircuitComponent>();
        it.ChainTo(receiver);
    }

    public void TheEmitterSparks()
    {
        emitter.Spark();
    }

    public void TheReceiverShouldBeEnabled()
    {
        receiver.enabled.ShouldBe(true);
    }
}
