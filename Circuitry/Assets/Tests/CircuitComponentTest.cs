using UnityEngine;
using System.Collections.Generic;
using Shouldly;
using Require;

public class CircuitComponentTest : TestBehaviour
{
    MockCircuitComponent it;
    MockCircuitComponent receiver;
    MockCircuitComponent otherReceiver;

    public override void Spec()
    {
        Scenario("is a Circuit Component");
        Given("its next is chained to a receiver").When("it sparks next").Then("it should be disabled").And("the receiver should be enabled").Because("sparks should travel down the circuit");
        Given("its next is chained to a receiver").And("its next is chained to another receiver").When("it sparks next").Then("the receiver should be enabled").And("the other receiver should be enabled").Because("sparks should branch");
        Given("its next is chained to a receiver").And("its nuhUh is chained to another receiver").When("it sparks next").Then("the receiver should be enabled").And("the other receiver should not be enabled").Because("it should only be able to send one signal at a time");
    }

    public void ItsNextIsChainedToAReceiver()
    {
        it = transform.Require<MockCircuitComponent>();
        it.next = new List<CircuitComponent>();
        it.nuhUh = new List<CircuitComponent>();
        receiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.next.Add(receiver);
        it.enabled = false;
        receiver.enabled = false;
    }

    public void ItsNextIsChainedToAnotherReceiver()
    {
        otherReceiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.next.Add(otherReceiver);
        otherReceiver.enabled = false;
    }

    public void ItsNuhUhIsChainedToAnotherReceiver()
    {
        otherReceiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.nuhUh.Add(otherReceiver);
        otherReceiver.enabled = false;
    }

    public void ItSparksNext()
    {
        it.Spark(it.next);
    }

    public void ItShouldBeDisabled()
    {
        it.enabled.ShouldBe(false);
    }

    public void TheReceiverShouldBeEnabled()
    {
        receiver.enabled.ShouldBe(true);
    }

    public void TheOtherReceiverShouldBeEnabled()
    {
        otherReceiver.enabled.ShouldBe(true);
    }

    public void TheOtherReceiverShouldNotBeEnabled()
    {
        otherReceiver.enabled.ShouldBe(false);
    }
}
