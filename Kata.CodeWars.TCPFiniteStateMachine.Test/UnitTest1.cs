namespace Kata.CodeWars.TCPFiniteStateMachine.Test
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SolutionTest
    {
        [Test]
        public void SampleTests()
        {
            Assert.AreEqual("CLOSE_WAIT", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN", "RCV_SYN_ACK", "RCV_FIN" }));
            Assert.AreEqual("ESTABLISHED", TCP.TraverseStates(new[] { "APP_PASSIVE_OPEN", "RCV_SYN", "RCV_ACK" }));
            Assert.AreEqual("LAST_ACK", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN", "RCV_SYN_ACK", "RCV_FIN", "APP_CLOSE" }));
            Assert.AreEqual("SYN_SENT", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN" }));
            Assert.AreEqual("ERROR", TCP.TraverseStates(new[] { "APP_PASSIVE_OPEN", "RCV_SYN", "RCV_ACK", "APP_CLOSE", "APP_SEND" }));
        }
    }
}