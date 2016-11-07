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
using Realms;

namespace Zhuanlan.Droid.Model
{
    public class BestAnswererModel : RealmObject
    {
        /// <summary>
        /// ����
        /// </summary>
        public IList<int> Topics { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Description { get; set; }
    }
}