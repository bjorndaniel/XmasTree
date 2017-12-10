using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Devices.Gpio.Abstractions;
using Bifrost.Devices.Gpio.Core;
using Ooui;
using Xamarin.Forms;

namespace XmasTree
{
    public class Tree
    {
        private IGpioController _gpioController;

        public string Title => "Button Counter";

        public List<LedBlink> SelectedLights = new List<LedBlink>();

        public List<Switch> Switches = new List<Switch>();

        public Xamarin.Forms.Label Selected;

        public void Publish()
        {
            UI.Publish("/tree", CreateTree);
        }

        private Ooui.Element CreateTree()
        {
            _gpioController = Bifrost.Devices.Gpio.GpioController.Instance;
            var layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = 25,
                WidthRequest = 500
            };
            var sw = new Switch
            {
                ClassId = TreeLight.STAR.ToString(),
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 0, 50)
            };
            sw.Toggled += Sw_Toggled;
            Switches.Add(sw);
            layout.Children.Add(sw);
            layout.Children.Add(CreateGrid());
            var button = new Xamarin.Forms.Button
            {
                Text = "Update"
            };
            button.Clicked += Button_Clicked;
            layout.Children.Add(button);
            //button = new Xamarin.Forms.Button
            //{
            //    Text = "On"
            //};
            //button.Clicked += BtnOn;
            //layout.Children.Add(button);
            //button = new Xamarin.Forms.Button
            //{
            //    Text = "Off"
            //};
            //button.Clicked += BtnOff;
            //layout.Children.Add(button);
            return new ContentPage
            {
                Content = layout,
                WidthRequest = 1000,
                HeightRequest = 1200

            }.GetOouiElement();
        }

        private void BtnOff(object sender, EventArgs e)
        {
            Switches.ForEach(_ =>
            {
                _.IsToggled = false;
                Sw_Toggled(_, null);
            });
        }

        private void BtnOn(object sender, EventArgs e)
        {
            Switches.ForEach(_ =>
            {
                if (!_.IsToggled)
                {
                    _.IsToggled = true;
                    Sw_Toggled(_, null);
                }
            });
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Off();
            if (!SelectedLights.Any())
            {
                return;
            }
            SelectedLights?.ForEach(_ =>
            {
                try
                {
                    if (_.GpioPin == TreeLight.ELEVEN)
                    {
                        var altPin = Unosquare.RaspberryIO.Pi.Gpio.Pin02;
                        altPin.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
                        altPin.Write(_.IsOn ?
                           Unosquare.RaspberryIO.Gpio.GpioPinValue.High :
                           Unosquare.RaspberryIO.Gpio.GpioPinValue.Low);
                    }
                    else
                    {
                        var pin = _gpioController.OpenPin((int)_.GpioPin);
                        pin.SetDriveMode(Bifrost.Devices.Gpio.Core.GpioPinDriveMode.Output);
                        pin.Write(_.IsOn ?
                            GpioPinValue.High :
                            GpioPinValue.Low
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        private Grid CreateGrid()
        {
            var grid = new Grid
            {
                WidthRequest = 500,
                MinimumHeightRequest = 800,
                Margin = new Thickness(10),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto}
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition{Width = GridLength.Auto},
                    new ColumnDefinition{Width = GridLength.Auto},
                    new ColumnDefinition{Width = GridLength.Auto},
                    new ColumnDefinition{Width = GridLength.Auto}
                }
            };

            Switch sw;
            var counter = 1;
            for (var row = 0; row < 6; row++)
            {
                for (var column = 0; column < 4; column++)
                {
                    sw = new Switch
                    {
                        ClassId = GetName(counter)
                    };
                    sw.Toggled += Sw_Toggled;
                    grid.Children.Add(sw, column, row);
                    Switches.Add(sw);
                    counter++;
                }

            }
            return grid;
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            var sw = sender as Switch;
            if (sw.IsToggled)
            {
                SelectedLights.Add(new LedBlink
                {
                    IsOn = true,
                    GpioPin = (TreeLight)Enum.Parse(typeof(TreeLight), sw.ClassId)
                });
            }
            else
            {
                var light = SelectedLights.FirstOrDefault(_ => _.GpioPin.ToString().Equals(sw.ClassId));
                if (light != null)
                {
                    SelectedLights.Remove(light);
                }
            }
        }

        private string GetName(int counter)
        {
            switch (counter)
            {
                case 1:
                    return TreeLight.ONE.ToString();
                case 2:
                    return TreeLight.TWO.ToString();
                case 3:
                    return TreeLight.THREE.ToString();
                case 4:
                    return TreeLight.FOUR.ToString();
                case 5:
                    return TreeLight.FIVE.ToString();
                case 6:
                    return TreeLight.SIX.ToString();
                case 7:
                    return TreeLight.SEVEN.ToString();
                case 8:
                    return TreeLight.EIGHT.ToString();
                case 9:
                    return TreeLight.NINE.ToString();
                case 10:
                    return TreeLight.TEN.ToString();
                case 11:
                    return TreeLight.ELEVEN.ToString();
                case 12:
                    return TreeLight.TWELVE.ToString();
                case 13:
                    return TreeLight.THIRTEEN.ToString();
                case 14:
                    return TreeLight.FOURTEEN.ToString();
                case 15:
                    return TreeLight.FIFTEEN.ToString();
                case 16:
                    return TreeLight.SIXTEEN.ToString();
                case 17:
                    return TreeLight.SEVENTEEN.ToString();
                case 18:
                    return TreeLight.EIGHTEEN.ToString();
                case 19:
                    return TreeLight.NINETEEN.ToString();
                case 20:
                    return TreeLight.TWENTY.ToString();
                case 21:
                    return TreeLight.TWENTYONE.ToString();
                case 22:
                    return TreeLight.TWENTYTWO.ToString();
                case 23:
                    return TreeLight.TWENTYTHREE.ToString();
                case 24:
                    return TreeLight.TWENTYFOUR.ToString();
                default:
                    return TreeLight.STAR.ToString();

            }
        }

        private void Off()
        {
            foreach (int val in Enum.GetValues(typeof(TreeLight)))
            {
                Enum.TryParse(typeof(TreeLight), val.ToString(), out var light);
                if (val == 27)
                {
                    var altPin = Unosquare.RaspberryIO.Pi.Gpio.Pin02;
                    altPin.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
                    altPin.Write(Unosquare.RaspberryIO.Gpio.GpioPinValue.Low);
                }
                else
                {
                    var pin = _gpioController.OpenPin(val);
                    pin.SetDriveMode(GpioPinDriveMode.Output);
                    pin.Write(GpioPinValue.Low);
                }
            }
        }
    }
}
