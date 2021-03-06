#region Copyright
// <copyright file="MvxAndroidViewPresenter.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com
#endregion
using Android.App;
using Cirrious.MvvmCross.Android.Interfaces;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Interfaces.Views;
using Cirrious.MvvmCross.Platform.Diagnostics;
using Cirrious.MvvmCross.Views;

namespace Cirrious.MvvmCross.Android.Views
{
    public class MvxAndroidViewPresenter
        : IMvxAndroidViewPresenter
          , IMvxServiceConsumer<IMvxAndroidCurrentTopActivity>
          , IMvxServiceConsumer<IMvxAndroidViewModelRequestTranslator>
    {
        private Activity Activity
        {
            get { return this.GetService<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        public void Show(MvxShowViewModelRequest request)
        {
            var requestTranslator = this.GetService<IMvxAndroidViewModelRequestTranslator>();
            var intent = requestTranslator.GetIntentFor(request);
            Activity.StartActivity(intent);
        }

        public void Close(IMvxViewModel toClose)
        {
            toClose.ActOnRegisteredViews(view =>
                                             {
                                                 var activity = view as Activity;
                                                 if (activity != null)
                                                     activity.Finish();
                                             });
        }
    }
}