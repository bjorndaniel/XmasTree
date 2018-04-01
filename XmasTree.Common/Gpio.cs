using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio.Core;

namespace XmasTree.Common
{
    public class Gpio
    {
        private IGpioController _gpioController;
        private bool _fake = true;

        public Gpio()
        {
            try
            {
                _gpioController = Bifrost.Devices.Gpio.GpioController.Instance;
                _fake = false;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                _gpioController = new FakeGpioController();
            }
            
        }

        public void Off()
        {
            foreach (int val in Enum.GetValues(typeof(TreeLight)))
            {
                WritePin(val, false);
            }
        }

        public void WritePin(int pinNumber, bool on)
        {
            if (_fake)
            {
                Console.WriteLine($"Pin: {pinNumber} set to : {(on ? "HIGH" : "LOW")}");
            }
            else
            {
                if(pinNumber == 27)
                {
                    var altPin = Unosquare.RaspberryIO.Pi.Gpio.Pin02;
                    altPin.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
                    altPin.Write(on ?
                       Unosquare.RaspberryIO.Gpio.GpioPinValue.High :
                       Unosquare.RaspberryIO.Gpio.GpioPinValue.Low
                    );
                }
                else
                {
                    var pin = _gpioController.OpenPin(pinNumber);
                    pin.SetDriveMode(GpioPinDriveMode.Output);
                    pin.Write(on ?
                        GpioPinValue.High :
                        GpioPinValue.Low
                    );
                }

            }
        }

        private class FakeGpioController : IGpioController
        {
            public IDictionary<string, string> Pins => new Dictionary<string, string>();
            private Dictionary<int, FakeGpioPin> _pins = new Dictionary<int, FakeGpioPin>();

            public FakeGpioController()
            {
                CreatePins();
            }

            private void CreatePins()
            {
                for (int i = 1; i < 28; i++)
                {
                    _pins.Add(i, new FakeGpioPin());
                }
            }

            public IGpioPin OpenPin(int pinNumber) => _pins[pinNumber];

        }

        private class FakeGpioPin : IGpioPin
        {
            private GpioPinValue _value;
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public GpioPinValue Read() => _value;

            public void SetDriveMode(GpioPinDriveMode driveMode)
            {

            }

            public void Write(GpioPinValue pinValue)
            {
                _value = pinValue;
            }
        }
    }
}