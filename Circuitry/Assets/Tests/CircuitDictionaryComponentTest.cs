using UnityEngine;
using System.Collections.Generic;
using Shouldly;
using Require;
using System;

public class CircuitDictionaryComponentTest : TestBehaviour
{
    CircuitDictionaryComponent it;
    MockCircuitComponent receiver;
    MockCircuitComponent otherReceiver;
    Exception exception;

    public override void Spec()
    {
        Scenario("is a Circuit Dictionary Component");
        Given("its 'next' is chained to a receiver").When("it sparks 'next'").Then("it should be disabled").And("the receiver should be enabled").Because("sparks should travel down the circuit");
        Given("its 'next' is chained to a receiver").And("its 'next' is chained to another receiver").When("it sparks 'next'").Then("the receiver should be enabled").And("the other receiver should be enabled").Because("sparks should branch");
        Given("its 'next' is chained to a receiver").And("its 'nuhUh' is chained to another receiver").When("it sparks 'next'").Then("the receiver should be enabled").And("the other receiver should not be enabled").Because("it should only be able to send one signal at a time");
        Given("its 'next' is chained to a receiver").When("it sparks something that doesnt exist").Then("it should throw a KeyNotFound exception").Because("it should notify me of a potential typo");
    }

    public void Its__IsChainedToAReceiver(string transition)
    {
        it = transform.Require<CircuitDictionaryComponent>();
        receiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.Chain(transition, receiver);
        it.enabled = false;
        receiver.enabled = false;
    }

    public void Its__IsChainedToAnotherReceiver(string transition)
    {
        otherReceiver = new GameObject().transform.Require<MockCircuitComponent>();
        it.Chain(transition, otherReceiver);
        otherReceiver.enabled = false;
    }

    public void ItSparks__(string transition)
    {
        it.Spark("next");
    }

    public void ItSparksSomethingThatDoesntExist()
    {
        try
        {
            it.Spark("doesntExist");
        }
        catch (Exception e)
        {
            exception = e;
        }
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

    public void ItShouldThrowAKeyNotFoundException()
    {
        exception.ShouldBeTypeOf<KeyNotFoundException>();
    }
}
