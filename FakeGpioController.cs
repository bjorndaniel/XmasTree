using System;
using System.Collections.Generic;
using Bifrost.Devices.Gpio.Abstractions;

namespace XmasTree
{
    public class FakeGpioController : IGpioController
    {
        public IDictionary<string, string> Pins => throw new NotImplementedException();

        public IGpioPin OpenPin(int pinNumber)
        {
            return null;
        }
    }
}
