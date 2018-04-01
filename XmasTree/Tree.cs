using Ooui;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XmasTree.Common;

namespace XmasTree
{
    public class Tree
    {
        private Gpio _gpio = new Gpio();

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
            var layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            layout.Children.Add(CreateSwitch(Common.Utilities.GetName(0)));
            layout.Children.Add(CreateGrid());
            var button = new Xamarin.Forms.Button
            {
                Text = "Update"
            };
            button.Clicked += Button_Clicked;
            layout.Children.Add(button);
            return new ContentPage
            {
                Content = layout,
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
            _gpio.Off();
            if (!SelectedLights.Any())
            {
                return;
            }
            SelectedLights?.ForEach(_ =>
            {
                try
                {
                    _gpio.WritePin((int)_.GpioPin, _.IsOn);
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
                HorizontalOptions = LayoutOptions.CenterAndExpand,
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
            var counter = 1;
            for (var row = 0; row < 6; row++)
            {
                for (var column = 0; column < 4; column++)
                {
                    grid.Children.Add(CreateSwitch(Common.Utilities.GetName(counter)), column, row);
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

        private StackLayout CreateSwitch(string name)
        {
            var sw = new Switch
            {
                ClassId = name,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(40,0,40,0)
            };
            sw.Toggled += Sw_Toggled;
            Switches.Add(sw);
            return new StackLayout
            {
                Children =
                {
                    new Xamarin.Forms.Label
                    {
                        Text = name,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    sw
                }
            };
        }
    }
}
