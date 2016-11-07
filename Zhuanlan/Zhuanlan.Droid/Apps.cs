using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Zhuanlan.Droid.Utils;

namespace Zhuanlan.Droid
{
    [Application]
    public class Apps : Application
    {
        public Apps() { }
        public Apps(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public override void OnCreate()
        {
            base.OnCreate();
            // ����ȫ���쳣����
            //if (!BuildConfig.Debug)
            {
                AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            }
        }
        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironment_UnhandledExceptionRaiser;
            base.Dispose(disposing);
        }

        async void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs ex)
        {
            await Task.Run(() =>
            {
                new Handler().Post(() =>
                {
                    Toast.MakeText(this, "�ܱ�Ǹ����������쳣�����ڴ��쳣�лָ�", ToastLength.Long).Show();
                });
            });

            //���������־
            try
            {
                await LogUtils.SaveAsyn(this, ex.Exception);
                //�ó����������2�룬��֤������־�ı���
                System.Threading.Thread.Sleep(2000);
            }
            catch (Java.Lang.Exception e)
            {
                e.PrintStackTrace();
            }
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            Java.Lang.JavaSystem.Exit(1);
        }
    }
}