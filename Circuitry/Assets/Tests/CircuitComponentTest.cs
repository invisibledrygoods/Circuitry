using UnityEngine;
using System.Collections;
using Shouldly;
using Require;

public class CircuitComponentTest : TestBehaviour
{
    ComplexCircuitComponent it;
    SimpleCircuitComponent receiver;
    SimpleCircuitComponent otherReceiver;

    public override void Spec()
    {
        Given("its 'next' is chained to a receiver").When("it sparks 'next'").Then("it should be disabled").And("the receiver should be enabled");
        Given("its 'next' is chained to a receiver").And("its 'next' is chained to another receiver").When("it sparks 'next'").Then("the receiver should be enabled").And("the other receiver should be enabled");
        Given("its 'next' is chained to a receiver").And("its 'nuh-uh' is chained to another receiver").When("it sparks 'next'").Then("the receiver should be enabled").And("the other receiver should not be enabled");
    }

    public void Its__IsChainedToAReceiver(string edge)
    {
        it = transform.Require<ComplexCircuitComponent>();
        receiver = new GameObject().transform.Require<SimpleCircuitComponent>();
        it.Chain(edge).To(receiver);
        it.enabled = false;
        receiver.enabled = false;
    }

    public void Its__IsChainedToAnotherReceiver(string edge)
    {
        otherReceiver = new GameObject().transform.Require<SimpleCircuitComponent>();
        it.Chain(edge).To(otherReceiver);
        otherReceiver.enabled = false;
    }

    public void ItSparks__(string edge)
    {
        it.Spark(edge);
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
