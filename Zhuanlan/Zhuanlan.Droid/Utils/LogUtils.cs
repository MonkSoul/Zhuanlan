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
using System.IO;

namespace Zhuanlan.Droid.Utils
{
    public class LogUtils
    {

        public static async Task SaveAsyn(Context context, Exception ex)
        {
            await Task.Run(() =>
            {
                var PackageInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                var path = Android.OS.Environment.ExternalStorageDirectory.Path + "/" + PackageInfo.PackageName + "/log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string dbPath = System.IO.Path.Combine(path, "log.txt");
                if (!System.IO.File.Exists(dbPath))
                {
                    System.IO.File.Create(dbPath);
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("-----" + DateTime.Now.ToString() + "-----\n");
                sb.Append("�������̣�\n");
                sb.Append(Build.Manufacturer).Append("\n\n");
                sb.Append("�ֻ��ͺţ�\n");
                sb.Append(Build.Model).Append("\n\n");
                sb.Append("ϵͳ�汾��\n");
                sb.Append(Build.VERSION.Release).Append("\n\n");
                sb.Append("�쳣ʱ�䣺\n");
                sb.Append(DateTime.Now.ToString()).Append("\n\n");
                sb.Append("�쳣��Ϣ��\n");
                sb.Append(ex).Append("\n");
                sb.Append(ex.Message).Append("\n\n");
                System.IO.File.AppendAllText(dbPath, sb.ToString());
            });
        }
    }
}