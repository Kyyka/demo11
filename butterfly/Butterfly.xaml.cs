using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace butterfly
{
    public sealed partial class Butterfly : UserControl
    {
        // animate butterfly
        private DispatcherTimer timer;

        // offset to show sprite
        private int currentframe = 0;
        private int dircetion = 1;
        private int frameheight = 132;

        // speed, accelerate, angle
        private readonly double MaxSpeed = 10.0;
        private readonly double Accelerate = 0.5;
        private readonly double AngleStep = 5;
        private double Angle = 0;
        private double Speed = 0;

        // Location
        public double locationX { get; set; }
        public double locationY { get; set; }

        public Butterfly()
        {
            this.InitializeComponent();

            // animate
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 125);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // frame
            if (dircetion == 1) currentframe++;
            else if (dircetion == -1) currentframe--;
            // border value...
            if (currentframe == 0 || currentframe == 4)
            {
                dircetion = -1 * dircetion; // 1 or -1
            }
            // set offset
            SpriteSheetOffset.Y = currentframe * -frameheight;
        }

        // show butterfly
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, locationX);
            SetValue(Canvas.TopProperty, locationY);
        }

        // move
        public void Move()
        {
            // more speed
            Speed += Accelerate;
            if (Speed > MaxSpeed) Speed = MaxSpeed;

            // update location value (width angle and speed)
            locationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * Speed;
            locationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * Speed;
            // update in canvas
            //SetLocation();
        }

        // rotate
        public void Rotate(int direction)
        {
            Angle += direction * AngleStep; // -1 or 1 -> -5 or 5
            ButterflyRotateAngle.Angle = Angle; 
        }
    }
}
