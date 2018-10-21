using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopaFilmes.Services.Abstract;
using Foundation;
using UIKit;

namespace CopaFilmes.iOS.Service
{
    public class ToastService : IToastService
    {

        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        public void DisplayMessage(string message)
        {
            alertDelay = NSTimer.CreateScheduledTimer(LONG_DELAY, (obj) =>
            {
                DismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            var tView = alert.View;            

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void DismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}